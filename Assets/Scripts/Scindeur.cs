using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scindeur : Entity
{
    public int[][] angleConfigurations = new int[2][];
    public Laser laser2;
    public Laser laser1;
    public int angleLaser1;
    public int angleLaser2;
    public Vector2 castDirection;
    public int currentAngleConfigurationIndex = 0;
    private void Awake()
    {
        angleConfigurations[0] = new int[] { 0, 90 };
        angleConfigurations[1] = new int[] { 0, -90 };
        laser1 = GetComponentsInChildren<Laser>()[0];
        laser2 = GetComponentsInChildren<Laser>()[1];
        angleLaser1 = angleConfigurations[currentAngleConfigurationIndex][0];
        angleLaser2 = angleConfigurations[currentAngleConfigurationIndex][1];
        
    }
    private void Update()
    {
        if (isGenerating)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1) && collider != null && collider.gameObject == gameObject)
            {
                currentAngleConfigurationIndex = (currentAngleConfigurationIndex + 1) % angleConfigurations.Length;
                angleLaser1 = angleConfigurations[currentAngleConfigurationIndex][0];
                angleLaser2 = angleConfigurations[currentAngleConfigurationIndex][1];
                Debug.Log(currentAngleConfigurationIndex);
            }
            Vector2 rotatedLaser1 = Rotate(castDirection, angleLaser1);
            Vector2 rotatedLaser2 = Rotate(castDirection, angleLaser2);
            GetComponent<BoxCollider2D>().enabled = false;
            laser2.Cast(transform.position, rotatedLaser2);
            laser1.Cast(transform.position, rotatedLaser1);
            GetComponent<BoxCollider2D>().enabled = true;
        }
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
