using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDraggingBehavior_L2 : MonoBehaviour
{
    public Transform restTransform;
    public Transform pickUpTransform;
    float startTime = 0;
    public float lerpDuration;
    public bool pickedUp = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {


        print(Time.time - startTime);
        if (Time.time - startTime < lerpDuration) return;
        startTime = Time.time;
        print("clicked");
        StartCoroutine(lerpToTargetTrans());

        
    }

    IEnumerator lerpToTargetTrans()
    {
        float journeyTime = Time.time - startTime;
        if (pickedUp == false)
        {
            lerpTransform(this.transform, restTransform, pickUpTransform, journeyTime / lerpDuration);
        }
        else
        {
            lerpTransform(this.transform, pickUpTransform, restTransform, journeyTime / lerpDuration);
        }
        
        

        yield return new WaitForFixedUpdate();
        if (journeyTime < lerpDuration)
        {
            StartCoroutine(lerpToTargetTrans());
        }
        else
        {
            pickedUp = !pickedUp;
        }
    }
    //===============Helper Script=======================

    void lerpTransform(Transform target, Transform start, Transform end, float journey)
    {
        Vector3 pos = Vector3.Lerp(start.position, end.position, journey);
        Quaternion rot = Quaternion.Lerp(start.rotation, end.rotation, journey);
        target.position = pos;
        target.rotation = rot;
    }

}
