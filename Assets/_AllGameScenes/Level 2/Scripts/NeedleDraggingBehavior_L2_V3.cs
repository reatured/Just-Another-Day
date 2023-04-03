using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;

public class NeedleDraggingBehavior_L2_V3 : MonoBehaviour
{
    public GameObject currentHole;
    public GameObject bearModel; //facing right
    public Transform trans_tiltingRight, trans_tiltingLeft;
    public float tiltingMultiplier = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = getImpactPoint();
        getRotation(); 

        

    }

    public void getRotation()
    {
        Vector3 vecCamToBear = bearModel.transform.position - Camera.main.transform.position;
        vecCamToBear = Vector3.Cross(vecCamToBear, -Vector3.up);
        Vector3 vecTONeedle = transform.position - bearModel.transform.position;
        float dotProduct = Vector3.Dot(vecTONeedle, vecCamToBear);
        dotProduct = map(dotProduct, -0.4f * tiltingMultiplier, 0.4f * tiltingMultiplier, 0, 1);
        print(dotProduct);
        transform.rotation = Quaternion.LerpUnclamped(trans_tiltingLeft.rotation, trans_tiltingRight.rotation, dotProduct);
    }

    //===============Helper Script=======================
    //collider impactPoint;
    public Collider movementSurface;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Vector3 getImpactPoint()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementSurface.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;


        }
        return impactPoint;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
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
