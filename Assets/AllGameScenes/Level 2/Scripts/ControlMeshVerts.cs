using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeshVerts : MonoBehaviour
{
    public bool showMesh; 
    public PDvert pdvert; 
    public int index; 
    public MeshManager meshManager;

    public GameObject needle;
    // Start is called before the first frame update
    void Start()
    {
        needle = GameObject.Find("Needle"); 
        GetComponent<DragObjectScript>().onNeedleDragged += updateVertPos;
        GetComponent<MeshRenderer>().enabled = showMesh;

        needle.GetComponent<RopeDragger>().onDrag += updateVertPos;
    }
    public void initate()
    {
        transform.position = pdvert.position;
        index = pdvert.index;
        this.transform.parent = null; 
    }

    public void updateVertPos()
    {
        pdvert.position = transform.position;
        meshManager.updateMesh(pdvert);
        
    }

    
}
