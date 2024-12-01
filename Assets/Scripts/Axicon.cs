using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Axicon : Entity
{
    public Laser laser1;
    public Laser laser2;

    public Vector2 laser1Direction;
    public Vector2 laser2Direction;
    private int currentDirectionIndex = 0;
    public List<Tuple<Laser, Vector2, Vector2>> collisions = new();
    public int currentNumberCollision = 0;

    public void Awake()
    {
        laser1 = GetLaserByName("Laser1");
        laser2 = GetLaserByName("Laser2");
    }

    private void CalculateNumberCollision()
    {
        currentNumberCollision = 0;
        var collisions_copy = collisions.ToArray();
        foreach (var collision in collisions_copy)
        {
            Vector2 direction = collision.Item2; // Direction of the laser
            Laser laser = collision.Item1;      // Laser instance
            Vector2 position = collision.Item3;

            if (laser.lastCast != direction || laser.lastPosition != position)
            {
                collisions.Remove(collision);
            }
            else
            {
                currentNumberCollision++;
            }
        }
    }

    public void Update()
    {
        if (currentNumberCollision < 2)
        {
            StopGenerating();
            isGenerating = false;
        }

        if (isGenerating)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1) && collider != null && collider.gameObject == gameObject)
            {
                currentDirectionIndex = (currentDirectionIndex + 1) % 2;
            }

                GetComponent<BoxCollider2D>().enabled = false;
            Vector2 laserDirection;
            if (currentDirectionIndex == 0) laserDirection = laser1Direction;
            else laserDirection = laser2Direction;

            laser1.Cast(transform.position, laserDirection, 2);
            GetComponent<BoxCollider2D>().enabled = true;

            CalculateNumberCollision();
        }
    }

    private Laser GetLaserByName(string name)
    {
        GameObject laserObject = GameObject.Find(name);
        if (laserObject != null)
        {
            return laserObject.GetComponent<Laser>();
        }
        else
        {
            Debug.LogError($"Laser GameObject with name {name} not found!");
            return null;
        }
    }

    public bool IsLaserAlreadyColliding(Laser laser)
    {
        // Check if the laser is already in the list
        return collisions.Any(collision => collision.Item1 == laser);
    }

    // Laser - direction - position
    public void addCollision(Tuple<Laser, Vector2, Vector2> collision) 
    {
        collisions.Add(collision);
    }

    public void ActivateLaser(MaterialsType type)
    {
        if (collisions.Count >= 2)
        {
            CalculateNumberCollision();
            if (currentNumberCollision >= 2)
            {
                isGenerating = true;
                laser1.type = type;
                laser2.type = type;
                laser1Direction = collisions[0].Item2.normalized;
                laser2Direction = collisions[1].Item2.normalized;
            } else
            {
                isGenerating = false;
            }
        }
    }
    public override void StopGenerating()
    {
        base.StopGenerating();
        laser1.Reset();
        laser2.Reset();
    }
}
