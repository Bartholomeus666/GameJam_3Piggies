using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerManager : MonoBehaviour
{
    public List<Collider> Colliders = new List<Collider>();

    private Collider _collider;
    private MeshRenderer _meshRenderer;

    [SerializeField] private Material AttachMat;
    private Material _originalMat;

    private void Start()
    {
        _originalMat = GetComponent<MeshRenderer>()?.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        Colliders.Add(other);

        _meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        if (_meshRenderer != null && this.gameObject.GetComponent<Collider>().isTrigger)
        {
            _meshRenderer.material = AttachMat;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Colliders.Remove(other);

        _meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        if (_meshRenderer != null)
        {
            _meshRenderer.material = _originalMat;
        }
    }
}
