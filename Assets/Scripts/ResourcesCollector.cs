using UnityEngine;

public class ResourcesCollector : MonoBehaviour
{
    private Resources _resources;

    public Resources Resources => _resources;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resources>(out Resources resources))
        {
            _resources = resources;
        }
    }

    public void GiveResourcesToBase()
    {
        _resources = null;
    }
}
