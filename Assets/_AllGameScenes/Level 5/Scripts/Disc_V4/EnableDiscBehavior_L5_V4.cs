using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDiscBehavior_L5_V4 : MonoBehaviour
{
    public DiscBehavior_L5_V5 script;
    private void OnEnable()
    {
        script.onStage = true;
    }

    private void OnDisable()
    {
        script.onStage = false;
    }
}
