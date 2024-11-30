using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Mirror : Entity
{
    public Laser laser;
    public Vector2 Orientation;
    public void Awake()
    {
        laser = GetComponentInChildren<Laser>();
    }
    public void Update()
    {
        if (isGenerating)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            laser.Cast(transform.position, Orientation);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ActivateLaser(int newType)
    {
        isGenerating = true;
        laser.type = newType;
    }
    public override void StopGenerating()
    {
        base.StopGenerating();
        laser.Reset();
    }
}
