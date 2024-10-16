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
        if (plate.NowElemental != elementalNum)
        {
            plate.NowElemental = elementalNum;
            if (plate.isSosu)
            {
                SoundManager.Instance.PlaySound(Sound.toppingSelect);
            }
            else
            {
                SoundManager.Instance.PlaySound(Sound.SauceandCheeseSelect);
            }
        }
    }
}
