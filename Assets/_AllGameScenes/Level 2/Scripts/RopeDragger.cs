using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDragger : MonoBehaviour
{


    public event Action onDrag;

    public event Action<Transform> onAttachingRope;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        if (onDrag != null) 
        onDrag(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttachingHole"))
        {

            //print("Collided");
            if (onAttachingRope != null) onAttachingRope(other.transform);

            other.enabled = false;
        }
    }
}
