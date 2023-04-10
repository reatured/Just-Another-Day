using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Tool_KnifeBehavior_L4 : ToolProperty_L4
{


    private void Update()
    {
        
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = newPosition;
        
    }

    public override void clickBehavior()
    {



        print("Knife Click Behavior");
        base.clickBehavior();
    }
}
