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
    public void Cast(Vector2 Origin, Vector2 Orientation)
    {
        int layer_mask = LayerMask.GetMask("Obstacles", "DragAndDropEntity");
        var hit = Physics2D.Raycast(transform.position, Orientation, 1000f, layer_mask);
        lineRenderer.SetPosition(0,Origin);
        if(hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.tag == "Mirror")
            {
                hit.collider.gameObject.GetComponent<Mirror>().ActivateLaser(type);
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
        }
        else
        {
            lineRenderer.SetPosition(1, Orientation * 1000f);
        }
        
        
    }
    public void Reset()
    {
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,transform.position);
    }
}
