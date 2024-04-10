using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SphereManager : MonoBehaviour
{
    public static SphereManager Instance;
    public GameObject sphere;

    [SerializeField] private Material changingColorMaterial;
    [SerializeField] private ParticleSystem fireworks;
    
    //Center
    [SerializeField] private Vector3 center = new (0.0f, 0.0f, 0.0f);
    
    //For movement
    [SerializeField] private float maxRotateSpeed = 200.0f;
    [SerializeField] private float approachSpeed = 0.5f;
    [SerializeField] private float timeToAccelerate = 3.0f;
    [SerializeField] private float shrinkRate = 0.1f;
    
    private float _distance = 0;
    private Vector3 _previousPosition;
    private bool _hasParticlesSystemFired = false;

    //Sphere's components
    private Renderer _sphereRenderer;
    private float _actualSpeed = 0.0f;
    private float _acceleration = 0.0f;

    private bool _sphereStopInProgress;

    public bool IsSphereGenerated()
    {
        return sphere;
    }
    
    public void StopSphere()
    {
        if (_sphereStopInProgress)
        {
            return;
        }
        
        StartCoroutine(SphereWait());
        UIManager.Instance.WriteDistance(_distance);
    }
    
    public void CreateSphere()
    {
        if (sphere != null)
        {
            DestroySphere();
        }

        _hasParticlesSystemFired = false;
        StopAllCoroutines();
        
        //Calculating acceleration needed to reach designed speed in given time
        _acceleration = (maxRotateSpeed - 0.0f) / timeToAccelerate;
        
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        _previousPosition = sphere.transform.position;
        _sphereRenderer = sphere.GetComponent<Renderer>();
        _sphereRenderer.material = changingColorMaterial;
    }
    
    public void Awake()
    {
        // Singleton pattern
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Update()
    {
        if (!sphere)
        {
            return;
        }
        
        Vector3 sPosition = sphere.transform.position;
        float sPosX = sPosition.x;
        float sPosY = sPosition.y;
        float sPosZ = sPosition.z;
        float epsilon = 0.0001f;

        if (!(Math.Abs(sPosX - center.x) < epsilon && 
              Math.Abs(sPosY - center.y) < epsilon &&
              Math.Abs(sPosZ - center.z) < epsilon))
        {
            _actualSpeed += _acceleration * Time.deltaTime;
            if (_actualSpeed > maxRotateSpeed)
            {
                _actualSpeed = maxRotateSpeed;
            }
            MoveSphere();
            //Calculating distance travelled from the start
            _distance += Vector3.Distance(sPosition, _previousPosition);
            _previousPosition = sPosition;
        }
        else
        {
            // When the sphere reaches the center
            sphere.transform.localScale -= sphere.transform.localScale * (shrinkRate * Time.deltaTime);

            if (sphere.transform.localScale.sqrMagnitude > 0.01f)
            {
                return;
            }
            
            if (_hasParticlesSystemFired)
            {
                return;
            }
            
            StartCoroutine(ParticleWait());
            _hasParticlesSystemFired = true;
        }
    }

    private void MoveSphere()
    {
        sphere.transform.LookAt(center);
        //For getting closer to the center
        sphere.transform.Translate(transform.forward * (Time.deltaTime * approachSpeed));
        //For rotating around the center
        sphere.transform.RotateAround(center, Vector3.forward,_actualSpeed * Time.deltaTime);
    }

    private IEnumerator SphereWait()
    {
        //Remember speeds in tmp variables and set them 0
        _sphereStopInProgress = true;
        float tmpApproach = approachSpeed;
        float tmpRotate = maxRotateSpeed;
        maxRotateSpeed = 0;
        approachSpeed = 0;
        //Wait for 5 seconds
        yield return new WaitForSeconds(5.0f);
        //Return previous speeds' values
        _sphereStopInProgress = false;
        maxRotateSpeed = tmpRotate;
        approachSpeed = tmpApproach;
    }
    
    private IEnumerator ParticleWait()
    {
        fireworks.Play(false);
        yield return new WaitForSeconds(5.0f);
        DestroySphere();
    }

    private void DestroySphere()
    {
        Destroy(sphere);
        _distance = 0;
        _actualSpeed = 0;
    }
}
