using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_PinOffset_L2_V3 : MonoBehaviour
{
    public MeshManager_L2_V3 meshManager;
    public Transform left, right; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        meshManager.offsetMultiplier = Vector3.Distance(left.position, right.position);
        Destroy(gameObject);
    }

}
