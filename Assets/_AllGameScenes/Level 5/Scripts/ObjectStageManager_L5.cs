using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectStageManager_L5 : MonoBehaviour
{
    public int totalStages;
    public int currentStage = 0;
    public UnityEvent<int> stageProgressEvent; 



    public int CurrentStage
    {
        get { return currentStage; }
        set
        {

            currentStage = value;
            stageProgressEvent.Invoke(value);
        }
    }

    private void Start()
    {
        CurrentStage = 0;   
    }
}

/*>. Soup Object Stages
 *  0. Pick Up Soup Stage
 *  1. Give it to sister
 *  2. Dump the soup 
 *  







*/