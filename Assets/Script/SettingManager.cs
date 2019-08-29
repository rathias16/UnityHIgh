using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {
    private bool reverse = CameraController.reverse;
    private GameObject CameraText;
	// Use this for initialization
	void Start () {
        CameraText = GameObject.Find("CameraMode");
        CameraTextChange();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushReverseButton()
    {
        reverse = !reverse;
        CameraTextChange();
    }
    private void CameraTextChange()
    {
        if (reverse == true)
            CameraText.GetComponent<Text>().text = "Normal";
        else
            CameraText.GetComponent<Text>().text = "reverse";
    }
}
