using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _textDistance;

    private void Awake()
    {
        Instance = this;
    }

    public void UIStartSphere()
    {
        GameManager.Instance.SphereBegin();
    }

    public void WriteDistance(float distance)
    {
        _textDistance.text = "Distance: " + Math.Round(distance,2);
    }
}
