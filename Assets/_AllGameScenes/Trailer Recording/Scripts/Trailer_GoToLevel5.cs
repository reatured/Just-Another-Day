using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer_GoToLevel5 : MonoBehaviour
{
    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        print("Enabled");
        levelManager.nextStage();
    }
}
