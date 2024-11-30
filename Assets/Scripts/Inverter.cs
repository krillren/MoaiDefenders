using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Entity
{
    public Laser laser;
    public Vector2 orientation = Vector2.zero;
    public int type;
    public void Awake()
    {
        laser = GetComponentInChildren<Laser>();
    }
    public void Update()
    {
        if (isGenerating)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            laser.Cast(transform.position, orientation);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ActivateLaser(Vector2 _orientation)
    {
        isGenerating = true;
        laser.type = type;
        orientation = _orientation;
    }
}
