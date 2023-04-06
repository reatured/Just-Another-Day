using System.Collections.Generic;
using UnityEngine;

public class NeedleStitchingBehavior_L2_V3 : MonoBehaviour
{
    public MeshManager_L2_V3 bearMeshManager;
    public Collider activePin;

    public List<PinOnVertex_L2_V3> pinsInOrder;
    public int activePinLeft = 0, activePinRight = 0;

    public int currentPin = 1;
    // Start is called before the first frame update
    void Start()
    {
        PinOnVertex_L2_V3[] pinsLeft, pinsRight;
        pinsLeft = bearMeshManager.pinsLeft; //original array from mesh manager
        pinsRight = bearMeshManager.pinsRight;
        int _length = pinsLeft.Length + pinsRight.Length;

        pinsInOrder.Add(pinsRight[^1]);

        //to reorder the pins in zigzag orders.
        int left = 0, right = 1;
        bool checkLeft = false;
        while (right < pinsRight.Length || left < pinsLeft.Length)
        {
            if (!checkLeft)
            {
                if (right > left)
                {
                    checkLeft = !checkLeft;

                }
                else
                {
                    pinsInOrder.Add(pinsRight[^(right + 1)]);
                    right++;
                }
            }
            else
            {
                if (right < left)
                {
                    checkLeft = !checkLeft;

                }
                else
                {
                    pinsInOrder.Add(pinsLeft[^(left + 1)]);
                    left++;
                }
            }
        }

        //set only one active pin collider 
        for (int i = 0; i < _length; i++)
        {
            pinsInOrder[i].GetComponent<Collider>().enabled = false;
        }


        PinOnVertex_L2_V3 pin = pinsInOrder[currentPin];
        pin.GetComponent<Collider>().enabled = true;
        pin.setColor(Color.red);

        alignColliderSurface();
    }

    public Transform stitchingCollider;
    public Transform lookAtTrans; 
    public void stitchTheHole()
    {
        PinOnVertex_L2_V3 pin = pinsInOrder[currentPin];
        pin.GetComponent<Collider>().enabled = false;
        pin.setColor(Color.yellow);
        currentPin += 2;
        pin = pinsInOrder[currentPin];
        pin.GetComponent<Collider>().enabled = true;
        pin.setColor(Color.red);

        alignColliderSurface();


    }

    public void alignColliderSurface()
    {
        print("aligning");
        PinOnVertex_L2_V3 pin = pinsInOrder[currentPin];
        stitchingCollider.position = pin.transform.position;
        stitchingCollider.transform.LookAt(lookAtTrans);
    }
}
