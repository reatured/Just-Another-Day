using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Toon; 

public class SisterFadeOut_L5 : MonoBehaviour
{
    public List<Material> sisterMaterials;
    public MeshRenderer[] sisterMeshes;
    MeshRenderer pointMesh;
    public Color matColor = Color.white;
    public int totalHealth = 5;
    public int currentHealth; 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;

        sisterMeshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < sisterMeshes.Length; i++)
        {
            pointMesh = sisterMeshes[i];
            for(int j = 0; j< pointMesh.materials.Length; j++)
            {
                sisterMaterials.Add(pointMesh.materials[j]);
            }
        }

        foreach(Material mat in sisterMaterials)
        {
            MK.Toon.Properties.albedoColor.SetValue(mat, matColor);
        }

        //testMaterial = testMesh.material;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
         
        if (other.gameObject.tag != "Soup") return;

        currentHealth--;
        float colorPercent = 1f * currentHealth / totalHealth;
        matColor = new Color(colorPercent, colorPercent, colorPercent, colorPercent);
        foreach (Material mat in sisterMaterials)
        {
            MK.Toon.Properties.albedoColor.SetValue(mat, matColor);
        }
    }
}
