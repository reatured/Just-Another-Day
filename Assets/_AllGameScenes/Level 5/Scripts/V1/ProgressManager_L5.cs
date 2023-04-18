using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//1. enable colliders in order


public class ProgressManager_L5 : MonoBehaviour
{
    public int totalStages;
    public int currentStage = 0;
    public Collider[] colliders; 
    public int CurrentStage
    {
        get { return currentStage; }
        set
        {

            currentStage = value;
            enableCorrespondingCollider(currentStage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentStage = currentStage;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableCorrespondingCollider(int index)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        if (colliders.Length > 0)
        {
            colliders[index].enabled = true;
        }

    }
    public void nextStage()
    {

    }
}
