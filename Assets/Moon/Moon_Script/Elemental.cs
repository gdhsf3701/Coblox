using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : MonoBehaviour
{
    [SerializeField] int elementalNum;
    Plate plate;
    private void Awake()
    {
        plate = GetComponentInParent<Plate>();
    }
    public void ChangeElemental()
    {
        if (plate.nowElemental != elementalNum)
        {
            plate.nowElemental = elementalNum;
        }
    }
}
