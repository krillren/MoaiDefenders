using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mirror : LaserGenerator
{
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
}
