using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickUpObjectPrefab_L5 : MonoBehaviour
{
    public GameObject objectToPickUp;
    public Transform trans_PickUp, trans_rest;
    private bool pickedUp = false; //false: on the table
    [SerializeField]private bool defaultBehavior = true;
    public bool DefaultBehavior
    {
        get { return defaultBehavior; }
        set { defaultBehavior = value; }
    }

    public bool PickedUp
    {
        set
        {

            pickedUp = value;
            if (lerpCoroutine != null) StopCoroutine(lerpCoroutine);
            //GetComponent<Collider>().enabled = false;
            startTime = Time.time; 
            if (pickedUp)
            {
                lerpCoroutine = lerpPosition(objectToPickUp.transform, trans_PickUp, objectToPickUp.transform);
            }
            else
            {
                lerpCoroutine = lerpPosition(objectToPickUp.transform, trans_rest, objectToPickUp.transform);
            }
            StartCoroutine(lerpCoroutine);

        }
        get { return pickedUp; }
    }

    private void Start()
    {//disable coordinate transforms at the beginning
        trans_PickUp.gameObject.SetActive(false);
        trans_rest.gameObject.SetActive(false);
    }
    public UnityEvent clickStartEvent;
    public UnityEvent clickEndEvent;
    public UnityEvent pickUpEvent, putDownEvent;
    IEnumerator lerpCoroutine; 
    //public void pickUpOrPutDownCoroutine()
    //{
    //    if(lerpCoroutine != null) StopCoroutine(lerpCoroutine);
    //    startTime = Time.time;
        
    //}

    private void OnMouseDown()
    {//disable default behavior. ex. prevent putting down object

        //if (!defaultBehavior) return;
        PickedUp = !PickedUp;
        //if(clickEvent != null) clickEvent.Invoke();
    }

    private void OnMouseUp()
    {
        doubleTriggerEvent();
    }

    //===============Helper Script=======================
    float startTime = 0;

    public float animationDuration;
    
    float journey = 0;
    bool firstTriggered = false;

    IEnumerator lerpPosition(Transform start, Transform end, Transform movingTrans, float eventWaitTime = 0.3f)
    {
        journey = (Time.time - startTime) / animationDuration;
        //bool activated = false;

        while (journey < 1.1f)
        {
            //print("running" + Vector3.Lerp(start, end, journey) + "\nJourney" + journey);
            movingTrans.position = Vector3.Lerp(start.position, end.position, journey);
            movingTrans.rotation = Quaternion.Slerp(start.rotation, end.rotation, journey);
            movingTrans.localScale = Vector3.Lerp(start.localScale, end.localScale, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;

            //if (journey > eventWaitTime && !activated)
            //{
            //    doubleTriggerEvent(); 
            //    activated = true;
            //    print(PickedUp + " Triggered at " + journey);
            //}
        }
        
        
        GetComponent<Collider>().enabled = true;
        movingTrans.position = end.position;
        movingTrans.rotation = end.rotation;
        movingTrans.localScale = end.localScale;
    }

    public void doubleTriggerEvent()
    {
        print("Trigger: "+firstTriggered);
        if (firstTriggered)
        {
            if (pickedUp)
            {

                if (pickUpEvent != null) pickUpEvent.Invoke();

            }
            else
            {
                if (putDownEvent != null) putDownEvent.Invoke();

            }

            firstTriggered = false;
            
        }
        else
        {
            firstTriggered = true;
            print("switchTrigger");
        }
    }
}

