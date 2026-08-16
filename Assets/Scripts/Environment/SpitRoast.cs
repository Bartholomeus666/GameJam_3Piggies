using UnityEngine;

public class SpitRoast : MonoBehaviour
{
    [SerializeField] private GameObject _skewer;
    [SerializeField] private float rotationSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _skewer.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
