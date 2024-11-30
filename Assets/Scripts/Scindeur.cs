using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scindeur : Entity
{
    public Laser laser2;
    public Laser laser1;
    public Vector2 Orientation2;
    public Vector2 Orientation1;
    public Vector2 castDirection;
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
            Vector2 rotatedLaser1 = Rotate(castDirection, 45);
            Vector2 rotatedLaser2 = Rotate(castDirection, -45);
            laser2.Cast(transform.position, rotatedLaser2);
            laser1.Cast(transform.position, rotatedLaser1);
        }
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public static Vector2 Rotate(Vector2 v, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians
        float cosTheta = Mathf.Cos(angleRadians);
        float sinTheta = Mathf.Sin(angleRadians);

        float x = v.x * cosTheta - v.y * sinTheta;
        float y = v.x * sinTheta + v.y * cosTheta;

        return new Vector2(x, y);
    }

    public override void StopGenerating()
    {
        base.StopGenerating();
        laser1.Reset();
        laser2.Reset();
    }
    public void ActivateLaser(int newType)
    {
        isGenerating = true;
        laser1.type = newType;
        laser2.type = newType;
    }
}
