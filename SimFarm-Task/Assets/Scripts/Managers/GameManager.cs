using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject sphere;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SphereManager.Instance.CreateSphere();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
