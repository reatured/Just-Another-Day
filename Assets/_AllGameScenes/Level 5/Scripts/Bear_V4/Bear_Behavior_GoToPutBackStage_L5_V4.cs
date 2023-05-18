using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_Behavior_GoToPutBackStage_L5_V4 : MonoBehaviour
{
    public BearBehavior_L5_V4 script;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (script.stage == script.STAGE_ClickToTear)
        {
            script.nextStage();
        }

    }
}
