using UnityEngine;
using System.Collections;
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
        stageIndex = stage;
        defaultScript.defaultBehavior = stage==0?true : false;
        condition_canDrag = stage==1? true : false;
        defaultScript.pickUpEvent.RemoveAllListeners();
        defaultScript.putDownEvent.RemoveAllListeners();
        defaultScript.nextLevelEvent.RemoveAllListeners();

        switch (stage)
        {
            case 0://pick up soup
                print("Case 0");

                defaultScript.trans_rest = trans_Rest;
                defaultScript.trans_PickUp = trans_PickUp;

                defaultScript.nextLevelEvent.AddListener(nextStage);
                //defaultScript.pickUpEvent.AddListener(nextStage);
                break;

            case 1://drag the soup and disable the default behavior
                print("Case 1");
                //condition_canDrag = true; 
                break;
            case 2://pour out the soup from the bowl and have to back
                //condition_canDrag = false; 
                defaultScript.trans_rest = Trans_PourSoup;
                defaultScript.trans_PickUp = this.transform;
                defaultScript.defaultBehavior = true;

                defaultScript.putDownEvent.AddListener(pourOutSoup);

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
        defaultScript.pickUpOrPutDownCoroutine();
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!getImpactPoint(soupCollider)) //put the soup back
            {
                defaultScript.pickUpOrPutDownCoroutine();
                stageManager.CurrentStage = 0;
            }

        }
    }

    private void OnMouseDown() //find the offset
    {
        if (stageIndex != 1) return; 
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
