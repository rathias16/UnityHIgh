using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {


    private SoundController sound;

    public void PushStartButton()
    {
       // sound.PushSelectButton();
        SceneManager.LoadScene("SelectScene");
        
    }
    public void PushOptionButton()
    {
        sound.PushSelectButton();
        SceneManager.LoadScene("OptionScene");

    }
    public void PushExitButton()
    {
        sound.PushCancelButton();
        Application.Quit();


    }
    public void PushStage1()
    {
       // sound.PushSelectButton();
        SceneManager.LoadScene("SearchScene1");
       
    }

    public void PushBackButton()
    {
        sound.PushCancelButton();
        SceneManager.LoadScene("StartScene");
    }
   
}
