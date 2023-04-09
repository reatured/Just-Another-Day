using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager_L4 : MonoBehaviour
{
    public GameObject toolInHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pickUpObject(GameObject tool)
    {
        toolInHand = tool;
        toolInHand.GetComponent<ToolProperty_L4>().enabled = true;
    }

    public void putDownObject()
    {
        toolInHand.GetComponent<ToolProperty_L4>().enabled = false;
        toolInHand = null;
    }
}
