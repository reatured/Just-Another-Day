using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//class that should be overrided:
//public override void initiatePlane()
//{
//    base.initiatePlane();
//}
//public override void toolUpdate()
//{
//    base.toolUpdate();
//}

//public override void clickBehavior()
//{
//    base.clickBehavior();
//}
public class PO_ToolProperty_L4_2 : PickableObject_L4_2
{
    //protected Vector3 screenPoint;
    //protected Vector3 offset;
    public float putBackAreaSize = 0.35f;
    public Plane toolMovementPlane;
    public Collider toolMovementCollider;
    public Collider holderCollider;
    public virtual void initiatePlane()
    {
        //Modify in Knife Class
    }
    //1. onMouseDown(Pick Up)!: 
    // Base Class -> pickedUp(); 
    // Tool Class -> pickUpObject(); 


    private void OnMouseDown()
    {
        Debug.Log("```````Mouse Down");
        if (isActive == false)
        {
            pickedUp(); //in Base Class
            pickUpObject(); //in Tool Class

        }
    }


    public void pickUpObject()
    {
        initiatePlane();
        Debug.Log("Set up restTransform");
        copyTransform(this.transform, restTransform);
        if(toolMovementCollider != null)
        {
            toolMovementCollider.gameObject.SetActive(true);
        }
        if (handManager.objectInHand == null)
        {
            handManager.pickUpObject(this.gameObject);
        }

        lerpToPickUpTransform();
        //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        Cursor.visible = false;
    }

    public void lerpToPickUpTransform()
    {

        pickUpTransform.position = getImpactPoint();
        print(pickUpTransform.position);
        copyTransform(pickUpTransform, this.transform);
    }


    //2. Update 
    //Hand Manager -> pickable script (base) -> here -> knife class
    public override void updateBehavior()
    {
        base.updateBehavior();

        //Debug.Log("Tool Class");
        toolUpdate();
    }
    //Here -> knife class
    public virtual void toolUpdate()
    {
        followMouse();
    }
    
    public void followMouse()
    {

        Vector3 newPosition = getImpactPoint();
        //print("New Position: " + newPosition);
        transform.position = newPosition;

        if (Input.GetMouseButtonDown(0))
        {

            ray = getRay(Camera.main.transform, transform);
            if(holderCollider.Raycast(ray, out hit, 100f)){
                copyTransform(restTransform, transform);

                Cursor.visible = true;
                putDown();
            }
        }
    }
    public override void putDownBehavior()
    {
        print("put down in 2nd level");
        base.putDownBehavior();
        if (toolMovementCollider != null)
        {
            toolMovementCollider.gameObject.SetActive(false);
        }
    }

    //3. Mouse Clicked:: Click Behavior::

    public override void clickBehavior()
    {
        base.clickBehavior();
    }

    

    //__________________Helper Functions
    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
        print("Copy Transform: "+ to.position);
    }

    public Ray getRay(Transform obj1, Transform obj2)
    {
        return new Ray(obj1.position, obj2.position - obj1.position);
    }

    Ray ray;
    RaycastHit hit;
    public Vector3 impactPoint;

    public Vector3 getImpactPoint()
    {
        float enter = 0f;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (toolMovementCollider != null)
        {
            if (toolMovementCollider.Raycast(ray, out hit, 100))
            {
                impactPoint = hit.point;
                return impactPoint;
            }

        }
        if (toolMovementPlane.Raycast(ray, out enter))
        {
            impactPoint = ray.GetPoint(enter);

        }

        return impactPoint;
    }
}
