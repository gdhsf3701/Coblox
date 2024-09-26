using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Particle : MonoBehaviour
{
    public ParticleSystem particle;

    void Update()
    {

        Vector2 ming = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        particle.transform.position = ming;
    }
}
