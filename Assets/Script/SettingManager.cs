﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {
    private bool reverse = CameraController.reverse;
    private GameObject CameraText;
    [SerializeField]
    private GameObject OptionPanel;
    [SerializeField]
    private GameObject RightPanel;

    SoundController sound;
	// Use this for initialization
	void Start () {
        CameraText = GameObject.Find("CameraMode");
        CameraTextChange();
        sound = GameObject.Find("sound").GetComponent<SoundController>();
    }
	
    public void PushRightButton()
    {
        OptionPanel.gameObject.SetActive(false);
        RightPanel.gameObject.SetActive(true);
        sound.PushSelectButton();
    }

    public void PushCloseButton()
    {
        OptionPanel.gameObject.SetActive(true);
        RightPanel.gameObject.SetActive(false);
        sound.PushCancelButton();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PushReverseButton()
    {
        reverse = !reverse;
        CameraTextChange();
        sound.PushSelectButton();
    }
    private void CameraTextChange()
    {
        if (reverse == true)
            CameraText.GetComponent<Text>().text = "Normal";
        else
            CameraText.GetComponent<Text>().text = "reverse";
    }
}
