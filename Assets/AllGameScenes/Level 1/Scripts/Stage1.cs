using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{

    [Header("For Stage 1")]
    public bool showPaper;
    public GameObject paper;
    

    public override void startStage()
    {
        base.startStage();
        paper.SetActive(showPaper);
    }
}
