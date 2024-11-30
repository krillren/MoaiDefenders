using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDropManager : MonoBehaviour
{
    public GameObject Entities;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LayerMask layermask = LayerMask.GetMask("DragAndDropEntity");
            var hit = Physics2D.Raycast(GetMousePosition(), Vector2.down,1000f,layermask);

            if (hit.collider != null)
            {
                var point = hit.point;
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.tag == "LaserGenerator" || hit.collider.gameObject.tag == "Mirror" || hit.collider.gameObject.tag == "Scindeur" || hit.collider.gameObject.tag == "Inverter")
                {
                    hit.collider.gameObject.GetComponent<DragAndDropEntity>().isDragged = true;
                    PauseLasers();
                }
            }
        }
    }
    private Vector2 GetMousePosition()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        return pos;
    }

    public void PauseLasers()
    {
        foreach(Entity ent in Entities.GetComponentsInChildren<Entity>())
        {
            ent.StopGenerating();
        }
    }
    public void PlayLasers()
    {
        foreach (Entity ent in Entities.GetComponentsInChildren<Entity>())
        {
            if(ent.gameObject.tag == "LaserGenerator") ent.isGenerating = true;
        }
    }
}
