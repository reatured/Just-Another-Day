using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Painting_L3 : MonoBehaviour
{
    public Texture2D[] layers;
    public GameObject paintingPaper;
    public Texture2D paintingTexture;
    public Material paintingMaterial;

    public Vector3 contactPoint; 
    // Start is called before the first frame update
    void Start()
    {
        getPaperSize();
        paintingMaterial = paintingPaper.GetComponent<MeshRenderer>().materials[0];
        //paintingTexture = new Texture2D(3300, 2550);

        //for (int y = 0; y < paintingTexture.height; y++)
        //{
        //    for (int x = 0; x < paintingTexture.width; x++)
        //    {
        //        Color color = layers[1].GetPixel(x, y);
        //        Color original = paintingTexture.GetPixel(x, y);
        //        paintingTexture.SetPixel(x, y, Color.Lerp(color, original, 0.5f));
        //    }
        //}
        //paintingTexture.Apply();

        paintingMaterial.SetTexture("_Texture2D", paintingTexture);
        paintingMaterial.SetVector("_ClickedPos", new Vector2(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 uv = Vector2.zero; 
    private void OnMouseDown()
    {
        contactPoint = getImpactPoint();
        uv.x = Vector3.Dot(contactPoint - b.position, widthVec);
        uv.y = Vector3.Dot(contactPoint - b.position, lengthVec);
        print(uv);

    }


    //===============Helper Script=======================
    //collider impactPoint;
    public Collider movementPlane;
    public Vector3 impactPoint;
    Ray ray;
    RaycastHit hit;
    public Vector3 getImpactPoint()
    {
        print(impactPoint);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementPlane.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;


        }
        return impactPoint;
    }

    //  A-D             Width
    //  | |     length          Length
    //  B-C             Width
    public float length;
    public float width;
    public Transform a, b, c;

    public Vector3 lengthVec, widthVec;

    public void getPaperSize() {
        length = Vector3.Distance(a.position, b.position);
        width = Vector3.Distance(b.position, c.position);
        lengthVec = a.position - b.position;
        widthVec = c.position - b.position;

    }

}
