using System;
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
    private float _distance = 0;
    private Vector3 _previousPosition;

    //Sphere's components
    private Renderer _sphereRenderer;
    
    //Center
    [SerializeField] private Vector3 _center = new (0.0f, 0.0f, 0.0f);
    
    //For movement
    [FormerlySerializedAs("_rotateSpeed")] [FormerlySerializedAs("_movementSpeed")] [SerializeField] private float _maxRotateSpeed = 200.0f;
    [FormerlySerializedAs("_radialSpeed")] [SerializeField] private float _approachSpeed = 0.5f;
    [SerializeField] private float _timeToAccelerate = 3.0f;
    private float _actualSpeed = 0.0f;
    private float _acceleration = 0.0f;

    public void Awake()
    {
        Instance = this;
    }
    
    void FixedUpdate()
    {
        if (sphere != null)
        {
            if (sphere.transform.position != _center)
            {
                _actualSpeed += _acceleration * Time.deltaTime;
                if (_actualSpeed > _maxRotateSpeed)
                {
                    _actualSpeed = _maxRotateSpeed;
                }
                MoveSphere();
                //Calculating distance travelled from the start
                _distance += Vector3.Distance(sphere.transform.position, _previousPosition);
                _previousPosition = sphere.transform.position;
            }
            else
            {
                if (sphere.transform.localScale != new Vector3(0.0f, 0.0f, 0.0f))
                {
                    sphere.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
                else
                {
                    
                }
            }
        }
    }

    public void CreateSphere()
    {
        if (sphere != null)
        {
            Destroy(sphere);
            _distance = 0;
            _actualSpeed = 0;
        }
        //Calculating acceleration needed to reach designed speed in given time
        _acceleration = (_maxRotateSpeed - 0.0f) / _timeToAccelerate;
        
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        _previousPosition = sphere.transform.position;
        _sphereRenderer = sphere.GetComponent<Renderer>();
        _sphereRenderer.material = _changingColorMaterial;
    }

    public void MoveSphere()
    {
        sphere.transform.LookAt(_center);
        //For getting closer to the center
        sphere.transform.Translate(transform.forward * Time.deltaTime * _approachSpeed);
        //For rotating around the center
        sphere.transform.RotateAround(_center, Vector3.forward,_actualSpeed * Time.deltaTime);
    }

    public void StopSphere()
    {
        StartCoroutine(SphereWait());
        UIManager.Instance.WriteDistance(_distance);
    }

    IEnumerator SphereWait()
    {
        //Remember speeds in tmp variables and set them 0
        float tmpApproach = _approachSpeed;
        float tmpRotate = _maxRotateSpeed;
        _maxRotateSpeed = 0;
        _approachSpeed = 0;
        //Wait for 5 seconds
        yield return new WaitForSeconds(5);
        //Return previous speeds' values
        _maxRotateSpeed = tmpRotate;
        _approachSpeed = tmpApproach;
    }

    public bool IsSphereGenerated()
    {
        if (sphere != null)
        {
            return true;
        }

        return false;
    }
}
