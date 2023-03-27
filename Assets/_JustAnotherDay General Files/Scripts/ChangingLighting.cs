using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangingLighting : MonoBehaviour
{
    public Camera mainCam;
    Button button;
    public Color color; 
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(changeColor);
    }

    // Update is called once per frame


    public void changeColor()
    {
        RenderSettings.ambientSkyColor = color; 
    }
}
