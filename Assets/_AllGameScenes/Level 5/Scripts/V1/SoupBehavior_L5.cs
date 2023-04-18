using System.Collections;
using UnityEngine;

//1. disable default behavior. Done
//2. click anywhere else to putback the soup 
//3. click on soup to drag the soup upward. 
public class SoupBehavior_L5 : MonoBehaviour
{
    public PickUpObjectPrefab_L5 defaultScript;
    public Vector3 offset = Vector3.zero;
    public ObjectStageManager_L5 stageManager;
    public Transform trans_Rest, trans_PickUp, Trans_PourSoup;
    public bool condition_canDrag = false;
    public GameObject soup;
    public int stageIndex = 0;
    public void checkStage(int stage)
    {
        print("checking Stage");
        stageIndex = stage;
        defaultScript.DefaultBehavior = stage == 0 ? true : false;
        condition_canDrag = stage == 1 ? true : false;
        defaultScript.pickUpEvent.RemoveAllListeners();
        defaultScript.putDownEvent.RemoveAllListeners();
        defaultScript.clickEndEvent.RemoveAllListeners();

        switch (stage)
        {
            case 0://pick up soup


                defaultScript.trans_rest = trans_Rest;
                defaultScript.trans_PickUp = trans_PickUp;

                defaultScript.clickEndEvent.AddListener(nextStage);
                defaultScript.pickUpEvent.AddListener(nextStage);
                break;

            case 1://drag the soup and disable the default behavior

                //condition_canDrag = true; 
                break;
            case 2://pour out the soup from the bowl and have to back
                //condition_canDrag = false; 
                defaultScript.trans_rest = Trans_PourSoup;
                GameObject emptyObject = new GameObject();

                // Set the new transform to be a child of the original transform's parent
                emptyObject.transform.SetParent(transform.parent);

                // Assign the same position, rotation, and scale values as the original transform
                emptyObject.transform.position = transform.position;
                emptyObject.transform.rotation = transform.rotation;
                emptyObject.transform.localScale = transform.localScale;

                // Assign the new transform to the newly created game object
                Transform newTransform = emptyObject.transform;
                defaultScript.trans_PickUp = newTransform;
                defaultScript.DefaultBehavior = true;
                defaultScript.pickUpEvent.AddListener(nextStage);
                defaultScript.putDownEvent.AddListener(pourOutSoup);

                break;
            case 3:
                defaultScript.trans_rest = trans_Rest;
                defaultScript.trans_PickUp = trans_PickUp;
                break;

        }
    }

    public void pourOutSoup()
    {
        StartCoroutine(PourOutSoupCoroutine());
    }

    IEnumerator PourOutSoupCoroutine()
    {
        print("pouring");
        soup.SetActive(false);
        yield return new WaitForSeconds(1.5f); // wait for 1.5 seconds before calling the defaultScript.pickUpOrPutDownCoroutine() method
        defaultScript.PickedUp = true; 
    }

    public void nextStage()
    {
        print("next Sage");
        stageManager.CurrentStage++;
    }

    // Start is called before the first frame update
    void Start()
    {
        Trans_PourSoup.gameObject.SetActive(false);
        defaultScript = GetComponent<PickUpObjectPrefab_L5>();
        soupCollider = GetComponent<Collider>();
    }
    private bool condition_backTo0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!getImpactPoint(soupCollider)) //put the soup back
            {
                if (stageIndex == 1 || stageIndex == 3)
                {
                    defaultScript.PickedUp = false;
                    condition_backTo0 = true;

                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (condition_backTo0 == true)
            {
                stageManager.CurrentStage = 0;
                condition_backTo0 = false;
            }

        }
    }

    private void OnMouseDown() //find the offset
    {

        if (stageIndex != 1) return;
        StopAllCoroutines(); 
        Cursor.visible = false;
        getImpactPoint(movementSurface);
        offset = transform.position - impactPoint;
        //debugSphere.transform.position = impactPoint;


    }

    private void OnMouseDrag() //drag the soup
    {
        if (stageIndex != 1) return;
        if (!condition_canDrag) return;
        getImpactPoint(movementSurface);
        transform.position = impactPoint + offset;

    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
    }

    //===============Helper Script=======================
    //collider impactPoint;
    public Collider soupCollider;
    public Collider movementSurface;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
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
