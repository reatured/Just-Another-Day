using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cursor_Choose_L5_V3 : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    public Transform cursor;
    public Collider currentCollider;
    public bool checkSelection = true; 
    public bool showCursor = true;
    
    public bool ShowCursor
    {
        get { return showCursor; }
        set { showCursor = value; 
            if(showCursor)
            {
                cursor.GetComponent<MeshRenderer>().enabled = true;

            }
            else
            {
                cursor.GetComponent<MeshRenderer>().enabled = false;

            }
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(checkSelection) {CheckForSelection(); }
        
    }

    void CheckForSelection()
    {

            Ray ray = new Ray(mainCamera.transform.position, cursor.position - mainCamera.transform.position);

            RaycastHit hit;
            if (currentCollider.Raycast(ray, out hit, 100))
            {
                currentCollider.GetComponent<Object_Selection_L5_V3>().Selected = true;
            }
            else
            {
                currentCollider.GetComponent<Object_Selection_L5_V3>().Selected = false;
            }

    }
}
