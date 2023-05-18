using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOutSoup_L4_V3 : MonoBehaviour
{
    public LevelManager lm;
    public float nextSceneTransitionTime = 5f;
    private void OnMouseDown()
    {
        pourOutSoup();
        lm.nextStageAfterSeconds(nextSceneTransitionTime);
        GetComponent<Collider>().enabled = false;
    }

    public GameObject[] soupInPot;
    public GameObject soupInBowl;
    public void pourOutSoup()
    {
        foreach (GameObject go in soupInPot)
        {
            go.SetActive(false);
        }

        soupInBowl.SetActive(true);
    }
}
