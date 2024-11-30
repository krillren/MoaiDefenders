using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    public int type = 0;
    [SerializeField] private GameObject LaserPrefab;
    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void Cast(Vector2 Origin, Vector2 Orientation)
    {
        SetColor();
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
    public void SetColor()
    {
        switch(type)
        {
            case 0:
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                break;
            case 1:
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                break;
            case 2:
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
                break;
        }
    }
}
