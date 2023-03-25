using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


//  A-D             Width
//  | |     length          Length
//  B-C             Width
public class GetPaperSize_L3 : MonoBehaviour
{
    public float length;
    public float width;
    public Transform a, b, c;

    public Vector3 lengthVec, widthVec;
    // Start is called before the first frame update
    void Start()
    {
        length = Vector3.Distance(a.position, b.position);
        width = Vector3.Distance(b.position, c.position);  
        lengthVec = a.position - b.position;
        widthVec = c.position - b.position; 
    }

}
