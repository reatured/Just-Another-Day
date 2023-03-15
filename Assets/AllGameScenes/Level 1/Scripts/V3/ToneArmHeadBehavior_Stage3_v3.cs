using UnityEngine;
using UnityEngine.Events;
public class ToneArmHeadBehavior_Stage3_v3 : MonoBehaviour
{
    public Collider playTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public UnityEvent toneArmPlayEvent;
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.name != playTrigger.name) return;
        if (toneArmPlayEvent != null)
        {
            toneArmPlayEvent.Invoke();
        }
    }

    public UnityEvent toneArmPauseEvent;

    public void OnTriggerExit(Collider other)
    {
        if (other.name != playTrigger.name) return;
        if (toneArmPauseEvent != null)
        {
            toneArmPauseEvent.Invoke();
        }
    }
}
