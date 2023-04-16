using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. disable default behavior. Done
//2. click anywhere else to putback the soup 
//3. click on soup to drag the soup upward. 
public class SoupBehavior_L5 : MonoBehaviour
{
    public PickUpObjectPrefab_L5 defaultScript;
    public Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        defaultScript = GetComponent<PickUpObjectPrefab_L5>();
        soupCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!getImpactPoint(soupCollider)) //put the soup back
            {
                this.enabled = false;
            }

        }
    }
    public GameObject debugSphere; 
    private void OnMouseDown() //find the offset
    {
        Cursor.visible = false;
        getImpactPoint(movementSurface);
        print("find impactPoint");
        print("transform position: " + transform.position + "\nimpactPoints: " + impactPoint);
        offset = transform.position - impactPoint;
        //debugSphere.transform.position = impactPoint;
        print(impactPoint);

    }

    private void OnMouseDrag() //drag the soup
    {
        getImpactPoint(movementSurface);
        transform.position = impactPoint + offset;

    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
    }


    private void OnEnable()
    {
        defaultScript.defaultBehavior = false;
    }

    private void OnDisable()
    {
        defaultScript.pickUpOrPutDownCoroutine();
        defaultScript.defaultBehavior = true;
        

    }

    //===============Helper Script=======================
    //collider impactPoint;
    public Collider soupCollider;
    public Collider movementSurface; 
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public bool getImpactPoint(Collider collider)
    {
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (collider.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;

            return true; 
        }
        return false;
    }
}
