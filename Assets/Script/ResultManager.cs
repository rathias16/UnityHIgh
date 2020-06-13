using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private Transform Player;
	public GameObject[] ResultText;
	
	private CharactorManager character;

	Stage stage;

	[SerializeField]
	private GameObject field;


	[SerializeField]
	private GameObject Panel;

    private float score;
	SoundController sound;


	private bool isPushButton;

    private float[] Highscore;
    
	// Use this for initialization
	void Start () {
		sound = GameObject.Find("sound").GetComponent<SoundController>();
		stage = field.GetComponent<Stage>();
        
        if (Highscore == null)
        {
            Highscore = new float[5];
        }

		character = Player.gameObject.GetComponent<CharactorManager>();
		isPushButton = false;

		StartCoroutine("Rank");

		Debug.Log(character.Finished.GetType());
	}
	public void GetRanking()
    {
		score = float.Parse(stage.timeText.GetComponent<Text>().text);

        Load();

        for (int i = 0;i < Highscore.Length;i++)
        {
            if (Highscore[i] > score)
            {

                float _tmp = Highscore[i];
                Highscore[i] = score;
                score = _tmp;
            }
            
           // Debug.Log(i+":" + Highscore[i]);
        }
        Save();
    }
    public void Ranking()
    {
        for (int i = 0; i < Highscore.Length; i++)
        {
            ResultText[i].GetComponent<Text>().text = (i+1) +":  "+ Highscore[i];
        }
    }

    public void Load()
    {
        for(int i = 0;i < Highscore.Length; i++)
        {
            string keyname = string.Format("score{0:D3}",i);
            Highscore[i] = PlayerPrefs.GetFloat(keyname);
        }
    }
    public void Save()
    {
        for(int i = 0;i < Highscore.Length; i++)
        {
            string keyname = string.Format("score{0:D3}", i);
            PlayerPrefs.SetFloat(keyname,Highscore[i]);
        }
    }

	IEnumerator Rank()
	{
		while (character.Finished == false)
		{
			yield return null;
		}

		Panel.SetActive(true);
		GetRanking();
		Ranking();
		while (isPushButton == false)
		{
			yield return null;
		}

	}
    // Update is called once per frame
	void Update () {
		
		
	}
	public void PushExitButton()
	{
		isPushButton = true;
		SceneManager.LoadScene("StartScene");
		sound.StopBGM();

	}
	public void PushReplayButton()
	{
		isPushButton = true;
		SceneManager.LoadScene("SearchScene2");
		sound.StopBGM();
	}
	
}
