using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private Transform Player;
	public GameObject[] ResultText;
	private CharactorManager character;
	Stage stage;

	private string Path;
	private ResultTestinfo resultdatainfo;

	[SerializeField]
	private GameObject field;


	[SerializeField]
	private GameObject Panel;
	
	SoundController sound;
	private bool isPushButton;
    
	// Use this for initialization
	void Start () {
		sound = GameObject.Find("sound").GetComponent<SoundController>();
		stage = field.GetComponent<Stage>();

		resultdatainfo = new ResultTestinfo();
		resultdatainfo.resluts = new ResultTest[5];
		for (int i = 0; i < 5; i++)
		{
			resultdatainfo.resluts[i] = new ResultTest();
		}
		Path = Application.persistentDataPath + "/" + ".savedata.json";
		character = Player.gameObject.GetComponent<CharactorManager>();
		isPushButton = false;

		StartCoroutine("Rank");

		//Debug.Log(character.Finished.GetType());
	}
	public void GetRanking(float score)
    {
		int tmprank = 7;
		for (int i = 0; i < 5; i++)
		{
			//
			if (resultdatainfo.resluts[i].time>score && tmprank>resultdatainfo.resluts[i].rank)
			{
				Debug.Log("!");
				tmprank = i+1;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			if (resultdatainfo.resluts[i].rank >= tmprank) {
				resultdatainfo.resluts[i].rank += 1;
				if (resultdatainfo.resluts[i].rank > 5) {
					resultdatainfo.resluts[i].rank = tmprank;
					resultdatainfo.resluts[i].time = score;
				}
			}
		}
	}
    public void Ranking()
    {
		for (int i = 0; i < 5; i++)
		{
			
			if(resultdatainfo.resluts[i].rank < 6)
				ResultText[resultdatainfo.resluts[i].rank - 1].GetComponent<Text>().text = resultdatainfo.resluts[i].time.ToString();
		}
	}

    public void Load()
    {
		if (File.Exists(Path))
		{
			ResultTest[] tmp;
			string data = File.ReadAllText(Path);
			Debug.Log(data);
			tmp = JsonUtility.FromJson<ResultTest[]>(data);
			for (int i = 0; i < tmp.Length; i++)
			{
				resultdatainfo.resluts[i] = tmp[i];
			}
		}
		else
		{
			
		}

    }
    public void Save()
    {
		string data = JsonUtility.ToJson(resultdatainfo);
		Debug.Log(data);
		File.WriteAllText(Path, data);
    }
	//ゲーム終了時にランキングを更新して表示する
	IEnumerator Rank()
	{
		while (character.Finished == false)
		{
			yield return null;
		}
		Load();
		float score = stage.time;
		Panel.SetActive(true);
		GetRanking(score);
		Ranking();
		Save();

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
