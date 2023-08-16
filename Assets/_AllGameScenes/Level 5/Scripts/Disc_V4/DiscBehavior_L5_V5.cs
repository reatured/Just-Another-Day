using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DiscBehavior_L5_V5 : MonoBehaviour
{
    public bool onStage = false;
    public GameObject objectToPickUp;
    public GameObject objectOfRestTransform;
    public Transform trans_PickUp, trans_rest;

    private bool pickedUp = false; //false: on the table
    [SerializeField] private bool defaultBehavior = true;
    public int stage = 0;
    public bool isSelected = false;
    IEnumerator lerpCoroutine;

    public int STAGE_pickUp = 0;
    public int STAGE_pickUpAnimating = 1;
    public int STAGE_ClickToTear = 3;
    public int STAGE_FinishingLevel = 4;


    public LevelManager level5Manager;
    public MeshCollider[] discPiecesColliders; 
    private bool canDrag = false;

    public LevelManager gameManager;
    public float delayToNextLevel = 3f; 

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
                if (stage == STAGE_FinishingLevel)
                {

                    
                }
            }
            StartCoroutine(lerpCoroutine);

        }
        get { return pickedUp; }
    }
    // Start is called before the first frame update
    void Start()
    {
        discPiecesColliders = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider col in discPiecesColliders) { col.enabled = false; }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onStage) return;
        if (stage == STAGE_pickUpAnimating)
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
    public Scene_FadeIn fadeInScript;
    private void OnMouseDown()
    {
        if (!onStage) return;
        print("paper selected");
        if (stage == STAGE_pickUp)
        {
            nextStage();
            lerpEndEvent.AddListener(goToStage_ClickToTear);
            PickedUp = true;
        }
        else if (stage == STAGE_ClickToTear)
        {
            
            foreach (MeshCollider col in discPiecesColliders) {
                if (getImpactPoint((MeshCollider)col)) {
                    col.GetComponent<Disc_TearBehavior_L5_V5>().tear();

                    bool shouldGoNextStage = true;
                    foreach (MeshCollider col2 in discPiecesColliders)
                    {
                        if (col2.GetComponent<Disc_TearBehavior_L5_V5>().isTeard == false)
                        {
                            shouldGoNextStage = false;
                        }
                    }

                    if(shouldGoNextStage)
                    {
                        stage = STAGE_FinishingLevel;
                    }
                    break;
                    
                }
            }
        }
        else if (stage == STAGE_FinishingLevel)
        {
            foreach (MeshCollider col in discPiecesColliders)
            {
               Rigidbody rb = col.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                
                
            }
            fadeInScript.closeEye();
            bgMusic.Stop();

            //gameManager.nextStageAfterSeconds(delayToNextLevel);
            Invoke("reload", 5f);
            utilityScript.restarted = true;
            stage = 100;
        }

        //isSelected = true;
    }

    public void reload()
    {
        SceneManager.LoadScene(1);
    }
    public AudioSource bgMusic; 
    public CheckIdleTimer checkIdleTimerScript;
    private void OnMouseEnter()
    {
        if (!onStage) return;
        isSelected = true;
    }
    private void OnMouseExit()
    {
        if (!onStage) return;
        isSelected = false;
    }
    //private void OnMouseDrag()
    //{
    //    if (!onStage) return;
    //    if (stage == STAGE_pickDragging && canDrag)
    //    {

    //            getImpactPoint(movementSurface);
    //            transform.position = impactPoint + offset;

    //    }
    //}
    private void OnMouseUp()
    {
        if (!onStage) return;
        canDrag = false;
        //isSelected = false;
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


    public void goToStage_ClickToTear()
    {
        canDrag = false;
        PickedUp = true;
        stage = STAGE_ClickToTear;
        //GetComponent<Collider>().enabled = false;   
        foreach (MeshCollider col in discPiecesColliders) { col.enabled = true; }
    }

    //===============Helper Script=======================
    float startTime = 0;

    public float animationDuration;
    public UnityEvent lerpEndEvent;
    public float journey = 0;


    IEnumerator lerpPosition(Transform start, Transform end, Transform movingTrans, float eventWaitTime = 0.3f)
    {
        print("start lerp");
        journey = (Time.time - startTime) / animationDuration;
        //bool activated = false;
        Vector3 startPos = start.position;
        Vector3 endPos = end.position;
        Quaternion startRotation = start.rotation; 
        Quaternion endRotation = end.rotation;
        Vector3 startScale = start.localScale;
        Vector3 endScale = end.localScale;
        while (journey < 1.1f)
        {
            //print("running" + Vector3.Lerp(start, end, journey) + "\nJourney" + journey);
            movingTrans.position = Vector3.Lerp(startPos, endPos, journey);
            movingTrans.rotation = Quaternion.Slerp(startRotation, endRotation, journey);
            movingTrans.localScale = Vector3.Lerp(startScale, endScale, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;

        }
        if (lerpEndEvent != null)
        {
            lerpEndEvent.Invoke();
            lerpEndEvent.RemoveAllListeners();
        }

        movingTrans.position = endPos;
        movingTrans.rotation = endRotation;
        movingTrans.localScale = endScale;
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
