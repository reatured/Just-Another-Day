using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] objects;
    public int stageIndex;

    private void Awake()
    {
        endStage();
    }
    public virtual void startStage()
    {
        print(stageIndex);
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject go = objects[i];
            if (go != null)
            {
                go.SetActive(true);
            }
        }

    }

    public void endStage()
    {
        print("ending stage" + gameObject.name);
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject go = objects[i];
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
}
