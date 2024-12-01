using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    private MaterialsType _type;
    public Vector2 lastCast;
    public Vector2 lastPosition;
    public MaterialsType type
    {
        get => _type;
        set
        {
            _type = value;
            Color laserColor = _type.ToColor();
            lineRenderer.startColor = laserColor;
            lineRenderer.endColor = laserColor;
        }
    }
    [SerializeField] private GameObject LaserPrefab;
    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        type = MaterialsType.Wood;
    }
    public void Cast(Vector2 Origin, Vector2 Orientation, float distance = 1000f)
    {
        int layer_mask = LayerMask.GetMask("Obstacles", "DragAndDropEntity");
        var hit = Physics2D.Raycast(transform.position, Orientation, distance, layer_mask);
        lineRenderer.SetPosition(0,Origin);
        if(hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
            lastCast = Orientation;
            lastPosition = transform.position;
            if (hit.collider.gameObject.tag == "Mirror")
            {
                hit.collider.gameObject.GetComponent<Mirror>().ActivateLaser(type,Orientation);
            }
            if (hit.collider.gameObject.tag == "Scindeur")
            {
                Scindeur scindeur = hit.collider.gameObject.GetComponent<Scindeur>();
                scindeur.ActivateLaser(type);
                scindeur.castDirection = Orientation;
            }
            if (hit.collider.gameObject.tag == "Inverter")
            {
                hit.collider.gameObject.GetComponent<Inverter>().ActivateLaser(Orientation);
            }
            if(hit.collider.gameObject.tag == "Axicon")
            {
                var axicon = hit.collider.gameObject.GetComponent<Axicon>();
                if (!axicon.IsLaserAlreadyColliding(this)) axicon.addCollision(new Tuple<Laser, Vector2, Vector2>(this, Orientation, transform.position));
                axicon.ActivateLaser(type);
            }
            if (hit.collider.gameObject.tag == "Destroyable")
            {
                var destroyable = hit.collider.gameObject.GetComponent<Destroyable>();
                var parent = transform.parent;
                if (destroyable.Material == type && parent.CompareTag("Axicon"))
                {
                    destroyable.isDestroyed = true;
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, Orientation * distance);
        }
        
        
    }
    public void Reset()
    {
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,transform.position);
    }
}
