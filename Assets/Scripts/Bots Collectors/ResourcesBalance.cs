using UnityEngine;
using UnityEngine.UI;

public class ResourcesBalance : MonoBehaviour
{
    [SerializeField] private Text _resources;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _resources.text = _base.Resources.ToString();
        _base.ResourcesChanged += OnResourcesChanged;
    }

    private void OnDisable()
    {
        _base.ResourcesChanged -= OnResourcesChanged;
    }

    private void OnResourcesChanged(int resources)
    {
        _resources.text = resources.ToString();
    }
}
