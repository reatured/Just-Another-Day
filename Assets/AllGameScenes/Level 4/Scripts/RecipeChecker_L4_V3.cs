using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MK.Toon;
public class RecipeChecker_L4_V3 : MonoBehaviour
{
    public MeshRenderer pot; 
    public Material potMaterial;

    public FoodPickUp_L4_V3 foodToBeAdd;

    public int stage = 0;
    public Color originalColor; 
    // Start is called before the first frame update
    void Start()
    {
        potMaterial = pot.material;
        originalColor = MK.Toon.Properties.albedoColor.GetValue(potMaterial); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    public void nextStgae()
    {
        stage++;

        if(stage > 3)
        {
            potAnimator = pot.GetComponent<Animator>();
            potAnimator.SetTrigger("Pour");
        }
        
    }
}
