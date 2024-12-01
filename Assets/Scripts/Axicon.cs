using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axicon : Entity
{
    public Laser laser;
    public Vector2 orientation = Vector2.zero;
    public int type;
    public Tuple<Vector2, bool> incLaser1;
    public Tuple<Vector2, bool> incLaser2;
    public static readonly Vector2[] Directions = new Vector2[]
    {
        new Vector2(0, 1),        // North
        //new Vector2(0.707f, 0.707f), // Northeast
        new Vector2(1, 0),        // East
        //new Vector2(0.707f, -0.707f), // Southeast
        new Vector2(0, -1),       // South
        //new Vector2(-0.707f, -0.707f), // Southwest
        new Vector2(-1, 0),       // West
        //new Vector2(-0.707f, 0.707f)  // Northwest
    };
    public int currentDirectionIndex = 0;
    public void Awake()
    {
        laser = GetComponentInChildren<Laser>();
        incLaser1 = new Tuple<Vector2, bool>(Vector2.zero, false);
        incLaser2 = new Tuple<Vector2, bool>(Vector2.zero, false);
        orientation = Directions[currentDirectionIndex];
    }
    public void Update()
    {
        if (isGenerating)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            laser.Cast(transform.position, orientation,2);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ActivateLaser(Vector2 _orientation)
    {
        if(!incLaser1.Item2)
        {
            incLaser1 = new Tuple<Vector2, bool>(_orientation, true);
            if (_orientation + orientation == Vector2.zero) Rotate();
            return;
        }
        else if(incLaser2.Item2 || _orientation != incLaser1.Item1)
        {
            incLaser2 = new Tuple<Vector2, bool>(_orientation, true);
            isGenerating = true;
            laser.type = type;
            orientation = _orientation;
        }
        
    }
    public override void StopGenerating()
    {
        base.StopGenerating();
        incLaser1 = new Tuple<Vector2, bool>(Vector2.zero, false);
        incLaser2 = new Tuple<Vector2, bool>(Vector2.zero, false);
        laser.Reset();
    }
    public void Rotate()
    {
        currentDirectionIndex = (currentDirectionIndex + 1) % Directions.Length;
        orientation = Directions[currentDirectionIndex];
    }
}
