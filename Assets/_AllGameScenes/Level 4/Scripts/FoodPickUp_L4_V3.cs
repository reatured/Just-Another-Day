using UnityEngine;
using UnityEngine.Events;

public class FoodPickUp_L4_V3 : MonoBehaviour
{
    public int addOrder = 2;
    public Vector3 originalPos;

    public GameObject recipeCheckerObj;
    private RecipeChecker_L4_V3 recipeChecker;

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
        Cursor.visible = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnMouseDrag()
    {
        transform.position = getImpactPoint();


    }

    public UnityEvent mouseUpEvent;
    private void OnMouseUp()
    {
        Cursor.visible = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<Rigidbody>().useGravity = true;

        if (mouseUpEvent != null)
        {
            mouseUpEvent.Invoke();

        }
    }

    //===============Helper Script=======================
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
