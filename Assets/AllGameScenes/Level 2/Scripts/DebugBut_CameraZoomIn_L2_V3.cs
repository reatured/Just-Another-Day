using UnityEngine;
using UnityEngine.UI;

public class DebugBut_CameraZoomIn_L2_V3 : MonoBehaviour
{
    public Camera mainCam;
    Button button;
    bool isFar;
    public bool IsFar
    {
        get { return isFar; }
        set
        {

            isFar = value;
            mainCam.fieldOfView = isFar ? 40 : 60;

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(zoomIn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void zoomIn()
    {
        IsFar = !IsFar;
    }
}
