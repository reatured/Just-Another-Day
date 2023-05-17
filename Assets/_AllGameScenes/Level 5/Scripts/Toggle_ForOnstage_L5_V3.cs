using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_ForOnstage_L5_V3 : MonoBehaviour
{
    public PaintingBehavior_L5_V2 targetScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        targetScript.onStage = true;

    }

    private void OnDisable()
    {
        targetScript.onStage = false;
    }


}
