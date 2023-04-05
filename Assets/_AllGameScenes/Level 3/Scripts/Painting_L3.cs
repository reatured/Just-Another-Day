using System.Collections;
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

    public Transform restTransform;
    public Transform pickUpTransform;
    public Transform movingObjTrans;
    bool pickedUp = false;

    public ColorBlock[] blocks = new ColorBlock[6];
    public int[] CorrectColorOrder;
    public int currentBrushColorIndex = 0;

    public LevelManager lm; 
    public BrushBehavior_L3 brushBehavior;
    public bool PickedUp
    {
        get { return pickedUp; }
        set
        {
            pickedUp = value;

            if (pickedUp)
            {

                copyTransform(pickUpTransform, movingObjTrans);
            }
            else
            {
                copyTransform(restTransform, movingObjTrans);
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = new ColorBlock(CorrectColorOrder[i]); //0~5
        }
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
    }

    Vector2 uv = Vector2.zero;
    private void OnMouseDown()
    {
        PickedUp = !PickedUp;
        //paintOnPaper(getImpactPoint());
    }

    public void paintOnPaper(Vector3 _contactPoint)
    {
        contactPoint = _contactPoint;
        Vector3 contactVec = contactPoint - b.position;
        uv.x = Vector3.Dot(contactVec, widthVec) / Vector3.SqrMagnitude(widthVec);
        uv.y = Vector3.Dot(contactVec, lengthVec) / Vector3.SqrMagnitude(lengthVec);
  

        float positionValue = colorMap_Texture.GetPixel((int)(uv.x * colorMap_Texture.width), (int)(uv.y * colorMap_Texture.height)).r;



        int colorBlock = (int)(positionValue / colorStep); //0~5
        print(colorBlock);
        string colorID = "Color_" + (colorBlock + 1);  //1~6
        paintingPaper_Mat.SetColor(colorID, brushColor);


        blocks[colorBlock].changeColor(currentBrushColorIndex);

        string result = "";
        bool checkingResult = true;
        for(int i = 0; i < blocks.Length; i++) {
            result += blocks[i].result.ToString();
            if (blocks[i].result == false)
            {
                checkingResult = false;
            }
        }
        print(result);

        if(checkingResult )
        {
            print("Done");
            PickedUp = true; //pickup the paper to the pick up transform
            brushBehavior.PickedUp = false; //put down brush.
            StartCoroutine(nextLevel());
        }
    }

    IEnumerator nextLevel()
    {
        
        yield return new WaitForSeconds(1);
        PickedUp = false;
        lm.nextStage();
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

    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
    }

}


public class ColorBlock
{
    public int colorIndex;
    public int currentColor;
    public bool result; 
    public ColorBlock(int colorIndex)
    {
        this.colorIndex = colorIndex;
        this.currentColor = 0;
        this.result = false;
    }

    public void changeColor(int index)
    {
        this.currentColor = index;

        Debug.Log("currentColor: " + this.currentColor + "\ncolorIndex: " + this.colorIndex);

        if(this.colorIndex == this.currentColor)
        {
            this.result = true;
        }
        else
        {
            this.result = false; 
        }
    }
}