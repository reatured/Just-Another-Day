using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyColorManager : MonoBehaviour
{
    public Color[] colors = new Color[6]; 
    public LevelManager levelManager;
    int currenetLevel; 
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        levelManager.nextLevelEvent.AddListener(changeSkyColor);
        changeSkyColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSkyColor()
    {
        print("Change Skycolor");
        currenetLevel = levelManager.currentStage; 
        startTime = Time.time;
        StartCoroutine(lerpSkyColor(RenderSettings.ambientSkyColor, colors[currenetLevel - 1]));
    }
    float startTime;
    public float animationDuration;
    public Material skyMaterial;
    public Light directLight; 
    IEnumerator lerpSkyColor(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animationDuration;

        while(journey<1)
        {
            journey = (Time.time - startTime) / animationDuration;
            Color currentColor = Color.Lerp(start, end, journey);
            RenderSettings.ambientSkyColor = currentColor;
            skyMaterial.SetColor("_Color2", currentColor);
            directLight.color = currentColor;
            yield return new WaitForFixedUpdate();
        }

        RenderSettings.ambientSkyColor = end;
        directLight.color = end;
        skyMaterial.SetColor("_Color2", end);


    }
}
