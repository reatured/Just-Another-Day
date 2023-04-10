using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public Transform originalTrans;
    public float rotateSpeed = 1f;
    public Transform mainCam;
    public Vector2 offset = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.y < 0)
        {
            print("y<0");
            //move down
            offset.y += rotateSpeed * Time.deltaTime;
        }
        else if (mousePos.y > Screen.height)
        {
            print("y>height");
            //move up
            offset.y -= rotateSpeed * Time.deltaTime;
        }

        if (mousePos.x < 0)
        {
            print("x<0");
            //move left
            offset.x -= rotateSpeed * Time.deltaTime;
        }
        else if (mousePos.x > Screen.width)
        {
            print("x>width");
            //move right
            offset.x += rotateSpeed * Time.deltaTime;
        }

        mainCam.rotation = originalTrans.rotation;
        mainCam.Rotate(new Vector3(offset.y, offset.x, 0));
    }


}
