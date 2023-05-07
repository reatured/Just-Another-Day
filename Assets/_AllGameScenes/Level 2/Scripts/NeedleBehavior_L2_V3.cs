using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//probably not used

public class NeedleBehavior_L2_V3 : MonoBehaviour
{
    public GameObject currentHole; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = getImpactPoint();
        


    }

    //===============Helper Script=======================
    //collider impactPoint;
    public Collider movementSurface;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Vector3 getImpactPoint()
    {
        print(impactPoint);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementSurface.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;


        }
        return impactPoint;
    }
}
