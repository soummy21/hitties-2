using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] float power = 10f;
    [SerializeField] float maxDrag = 5f;
    [SerializeField] Vector3 offset;
    Rigidbody2D rb;
    LineRenderer lr;

    Vector3 dragStartPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        
            if (Input.GetMouseButtonDown(0))
            {
                DragStart();
            }

            if (Input.GetMouseButton(0))
            {
                Dragging();
            }

            if (Input.GetMouseButtonUp(0))
            {
                DragRelease();
            }
     
       

    }

    void DragStart()
    {
        dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPosition.z = 0.0f;
        //lr.positionCount = 1;
        //lr.SetPosition(0, transform.position);

    }

    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingPos.z = 0.0f;
        Vector3 diff = draggingPos - dragStartPosition;
        lr.positionCount = 2;
        var linePos = transform.position + offset;
        lr.SetPosition(0, linePos);
        lr.SetPosition(1, linePos + diff);
    }

    void DragRelease()
    {
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragReleasePos.z = 0.0f;
        Vector3 force = dragStartPosition - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);

        //if((clampedForce.x >=1.0f || clampedForce.x<=-1.0f) && (clampedForce.y >= 1.0f || clampedForce.y <= -1.0f))
        //{
        //    levelController.DecreamentShot();
        //}


    }
}
