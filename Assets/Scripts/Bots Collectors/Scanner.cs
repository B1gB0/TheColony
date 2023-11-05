using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private List<Resources> _resources = new List<Resources>();

    public List<Resources> Resources => _resources;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Resources>(out Resources resources))
        {
            _resources.Add(resources);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Resources>(out Resources resources))
        {
            _resources.Remove(resources);
        }
    }
}
