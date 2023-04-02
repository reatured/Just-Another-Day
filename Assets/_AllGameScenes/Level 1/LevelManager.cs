using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{

    [Range(1, 5)]
    public int startFromStage = 1;
    [Space(10)]
    public Stage[] stages;
    public int totalStage;
    public int currentStage = 0;

    public UnityEvent nextLevelEvent;
    public UnityEvent setCamEvent;
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

        if (setCamEvent != null)
        {
            setCamEvent.Invoke();
        }
    }
    public Debug_CameraLerpNext camManager;
    public void setCam()
    {
        camManager.CurrentCam = startFromStage-1;
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

        if (nextLevelEvent != null)
        {
            nextLevelEvent.Invoke();
        }
        else
        {
            print("Stage Not Existing");
            
        }
        //print("Go To: " + stageIndex);
        if (stageIndex - 1 >= 0)
        {
            stages[stageIndex - 1].endStage();
        }
        else
        {
            stages[^1].endStage();
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


