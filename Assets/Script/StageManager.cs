using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;



public class StageManager : MonoBehaviour {
    public GameObject[] harb;
    public  Terrain terrain;

	[SerializeField] GameObject Player;

	CharactorManager charactorManager;

    [SerializeField] private LayerMask fieldLayer;
    private LayerMask defaultFieldLayer;
	private Transform AD_stage;
	private Transform LIFE_stage;
    public bool removeExistingColliders = true;

    private float range ;
    private float radian ;
    private int ItemNum = 5;

	//残り時間を表記するText
	private GameObject timeText;

	//時間切れの時に表示する文字
	private GameObject timeUpText;

	[SerializeField]
	private GameObject pauseUI;

	//制限時間
	private float timeLimit;

	

	enum GameState
	{
		LIFE,
		ADVENTURE,
		CITY,
	}
	GameState state;
	// Use this for initialization
	void Start () {
		AD_stage = transform.GetChild(0);
		LIFE_stage = transform.GetChild(1);
       
        fieldLayer = defaultFieldLayer;

		timeText = GameObject.Find("timeText");
		timeUpText = GameObject.Find("timeUpText");
		timeLimit = 50.0f;

		state = GameState.LIFE;
		Life_setup();
    }

	public void Life_setup()
	{
		CreateInvertedMeshCollider();
		RandomItem(ItemNum);
		StartCoroutine(CountDown());
		
	}

	public void Waitseconds(float time)
	{
		float count = 0.0f;
		while (count <= time)
		{
			count += Time.deltaTime;
		}

	}

	public void Life()
	{
		if (charactorManager.CountFinish)
		{
			//制限時間のカウントダウン
			timeLimit -= Time.deltaTime;

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
			if (timeLimit <= 0)
			{
				timeLimit = 0;
				timeUpText.GetComponent<Text>().text = "Finish!!";

				Invoke("FinishGame", 2);
				if (Input.GetKey("z"))
					state = GameState.ADVENTURE;
			}

			timeText.GetComponent<Text>().text = timeLimit.ToString("F2");
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
        for(i = 0;i < ItemNum; i++)
        {
            range = Random.Range(-50f,50f);
            radian = (Random.Range(0, 360f) * Mathf.Deg2Rad);

            float px = Mathf.Cos(radian) * range;
            float pz = Mathf.Sin(radian) * range;
            Vector3 pos = new Vector3(px + transform.position.x, terrainPos.y + Data.size.y, pz + transform.position.z);

            RaycastHit hit;
            if(Physics.Raycast(pos,Vector3.down,out hit, terrainPos.y + Data.size.y + 100f, fieldLayer))
            {
                pos.y = hit.point.y;
            }
			int item = Random.Range(0,2);
            Instantiate(harb[item], pos, transform.rotation);

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
    void Update () {
		switch (state)
		{
			case GameState.LIFE:
				Life();
				break;

			case GameState.ADVENTURE:
				break;
			case GameState.CITY:
				break;
		}
	}
}
