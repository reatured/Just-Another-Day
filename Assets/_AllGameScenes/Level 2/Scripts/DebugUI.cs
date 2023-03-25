using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Finished
public class DebugUI : MonoBehaviour
{

    public ObiRope rope;
    public List<TextMeshProUGUI> debugTextList;

    public GameObject debugTextPrefab;
    public RopeDragger ropeDragger;

    // Start is called before the first frame update
    void Start()
    {
        resetArray();
        ropeDragger = FindObjectOfType<RopeDragger>();
        ropeDragger.onDrag += resetArray;
    }

    public void resetArray()
    {
        int pointCount = rope.elements.Count;
        for (int i = debugTextList.Count; i < pointCount; i++)
        {
            GameObject currentText = Instantiate(debugTextPrefab, this.transform);
            debugTextList.Add(currentText.GetComponent<TextMeshProUGUI>());
            debugTextList[i].text = i + "";
        }
    }


    // Update is called once per frame
    void Update()
    {
        updateTextPosition();
    }

    public void updateTextPosition()
    {
        for (int i = 0; i < debugTextList.Count; i++)
        {
            debugTextList[i].transform.position = rope.solver.positions[i];
            debugTextList[i].transform.Translate(Vector3.up);
        }
    }


}
