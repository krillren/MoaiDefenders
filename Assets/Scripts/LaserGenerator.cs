using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserGenerator : Entity
{
    public Laser laser;
    public Vector2 Orientation = Vector2.left;
    private void Awake()
    {
        laser = GetComponentInChildren<Laser>();
    }
    private void Update()
    { 
        GetComponent<BoxCollider2D>().enabled = false;
        laser.Cast(transform.position,Orientation);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
