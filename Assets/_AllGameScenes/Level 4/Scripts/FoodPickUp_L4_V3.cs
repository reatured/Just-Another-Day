using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FoodPickUp_L4_V3 : MonoBehaviour
{
    public int addOrder = 2;
    public Vector3 originalPos;

    public GameObject recipeCheckerObj;
    private RecipeChecker_L4_V3 recipeChecker;

    public Transform trans_pickUp;
    public bool cutInAir = true; 

    public FoodInHand_Manager_L4_V3 hand;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        recipeChecker = recipeCheckerObj.GetComponent<RecipeChecker_L4_V3>();
        mouseUpEvent.AddListener(putBack);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if(cutInAir) { 
            pickUpToCut();
            return;
        }
        Cursor.visible = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnMouseDrag()
    {
        //transform.position = getImpactPoint();


    }

    public UnityEvent mouseUpEvent;
    private void OnMouseUp()
    {
        //Cursor.visible = true;
        ////GetComponent<Rigidbody>().isKinematic = false;
        ////GetComponent<Rigidbody>().useGravity = true;

        //if (mouseUpEvent != null)
        //{
        //    mouseUpEvent.Invoke();

        //}
    }

    //===============Helper Script=======================
    public float animationDuration;
    public float startTime; 
    public void pickUpToCut()
    {
        if (hand.HandIsFull) return;
        hand.HandIsFull = true;
        startTime = Time.time;
        StartCoroutine(lerpPosAndRotation(this.transform, trans_pickUp)); 
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator lerpPosAndRotation(Transform start, Transform end)
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey < 1)
        {
            transform.position = Vector3.Lerp(start.position, end.position, journey);
            transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, journey);

            print(journey);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;


        }

        transform.position = end.position;
        transform.rotation = end.rotation;
    }



    //collider impactPoint;
    public Collider movementSurface;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Vector3 getImpactPoint()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementSurface.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;
        }
        return impactPoint;
    }

    public void putBack()
    {

        transform.position = originalPos;
        
    }

    public void addToPot()
    {
        recipeChecker.nextStgae();

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void addFoodFeedback(bool correctItem)
    {
        mouseUpEvent.RemoveAllListeners();
        if (correctItem)
        {
            mouseUpEvent.AddListener(addToPot);
            //UnityEventTools.AddPersistentListener(mouseUpEvent, addToPot);
        }
        else
        {
            mouseUpEvent.AddListener(putBack);
            //UnityEventTools.AddPersistentListener(mouseUpEvent, putBack);
        }

    }
}
