using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickerScript_L4 : MonoBehaviour
{
    public ToolManager_L4 toolManager;
    public Transform RestTransform;
    public float putBackAreaSize;
    // Start is called before the first frame update
    void Start()
    {
        toolManager = GetComponentInParent<ToolManager_L4>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(toolManager.toolInHand == null)
        {
            toolManager.pickUpObject(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        float distance = Vector3.Distance(this.transform.position, RestTransform.position);
        if(distance < putBackAreaSize)
        {
            toolManager.putDownObject();
        }
    }
}
