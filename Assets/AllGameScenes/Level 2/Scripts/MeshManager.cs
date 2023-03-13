using UnityEngine;

//Install needles on the model, and move the vertices.

public class MeshManager : MonoBehaviour
{
    public GameObject model;
    public Mesh modelMesh;
    public PDvert[] pdVerts;

    public string debugText;
    public Transform debugSphere;
    public int debugIndex = 0;


    public ControlMeshVerts[] needles; 
    
    // Start is called before the first frame update
    void Start()
    {

        needles = GetComponentsInChildren<ControlMeshVerts>();
        //initiate array of PDverts; 
        initializeData(); 

        //initiate needles and attach verts; 
        foreach (ControlMeshVerts t in needles)
        {

            t.pdvert = getClosetVert(t.transform.localPosition);
            t.initate();

            t.meshManager = this; 
        }
    }
    public void initializeData()
    {
        modelMesh = model.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = modelMesh.vertices;
        pdVerts = new PDvert[vertices.Length];
        print(pdVerts.Length);
        for (int i = 0; i < pdVerts.Length; i++)
        {
            PDvert cur = new PDvert(model.transform.TransformPoint(vertices[i]), i);
            pdVerts[i] = cur;
            debugText += cur.index + ": " + cur.position.x + "\n";
        }
    }
    public PDvert getClosetVert(Vector3 hitPos)
    {
        Vector3 cursorPos = Vector3.zero;
        Vector3[] vertices = modelMesh.vertices;
        float shortestDist = 1000;

        PDvert cloestVert = null;
        foreach (PDvert pdv in pdVerts)
        {
            float dist = (pdv.position - hitPos).magnitude;

            if (dist < shortestDist)
            {
                cursorPos = pdv.position;
                shortestDist = dist;
                cloestVert = pdv;

            }
        }
        return cloestVert;
    }

    public void updateMesh(PDvert pdv)
    {
        Vector3[] vertices = modelMesh.vertices;
        vertices[pdv.index] = model.transform.InverseTransformPoint(pdv.position);
        modelMesh.vertices = vertices;
    }
}



public class PDvert
{
    public Vector3 position;
    public int index;

    public PDvert(Vector3 position, int index)
    {
        this.position = position;
        this.index = index; 
    }

}
