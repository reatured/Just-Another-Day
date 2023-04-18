using UnityEngine;

public class SoupBehavior_L5_V2 : MonoBehaviour
{
    public PickUpObjectPrefab_L5 pickUpScript;
    public bool itemSelected = false;
    // Start is called before the first frame update
    private void OnMouseDown()
    {//disable default behavior. ex. prevent putting down object
        itemSelected = true;
        //if (!defaultBehavior) return;
        if (pickUpScript != null)
            pickUpScript.PickedUp = true;
        //if(clickEvent != null) clickEvent.Invoke();
    }


    private void OnMouseUp()
    {
        itemSelected = false;
        //if (pickUpScript != null)
        //    pickUpScript.doubleTriggerEvent();
    }

    private void Update()
    {
        if(!itemSelected && pickUpScript != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                pickUpScript.doubleTriggerEvent();
            }
        }
            
    }

}
