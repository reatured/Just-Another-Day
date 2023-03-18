using UnityEngine;

public class BrushBehavior_L3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform restTransform;
    public Transform pickUpTransform;

    public Transform movingObjTrans;
    bool pickedUp = false;

    public GameObject brushHead; 
    private Material mat; 
    private Color brushColor = Color.white;
    public Color BrushColor
    {
        get { return brushColor; }
        set { brushColor = value;
            MK.Toon.Properties.albedoColor.SetValue(mat, brushColor);
        
        }
    }
    private void Start()
    {
        movementPlane = new Plane(Vector3.up, pickUpTransform.position);
        mat  = brushHead.GetComponent<MeshRenderer>().material;
    }
    public bool PickedUp
    {
        get { return pickedUp; }
        set
        {
            pickedUp = value;

            if(pickedUp )
            {
                Cursor.visible = false;
                copyTransform(pickUpTransform, transform);
            }
            else
            {
                Cursor.visible = true;
                copyTransform(restTransform, transform);
            }

        }
    }


    private void OnMouseDown()
    {
        if(!pickedUp)
        {
            PickedUp = true;
        }
        else
        {
            if (getImpactPoint(brushRestCollider))
            {
                PickedUp = false; 
            }
        }

        
    }

    private void Update()
    {
        if(pickedUp)
        {
            getImpactPoint();
            transform.position = impactPoint;
        }
    }



    //===============Helper Script=======================
    //collider impactPoint;
    public Collider movementSurface;
    public Collider brushRestCollider; 
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Plane movementPlane;
    public bool getImpactPoint()
    {
        
        float enter = 0f;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementPlane.Raycast(ray, out enter))
        {
            impactPoint = ray.GetPoint(enter);

            return true;
        }
        else
        {
            return false;
        }


    }

    public bool getImpactPoint(Collider ms)
    {
        print(impactPoint);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (ms.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;
            return true;

        }
        else
        {
            return false;
        }

    }

    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
    }

}
