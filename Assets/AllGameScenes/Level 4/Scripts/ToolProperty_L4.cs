using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolProperty_L4 : MonoBehaviour
{
    public bool isActive = false;
    public ToolManager_L4 toolManager;
    public Transform restTransform, pickUpTransform;
    public float putBackAreaSize;
    // Start is called before the first frame update

    private void Awake()
    {
        toolManager = GetComponentInParent<ToolManager_L4>();
        copyTransform(this.transform, restTransform);
    }
    void Start()
    {

        
        //this.enabled = false;

        print("start");
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            print("clicked");
            if (!checkIfPutBack())
            {
                clickBehavior();
            }
        }
    }

    protected Vector3 screenPoint;
    protected Vector3 offset;
    private void OnMouseDown()
    {
        if (!isActive)
        {
            pickUpObject();
        }
        
    }

    public virtual void clickBehavior()
    {
        
    }

    public bool checkIfPutBack()
    {
        float distance = Vector3.Distance(transform.position, restTransform.position);
        print("distance: " + distance);
        if(distance< putBackAreaSize)
        {
            copyTransform(restTransform, transform);
            isActive = false;
            toolManager.putDownObject();
            Cursor.visible = true;
            return true;

        }

        return false;
    }

    public void pickUpObject()
    {
        if (toolManager.toolInHand == null)
        {
            toolManager.pickUpObject(this.gameObject);
        }
        
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isActive = true;
        copyTransform(pickUpTransform, transform);
        Cursor.visible = false;
    }


    //=======================Helper Script
    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
        print(to.position);
    }

}
