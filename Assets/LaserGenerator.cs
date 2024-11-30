using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserGenerator : MonoBehaviour
{
    public Laser laser;
    public Grid tilemap;
    private void Awake()
    {
        laser = GetComponentInChildren<Laser>();
    }
    private void Update()
    {
        laser.Reset();
        laser.AddPosition(transform.position);
        laser.Cast(Vector2.left);
    }
}
