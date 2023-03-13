using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4RecordBehavior : MonoBehaviour
{
    private float startTime;
    public float rotateSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
