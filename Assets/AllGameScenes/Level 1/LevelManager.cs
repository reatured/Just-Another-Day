using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Stage[] stages;
    private int totalStage;
    private int currentStage = 0;

    [Range(1, 4)]
    public int startFromStage = 1;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        totalStage = stages.Length;
        currentStage = startFromStage;
        goToStage(currentStage);

        
    }


    public void nextStage()
    {
        currentStage++;

        goToStage(currentStage);
    }

    public void goToStage(int stageIndex)
    {
        stageIndex--; 

        if (stageIndex < stages.Length)
        {
            stages[stageIndex].startStage();
        }
        else
        {
            print("Stage Not Existing");
        }
        print("Go To: " + stageIndex);
        if (stageIndex - 1 >= 0)   
        {
            stages[stageIndex - 1].endStage();
        }
        
    }

    public void nextStageAfterSeconds(float delay)
    {
        StartCoroutine(goToStageAfterSecondsIE(delay));
    }

    public IEnumerator goToStageAfterSecondsIE(float delay)
    {
        yield return new WaitForSeconds(delay);
        nextStage(); 
    }

    public void nextStage(float delayForKillingPreviousStage)
    {
        currentStage++;

        StartCoroutine(goToStageWithDelay(currentStage, delayForKillingPreviousStage));
    }

    public IEnumerator goToStageWithDelay(int stageIndex, float delay)
    {
        stageIndex--;
        print("Go To: " + stageIndex);

        if (stageIndex < stages.Length)
        {
            stages[stageIndex].startStage();
        }
        else
        {
            print("Stage Not Existing");
        }

        yield return new WaitForSeconds(delay);


        if (stageIndex - 1 >= 0)
        {
            stages[stageIndex - 1].endStage();
        }
    }

    
}


