using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker_L3 : MonoBehaviour
{
    public Color color = Color.white;
    public Material material;

    public Painting_L3 paintingManager;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        MK.Toon.Properties.albedoColor.SetValue(material, color);
        material = new Material(material.shader);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        paintingManager.brushColor = color; 
    }
}
