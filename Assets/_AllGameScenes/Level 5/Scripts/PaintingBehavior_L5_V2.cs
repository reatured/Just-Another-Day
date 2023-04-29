using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintingBehavior_L5_V2 : MonoBehaviour
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
    public int STAGE_ClickToTear = 3;
    public int STAGE_PutPaintingBack = 4;


    public LevelManager level5Manager;
    private bool offsetUpdated = false;
    public GameObject fullPainting; 
    public bool PickedUp //for lerp animation
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
                if (stage == STAGE_PutPaintingBack)
                {
                    print("next object");
                    level5Manager.nextStage();
                }
            }
            StartCoroutine(lerpCoroutine);

        }
        get { return pickedUp; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stage == STAGE_pickUpAnimating || stage == STAGE_pickDragging || stage == STAGE_PutPaintingBack)
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
    private void OnEnable()
    {
        fullPainting.SetActive(false);
    }
    private void OnDisable()
    {
        fullPainting.SetActive(true);
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
            offsetUpdated = true; 
        }
        //else if (stage == STAGE_ClickToPour)
        //{

        //    lerpEndEvent.AddListener(pickUp);
        //    lerpEndEvent.AddListener(emptySoup);
        //    lerpEndEvent.AddListener(goToStage_Last);

        //    //empty soup.,.

        //    PickedUp = false;
        //}
        //else if (stage == STAGE_PutSoupBack)
        //{
        //    lerpEndEvent.RemoveAllListeners();

        //}
        isSelected = true;
    }
    private void OnMouseDrag()
    {
        if (stage == STAGE_pickDragging && offsetUpdated)
        {
            getImpactPoint(movementSurface);
            transform.position = impactPoint + offset;
        }
    }
    private void OnMouseUp()
    {
        offsetUpdated = false;
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
    public Collider objectCollider;
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
