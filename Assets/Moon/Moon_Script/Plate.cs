using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public int nowElemental = -1;
    [SerializeField] GameObject[] elementals;
    [SerializeField] Camera cam;
    [SerializeField] Collider2D Collider;
    public string[] elemental = { "ham", "mushroom", "pepper", "olive", "Pepperoni" };
    public int[] elementalWant = { 0, 0, 0, 0, 0 };
    public int[] elementalCount = { 0, 0, 0, 0, 0 };

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && nowElemental != -1)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            if (Collider.OverlapPoint(mousePosition))
            {
                Instantiate(elementals[nowElemental], mousePosition, Quaternion.identity);
                elementalCount[nowElemental]++;
            }
        }
    }
}
