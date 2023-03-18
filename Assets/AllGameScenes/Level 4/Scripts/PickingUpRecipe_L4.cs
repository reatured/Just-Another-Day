using UnityEngine;


public class PickingUpRecipe_L4 : MonoBehaviour
{
    public Transform trans_pickedUp, trans_rest;
    public Transform movingObjTrans;
    bool pickedUp = false;
    public bool PickedUp
    {
        get { return pickedUp; }
        set
        {
            pickedUp = value;
            if (pickedUp)
            {
                pickUp();
            }
            else
            {
                pickDown();
            }

        }
    }


    private void OnMouseDown()
    {
        PickedUp = !PickedUp;
    }



    void pickUp()
    {
        copyTransform(trans_pickedUp, movingObjTrans);
    }
    void pickDown()
    {
        copyTransform(trans_rest, movingObjTrans);
    }

    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
    }

}
