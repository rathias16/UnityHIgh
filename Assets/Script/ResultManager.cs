using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {
    public GameObject[] ResultText;

    public int score;

    private int[] Highscore = {0,0,0,0,0};
    
	// Use this for initialization
	void Start () {
        score = CharactorManager.jewelNum;

        GetRanking();
      
	}
	public void GetRanking()
    {
        for (int i = 0;i < Highscore.Length;i++)
        {
            if (Highscore[i] < score)
            {

                int _tmp = Highscore[i];
                Highscore[i] = score;
                score = _tmp;
            }
            else
            {
                Highscore[0] = score;
            }

        }
        for(int i = 0;i < Highscore.Length; i++)
        {
            ResultText[i].GetComponent<Text>().text = Highscore[i].ToString();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
