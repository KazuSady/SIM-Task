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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SphereManager.Instance.IsSphereGenerated())
        {
            SphereManager.Instance.StopSphere();
        }
    }

    public void SphereBegin()
    {
        SphereManager.Instance.CreateSphere();
    }
}
