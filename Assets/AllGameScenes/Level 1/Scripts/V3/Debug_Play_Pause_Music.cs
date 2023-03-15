using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_Play_Pause_Music : MonoBehaviour
{
    public Button butt;
    public AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        butt = gameObject.GetComponent<Button>();
        butt.onClick.AddListener(playOrPause);
        _audio = gameObject.GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playOrPause()
    {
        if (_audio.isPlaying)
        {
            _audio.Pause();
        }
        else
        {
            _audio.Play();
        }
    }

}
