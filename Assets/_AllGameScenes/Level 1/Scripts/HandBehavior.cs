using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehavior : MonoBehaviour
{
    public Plane hit_plane;
    public bool t_showCursor = false; 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = t_showCursor; 
        hit_plane = new Plane(Vector3.up, transform.position);   
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if(hit_plane.Raycast(ray, out enter))
        {
            transform.position = ray.GetPoint(enter);
        }
    }
}
