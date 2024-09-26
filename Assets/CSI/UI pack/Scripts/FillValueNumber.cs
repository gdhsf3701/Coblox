using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillValueNumber : MonoBehaviour
{
    public Image TargetImage;
    // Update is called once per frame
    void Update()
    {
        float amount = TargetImage.fillAmount * 100;
        if (amount < 10)
        {
            TargetImage.color = Color.red;
        }else if (amount < 60)
        {
            TargetImage.color = Color.yellow;

        }
        else
        {
            TargetImage.color = Color.green;
        }
        gameObject.GetComponent<Text>().text = amount.ToString("F0");
    }
}
