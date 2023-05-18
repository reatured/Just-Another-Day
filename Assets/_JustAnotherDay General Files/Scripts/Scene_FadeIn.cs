using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_FadeIn : MonoBehaviour
{
    public Image blackImage;
    public LevelManager levelManager;
    private void Awake()
    {
        blackImage.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        eyeMat = eyeMesh.material; 
        //fadeInScene();
    }

    float startTime;
    public float animationDuration = 0.5f;
    public UnityEvent fadingEndEvent;
    public Debug_CameraLerpNext camScript; 

    //turn to colors
    public void fadeInScene() //turn into transparent
    {

        startTime = Time.time;

        StartCoroutine(fadeIn(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0)));
    }
 
    IEnumerator fadeIn(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey < 1.1f)
        {

            blackImage.color = Color.Lerp(start, end, journey);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;

        }
        blackImage.color = end;
        yield return new WaitForEndOfFrame();

    }
    //turn to black
    public void fadeOutScene() //turn into black
    {

        startTime = Time.time;
        StartCoroutine(fadeOut(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1)));
    }

    IEnumerator fadeOut(Color start, Color end)
    {
        float journey = (Time.time - startTime) / animationDuration;
        while (journey < 1.1f)
        {

            blackImage.color = Color.Lerp(start, end, journey);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;

        }
        blackImage.color = end;
        yield return new WaitForEndOfFrame();
        levelManager.goToStage();
        //fadeInScene();
        camScript.goToNextCam();
    }
    public MeshRenderer eyeMesh;
    public Material eyeMat; 
    public void openEye()
    {
        StartCoroutine(blink(-0.2f, 1.2f));
    }

    public void closeEye()
    {
        lerpEndEvent.AddListener(closeEyeEvent);
        StartCoroutine(blink(1.2f, -0.2f));
    }

    UnityEvent lerpEndEvent = null; 
    IEnumerator blink (float start, float end)
    {
        float journey = (Time.time - startTime-1) / animationDuration;

        while (journey < 1.1f)
        {
            float value = utilityScript.remap(journey, 0, 1, start, end);
            eyeMesh.material.SetFloat("_BlinkDegree", value);
            yield return new WaitForEndOfFrame();
            journey = (Time.time - startTime) / animationDuration;

        }
        if(lerpEndEvent != null)
        {
            lerpEndEvent.Invoke();
        }
    }

    public void closeEyeEvent()
    {
        SceneManager.LoadScene(1);
    }

}
