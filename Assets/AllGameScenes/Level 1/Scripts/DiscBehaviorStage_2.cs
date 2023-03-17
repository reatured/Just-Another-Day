using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;
public class DiscBehaviorStage_2 : MonoBehaviour
{
    public LevelManager levelManager;
    public Transform discMesh;
    public float snapDiscThreshold = 0.1f;
    public Transform discOnPlayerTrans;
    public Animator animator_controller;
    public DraggingBehavior currentDB;

    public bool readyToPutIntoPlayer = false;
    public Vector3 positionOfRecordOnPlayer;
    public float distance;
    public float pickUpHeight = 1f;
    public GlobalValues globalValue;

    public Vector3 startingPosition;

    IEnumerator lerpCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        positionOfRecordOnPlayer = discOnPlayerTrans.position;

        startingPosition = globalValue.stage1RecordPosition;
        this.transform.position = startingPosition;

        startingPosition = discMesh.localPosition;
        currentDB = GetComponent<DraggingBehavior>();

        currentDB.dragEnterEvent.AddListener(pickedUpY);

        currentDB.dragEndEvent.AddListener(putDownY);
    }
    float startTime;
    public float animationDuration;
    public void pickedUpY()
    {
        startTime = Time.time;
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }

        lerpCoroutine = lerpLocalPosition(discMesh.localPosition, startingPosition + Vector3.up * pickUpHeight, discMesh);
        StartCoroutine(lerpCoroutine);
    }

    public void putDownY()
    {
        startTime = Time.time;
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }
        if(readyToPutIntoPlayer)
        {
            UnityEventTools.AddPersistentListener(lerpFinishEvent, beginStage3);
            lerpCoroutine = lerpPosition(discMesh.position, positionOfRecordOnPlayer, discMesh);
        }
        else
        {
            lerpCoroutine = lerpLocalPosition(discMesh.localPosition, startingPosition, discMesh);
        }
        
        StartCoroutine(lerpCoroutine);

    }
    IEnumerator lerpLocalPosition(Vector3 start, Vector3 end, Transform movingTrans)
    {

        float journey = (Time.time - startTime) / animationDuration;

        movingTrans.localPosition = Vector3.Lerp(start, end, journey);
        yield return new WaitForFixedUpdate();

        journey = (Time.time - startTime) / animationDuration;
        if (journey < 1)
        {
            StartCoroutine(lerpLocalPosition(start, end, movingTrans));
        }
        else
        {

            movingTrans.localPosition = end;
        }

    }



    private void OnTriggerEnter(Collider other)
    {

        print(other.name);
        if (other.name == "Record Player")
        {

            readyToPutIntoPlayer = true;
        }
    }

    void beginStage3()
    {
        levelManager.nextStageAfterSeconds(animationDuration);

    }


    //===============Helper Script=======================

    public UnityEvent lerpFinishEvent;
    IEnumerator lerpPosition(Vector3 start, Vector3 end, Transform movingTrans)
    {

        float journey = (Time.time - startTime) / animationDuration;
        print(journey);
        movingTrans.position = Vector3.Lerp(start, end, journey);
        yield return new WaitForFixedUpdate();
        
        journey = (Time.time - startTime) / animationDuration;
        if (journey < 1)
        {
            StartCoroutine(lerpPosition(start, end, movingTrans));
        }
        else
        {
            print(movingTrans.position);
            movingTrans.position = end;
            if(lerpFinishEvent != null)
            {
                lerpFinishEvent.Invoke();
            }
        }

    }

}
