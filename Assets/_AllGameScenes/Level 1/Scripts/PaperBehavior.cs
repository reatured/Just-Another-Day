using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Send Position information to Shader for shape deform...
public class PaperBehavior : MonoBehaviour
{
    public DraggingBehavior dragScript;
    public Material paperMaterial;
    private Vector3 posShift;
    private float yShift = 0; 
    // Start is called before the first frame update
    void Start()
    {
        dragScript = GetComponent<DraggingBehavior>();
        paperMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        posShift = Vector3.Lerp(posShift, new Vector3(0f, 0.19f, 0f), 0.1f);
        paperMaterial.SetVector("_PosShift", posShift);


        //updateTouchPos(); 
        yShift = Mathf.Lerp(yShift, 0.09f, 0.1f);
        paperMaterial.SetFloat("_BendHeight", yShift);
    }

    public Vector3 offset = Vector3.zero; 
    public void setTouchPos()
    {

        //Debug.Log(paperMaterial);
        paperMaterial.SetVector("_Touch_Position", dragScript.impactPoint);
        offset = dragScript.impactPoint - transform.position;
    }

    public void resetPosShift()
    {
        posShift = Vector3.zero;
        paperMaterial.SetVector("_PosShift", posShift);

        yShift = 0f;
        paperMaterial.SetFloat("_BendHeight", yShift);
    }

    public void updateTouchPos()
    {
        paperMaterial.SetVector("_Touch_Position", offset + transform.position);
    }

    private void OnEnable()
    {
        Cursor.visible = false;

    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }


}
