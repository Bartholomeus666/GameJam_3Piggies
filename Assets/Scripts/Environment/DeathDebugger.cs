using System;
using UnityEngine;

public class DeathDebugger : MonoBehaviour
{
    [SerializeField] private Transform _skewer;
    
    private void OnTriggerEnter(Collider other)
    {
        var pig = other.GetComponentInParent<Pig>();
        if (pig == null || pig.IsCaptured) return;
        pig.Capture(_skewer);
    }
}
