using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPieces_FallingTriggerNextStage_L5_V2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public LevelManager levelManager;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 0)
        {
            levelManager.nextStage();
            Destroy(this);
        }
    }
}
