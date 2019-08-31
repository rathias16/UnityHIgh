using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {
    public GameObject[] ResultText;

    private int score;

    private int[] Highscore;
    
	// Use this for initialization
	void Start () {
        score = CharactorManager.jewelNum;
        if (Highscore == null)
        {
            Highscore = new int[5];
        }

        GetRanking();
    }
	public void GetRanking()
    {
        Debug.Log("HighScore.Length : " + Highscore.Length);

        Load();

        for (int i = 0;i < Highscore.Length;i++)
        {
            if (Highscore[i] < score)
            {

                int _tmp = Highscore[i];
                Highscore[i] = score;
                score = _tmp;
            }
            
            Debug.Log(i+":" + Highscore[i]);
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
            Highscore[i] = PlayerPrefs.GetInt(keyname);
        }
    }
    public void Save()
    {
        for(int i = 0;i < Highscore.Length; i++)
        {
            string keyname = string.Format("score{0:D3}", i);
            PlayerPrefs.SetInt(keyname,Highscore[i]);
        }
    }
    // Update is called once per frame
    void Update () {
        Ranking();
	}
}
