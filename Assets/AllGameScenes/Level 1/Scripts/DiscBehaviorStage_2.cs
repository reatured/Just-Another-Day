using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscBehaviorStage_2 : MonoBehaviour
{
    public LevelManager levelManager;

    public Animator animator_controller;
    public DraggingBehavior currentDB;

    public bool readyToPutIntoPlayer = false;
    private Vector3 positionOfRecordOnPlayer;


    public GlobalValues globalValue;
    // Start is called before the first frame update
    void Start()
    {
        positionOfRecordOnPlayer = new Vector3(3.256f, 0f, 0.01039798f);

        Vector3 startingPosition = globalValue.stage1RecordPosition;
 
        //startingPosition.y = 0f;
        this.transform.position = startingPosition;

        


        animator_controller = GetComponentInChildren<Animator>();
        currentDB = GetComponent<DraggingBehavior>();

        currentDB.dragEnterEvent.AddListener(pickedUpY);
        currentDB.dragEndEvent.AddListener(putDownY);
    }

    public void pickedUpY()
    {
        animator_controller.SetBool("PickedUp", true);
    }

    public void putDownY()
    {
        if (!readyToPutIntoPlayer)
        {
            animator_controller.SetBool("PickedUp", false);
        }
        else
        {
            animator_controller.SetBool("OnPlayer", true);
            
            beginStage3();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Record Player")
        {
            readyToPutIntoPlayer = true; 
        }
    }

    void beginStage3()
    {
        putRecordOnPlayer();
        
    }


    float startTime; 
    void putRecordOnPlayer()
    {
        startTime = Time.time; 
        StartCoroutine(moveToward(positionOfRecordOnPlayer, 1f));
    }

    
    IEnumerator moveToward(Vector3 targetPos, float movingTime)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
 
        yield return new WaitForFixedUpdate(); 
        if(Time.time - startTime < movingTime)
        {
            StartCoroutine(moveToward(targetPos, movingTime));
        }
        else
        {
            levelManager.nextStage();
        }

    }


}
