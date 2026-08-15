using System;
using UnityEngine;

public class DeathDebugger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered: " + other.name);
    }
}
