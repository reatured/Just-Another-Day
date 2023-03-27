using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugSkyColor : MonoBehaviour
{
    public Color color;
    public Material skyMaterial;
    public Light directLight;


    private void Update()
    {
        RenderSettings.ambientSkyColor = color;
        directLight.color = color;
        skyMaterial.SetColor("_Color2", color);
    }
}
