using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Inverter : Entity
{
    public Laser laser;
    public Vector2 orientation = Vector2.zero;
    public int currentIndexType = 1;
    public void Awake()
    {
        laser = GetComponentInChildren<Laser>();
    }
    public void Update()
    {
        if (isGenerating)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1) && collider != null && collider.gameObject == gameObject)
            {
                int length = Enum.GetValues(typeof(MaterialsType)).Length;
                currentIndexType = (currentIndexType + 1) % length;
                if (currentIndexType == 0)
                {
                    currentIndexType++;
                }
                MaterialsType material = (MaterialsType)currentIndexType;
                laser.type = material;
            }
            GetComponent<BoxCollider2D>().enabled = false;
            laser.Cast(transform.position, orientation);
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ActivateLaser(Vector2 _orientation)
    {
        isGenerating = true;
        orientation = _orientation;
    }
    public override void StopGenerating()
    {
        base.StopGenerating();
        laser.Reset();  
    }
}
