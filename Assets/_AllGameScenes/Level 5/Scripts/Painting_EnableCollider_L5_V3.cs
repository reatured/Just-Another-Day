using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting_EnableCollider_L5_V3 : MonoBehaviour
{

    public PaintingBehavior_L5_V2 paintingScript; 
    private void OnEnable()
    {
        paintingScript.onStage = true; 
    }
}
