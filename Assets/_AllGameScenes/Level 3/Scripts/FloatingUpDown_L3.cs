using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUpDown_L3 : MonoBehaviour
{
    Vector3 startPos;
    public float frequency;
    public float amplitude; 
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }
    public float yOffset; 
    // Update is called once per frame
    void Update()
    {
        yOffset = Mathf.Sin(Time.time * frequency) * amplitude ;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
