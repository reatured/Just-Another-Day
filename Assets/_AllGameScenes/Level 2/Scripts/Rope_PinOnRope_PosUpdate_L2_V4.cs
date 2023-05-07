using UnityEngine;

public class Rope_PinOnRope_PosUpdate_L2_V4 : MonoBehaviour
{
    public GameObject followedObject;
    //Rope should only be able to follow the needle after initiated properly
    public bool canUpdatePos = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canUpdatePos)
        {
            this.transform.position = followedObject.transform.position;
        }
    }

    public void updatePosition()
    {
        canUpdatePos = !canUpdatePos;

    }

    public void enableUpdate()
    {
        canUpdatePos = true;
    }
    public void disableUpdate()
    {
        canUpdatePos = false;
    }

}
