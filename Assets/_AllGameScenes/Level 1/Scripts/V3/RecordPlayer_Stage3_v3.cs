using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RecordPlayer_Stage3_v3 : MonoBehaviour
{
    public Collider movementSurface;
    public Collider secondaryMovementSurface;
    public Collider playerToneArmCollider;
    public Transform toneArmPivot;
    Ray ray;
    public Vector3 impactPoint;
    public bool canDrag = false;

    float yMin = 0f;

    public ToneArmHeadBehavior_Stage3_v3 toneArmHead;

    //public Collider toneArmPlayTrigger;

    public AudioSource _audio;
    public Stage4RecordBehavior disc;
    // Start is called before the first frame update
    void Start()
    {

        nextLevelButton.SetActive(false);
        yMin = toneArmPivot.position.y;
        toneArmHead.toneArmPlayEvent.AddListener(playRecord);
        toneArmHead.toneArmPauseEvent.AddListener(pauseRecord);

        disc.enabled = false;
    }

    public void pauseRecord()
    {
        print("pause record");
        _audio.Pause();
        disc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        print("mouse down");
        if (getImpactPoint(playerToneArmCollider)) canDrag = true;
    }

    private void OnMouseDrag()
    {
        if (canDrag)
        {

            if (getImpactPoint())
            {
                toneArmPivot.LookAt(impactPoint);
            }
            else
            {
                getImpactPoint(secondaryMovementSurface);
                toneArmPivot.LookAt(impactPoint);
            }
        }
    }

    private void OnMouseUp()
    {
        canDrag = false;

    }

    public void playRecord()
    {
        print("play record0;");
        _audio.Play();
        disc.enabled = true;
        if (!buttonReadyToShow)
        {
            StartCoroutine(enableNextLevelButton());
        }
    }



    public void beginStage4update()
    {
        toneArmPivot.LookAt(debugSphere.transform.position);
    }

    public LevelManager levelManager;
    public void beginStage4()
    {
        levelManager.nextStage();
    }

    public GameObject nextLevelButton;
    public float buttonWaitTime = 5;
    public bool buttonReadyToShow = false; 
    IEnumerator enableNextLevelButton()
    {
        buttonReadyToShow = true;
        yield return new WaitForSeconds(buttonWaitTime);
        levelManager.nextStage();
    }
    //===============Helper Script=======================

    //public UnityEvent lerpFinishEvent;
    //public UnityEvent lerpUpdateEvent;
    //float startTime;
    //public float animationDuration;

    //IEnumerator lerpPosition(Vector3 start, Vector3 end, Transform movingTrans)
    //{

    //    float journey = (Time.time - startTime) / animationDuration;
    //    print(journey);
    //    movingTrans.position = Vector3.Lerp(start, end, journey);
    //    if (lerpUpdateEvent != null)
    //    {
    //        lerpUpdateEvent.Invoke();
    //    }
    //    yield return new WaitForFixedUpdate();

    //    journey = (Time.time - startTime) / animationDuration;
    //    if (journey <= animationDuration)
    //    {
    //        StartCoroutine(lerpPosition(start, end, movingTrans));
    //    }
    //    else
    //    {
    //        print(movingTrans.position);
    //        movingTrans.position = end;
    //        if (lerpFinishEvent != null)
    //        {
    //            lerpFinishEvent.Invoke();
    //        }
    //    }
    //}


    //===============Helper Script=======================
    public Ray getRay(Transform obj1, Transform obj2)
    {
        return new Ray(obj1.position, obj2.position - obj1.position);
    }

    public bool getImpactPoint()
    {
        return getImpactPoint(movementSurface);
    }

    public bool getImpactPoint(Collider ms)//movement Surface
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ms.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;
        }
        else
        {
            return false;
        }
        impactPoint.y = Mathf.Max(impactPoint.y, yMin);
        //print(impactPoint);
        debuggingImpactPoint();
        return true;
    }

    public GameObject debugSphere;

    public void debuggingImpactPoint()
    {
        debugSphere.transform.position = impactPoint;
    }
}
