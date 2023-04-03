using System.Collections;
using UnityEngine;
using UnityEngine.Events;
//      /0-1-2-3-C-5-6-7-8-c\           :pins orders        || center || pins orders, current = 9
// Head|                     |Tail      :tatal 10 pins;
//      \4-3-2-1-C-0-0-0-0-c/           :Distance to center || center || pins
//c: currentPin
//C: center
public class MeshManager_L2_V3 : MonoBehaviour
{
    public PinOnVertex_L2_V3[] pinsLeft, pinsRight, pinsInOrder;
    public Transform head;
    public Transform tail;
    public float tearLOverW = 10f;
    public int currentPin;
    public float center;
    public float tearLength;
    public float tearWidth;
    private float totalLength;
    private int totalPins;

    public GameObject debugText_distance;

    public bool alignToModel = true;
    public GameObject model;
    public Mesh modelMesh;
    private Vector3[] vertices;

    public float pin_animationDuration = 1f;
    public float offsetMultiplier = 0.1f;
    public float TearLength
    {
        get { return tearLength; }
        set
        {
            tearLength = value;
            tearWidth = tearLength / tearLOverW;
        }
    }
    public int CurrentPin
    {
        get { return currentPin; }
        set
        {
            currentPin = value;
            activePins = value + 1;
        }
    }
    private int activePins;
    public AnimationCurve tearShape;

    // Start is called before the first frame update
    void Start()
    {
        if (pinsLeft.Length != pinsRight.Length)
        {
            print("uneven pins");
        }


        totalLength = Vector3.Distance(head.position, tail.position);
        totalPins = pinsLeft.Length;//10
        CurrentPin = pinsLeft.Length;
        setAnimationDuration(pin_animationDuration);

        if (alignToModel) initiateVariables();

        //attachPinsOnVertices();
        
        //nextPin();


    }
    //Set animation time duration for each pin
    public void setAnimationDuration(float t)
    {
        for (int i = 0; i < pinsLeft.Length; i++)
        {
            PinOnVertex_L2_V3 pin = pinsLeft[i];
            pin.animationDuration = t;
            pin.offsetMultiplier = offsetMultiplier;
        }

        for (int i = 0; i < pinsRight.Length; i++)
        {

            PinOnVertex_L2_V3 pin = pinsRight[i];
            pin.animationDuration = t;
            pin.offsetMultiplier = offsetMultiplier;
        }
        animationDuration = t;
    }

    string debugText = "";
    //for align pins on the model. 
    public void initiateVariables()
    {
        modelMesh = model.GetComponent<MeshFilter>().mesh;
        vertices = modelMesh.vertices;

        //update pins in all pin script
        for (int i = 0; i < pinsLeft.Length; i++)
        {
            PinOnVertex_L2_V3 pin = pinsLeft[i];
            getClosestVertex(pin);
        }

        for (int i = 0; i < pinsRight.Length; i++)
        {

            PinOnVertex_L2_V3 pin = pinsRight[i];
            getClosestVertex(pin);
        }

        //update vecOffset in each pin script
        for (int i = 0; i < pinsLeft.Length; i++)
        {
            PinOnVertex_L2_V3 pinLeft = pinsLeft[i];
            PinOnVertex_L2_V3 pinRight = pinsRight[i];
            pinLeft.setOffset(pinRight);
            pinRight.setOffset(pinLeft);


        }



        //print(debugText);
    }

    //helper script for updating the closest vertex next to the pin in the *Pin Script*
    //SHOULD only change the index in the *Pin Script
    public void getClosestVertex(PinOnVertex_L2_V3 pin)
    {
        float shortestDist = 1000;
        int shortestIndex = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexWorldPos = getVertPos(i);
            float currentDist = Vector3.Distance(vertexWorldPos, pin.transform.position);
            if (currentDist < shortestDist)
            {
                shortestIndex = i;
                shortestDist = currentDist;
            }
        }
        pin.index = shortestIndex;
        pin.vertPosition = getVertPos(pin.index);
        //update pin's position as well;
        //pin.transform.position = getVertPos(pin.index);
        debugText += pin.index + "\n";
    }

    //helper script for getting the position of the vertex
    public Vector3 getVertPos(int index)
    {
        return model.transform.TransformPoint((Vector3)vertices[index]);
    }

    //helper script for setting the position of the vertex.
    public void setVertPos(PinOnVertex_L2_V3 pin)
    {
        vertices[pin.index] = model.transform.InverseTransformPoint(pin.VertPosition);

    }

    //FUNCTION to be called when the next pin is stitched. 
    public void nextPin()
    {
        CurrentPin--; //activePins = this+1
        TearLength = 1f * activePins / totalPins * totalLength;//(9+1)/10
        //pair pins;
        for (int i = 0; i < activePins; i++)//activePins is causing the coroutine to stop when clicking too fast. 
        {
            pairPins(pinsLeft[i].transform, pinsRight[i].transform, (i + 1) * 1f / (activePins + 1));//currentPin = 9
        }
        if (activePins < totalPins) // Stitches the previous pair pins.
        {
            pairPins(pinsLeft[activePins].transform, pinsRight[activePins].transform, 1);//currentPin = 9
        }

        startTime = Time.time;
        StartCoroutine(stitchingTheTear());

    }

    //helper script for animating the stitching of each pin. 
    float startTime;
    float animationDuration;
    public UnityEvent lerpFinishEvent;
    IEnumerator stitchingTheTear()
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey <= 1)
        {
            for (int i = 0; i < activePins; i++)
            {
                setVertPos(pinsLeft[i]);
                setVertPos(pinsRight[i]);

            }
            if (activePins < totalPins) // Stitches the previous pair pins.
            {
                setVertPos(pinsLeft[activePins]);
                setVertPos(pinsRight[activePins]);
            }
            modelMesh.vertices = vertices;
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;

        }

        if (lerpFinishEvent != null)
        {
            lerpFinishEvent.Invoke();
        }

    }

    //helper script for updating the position of pins on the both side. 
   //GET RID OF THIS!!!
    public void pairPins(Transform left, Transform right, float percentile)
    {


        float distance = Vector3.Distance(left.position, right.position);
        float targetDistance = tearShape.Evaluate(percentile) * tearWidth;

        float lerpPercentage = targetDistance / distance;
        lerpPercentage = 1 - lerpPercentage;


        //percentil = 1 is center, must be smaller than 1
        Vector3 mid = (left.position + right.position) / 2;
        Vector3 tempVec = Vector3.Lerp(left.position, mid, lerpPercentage);

        left.GetComponent<PinOnVertex_L2_V3>().lerpTowards(tempVec);
        right.GetComponent<PinOnVertex_L2_V3>().lerpTowards(Vector3.Lerp(right.position, mid, lerpPercentage));

    }
}


//Useless---//print(left.gameObject.name + " --------------------------" +
//    "\npercentile: " + percentile +
//    "\nTear shape evaluated: " + tearShape.Evaluate(percentile) +
//    "\nTear shape evaluated: " + tearShape.Evaluate(percentile) +
//    "\ndistance: " + distance +
//    "\ntargetDistance: " + targetDistance +
//    "\nlerpPercentage: " + lerpPercentage +
//    "\nleftVectorPos: " + tempVec.ToString());
