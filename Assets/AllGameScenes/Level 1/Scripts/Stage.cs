using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] objects;
    public int stageIndex;
    public string cameraTrigger;

    private void Awake()
    {
        endStage();
    }
    public virtual void startStage()
    {
        print(stageIndex);
        //Camera.main.GetComponent<Animator>().SetTrigger(cameraTrigger);
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject go = objects[i];
            if (go != null)
            {
                go.SetActive(true);
            }
        }
        
    }

    public void endStage()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject go = objects[i];
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
}
