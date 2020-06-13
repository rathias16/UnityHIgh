using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	[SerializeField]
	private Dictionary<string, int> SEDic = new Dictionary<string, int>();
	private Dictionary<string, int> BGMDic = new Dictionary<string, int>();
 	AudioClip[] bgm;
	AudioClip[] se;

	AudioSource bgmSource;
	AudioSource seSource;





	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(this);
		bgmSource = this.GetComponent<AudioSource>();
		seSource = this.GetComponent<AudioSource>();
		

		bgm = Resources.LoadAll<AudioClip>("Audio/BGM");
		se = Resources.LoadAll<AudioClip>("Audio/SE");

		for (int i = 0;i < bgm.Length; i++)
		{
			BGMDic.Add(bgm[i].name,i);
		}
		for(int i = 0;i < se.Length;i++)
		{
			SEDic.Add(se[i].name,i);
		}

	}
	public int GetBGMIndex(string name)
	{
		if (BGMDic.ContainsKey(name))
			return BGMDic[name];
		else
		{
			Debug.LogError("ファイルが存在しません");
			return 0;
		}
	}
	public int GetSEIndex(string name)
	{
		if (SEDic.ContainsKey(name))
			return SEDic[name];
		else
		{
			Debug.LogError("ファイルが見つかりません");
			return 0;
		}
	}

	public void PlayBGM(int index)
	{
		
		index = Mathf.Clamp(index,0,bgm.Length);
		bgmSource.clip = bgm[index];
		bgmSource.Play();
	}
	public void PlayBGMByname(string name)
	{
		PlayBGM(GetBGMIndex(name));
	}

	public void StopBGM()
	{
		bgmSource.Stop();
		bgmSource.clip = null;
	}
	public void PlaySE(int index)
	{
		index = Mathf.Clamp(index, 0, se.Length);
		seSource.PlayOneShot(se[index]);
	}
	public void PlaySEByname(string name)
	{
		PlaySE(GetSEIndex(name));
	}
	public void StopSE()
	{
		seSource.Stop();
		seSource.clip = null;
	}
	
}