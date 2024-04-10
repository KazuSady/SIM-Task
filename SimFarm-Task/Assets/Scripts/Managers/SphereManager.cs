using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public static SphereManager Instance;
    public GameObject sphere;

    private Renderer _sphereRenderer;

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
        if (sphere != null)
        {
            _sphereRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), 0.5f, 0.0f);
        }
    }

    public void CreateSphere()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
        _sphereRenderer = sphere.GetComponent<Renderer>();
    }
}
