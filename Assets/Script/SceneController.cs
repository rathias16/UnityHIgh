using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//残り時間を表記するText
	private GameObject timeText;

	//時間切れの時に表示する文字
	private GameObject timeUpText;

	private SoundController sound;

    void Start()
    {
        sound = GameObject.Find("sound").GetComponent<SoundController>();
		timeText = GameObject.Find("timeText");
		timeUpText = GameObject.Find("timeUpText");
	}

    public void PushStartButton()
    {
		sound.PlaySEByname("kettei-01");
		SceneManager.LoadScene("SelectScene");
        
    }
    public void PushOptionButton()
    {
        sound.PlaySEByname("kettei-01");
        SceneManager.LoadScene("OptionScene");

    }
    public void PushExitButton()
    {
        sound.PlaySEByname("cancel-01");
        Application.Quit();


    }
    public void PushStage1()
    {
		sound.PlaySEByname("kettei-01");
		SceneManager.LoadScene("SearchScene2");
       
    }

    public void PushBackButton()
    {
        sound.PlaySEByname("cancel-01");
		SceneManager.LoadScene("StartScene");
    }
   
}
