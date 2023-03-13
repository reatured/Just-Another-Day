using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public Vector3 stage1RecordPosition;

    public void sendPositionToStage2(Vector3 pos)
    {
        stage1RecordPosition = pos; 
    }
}
