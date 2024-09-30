using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartSonNom : MonoBehaviour
{
    bool done = true;
    [SerializeField] Sprite[] SonNomSprites;
    SpriteRenderer SonNomRenderer;
    public bool Done 
    {
        get
        {
            return done;
        }
        set 
        {
            done = value;
            if (done)
            {
                OnDone?.Invoke();
            }
        } 
    }

    public Action OnDone;

    float time;
    [SerializeField] Transform start, end,endEnd;
    private void Awake()
    {
        StartCoroutine(StartDoneChenge());
        SonNomRenderer = GetComponent<SpriteRenderer>();
        SonNomRenderer.sprite = SonNomSprites[Random.Range(0, SonNomSprites.Length)];
    }
    private void Update()
    {
        if (!done)
        {
            time += Time.deltaTime/5f;
            transform.position = new Vector3(Mathf.Lerp(start.position.x, end.position.x, time),transform.position.y,transform.position.z);
            if (Mathf.Abs(transform.position.x - end.position.x) < 0.01f)
            {
                time = 0;
                transform.position = end.position;
                Done = true;
            }
        }
    }
    IEnumerator StartDoneChenge()
    {
        transform.position = start.position;
        yield return new WaitForSeconds(3);
        done = false;
    }
    public IEnumerator NextDoneChenge()
    {
        time = 0;
        while (Mathf.Abs(transform.position.x - endEnd.position.x) > 0.01f)
        {
            time += Time.deltaTime / 5f;
            transform.position = new Vector3(Mathf.Lerp(end.position.x, endEnd.position.x, time), transform.position.y, transform.position.z);
            yield return null;
        }
        transform.position = start.position;
        SonNomRenderer.sprite = SonNomSprites[Random.Range(0,SonNomSprites.Length)];
        time = 0;
        yield return new WaitForSeconds(3);
        done = false;
    }
}
