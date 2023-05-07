using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Behavior_L2_V4 : MonoBehaviour
{
    public ObiRope rope;
    public ObiParticleAttachment attachment;
    public ObiRopeCursor cursor;

    public GameObject ropeHead, ropeTail;
    // Start is called before the first frame update
    void Start()
    {
        rope = GetComponent<ObiRope>();
        cursor = GetComponent<ObiRopeCursor>();

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

        //updatePointsOnRope(ropeTail.transform);

        print("position initiated0");
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
