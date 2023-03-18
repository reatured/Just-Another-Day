using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PO_TP_KnifeBehavior_L4_2 : PO_ToolProperty_L4_2
{
    public AnimationCurve chopCurveMovement;
    private float animationProgressInSecond = 0;
    public float animationDurationInSecond;
    public float chopLength = 0.5f;
    private Vector3 choppingOffset = Vector3.zero;
    public float knifePickUpHeight; 

    public bool isChopping = false;
    public Transform choppingBoard;
    public override void initiatePlane()
    {
        base.initiatePlane();
        toolMovementPlane = new Plane(Vector3.up, choppingBoard.position + new Vector3(0, chopLength + 0.1f, 0));
        print(toolMovementPlane);
    }

    public override void toolUpdate()
    {
        base.toolUpdate();
        Vector3 tempPos = transform.position;
        tempPos.y += knifePickUpHeight;
        tempPos += choppingOffset;
        transform.position = tempPos;
    }

    public override void clickBehavior()
    {
        base.clickBehavior();
        if (!isChopping)
        {
            isChopping = true;
            StartCoroutine(chop());
        }
    }

    IEnumerator chop()
    {
        choppingOffset = new Vector3(0, -chopLength, 0);

        yield return new WaitForSeconds(animationDurationInSecond);

        choppingOffset = Vector3.zero;
        isChopping = false;
    }
}
