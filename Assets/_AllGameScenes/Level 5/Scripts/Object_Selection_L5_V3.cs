using UnityEngine;

public class Object_Selection_L5_V3 : MonoBehaviour
{
    public bool selected = false;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            if (selected != value)
            {
                selected = value;
                if (selected)
                {
                    MK.Toon.Properties.albedoColor.SetValue(objectMaterial, new Color(1, 0, 0, 1));

                }
                else
                {
                    MK.Toon.Properties.albedoColor.SetValue(objectMaterial, new Color(1, 1, 1, 1));
                }
            }
        }
    }

    private bool pickedUp = false;
    public bool PickedUp
    {
        get { return pickedUp; }
        set
        {
            pickedUp = value;
            if (pickedUp)
            {
                transform.rotation = pickedUpTransform.rotation;
                cursorScript.ShowCursor = false;
                cursorScript.checkSelection = false;
                Selected = false;
            }
            else
            {
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                cursorScript.ShowCursor = true;
                cursorScript.checkSelection = true;
            }
        }
    }

    public Transform pickedUpTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public Renderer objectMesh;
    public Material objectMaterial;
    public Transform leftHand, rightHand;
    public Cursor_Choose_L5_V3 cursorScript;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = objectMesh.material;
        cursorScript = FindObjectOfType<Cursor_Choose_L5_V3>();
        originalPosition = transform.position; 
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            holdingInHand();
            if (Input.GetMouseButtonDown(0))
            {
                PickedUp = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Selected)
                {
                    PickedUp = true;
                }
            }
        }
        
    }

    public void holdingInHand()
    {
        Vector3 midpoint = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
        transform.position = midpoint;
    }
}
