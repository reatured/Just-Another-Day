using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MK.Toon;

public class PaintingBehavior_L5_V2 : MonoBehaviour
{
    public bool onStage = false;
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
    public int STAGE_pickDragging = 100;
    public int STAGE_ClickToTear = 3;
    public int STAGE_PutPaintingBack = 4;


    public LevelManager level5Manager;

    public GameObject fullPainting;

    public Texture newTexture;
    public Renderer[] paintingMeshs;
    public List<Material> paintingMats;
    private bool canDrag = false; 
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
        paintingMeshs = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in paintingMeshs)
        {
            foreach (Material material in renderer.materials)
            {
                paintingMats.Add(material);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onStage) return; 
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
    private void OnMouseDown()
    {
        if (!onStage) return;
        if (stage == STAGE_pickUp)
        {
            nextStage();
            lerpEndEvent.AddListener(goToStage_ClickToTear);
            PickedUp = true;
        }
        else if (stage == STAGE_pickDragging)
        {
            StopAllCoroutines();
            Cursor.visible = false;
            getImpactPoint(movementSurface);
            offset = transform.position - impactPoint;
            canDrag = true; 
        }
        else if(stage == STAGE_ClickToTear)
        {
            foreach(Material mat in paintingMats)
            {
                MK.Toon.Properties.albedoMap.SetValue(mat, newTexture);
            }
            GetComponent<Animator>().SetTrigger("Tear");
        }
        //isSelected = true;
    }

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
    public void goToStage_Drag()
    {
        stage = STAGE_pickDragging;
    }

    public void goToStage_ClickToTear()
    {
        canDrag = false;
        PickedUp = true;
        stage = STAGE_ClickToTear; 
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
