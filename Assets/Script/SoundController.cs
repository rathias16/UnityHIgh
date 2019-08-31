using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public  AudioSource audiosource;
    public  AudioClip Clip1;
    public AudioClip Clip2;
    public  AudioClip Clip3;
   


    // Use this for initialization
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }
    public void PushSelectButton()
    {
        audiosource.clip = Clip1;
        audiosource.Play();
    }
    public void PushCancelButton()
    {
        audiosource.clip = Clip2;
        audiosource.Play();
    }
    public void GetJewel()
    {
        
        audiosource.clip = Clip3;
        audiosource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
