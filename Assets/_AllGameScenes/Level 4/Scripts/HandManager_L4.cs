using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HandManager_L4 : MonoBehaviour
{
    public GameObject objectInHand;

    public void pickUpObject(GameObject targetObj)
    {
        objectInHand = targetObj;
        print("PickUp Object");

    }

    public void putDownObj()
    {
        print("PutDown Object");
        objectInHand = null;
        
    }
    public UnityEvent ActiveObjectUpdateEvent; 
    private void Update()
    {
        if (objectInHand == null) return;

        PickableObject_L4_2 pickableScript = objectInHand.GetComponent<PickableObject_L4_2>();
        if (pickableScript.canUpdate == true)
        {
            ActiveObjectUpdateEvent.Invoke();
        } 

        if (Input.GetMouseButtonDown(0))//avoid pick up and clickbehavior to be called in the same frame.
        {
            if (pickableScript.canUpdate != pickableScript.isActive)
            {
                pickableScript.canUpdate = pickableScript.isActive;
            }
            else
            {
                clickBehavior();
            }
        }
    }

    public UnityEvent clickEvent; 
    public void clickBehavior()
    {
        clickEvent.Invoke(); 
        print("clicked");
    }
}
