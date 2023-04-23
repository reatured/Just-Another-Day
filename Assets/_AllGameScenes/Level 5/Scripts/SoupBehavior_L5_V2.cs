using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class SoupBehavior_L5_V2 : MonoBehaviour
{
    public GameObject objectToPickUp;
    public GameObject objectOfRestTransform; 
    public Transform trans_PickUp, trans_rest;
    public Transform trans_Pour; 
    private bool pickedUp = false; //false: on the table
    [SerializeField] private bool defaultBehavior = true;
    public int stage = 0;
    public bool isSelected = false;
    IEnumerator lerpCoroutine;

    public int STAGE_pickUp = 0;
    public int STAGE_pickUpAnimating = 1;
    public int STAGE_pickDragging = 2;
    public int STAGE_ClickToPour = 3;
    public int STAGE_PutSoupBack = 4;

    public GameObject soupObj;

    public LevelManager level5Manager; 
    public int Stage
    {
        get { return stage; }
        set
        {
            stage = value;
            
        }
    }

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
                if(stage == STAGE_PutSoupBack)
                {
                    print("next object");
                    level5Manager.nextStage(); 
                }
            }
            StartCoroutine(lerpCoroutine);

        }
        get { return pickedUp; }
    }

    private void Start()
    {//disable coordinate transforms at the beginning
        trans_PickUp.gameObject.SetActive(false);
        trans_rest.gameObject.SetActive(false);
        inactiveSoup.SetActive(false);
    }

    private void Update()
    {
        if (stage == STAGE_pickUpAnimating || stage == STAGE_pickDragging || stage == STAGE_PutSoupBack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isSelected == false)
                {
                    PickedUp = false;
                    stage = 0;
                    
                }
            }
        }
    }



    private void OnMouseDown()
    {
        if (stage == STAGE_pickUp)
        {
            nextStage();
            lerpEndEvent.AddListener(goToStage_Drag);
            PickedUp = true;
        }
        else if (stage == STAGE_pickDragging)
        {
            StopAllCoroutines(); 
            Cursor.visible = false;
            getImpactPoint(movementSurface);
            offset = transform.position - impactPoint;
        }else if(stage == STAGE_ClickToPour)
        {
            
            lerpEndEvent.AddListener(pickUp);
            lerpEndEvent.AddListener(emptySoup);
            lerpEndEvent.AddListener(goToStage_Last);

            //empty soup.,.

            PickedUp = false;
        }else if(stage == STAGE_PutSoupBack)
        {
            lerpEndEvent.RemoveAllListeners(); 

        }
        isSelected = true;


    }

    
    

    private void OnMouseDrag()
    {
        if (stage == STAGE_pickDragging)
        {
            getImpactPoint(movementSurface);
            transform.position = impactPoint + offset;
        }
    }
    private void OnMouseUp()
    {
        isSelected = false;
        Cursor.visible = true;

    }

    public void pickUp()
    {
        PickedUp = true;
    }

    public void nextStage()
    {
        print("nextStage");
        stage++;
    }

    public void goToStage_Drag()
    {
        stage = STAGE_pickDragging; 
    }
    //Get ready for STAGE 3
    //switch the rest transform. 
    public void goToStage_Pour()
    {
        stage = STAGE_ClickToPour;

        trans_rest = trans_Pour;

        GameObject emptyObject = new GameObject(); // Set the new transform to be a child of the original transform's parent
        emptyObject.transform.SetParent(transform.parent);// Assign the same position, rotation, and scale values as the original transform
        emptyObject.transform.position = transform.position;
        emptyObject.transform.rotation = transform.rotation;
        emptyObject.transform.localScale = transform.localScale;
        Transform newTransform = emptyObject.transform; // Assign the new transform to the newly created game object

        trans_PickUp = newTransform;
    }
    public void emptySoup()
    {
        soupObj.SetActive(false);
    }
    //Get ready for STAGE 4
    //switch the rest transform back.
    //

    public void goToStage_Last()
    {
        nextStage(); 
        trans_rest = objectOfRestTransform.transform; 
    }

    public GameObject inactiveSoup; 
    private void OnDisable()
    {
        inactiveSoup.SetActive(true); 
        print("soup disabled");
    }


    //===============Helper Script=======================
    float startTime = 0;

    public float animationDuration;
    public UnityEvent lerpEndEvent;
    float journey = 0;
    bool firstTriggered = false;

    IEnumerator lerpPosition(Transform start, Transform end, Transform movingTrans, float eventWaitTime = 0.3f)
    {
        print("start lerp");
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

        }
        if (lerpEndEvent != null)
        {
            lerpEndEvent.Invoke();
            lerpEndEvent.RemoveAllListeners();
        }

        movingTrans.position = end.position;
        movingTrans.rotation = end.rotation;
        movingTrans.localScale = end.localScale;
    }

    //===============Helper Script=======================
    //collider impactPoint;
    public Collider soupCollider;
    public Collider movementSurface;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Vector3 offset; 
    public bool getImpactPoint(Collider collider)
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (collider.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;

            return true;
        }
        return false;
    }
}




