using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
