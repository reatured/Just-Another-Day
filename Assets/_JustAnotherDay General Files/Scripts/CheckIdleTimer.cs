using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIdleTimer : MonoBehaviour
{
    public bool restarted = false; 
    public IdleTimeDetector script;
    // Start is called before the first frame update
    void Start()
    {
        print(utilityScript.restarted);
        if(restarted == true)
        {
            script.enabled = true;
        }
        else
        {
            script.enabled= false;
        }
    }

    // Update is called once per frame
    public void enableTimer()
    {
        script.enabled = true;
    }
}
