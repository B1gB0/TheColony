using UnityEngine;

public class Resources : MonoBehaviour
{
    public bool IsTaken = false;
    public bool IsMove = false;
    public Unit Unit;

    private void Update()
    {
        if (IsMove)
        {
            transform.position = Unit.CollectPoint.transform.position;
            transform.SetParent(Unit.transform);
        }
    }
}
