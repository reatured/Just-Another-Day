using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesTransform : MonoBehaviour
{

    public Vector3 position;
    private Transform objTrans;
    private Transform rootTrans;
    private bool checkDistance = true;
    public int index;

    private DiscSnappingManager snappingManager;
    // Start is called before the first frame update
    void Start()
    {
        snappingManager = transform.parent.GetComponent<DiscSnappingManager>();
        position = transform.position;
        objTrans = transform;
        rootTrans = objTrans;
    }
    private void FixedUpdate()
    {
        position = transform.position;
    }


    public void beingDragged()
    {
        checkDistance = false;
    }

    public GameObject getParentObj()
    {
        return this.rootTrans.gameObject;
    }

    public void attachedTo(Transform parentTrans)
    {
        transform.parent = parentTrans;
        //transform.localPosition = Vector3.zero;

        PiecesTransform[] childs = GetComponentsInChildren<PiecesTransform>();
        DraggingBehavior[] dgBehavior = GetComponentsInChildren<DraggingBehavior>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].rootTrans = parentTrans;
            childs[i].GetComponent<DraggingBehavior>().draggedObj = parentTrans; 
        }
        checkDistance = false;
        //GetComponent<PiecesAttachingBehavior>().setParentRotation(GetComponentInParent<ManageDiscPieces>().getRotation());
        //GetComponent<PiecesAttachingBehavior>().enabled = true;

    }

    public void moveTowardParent()
    {
        //position = rootTrans.position;

        //transform.position = position;

        //---
        startingTime = Time.time;
        
        startingPos = position;

        StartCoroutine(animateTowardParent());
    }

    public float startingTime;
    public AnimationCurve curve;
    public float animateTime;
    public Vector3 startingPos; 

    public IEnumerator animateTowardParent()
    {
        float journeyTime = (Time.time - startingTime) / animateTime;
        position = Vector3.Lerp(startingPos, rootTrans.position, journeyTime);

        transform.position = position;

        //print(journeyTime);
        yield return new WaitForFixedUpdate();

        if (journeyTime < 1.1f)
        {
            StartCoroutine(animateTowardParent());
        }

    }

    public void moveRoot(Vector3 rootPos)
    {
        rootTrans.position = rootPos;
    }

    public void checkSnappingDistance() {
 

        rootTrans.GetComponent<PiecesTransform>().position = rootTrans.position;
        int myIndex = rootTrans.GetComponent<PiecesTransform>().index;

        snappingManager.checkDistance(myIndex);

    }

    //*Done Stop Dragging
    public void onSnapEnter()
    {
        //GetComponent<DraggingBehavior>().onDragEnd();
    }

    //*Done Change the root 
    //*Done Change the bool: checkDistance 
    // Change the draggedObj in draggingBehaivor; 
    //Move Toward the Root
    public void beSnappedTo(Transform parentTrans)
    {
        checkDistance=false;
        attachedTo(parentTrans);
        moveTowardParent();
    }

    public bool needToCheckDistance()
    {
        return checkDistance; 
    }

    public Vector3 getRootPos()
    {
        return rootTrans.position;
    }

   

}
