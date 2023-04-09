using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInHand_Manager_L4_V3 : MonoBehaviour
{
    public bool handIsFull = false; 
    public bool HandIsFull
    {
        get
        {
            return handIsFull;
        }
        set
        {
            handIsFull = value;
        }
    }
}
