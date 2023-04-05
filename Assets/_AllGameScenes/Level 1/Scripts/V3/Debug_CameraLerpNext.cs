using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debug_CameraLerpNext : MonoBehaviour
{
    public Transform mainCam; 
    public Transform[] cams;
    public int currentCam = 0;
    public Scene_FadeIn fadeIn; 
    public int CurrentCam
    {
        get { return currentCam; }   // get method
        set { 
            startPos = cams[currentCam].position;
            startQua = cams[currentCam].rotation;

            int a = currentCam >= cams.Length? 0 : value;

            if (a >= cams.Length)
            {
                currentCam = 0;
            }
            else
            {
                currentCam = value;
            }
            endPos = cams[currentCam].position;
            endQua = cams[currentCam].rotation;

            lerpCam();
        }  // set method
    }
    Vector3 startPos, endPos;
    Quaternion startQua, endQua;

    public void goToNextCam()
    {
        CurrentCam++;
    }

    float startTime = 0f;
    public float lerpDuration = 0.1f;
    
    public void lerpCam()
    {
        startTime = Time.time;
        StartCoroutine(animateLerp());
    }
    public LevelManager lm;
    IEnumerator animateLerp()
    {
        float journey = (Time.time - startTime) / lerpDuration;

        while (journey < 1)
        {
            mainCam.position = Vector3.Lerp(startPos, endPos, journey);
            mainCam.rotation = Quaternion.Lerp(startQua, endQua, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / lerpDuration;
        }
        mainCam.position = endPos;
        mainCam.rotation = endQua;

        fadeIn.fadeInScene();
        
    }
}
