using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker_L3 : MonoBehaviour
{
    public Color color = Color.white;
    public Material material;

    public Painting_L3 paintingManager;
    public int index; 

    public BrushBehavior_L3 brushBehavior;
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "BrushHead")
        {
            
            paintingManager.brushColor = color;
            paintingManager.currentBrushColorIndex = index;
            brushBehavior.BrushColor = color; 
        }
    }
}
