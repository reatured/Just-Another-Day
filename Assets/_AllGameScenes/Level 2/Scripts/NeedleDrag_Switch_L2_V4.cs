using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleDrag_Switch_L2_V4 : MonoBehaviour
{
    public NeedleDraggingBehavior_L2_V3 dragScript;
    private void Start()
    {
        dragScript = GetComponent<NeedleDraggingBehavior_L2_V3>();  
    }

    private void OnEnable()
    {
        dragScript.canDrag = true;
        print("enabled");

    }

    private void OnDisable()
    {
        dragScript.canDrag = false; 
    }
}
