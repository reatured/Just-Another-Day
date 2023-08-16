using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIdleTimer : MonoBehaviour
{
    public bool restarted = false;

    public GameObject paper;
    public float startTime;
    public float waitTIme = 3f;
    public Scene_FadeIn fadeIn;
    public GameObject credit;
    public Stage1 paperScript;
    // Start is called before the first frame update
    void Start()
    {
        print(utilityScript.restarted);
        restarted = utilityScript.restarted;
        if (restarted == true)
        {
            StartCoroutine(disablePaperWithDelay());
            startTime = Time.time;
        }

    }
    private void Update()
    {
        if (restarted == true)
        {
            if (Time.time - startTime > waitTIme)
            {
                fadeIn.fadeOutScene();
                credit.SetActive(true);
            }

        }
    }

    public void disablePaper()
    {

    }

    IEnumerator disablePaperWithDelay()
    {
        yield return new WaitForEndOfFrame();
        paper.SetActive(false);

    }

    

}
