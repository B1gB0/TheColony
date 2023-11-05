using UnityEngine;

public class Resources : MonoBehaviour
{
    public bool _isTaken = false;
    public Unit Unit;

    private bool _isMove = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Unit>(out Unit unit))
        {
            _isMove = true;
            unit._isGet = true;
            Unit = unit;
        }
    }

    private void Update()
    {
        if (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, Unit.CollectPoint.position, 2f * Time.deltaTime);
        }
    }
}
