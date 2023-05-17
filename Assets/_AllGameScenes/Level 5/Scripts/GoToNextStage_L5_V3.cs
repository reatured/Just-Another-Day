using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextStage_L5_V3 : MonoBehaviour
{
    public LevelManager levelManager; 

    private void OnEnable()
    {
        levelManager.nextStage(); 

    }
}
