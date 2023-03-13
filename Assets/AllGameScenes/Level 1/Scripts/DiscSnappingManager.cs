using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscSnappingManager : MonoBehaviour
{
    public PiecesTransform[] childPiecesTransform;
    private PiecesTransform rootTransform; 
    public float snapDistance = 0.1f;
    
    private int totalPieces = 4;
    private int fixedPieces = 1;

    //public GameObject stage2Record; 

    public float pieceSnappingSpeedinSecs = 1;
    public AnimationCurve snappingCurve; 
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        childPiecesTransform = GetComponentsInChildren<PiecesTransform>();
        for(int i = 0; i < childPiecesTransform.Length; i++)
        {
            PiecesTransform currentPT = childPiecesTransform[i];
            DraggingBehavior currentDB = currentPT.GetComponent<DraggingBehavior>();

            currentPT.index = i;
            currentPT.animateTime = pieceSnappingSpeedinSecs;
            currentPT.curve = snappingCurve; 
            currentDB.dragEndEvent.AddListener(currentPT.checkSnappingDistance);

            //print("add listenneer");
        }

    }
    private float distance = 0f;

    public void checkDistance(int index)
    {
        //print("Checking Distance");
        PiecesTransform self = childPiecesTransform[index];
        for(int i = 0; i < childPiecesTransform.Length; i++)
        {
            if (i == index) continue; 

            PiecesTransform target = childPiecesTransform[i];
            if (target.needToCheckDistance() == false) continue; 

            distance = (self.position - target.position).magnitude;
            //Debug.Log("Distance to " + i + ": " + distance);
            if (distance < snapDistance)
            {
                //Debug.Log("Snapping " + i + " to " + index);
                self.onSnapEnter();
                target.beSnappedTo(self.GetComponent<Transform>());

                fixedPieces++;
                if(fixedPieces == totalPieces)
                {
                    rootTransform = self;
                    beginStage2();
                }
                return;
            }
        }
    }

    public GlobalValues globalValue; 
    public void beginStage2()
    {
        print("Begin Stage 2");
        globalValue.sendPositionToStage2(rootTransform.getRootPos());
        levelManager.nextStageAfterSeconds(1f);
    }
}
