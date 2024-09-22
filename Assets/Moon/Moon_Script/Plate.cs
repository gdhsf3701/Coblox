using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plate : MonoBehaviour
{
    [SerializeField]private int nowElemental = -1;
    public int NowElemental
    {
        get
        {
            return nowElemental;
        }
        set
        {
            nowElemental = value;
            SetCursor();
        }
    }
    [SerializeField] GameObject[] elementals;
    [SerializeField] Camera cam;
    [SerializeField] Collider2D Collider;
    [SerializeField] private Texture2D[] cursorTexture;
    public string[] elemental = { "ÇÜ", "¹ö¼¸", "ÇÇ¸Á", "¿Ã¸®ºê", "ÆäÆÛ·Î´Ï" };
    public int[] elementalWant = { 0, 0, 0, 0, 0 };
    public int[] elementalCount = { 0, 0, 0, 0, 0 };

    private void Awake()
    {
        NowElemental = -1;
    }
    private void Start()
    {
        int i = Random.Range(0, elemental.Length);
        int j = Random.Range(1, 21);
        elementalWant[i] = j;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && nowElemental != -1)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            if (Collider.OverlapPoint(mousePosition)) 
            {
                GameObject game = Instantiate(elementals[nowElemental], mousePosition, Quaternion.identity);
                if(game != null) 
                {
                    elementalCount[nowElemental]++;
                }
            }
        }
    }
    public void ChangeGoopGi()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void SetCursor()
    {
        if (nowElemental >= -1 && nowElemental < cursorTexture.Length - 1)
        {
            Cursor.SetCursor(cursorTexture[nowElemental + 1], new Vector2(cursorTexture[nowElemental + 1].width * 0.45f, cursorTexture[nowElemental + 1].height * 0.35f), CursorMode.ForceSoftware);
        }   
    }
}
