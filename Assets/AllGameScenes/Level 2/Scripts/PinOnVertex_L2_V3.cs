using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PinOnVertex_L2_V3 : MonoBehaviour
{
    private Material material;
    public int index; 
    // Start is called before the first frame update
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = Color.black;
    }
    void Start()
    {
        
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //print("journey:" + journey);
        movingTrans.position = Vector3.Lerp(start, end, journey);
        material.color = new Color(journey, journey, 0);
        yield return new WaitForFixedUpdate();

        journey = (Time.time - startTime) / animationDuration;
        if (journey < 1)
        {
            StartCoroutine(lerpPosition(start, end, movingTrans));
        }
        else
        {
            //print(movingTrans.position);
            movingTrans.position = end;
            if (lerpFinishEvent != null)
            {
                lerpFinishEvent.Invoke();
            }
        }

    }
}
