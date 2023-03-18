using UnityEngine;

public class Painting_L3 : MonoBehaviour
{
    public Texture2D[] layers;
    public GameObject paintingPaper;
    public Texture2D paintingTexture;
    public Material paintingPaper_Mat;

    public Vector3 contactPoint;


    public Texture2D colorMap_Texture; 
    public float colorStep; 

    // Start is called before the first frame update
    void Start()
    {
        getPaperSize();
        paintingPaper_Mat = paintingPaper.GetComponent<MeshRenderer>().materials[0];

        colorStep = 40/255f -0.01f;

        for (int i = 1; i <= layers.Length; i++)
        {
            string id = "Mask_" + i;
            paintingPaper_Mat.SetTexture(id, layers[i - 1]);
            id = "Color_" + i;

            paintingPaper_Mat.SetColor(id, Color.white);
        }
        //paintingTexture = new Texture2D(3300, 2550);

        //for (int y = 0; y < colorMap_Texture.height; y++)
        //{
        //    for (int x = 0; x < colorMap_Texture.width; x++)
        //    {

        //        Color pixelColor = new Color(0,0,0,0);
        //        for(int i =  0; i < layers.Length; i++)
        //        {
        //            int _x = x / colorMap_Texture.width * layers[i].width;
        //            int _y = y / colorMap_Texture.height * layers[i].height;
        //            pixelColor += layers[i].GetPixel(_x, _y);

        //        }
        //        colorMap_Texture.SetPixel(x, y, pixelColor) ;
        //    }
        //}
        //colorMap_Texture.Apply();
        //colorMap_mat.SetTexture("Mask_0", colorMap_Texture);

        //print(paintingTexture.GetPixel(20, 20));

        //paintingMaterial.SetTexture("_Texture2D", paintingTexture);
        paintingPaper_Mat.SetVector("_ClickedPos", new Vector2(1, 1));
    }

    Vector2 uv = Vector2.zero;
    private void OnMouseDown()
    {
        contactPoint = getImpactPoint();
        Vector3 contactVec = contactPoint - b.position;
        uv.x = Vector3.Dot(contactVec, widthVec) / Vector3.SqrMagnitude(widthVec);
        uv.y = Vector3.Dot(contactVec, lengthVec) / Vector3.SqrMagnitude(lengthVec);
        print(contactVec);
        print(uv);

        float positionValue = colorMap_Texture.GetPixel((int)(uv.x * colorMap_Texture.width), (int)(uv.y * colorMap_Texture.height)).r;
        print(positionValue);
        

        int colorBlock = (int)(positionValue / colorStep);
        print(colorBlock);
        string colorID = "Color_" + (colorBlock+1);
        paintingPaper_Mat.SetColor(colorID, brushColor);

    }

    public Color brushColor; 

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

    public void getPaperSize()
    {
        length = Vector3.Distance(a.position, b.position);
        width = Vector3.Distance(b.position, c.position);
        lengthVec = a.position - b.position;
        widthVec = c.position - b.position;

    }

}
