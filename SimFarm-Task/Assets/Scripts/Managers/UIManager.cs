using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI textDistance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void UIStartSphere()
    {
        GameManager.Instance.SphereBegin();
    }

    public void WriteDistance(float distance)
    {
        textDistance.text = "Distance: " + Math.Round(distance,2);
    }
}
