using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDropManager : MonoBehaviour
{
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
                if (hit.collider.gameObject.tag == "Dragable" || hit.collider.gameObject.tag == "Mirror" || hit.collider.gameObject.tag == "Scindeur")
                {
                    hit.collider.gameObject.GetComponent<DragAndDropEntity>().isDragged = true;
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
}
