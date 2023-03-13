using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviorStage3 : MonoBehaviour
{

    public GameObject toneArmPivot;
    public GameObject toneArmColliderSphere;
    public bool toneArmDragState = false; 

    public Animation animTest;

    private float toneArmMinimunY, toneArmMaximunY = 0.6f;
    //private float toneArmPlayingHeight = 0.2726682f;
    private Vector3 toneArmPlayingPos = new Vector3(3.539357f, 0.2746092f, -0.3927215f);

    // Start is called before the first frame update
    void Start()
    {
        toneArmColliderSphere.SetActive(false);

        toneArmMinimunY = toneArmColliderSphere.transform.position.y; 
    }

    // Update is called once per frame
    void Update()
    {
        if(toneArmDragState == false && Input.GetMouseButtonDown(0))
        {
           toneArmDragState = isSelected(toneArmPivot);
            if (toneArmDragState) startMoveToneArm(); 
        }

        if (toneArmDragState)
        {
            draggingToneArm(); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(toneArmDragState)
            endMoveToneArm();
        }
    }

    Ray ray;
    RaycastHit hit;
    private Vector3 offset = Vector3.zero;
    public Plane hit_plane;
    public Vector3 impactPoint;
    public bool isSelected(GameObject target)
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print("Raycast performed: " + hit.collider.name);
            if (GameObject.ReferenceEquals(hit.collider.gameObject, target))
            {
                //print("hit target: " + target.name);
                impactPoint = hit.point;
                impactPoint.y = Mathf.Clamp(impactPoint.y, toneArmMinimunY, toneArmMaximunY);

                //impactPoint.z = hit.collider.gameObject.transform.position.z;
                Vector3 planeNormal = Camera.main.transform.position - impactPoint;
                hit_plane = new Plane(planeNormal, impactPoint);
                offset = hit.collider.gameObject.transform.position - impactPoint;

                //startMoveToneArm(); 
                return true;
            }
            else
            {
                float enter = 0.0f;
                if (hit_plane.Raycast(ray, out enter))
                {
                    Vector3 newPosition = ray.GetPoint(enter);
                    impactPoint = newPosition;
                    impactPoint.y = Mathf.Clamp(impactPoint.y, toneArmMinimunY, toneArmMaximunY);
                }
            }
        }
        return false;
    }

    public void startMoveToneArm()
    {
        if(d_startDraggingText) print("startMove");
        toneArmColliderSphere.SetActive(true);
    }

    public bool sphereHit = false;

    public bool d_draggingText = false;
    public bool d_endDraggingText = false;
    public bool d_startDraggingText = false;

    //public GameObject debugSphere; 
    public void draggingToneArm()
    {
        if(d_draggingText) print("dragging");
        sphereHit = isSelected(toneArmColliderSphere);

        //debugSphere.transform.position = impactPoint; 
        toneArmPivot.transform.LookAt(impactPoint);
    }

    public float toneArmPlayThreshold = 1f;

    public LevelManager levelManager; 
    public void endMoveToneArm()
    {
        if(d_endDraggingText) print("endDragging");
        toneArmDragState = false;

        float distance = Vector3.Distance(impactPoint, toneArmPlayingPos);
        print(distance);
        if (distance < toneArmPlayThreshold)
        {
            print("Start to Play: " + distance);
            startFinishStage();
            levelManager.nextStage(animateTime * 11.1f);
        }




        toneArmColliderSphere.SetActive(false);
    }
    float startingTime;
    public AnimationCurve curve;
    public float animateTime; 
    public void startFinishStage()
    {
        startingTime = Time.time;
        StartCoroutine(finishingStage());
    }

    public IEnumerator finishingStage()
    {

        float journeyTime = (Time.time - startingTime) / animateTime; 

        impactPoint = Vector3.Lerp(impactPoint, toneArmPlayingPos, journeyTime);
        toneArmPivot.transform.LookAt(impactPoint);
        yield return new WaitForFixedUpdate();

        if (journeyTime < 1) 
        {
            StartCoroutine(finishingStage());
        }
    }
}
