using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableDrawer : MonoBehaviour
{
    public float minX = -0.5f; // how far left it can go
    public float maxX = 0.5f;  // how far right it can go

    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Constrain movement to X axis only
            float newX = Mathf.Clamp(curPosition.x, minX, maxX);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}


