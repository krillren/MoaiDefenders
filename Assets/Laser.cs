using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public List<Vector3> positions;
    LineRenderer lineRenderer;
    public void Awake()
    {
        positions = new List<Vector3>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void AddPosition(Vector3 position)
    {
        positions.Add(position);
        if(positions.Count > lineRenderer.positionCount)
        {
            lineRenderer.positionCount = positions.Count;
        }
        lineRenderer.SetPositions(positions.ToArray());
    }
    public void Cast(Vector2 Orientation)
    {
        int layer_mask = LayerMask.GetMask("Obstacles", "DragAndDropEntity");
        var hits = Physics2D.RaycastAll(transform.position, Orientation, 1000f, layer_mask);
        if (hits.Length > 1)
        {
            AddPosition(hits[1].point);
            if (hits[1].collider.gameObject.tag == "Mirror") Cast(hits[1].collider.gameObject.GetComponent<Mirror>().Orientation);

        }
    }
    public void Reset()
    {
        positions= new List<Vector3>();
    }
}
