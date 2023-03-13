using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DraggingBehavior : MonoBehaviour
{
    
    public bool dragState = false, selectState = false;
    public bool dragItself = true;
    public Transform draggedObj = null;


    // Start is called before the first frame update
    void Start()
    {
        if(draggedObj == null) draggedObj = transform;
    }

    // Update is called once per frame
    void Update()
    {
        checkDragState();
        if (dragState)
        {
            onDragging();
        }
    }

    Ray ray;
    RaycastHit hit;
    private Vector3 offset = Vector3.zero;
    public Plane hit_plane;
    public Vector3 impactPoint; 
    bool isSelected()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (GameObject.ReferenceEquals(hit.collider.gameObject, this.gameObject))
            {
                impactPoint = hit.point;

                hit_plane = new Plane(Vector3.up, impactPoint);
                offset = hit.collider.gameObject.transform.position - impactPoint;
                return true;
            }
        }
        return false;
    }

    void checkDragState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!dragState)
            {
                selectState = isSelected();
            }

            if (selectState)
            {
                if (!dragState)
                {
                    dragState = true;
                    onDragEnter();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (dragState)
            {
                
                onDragEnd();
            }
        }
    }

    public UnityEvent dragEnterEvent, dragEndEvent, draggingEvent;

    void onDragEnter()
    {

        dragEnterEvent.Invoke(); 
    }

    void onDragging()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (hit_plane.Raycast(ray, out enter))
        {
            Vector3 newPosition = ray.GetPoint(enter) + offset;
            newPosition.y = draggedObj.position.y; 
            draggedObj.position = newPosition;
        }

        draggingEvent.Invoke(); 
        //Add listener From DiscSnappingManager.cs 
        //For Record Pieces, check distance is added at the beginning and is removed after the record is fixed
        //Removed After the disk is fixed.

    }

    public void onDragEnd()
    {

        dragState = false;
        dragEndEvent.Invoke();
        
    }

    
}
