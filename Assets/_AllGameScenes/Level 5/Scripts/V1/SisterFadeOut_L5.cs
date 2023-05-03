using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Toon;
using UnityEngine.Events;

public class SisterFadeOut_L5 : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private List<Material> sisterMaterials = new List<Material>();
    [SerializeField] private MeshRenderer[] sisterMeshes = null;
    [SerializeField] private MeshRenderer pointMesh = null;
    [SerializeField] private Color matColor = Color.white;

    [Header("Health")]
    [SerializeField] private int totalHealth = 5;
    [SerializeField] private int currentHealth = 5;

    [Header("Events")]
    public UnityEvent sisterDisappearEvent;
    public Shader newShader;
    // Start is called before the first frame update
    private void Start()
    {


        currentHealth = totalHealth;

        sisterMeshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in sisterMeshes)
        {
            pointMesh = mesh;
            foreach (Material mat in pointMesh.materials)
            {
                sisterMaterials.Add(mat);
            }
        }

        foreach (Material mat in sisterMaterials)
        {
            MK.Toon.Properties.albedoColor.SetValue(mat, matColor);
            mat.shader = newShader;
            MK.Toon.Properties.surface.SetValue(mat, MK.Toon.Surface.Transparent);
            MK.Toon.Properties.blend.SetValue(mat, MK.Toon.Blend.Additive);
        }


    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Soup")
        {
            if (currentHealth <= 0) return;

            currentHealth--;

            float colorPercent = 1f * currentHealth / totalHealth;
            matColor = new Color(colorPercent, colorPercent, colorPercent, 1);

            foreach (Material mat in sisterMaterials)
            {
                Color startColor = MK.Toon.Properties.albedoColor.GetValue(mat);
                Color endColor = startColor;
                endColor.a = colorPercent;
                startTime = Time.time;
                StartCoroutine(lerpColor(startColor, endColor, mat));
                //MK.Toon.Properties.albedoColor.SetValue(mat, matColor);
            }

            if (currentHealth <= 0 && sisterDisappearEvent != null)
            {
                Invoke("disableMeshes", animationDuration);
                sisterDisappearEvent.Invoke();
            }
        }
        else if(other.gameObject.tag == "Painting")
        {
            other.GetComponent<PaintingBehavior_L5_V2>().goToStage_ClickToTear();

        }

        

        
    }

    public void disableMeshes()
    {
        foreach (MeshRenderer mesh in sisterMeshes)
        {
            mesh.enabled = false;
        }
    }

    float startTime;
    public float animationDuration;

    IEnumerator lerpColor(Color start, Color end, Material mat)
    {
        float journey = (Time.time - startTime) / animationDuration;
        
        while (journey < 1)
        {
            journey = (Time.time - startTime) / animationDuration;
            
            Color currentColor = Color.Lerp(start, end, journey);
            MK.Toon.Properties.albedoColor.SetValue(mat, currentColor);
            yield return new WaitForFixedUpdate();
        }

        MK.Toon.Properties.albedoColor.SetValue(mat, end);


    }
}