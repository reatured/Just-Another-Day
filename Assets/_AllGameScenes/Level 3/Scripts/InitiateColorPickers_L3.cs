using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateColorPickers_L3 : MonoBehaviour
{
    ColorPicker_L3[] pickers;
    // Start is called before the first frame update
    void Start()
    {
        pickers = GetComponentsInChildren<ColorPicker_L3>();
        for(int i = 0; i < pickers.Length; i++)
        {
            pickers[i].index = i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
