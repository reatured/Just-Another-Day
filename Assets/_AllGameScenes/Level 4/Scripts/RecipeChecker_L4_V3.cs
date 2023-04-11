using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Toon;
using UnityEngine.Events;

public class RecipeChecker_L4_V3 : MonoBehaviour
{
    public MeshRenderer pot; 
    public Material potMaterial;
    public MeshRenderer soup; 
    public Material _soupMaterial;


    public Color[] colors; 
    public FoodPickUp_L4_V3 foodToBeAdd;

    public int stage = 0;
    public Color originalColor;

    public LevelManager lm; 
    // Start is called before the first frame update
    void Start()
    {
        potMaterial = pot.material;
        _soupMaterial = soup.material; 
        originalColor = MK.Toon.Properties.albedoColor.GetValue(potMaterial);
        changeSoupColor(colors[0]);
    }



    public void potSelected()
    {
        if (potMaterial != null)
        {

            MK.Toon.Properties.albedoColor.SetValue(potMaterial, new Color(0,1,0,1));
        }
    }

    public void potUnselected()
    {
        if (potMaterial != null)
        {
            MK.Toon.Properties.albedoColor.SetValue(potMaterial, originalColor);
 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        potSelected(); 
        foodToBeAdd = other.GetComponent<FoodPickUp_L4_V3>();
        bool correctFood = foodToBeAdd.addOrder == stage;
        foodToBeAdd.addFoodFeedback(correctFood);

      

    }



    private void OnTriggerExit(Collider other)
    {

        potUnselected();
        foodToBeAdd = other.GetComponent<FoodPickUp_L4_V3>();
        foodToBeAdd.addFoodFeedback(false);
    }

    public Animator potAnimator;
    public int totalStage = 3; 
    public void nextStgae()
    {
        stage++;

        startTime = Time.time;
        StartCoroutine(lerpColor(colors[stage - 1], colors[stage]));
        changeSoupColor(colors[stage]);
        if(stage > totalStage)
        {
            lerpEndEvent.AddListener(enableScript);
        }
        
    }

    public float animtionDuration;
    public float startTime;
    public UnityEvent lerpEndEvent;
    IEnumerator lerpColor(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animtionDuration;
        while (journey < 1)
        {
            changeSoupColor(Color.Lerp(start, end, journey));
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animtionDuration;
        }

        changeSoupColor(end); 
        if(lerpEndEvent != null)
        {
            lerpEndEvent.Invoke();
        }

    }

    public void changeSoupColor(Color color)
    {
        _soupMaterial.SetColor("_ColorShallow", color);

    }

    public PourOutSoup_L4_V3 pourOutSoupScript;
    public void enableScript()
    {
        pourOutSoupScript.enabled = true;
    }

}
