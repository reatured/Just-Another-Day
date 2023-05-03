using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting_DisableAnimator_V2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Animator animator;

    private void OnEnable()
    {
        Destroy(animator);
        Destroy(this);
    }
}
