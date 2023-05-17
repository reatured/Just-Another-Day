using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Manager_L5_V3 : MonoBehaviour
{
    public int stage;
    public int Stage
    {
        get { return stage; }
        set
        {
            stage = value;
            ToggleCollider(0, stage == 0);
            ToggleCollider(1, stage == 1);
            ToggleCollider(2, stage == 2);
            ToggleCollider(3, stage == 3);

            cursorScript.currentCollider = colliders[stage];
        }
    }

    public Collider soupCollider, paintingCollider, bearCollider, recordCollider;
    private Collider[] colliders; 
    public Cursor_Choose_L5_V3 cursorScript; 
    // Start is called before the first frame update
    void Start()
    {
        colliders = new Collider[] { soupCollider, paintingCollider, bearCollider, recordCollider }; 
        // Initialize the array with the colliders
        Stage = stage;         
    }

    void ToggleCollider(int index, bool enabled)
    {
        if (colliders[index] != null)
        {
            colliders[index].enabled = enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
