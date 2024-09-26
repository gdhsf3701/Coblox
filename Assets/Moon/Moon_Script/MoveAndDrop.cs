using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDrop : MonoBehaviour
{
    [SerializeField] Camera cam;
    Collider2D Collider;
    bool isMove = false;
    [SerializeField]Transform fire;
    public bool InFire
    {
        get
        {
            return inFire;
        }
        set
        {
            inFire = value;
            if(inFire)
            {
                OnFireEnter?.Invoke();
            }
            else
            {
                OnFireExit?.Invoke();
            }
        }
    }
    public Action OnFireEnter;
    public Action OnFireExit;
    bool onFire = false;
    bool inFire = false;
    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if(onFire)
            {
                InFire = !inFire;
                isMove = !isMove;
                transform.position = fire.position;
            }
            else if (!isMove)
            {
                Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                if (Collider.OverlapPoint(mousePosition))
                {
                    isMove = true;
                }
            }
        }
        if(isMove)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onFire = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onFire = false;
    }
}
