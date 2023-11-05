using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.position = Input.mousePosition;
        }
    }
}
