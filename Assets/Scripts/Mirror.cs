using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngineInternal;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Mirror : Entity
{
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
    public Laser laser;
    public Vector2 Orientation;
    public int currentDirectionIndex = 0;
    public void Awake()
    {
        laser = GetComponentInChildren<Laser>();
        Orientation = Directions[currentDirectionIndex];
    }
    public void Update()
    {
        if (isGenerating)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over this object's 2D collider
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1) && collider != null && collider.gameObject == gameObject)
            {
                currentDirectionIndex = (currentDirectionIndex + 1) % Directions.Length;
                Orientation = Directions[currentDirectionIndex];
            }
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
