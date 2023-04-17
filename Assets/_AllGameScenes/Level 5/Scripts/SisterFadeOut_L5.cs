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
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Soup") return;

        if (currentHealth <= 0) return;

        currentHealth--;

        float colorPercent = 1f * currentHealth / totalHealth;
        matColor = new Color(colorPercent, colorPercent, colorPercent, colorPercent);

        foreach (Material mat in sisterMaterials)
        {
            MK.Toon.Properties.albedoColor.SetValue(mat, matColor);
        }

        if (currentHealth <= 0 && sisterDisappearEvent != null)
        {
            sisterDisappearEvent.Invoke();
        }
    }
}