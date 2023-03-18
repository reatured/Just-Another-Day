using UnityEngine;

public class BrushBehavior_L3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform restTransform;
    public Transform pickUpTransform;

    public Transform movingObjTrans;
    bool pickedUp = false;
    public bool PickedUp
    {
        get { return pickedUp; }
        set
        {
            pickedUp = value;

            if (pickedUp)
            {
                Cursor.visible = false;
                copyTransform(pickUpTransform, movingObjTrans);
            }
            else
            {
                Cursor.visible = true;
                copyTransform(restTransform, movingObjTrans);
            }

        }
    }

    public GameObject brushHead; 
    private Material mat; 
    private Color brushColor = Color.white;
    public Painting_L3 paintingManager; 
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
    


    private void OnMouseDown()
    {
        if(!PickedUp)
        {
            PickedUp = true;

        }
        else
        {
            if (getImpactPoint(brushRestCollider, getRay(transform, brushHead.transform)))
            {
                PickedUp = false;
            }
            else
            {
                if (getImpactPoint(paperCollider, getRay(transform, brushHead.transform)))
                {
                    paintingManager.paintOnPaper(impactPoint);
                }
            }
        }

        
    }

    private void Update()
    {
        if(PickedUp)
        {
            getImpactPoint();
            movingObjTrans.position = impactPoint;
        }
    }



    //===============Helper Script=======================
    //collider impactPoint;
    public Collider movementSurface;
    public Collider brushRestCollider;
    public Collider paperCollider;
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

    public Ray getRay(Transform obj1, Transform obj2)
    {
        return new Ray(obj1.position, obj2.position - obj1.position);
    }

    public bool getImpactPoint(Collider ms, Ray _ray)
    {
        ray = _ray;
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
