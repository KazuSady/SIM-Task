using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SphereManager : MonoBehaviour
{
    public static SphereManager Instance;
    public GameObject sphere;

    [SerializeField] private Material _changingColorMaterial; 

    //Sphere's components
    private Renderer _sphereRenderer;
    
    //Center
    [SerializeField] private Vector3 _center = new (0.0f, 0.0f, 0.0f);
    
    //For movement
    [FormerlySerializedAs("_movementSpeed")] [SerializeField] private float _rotateSpeed = 2.0f;
    [FormerlySerializedAs("_radialSpeed")] [SerializeField] private float _approachSpeed = 0.5f;

    public void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sphere != null && sphere.transform.position != _center)
        {
            MoveSphere();
        }
    }

    public void CreateSphere()
    {
        if (sphere != null)
        {
            Destroy(sphere);
        }
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        _sphereRenderer = sphere.GetComponent<Renderer>();
        _sphereRenderer.material = _changingColorMaterial;
    }

    public void MoveSphere()
    {
        sphere.transform.LookAt(_center);
        //For getting closer to the center
        sphere.transform.Translate(transform.forward * Time.deltaTime * _approachSpeed);
        //For rotating around the center
        sphere.transform.RotateAround(_center, Vector3.forward,_rotateSpeed * Time.deltaTime);
    }

    public void StopSphere()
    {
        StartCoroutine(SphereWait());
    }

    IEnumerator SphereWait()
    {
        //Remember speeds in tmp variables and set them 0
        float tmpApproach = _approachSpeed;
        float tmpRotate = _rotateSpeed;
        _rotateSpeed = 0;
        _approachSpeed = 0;
        //Wait for 5 seconds
        yield return new WaitForSeconds(5);
        //Return previous speeds' values
        _rotateSpeed = tmpRotate;
        _approachSpeed = tmpApproach;
    }
}
