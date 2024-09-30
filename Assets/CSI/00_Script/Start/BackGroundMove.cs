using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    private float scrollRange = 44.73801f;

    public Transform target;

    private void Update()
    {
        transform.position += -Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x < -scrollRange)
        {
            transform.position = Vector3.right * (96.48999f);
        }
    }
}