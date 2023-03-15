using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Stage[] stages;
    public int totalStage;
    public int currentStage = 0;

    [Range(1, 4)]
    public int startFromStage = 1;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        totalStage = stages.Length;
        currentStage = startFromStage;
        goToStage(currentStage);
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].stageIndex = i + 1;
        }

    }


    public void nextStage()
    {
        currentStage++;
        if (currentStage > totalStage)
        {
            currentStage = 1;
        }


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


