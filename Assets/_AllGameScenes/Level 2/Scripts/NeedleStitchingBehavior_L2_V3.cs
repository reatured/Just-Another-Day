using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleStitchingBehavior_L2_V3 : MonoBehaviour
{
    public MeshManager_L2_V3 bearMeshManager;
    public Collider activePin;

    public PinOnVertex_L2_V3 pinsInOrder; 
    public int activePinLeft = 0, activePinRight = 0; 
    // Start is called before the first frame update
    void Start()
    {
        PinOnVertex_L2_V3[] pinsLeft, pinsRight;
        pinsLeft = bearMeshManager.pinsLeft; //original array from mesh manager
        pinsRight = bearMeshManager.pinsRight;



    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
