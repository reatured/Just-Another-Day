using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the base class for all items in level4
//base class addlistner to HandManager.cs
//remove listner in HandManager.cs when put down; 
//update is not allowed. Only hand manager can update.
//virtual class that should be override: 
//public override void updateBehavior()
//{
//    base.updateBehavior();
//}
//public override void clickBehavior()
//{
//    base.clickBehavior();
//}



public class PickableObject_L4_2 : MonoBehaviour
{
    public bool isActive = false;
    public bool canUpdate = false; 
    public HandManager_L4 handManager;
    public Transform restTransform, pickUpTransform;
    private void Awake()
    {
        handManager = GameObject.Find("HandManager").GetComponent<HandManager_L4>();
    }

    private void Start()
    {
        if(pickUpTransform == null)
        {
            pickUpTransform = this.transform;
        }
    }

    public virtual void updateBehavior()
    {

    }

    //called by Tool Class mouseDown
    public void pickedUp()
    {
       
        handManager.clickEvent.AddListener(clickBehavior);
        handManager.ActiveObjectUpdateEvent.AddListener(updateBehavior);
        isActive = true;
    }

    //subscribe to hand manager script's clickEvent
    public virtual void clickBehavior()
    {

    }

    //called in Tool Class in click behavior
    public void putDown()
    {
        Debug.Log("Base put down");
        putDownBehavior();
        isActive = false;
        canUpdate = false; 
        handManager.clickEvent.RemoveListener(clickBehavior);
        handManager.ActiveObjectUpdateEvent.RemoveListener(updateBehavior);
        handManager.putDownObj(); 
    }

    public virtual void putDownBehavior()
    {


    }
     

}
