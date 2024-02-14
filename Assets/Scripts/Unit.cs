using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _collectPoint;

    public bool IsGet = false;
    public bool IsFree = true;

    private float _speed = 2f;
    private Resources _resources;
    private Transform _target;
    private Transform _transformWaitingPosition;
    private Transform _transformBase;

    public Transform CollectPoint => _collectPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (_target != null && collision.collider.gameObject == _target.gameObject)
        {
            _resources.IsMove = true;
            IsGet = true;
            _resources.Unit = this;
        }
    }

    private void Update()
    {
        if (IsGet == false && _target != null)
        {
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, _target.position.z));
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else if (IsGet == true)
        {
            transform.LookAt(new Vector3(transform.position.x, transform.position.y,  _transformBase.position.z));
            transform.position = Vector3.MoveTowards(transform.position, _transformBase.position, _speed * Time.deltaTime);
        }
        else if (IsFree == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _transformWaitingPosition.position, _speed * Time.deltaTime);
        }
        else
        {
            IsFree = true;
        }
    }

    public void GetResources(Resources resources)
    {
        IsFree = false;
        resources.IsTaken = true;

        _resources = resources;
        _target = _resources.transform;
    }

    public void GetPositionBase(Transform transformBase)
    {
        _transformBase = transformBase;
    }

    public void GetWaitingPosition(Transform transformWaitingPosition, float stepWaitingPosition)
    {
        Vector3 newWaitingPosition = transformWaitingPosition.position;
        newWaitingPosition.x += stepWaitingPosition;
        transformWaitingPosition.position = newWaitingPosition;
        _transformWaitingPosition = transformWaitingPosition;
    }
}
