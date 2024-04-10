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

    //Sphere's components
    private Renderer _sphereRenderer;
    private Rigidbody _sphereRigidbody;
    
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
            _sphereRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), 0.5f, 0.0f);
        }
    }

    public void CreateSphere()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sphereRigidbody = sphere.AddComponent<Rigidbody>();
        _sphereRigidbody.useGravity = false;
        sphere.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        _sphereRenderer = sphere.GetComponent<Renderer>();
    }

    public void MoveSphere()
    {
        sphere.transform.LookAt(_center);
        sphere.transform.Translate(transform.forward * Time.deltaTime * _approachSpeed);
        sphere.transform.RotateAround(_center, Vector3.forward,_rotateSpeed * Time.deltaTime);
    }
}
