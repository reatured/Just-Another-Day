using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PinOnVertex_L2_V3 : MonoBehaviour
{
    private Material material;
    public int index;
    public Vector3 vertPosition;
    public Vector3 vecOffset = Vector3.zero;
    public float offsetMultiplier = 1f;

    public bool pinSelected = false;

    public MeshManager_L2_V3 meshManager; 
    public Vector3 Pin
    {
        get { return transform.position; }
        set
        {
            transform.position = value;
            print("updateTransform");
        }
    }

    public Vector3 VertPosition
    {
        get { return vertPosition; }
        set
        {
            vertPosition = value;
            transform.position = vertPosition + vecOffset;

        }
    }

    public void setOffset(PinOnVertex_L2_V3 target)
    {
        Vector3 offset = vertPosition - target.vertPosition;
        print(offset.magnitude);
        offset = offset.normalized * offsetMultiplier;
        vecOffset = offset;
        VertPosition = VertPosition;
        print(gameObject.name + offset.ToString());
    }


    // Start is called before the first frame update
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = Color.black;
    }

    public void lerpTowards(Vector3 end)
    {
        startTime = Time.time;

        StartCoroutine(lerpPosition(transform.position, end, transform));
    }

    //===============Helper Script=======================
    float startTime;
    public float animationDuration;
    public UnityEvent lerpFinishEvent;
    IEnumerator lerpPosition(Vector3 start, Vector3 end, Transform movingTrans)
    {

        float journey = (Time.time - startTime) / animationDuration;
        print("journey:" + journey);




        while (journey < 1)
        {
            movingTrans.position = Vector3.Lerp(start, end, journey);
            vertPosition = movingTrans.position;
            material.color = new Color(journey, journey, 0);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;
        }

        //print(movingTrans.position);
        movingTrans.position = end;
        if (lerpFinishEvent != null)
        {
            lerpFinishEvent.Invoke();
        }


    }

    public void setColor(Color color)
    {
        material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.name == "Needle1")
        {
            pinSelected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Needle1")
        {
            pinSelected = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pinSelected)
            {
                meshManager.nextPin();
            }
        }
        
    }
}

