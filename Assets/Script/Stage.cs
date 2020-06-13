using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Stage : MonoBehaviour {

	public GameObject harb;
	public Terrain terrain;


	CharactorManager charactorManager;

	[SerializeField] private LayerMask fieldLayer;
	private LayerMask defaultFieldLayer;

	public bool removeExistingColliders = true;

	[SerializeField]
	private GameObject Treasure;

	private float range;
	private float radian;
	private int ItemNum = 5;

	//経過時間を表記するText

	public	GameObject timeText;

	private SoundController sound;

	private float time;

	//時間切れの時に表示する文字
	[SerializeField]
	private GameObject timeUpText;

	[SerializeField]
	private GameObject pauseUI;

	[SerializeField]
	GameObject Player;
	// Use this for initialization
	void Start () {
		fieldLayer = defaultFieldLayer;

		time = 0;

		sound = GameObject.Find("sound").GetComponent<SoundController>();

		CreateInvertedMeshCollider();
		charactorManager = Player.GetComponent<CharactorManager>();
		StartCoroutine("CountDown");
		sound.PlayBGMByname("game_maoudamashii_4_field08");
	}

	public void Waitseconds(float time)
	{
		float count = 0.0f;
		while (count <= time)
		{
			count += Time.deltaTime;
		}

	}
	

	IEnumerator CountDown()
	{
	timeUpText.GetComponent<Text>().text = "3";
	yield return new WaitForSeconds(1.0f);

	timeUpText.GetComponent<Text>().text = "2";
	yield return new WaitForSeconds(1.0f);

	timeUpText.GetComponent<Text>().text = "1";
	yield return new WaitForSeconds(1.0f);

	timeUpText.GetComponent<Text>().text = "GO!";
	yield return new WaitForSeconds(1.0f);

	timeUpText.GetComponent<Text>().text = "";
	charactorManager.CountFinish = true;
}

public void RandomItem(int ItemNum)
{
	Vector3 terrainPos = terrain.GetPosition();
	TerrainData Data = terrain.terrainData;

	int i;
	for (i = 0; i < ItemNum; i++)
	{
		range = Random.Range(-50f, 50f);
		radian = (Random.Range(0, 360f) * Mathf.Deg2Rad);

		float px = Mathf.Cos(radian) * range;
		float pz = Mathf.Sin(radian) * range;
		Vector3 pos = new Vector3(px + transform.position.x, terrainPos.y + Data.size.y, pz + transform.position.z);

		RaycastHit hit;
		if (Physics.Raycast(pos, Vector3.down, out hit, terrainPos.y + Data.size.y + 100f, fieldLayer))
		{
			pos.y = hit.point.y;
		}
		Instantiate(harb, pos, transform.rotation);

	}
}

public void CreateInvertedMeshCollider()
{
	if (removeExistingColliders)
		RemoveExistingColliders();

	InvertMesh();

	gameObject.AddComponent<MeshCollider>();
}

private void RemoveExistingColliders()
{
	Collider[] colliders = GetComponents<Collider>();
	for (int i = 0; i < colliders.Length; i++)
		DestroyImmediate(colliders[i]);
}

private void InvertMesh()
{
	Mesh mesh = GetComponent<MeshFilter>().mesh;
	mesh.triangles = mesh.triangles.Reverse().ToArray();
}

	// Update is called once per frame
	void Update()
	{
		if (charactorManager.CountFinish)
		{
			//制限時間のカウントダウン
			

			if (Input.GetKeyDown("q"))
			{
				//　ポーズUIのアクティブ、非アクティブを切り替え
				pauseUI.SetActive(!pauseUI.activeSelf);

				//　ポーズUIが表示されてる時は停止
				if (pauseUI.activeSelf)
				{
					Time.timeScale = 0f;
					//　ポーズUIが表示されてなければ通常通り進行
				}
				else
				{
					Time.timeScale = 1f;
				}
			}
			time += Time.deltaTime;
			
			timeText.GetComponent<Text>().text = time.ToString("F2");
			
		}
	}
	
	
}
