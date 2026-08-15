using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private Renderer targetRenderer;

    void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        int index = GetComponent<PlayerInput>().playerIndex;
        targetRenderer.sharedMaterial = playerMaterials[index % playerMaterials.Length];
    }
}