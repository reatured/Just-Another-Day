using JetBrains.Annotations;
using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ReadRopeData : MonoBehaviour
{
    public ObiRope rope;
    public ObiParticleAttachment attachment;

    public bool elongateRope = true;
    public bool AttachingToHole = true; 
    public ObiRopeCursor cursor;

    public float d_lastDist, d_lastSegment, d_lastDist101;
    public bool d_isTight;

    public GameObject ropeHead, ropeTail;

    [Range(0.8f, 1.4f)]
    public float tightThreshold = 1f;
    public float stitchedThreshold = 2f;

    public List<Transform> pointsOnRope = new List<Transform>();
    public List<int> elementSteps = new List<int>();
    public List<int> d_elements;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rope = GetComponent<ObiRope>();

        attachment = GetComponent<ObiParticleAttachment>();
        cursor = GetComponent<ObiRopeCursor>();

        if (elongateRope) ropeHead.GetComponent<RopeDragger>().onDrag += draggingTheRope;
        if (AttachingToHole) ropeHead.GetComponent<RopeDragger>().onAttachingRope += attachingRopeToHole;

        pointsOnRope.Add(ropeTail.transform);
        elementSteps.Add(0);
        initiateRopePosition();

    }

    public void initiateRopePosition()
    {
        //attaching head and tail to the needle and tail object.
        syncRopePosition(ropeHead, 0);
        var group = ScriptableObject.CreateInstance<ObiParticleGroup>();
        group.particleIndices.Add(rope.elements[0].particle1); // index of the particle in the actor

        attachment = this.gameObject.AddComponent<ObiParticleAttachment>();
        attachment.target = ropeHead.transform;
        attachment.particleGroup = group;

        syncRopePosition(ropeTail, 0, false);

        group = ScriptableObject.CreateInstance<ObiParticleGroup>();
        group.particleIndices.Add(rope.elements[^1].particle2); // index of the particle in the actor

        attachment = this.gameObject.AddComponent<ObiParticleAttachment>();
        attachment.target = ropeTail.transform;
        attachment.particleGroup = group;

        updatePointsOnRope(ropeTail.transform);

        print("position initiated0");
    }


    public void draggingTheRope()
    {

        //stage 0
        if (isActiveSegmentTight())
        {
            pullingBehavior();
        }
        
    }

    //stage 1
    public bool isActiveSegmentTight()
    {
        float activeDist = getDistance(ropeHead.transform, pointsOnRope[^1]);
        float activeSegment = getActiveSegmentLength();

        bool result = activeDist * tightThreshold >= activeSegment;

        return result;
    }

    public float getDistance(Transform trans1, Transform trans2)
    {
        float dist = Vector3.Distance(trans1.position, trans2.position);

        return dist;

    }

    public float getActiveSegmentLength()
    {
        int elementCount = rope.elements.Count;
        float lastElementStep = elementSteps[^1];

        float percentile = (elementCount - lastElementStep) / elementCount;

        float length = rope.restLength * percentile;
        return length;
    }

    //stage 2
    public void pullingBehavior()
    {
        print("pulling");
        if (isLastSegmentTight())
        {
            elongatingRope(); //stage 4 option 1
        }
    }

    
    public bool isLastSegmentTight()
    {
        if (elementSteps.Count < 2) return true;

        float lastDist = getDistance(pointsOnRope[^2], pointsOnRope[^1]);
        float lastSegment = getLastSegmentLength();

        bool isTight = lastDist * tightThreshold >= lastSegment;

        d_lastDist = lastDist;
        d_lastSegment = lastSegment;
        d_lastDist101 = lastDist * tightThreshold;
        d_isTight = isTight;
        if (isTight)
        {
        //stage 3
            if(lastDist > stitchedThreshold) 
            {
                stitching(pointsOnRope[^1], pointsOnRope[^2]); //stage 4 option 2
                return false;
            }
            
        }
        else
        {
            tighteningLastSegment(); //stage 4 option 3
        }



        //Debug.Log("Last Segment is tight: " + result);
        return isTight;
    }
    

    public float getLastSegmentLength()
    {
        int elementCount = rope.elements.Count;
        int elementDelta = elementSteps[^1] - elementSteps[^2];

        float percentile = (float)elementDelta / elementCount;

        float length = rope.restLength * percentile;
        return length;
    }
    //stage 4

    
    public void tighteningLastSegment()
    {
        

        elementSteps[^1] -= 1;
        
        d_elements = new List<int>();
        for(int i = rope.elements.Count -1 ; i >= 0; --i)
        {
            d_elements.Add(rope.elements[i].particle2);
        }

        int target = rope.elements[^elementSteps[^1]].particle2;
        print("tightening: " + target);
        rope.solver.positions[target] = pointsOnRope[^1].transform.position;

        var group = ScriptableObject.CreateInstance<ObiParticleGroup>();
        group.particleIndices.Add(target);
        
        attachment.particleGroup = group;

        print(attachment.particleGroup.particleIndices[0]);
 
    }

    public void elongatingRope()
    {
        print("Elongating" + rope.restLength);
        float dist = (ropeHead.transform.position - pointsOnRope.Last().position).magnitude;
        float activeSegment = getActiveSegmentLength();
        float ropeDelta = dist - activeSegment; 
       
        //debug.log("distance: " + dist + "\nrope length: " + ropelength);
        if (dist > activeSegment)
        {
            cursor.ChangeLength(rope.restLength + ropeDelta);
        }
        print("Elongated" + rope.restLength);
    }
    public float stitichingStep = 1f;
    public float stitichingSpeed = 1f; 
    public void stitching(Transform obj1, Transform obj2)
    {
        print("stitching");
        stitichingStep = rope.restLength / rope.elements.Count;


        obj1.LookAt(obj2);
        obj2.LookAt(obj1);

        obj1.Translate(Vector3.forward * stitichingStep * stitichingSpeed * Time.deltaTime);
        obj2.Translate(Vector3.forward * stitichingStep * stitichingSpeed * Time.deltaTime);
    }




    //on attaching to new point;

    public void attachingRopeToHole(Transform hole)
    {
        //1. move the particle to the hole
        syncRopePosition(hole.transform, 1);
        //2. instantiate a group and attach the group to the whole
        var group = ScriptableObject.CreateInstance<ObiParticleGroup>();
        group.particleIndices.Add(rope.elements[0].particle2); // index of the particle in the actor

        attachment = this.gameObject.AddComponent<ObiParticleAttachment>();
        attachment.target = hole;
        attachment.particleGroup = group;

        updatePointsOnRope(hole);
    }

    
    //===============Helper Script=======================
    public void syncRopePosition(Transform targetTrans, int vertices, bool startFromHead)
    {
        //int targetParticle = startFromHead ? rope.elements[vertices].particle1 : rope.elements[rope.elements.Count - 1 - vertices].particle2;
        int targetParticle;
        if (startFromHead)
        {
            targetParticle = rope.elements[vertices].particle1;
        }
        else
        {
            targetParticle = rope.elements[rope.elements.Count - 1 - vertices].particle2;
        }

        rope.solver.positions[targetParticle] = targetTrans.position;

    }

    public void syncRopePosition(Transform targetTrans, int vertices)
    {
        syncRopePosition(targetTrans, vertices, true);
    }

    public void syncRopePosition(GameObject targetObj, int vertices, bool startFromHead)
    {
        syncRopePosition(targetObj.transform, vertices, startFromHead);
    }

    public void syncRopePosition(GameObject targetObj, int vertices)
    {
        syncRopePosition(targetObj.transform, vertices, true);
    }

    public void updatePointsOnRope(Transform hole)
    {
        pointsOnRope.Add(hole);
        elementSteps.Add(rope.elements.Count - 1);
    }


}
