using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMovement : MonoBehaviour
{
    public RectTransform _transform;
    public float upSpeed = 4f;
    public float upperBounds = 1200f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public float y; 
    // Update is called once per frame
    void Update()
    {
        _transform.Translate(Vector3.up * upSpeed * Time.deltaTime) ;
        if(_transform.position.y > upperBounds)
        {
            
            SceneManager.LoadScene(0);
        }
        y = _transform.position.y;
    }
}
