using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class View_MiniGame : MonoBehaviour
{
    public void Show_MiniGame()
    {
        GameObject.Find("MiniGameParent").transform.Find("MiniGame").gameObject.SetActive(true);
    }
    
}
