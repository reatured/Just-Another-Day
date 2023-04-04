using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_FadeIn : MonoBehaviour
{
    public Image blackImage;
    // Start is called before the first frame update
    void Start()
    {
        FadeInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInScene()
    {
        startTime = Time.time;
        StartCoroutine(endScene(blackImage.color, new Color(0, 0, 0, 0)));
    }

    float startTime;
    public float animationDuration = 0.5f;
    IEnumerator endScene(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey < 1)
        {

            blackImage.color = Color.Lerp(start, end, journey);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;

        }
        blackImage.color = end;
        yield return new WaitForEndOfFrame();


    }
}
