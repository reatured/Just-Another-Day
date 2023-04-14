using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickUpObjectPrefab_L5 : MonoBehaviour
{
    public GameObject objectToPickUp;
    public Transform trans_PickUp, trans_rest;
    private bool pickedUp = false; //false: on the table
    public bool PickedUp
    {
        set
        {

            pickedUp = value;
            if (pickedUp)
            {
                StartCoroutine(lerpPosition(objectToPickUp.transform, trans_PickUp, objectToPickUp.transform));
                //StartCoroutine(lerpRotation(objectToPickUp.transform.rotation, trans_PickUp.rotation, objectToPickUp.transform));
            }
            else
            {
                StartCoroutine(lerpPosition(objectToPickUp.transform, trans_rest, objectToPickUp.transform));
                //StartCoroutine(lerpRotation(objectToPickUp.transform.rotation, trans_rest.rotation, objectToPickUp.transform));
            }

        }
        get { return pickedUp; }
    }

    private void Start()
    {
        trans_PickUp.gameObject.SetActive(false);
        trans_rest.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        StopAllCoroutines();
        startTime = Time.time;
        PickedUp = !PickedUp;
        print("clicked");

    }

    //===============Helper Script=======================
    float startTime = 0;

    public float animationDuration;
    public UnityEvent lerpFinishEvent;
    float journey = 0;
    IEnumerator lerpPosition(Transform start, Transform end, Transform movingTrans)
    {

        journey = (Time.time - startTime) / animationDuration;

        while (journey < 1.1f)
        {
            //print("running" + Vector3.Lerp(start, end, journey) + "\nJourney" + journey);
            movingTrans.position = Vector3.Lerp(start.position, end.position, journey);
            movingTrans.rotation = Quaternion.Slerp(start.rotation, end.rotation, journey);
            movingTrans.localScale = Vector3.Lerp(start.localScale, end.localScale, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;
        }

        movingTrans.position = end.position;
        movingTrans.rotation = end.rotation;
        movingTrans.localScale = end.localScale;
        if (lerpFinishEvent != null)
        {
            lerpFinishEvent.Invoke();
        }
    }

    IEnumerator lerpRotation(Quaternion start, Quaternion end, Transform movingTrans)
    {
        journey = (Time.time - startTime) / animationDuration;

        while (journey < 1.1f)
        {

            movingTrans.rotation = Quaternion.Slerp(start, end, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;
            print(journey);
        }
        movingTrans.rotation = end;

    }

}
