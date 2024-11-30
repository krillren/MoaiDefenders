using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scindeur : Entity
{
    public Laser laser2;
    public Laser laser1;
    public Vector2 Orientation2;
    public Vector2 Orientation1;
    private void Awake()
    {
        laser1 = GetComponentsInChildren<Laser>()[0];
        laser2 = GetComponentsInChildren<Laser>()[1];
    }
    private void Update()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        if (isGenerating)
        {
            laser2.Cast(transform.position, Orientation2);
            laser1.Cast(transform.position, Orientation1);
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void StopGenerating()
    {
        base.StopGenerating();
        laser1.Reset();
        laser2.Reset();
    }
}
