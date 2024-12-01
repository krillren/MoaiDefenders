using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDropEntity : MonoBehaviour
{
    public bool isDragged = false;
    public  DragAndDropManager dragManager;
    Vector2 originalPosition;
    public Grid tilemap;
    private void Awake()
    {
        originalPosition = transform.position;
        dragManager = FindAnyObjectByType<DragAndDropManager>();
        tilemap = FindAnyObjectByType<Grid>();
    }
    private void Update()
    {
        if (isDragged && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drop();
        }
        if (isDragged) FollowMouse();
    }
    private void Drop()
    {
        isDragged = false; // Arr?ter de suivre la souris
        LayerMask layerMask = LayerMask.GetMask("DragAndDropEntity","Obstacles");
        RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.down);
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D obstacleHit = Physics2D.Raycast(GetMousePosition(), Vector2.down,layerMask);
        GetComponent<BoxCollider2D>().enabled = true;

        if (hit.collider != null && obstacleHit.collider.tag == "Ground")
        {
            var point = hit.point;
            Debug.Log("Drop on : " + hit.collider.gameObject.tag);
            var tilePos = tilemap.WorldToCell(point);
            var spriteRenderer = GetComponent<SpriteRenderer>();
            transform.position = new Vector3(tilePos.x + spriteRenderer.bounds.size.x / 2, tilePos.y + spriteRenderer.bounds.size.y / 2, transform.position.z);
            originalPosition = transform.position;
            dragManager.PlayLasers();
        }
        else
        {
            Reset();
        }
    }
    private void Reset()
    {
        isDragged = false;
        transform.position = originalPosition;
    }
    private Vector2 GetMousePosition()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        return pos;
    }
    private void FollowMouse()
    {
       Vector2 mPos = GetMousePosition();
       transform.position= new Vector3(mPos.x, mPos.y,transform.position.z); 
    }
}
