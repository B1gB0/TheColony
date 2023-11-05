using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _collectPoint;

    public bool _isGet = false;
    public bool _isFree = true;

    private float _speed = 2f;
    private Resources _resources;
    private Transform _target;
    private Transform _transformBase;

    public Transform CollectPoint => _collectPoint;

    private void Update()
    {
        if (_isGet == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else if (_isGet == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _transformBase.position, _speed * Time.deltaTime);
        }
    }

    public void GetResources(Resources resources)
    {
        _isFree = false;
        resources._isTaken = true;

        _resources = resources;
        _target = _resources.transform;
    }

    public void GetPositionBase(Transform transformBase)
    {
        _transformBase = transformBase;
    }
}
