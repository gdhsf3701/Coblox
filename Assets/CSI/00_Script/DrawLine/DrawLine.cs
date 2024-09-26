using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject lineprefab;
    private GameObject currentLine;

    private LineRenderer linerenderer;
    private EdgeCollider2D edgeCollider;
    private Vector2[] FingerPositions;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        currentLine = Instantiate (lineprefab, Vector3.zero, Quaternion.identity);
        linerenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            CreateLine();
        }
        if (Input.GetMouseButton(0)) {
            Vector2 temFingerPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(temFingerPos, FingerPositions[FingerPositions.Length - 1]) > .1f) {

                UpdateLine(temFingerPos);
            }
        }
    }


    void CreateLine(){
        currentLine = Instantiate (lineprefab, Vector3.zero, Quaternion.identity);
        linerenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        FingerPositions = new Vector2[]{_camera.ScreenToWorldPoint(Input.mousePosition),_camera.ScreenToWorldPoint(Input.mousePosition)};
        linerenderer.positionCount++;
        linerenderer.SetPosition(0, FingerPositions[0]);
        linerenderer.positionCount++;
        linerenderer.SetPosition(1, FingerPositions[1]);
        edgeCollider.points = FingerPositions.ToArray();
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        FingerPositions.Concat(new Vector2[]{newFingerPos});
        linerenderer.positionCount++;
        linerenderer.SetPosition(linerenderer.positionCount - 1, newFingerPos);

    }
    
    
    
}
