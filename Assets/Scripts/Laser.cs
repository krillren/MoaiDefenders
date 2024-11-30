using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] private GameObject LaserPrefab;
    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
                hit.collider.gameObject.GetComponent<Mirror>().isGenerating = true;
            }
            if (hit.collider.gameObject.tag == "Scindeur")
            {
                hit.collider.gameObject.GetComponent<Scindeur>().isGenerating = true;
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
