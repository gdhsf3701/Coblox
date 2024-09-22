using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoneChack : MonoBehaviour
{
    [SerializeField] Plate plate;
    bool done = true;
    public void ClickGoopGi()
    {
        for(int i = 0; i < plate.elemental.Length; i++)
        {
            if (plate.elementalWant[i] == plate.elementalCount[i])
            {
                continue;
            }
            else
            {
                done = false;
                break;
            }
        }
        if(done)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
