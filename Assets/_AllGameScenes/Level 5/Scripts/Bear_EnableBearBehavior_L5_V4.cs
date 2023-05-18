using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_EnableBearBehavior_L5_V4 : MonoBehaviour
{
    public BearBehavior_L5_V4 script;
    private void OnEnable()
    {
        script.onStage = true;
    }

    private void OnDisable()
    {
        script.onStage = false;
    }
}
