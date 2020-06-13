using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactorManager : MonoBehaviour {

    private CharacterController characterController;

	 private SoundController sound;
    private Vector3 velocity;

	private float buf;

    //重力(LeftShiftをおすと飛び上がる)
    private Vector3 Gravity = new Vector3(0f, -1f, 0f);
    [SerializeField]
    //動くスピード
    private float walkSpeed;
    //走った時の倍率
    private float run;

    //獲得した宝石の数
    public static int Life;
	[SerializeField]
    //宝石の数を表記するText
    private GameObject numText;

	public bool CountFinish;
	[SerializeField]
	private GameObject timeUpText;

	public bool Finished;

	[SerializeField]
	private GameObject GameOver;

	// Use this for initialization
	void Start() {
		buf = 1f;
		Finished = false;
        characterController = GetComponent<CharacterController>();
        Life = 3;

        run = 1.5f;

		CountFinish = false;
		
		 sound = GameObject.Find("sound").GetComponent<SoundController>();
	}


    // Update is called once per frame
    void Update() {
		if (Finished == false)
		{
			numText.GetComponent<Text>().text = "Life: " + Life;
			//カメラの方向からxz平面の単位ベクトルを取得
			Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

			if (Input.GetKey(KeyCode.LeftShift))
			{
				Gravity = new Vector3(0f, 2f, 0f);
			}
			else
			{
				Gravity = new Vector3(0, -1f, 0);
			}
			//velocity = playerが動く方向
			velocity = (cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal")).normalized + Gravity;

			if (CountFinish)
			{
				//移動に必要なやつ

				if (velocity - Gravity != Vector3.zero)
				{
					if (Input.GetKey(KeyCode.Space))
					{
						characterController.Move(velocity * walkSpeed * Time.deltaTime * run * buf);
					}
					else
					{
						characterController.Move(velocity * walkSpeed * Time.deltaTime * buf);
					}
					transform.rotation = Quaternion.LookRotation(velocity - Gravity);

					//ここまで
				}

				//Debug.Log(velocity);
			}
		}
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Jewel") {
            Life++;
           
            Destroy(other.gameObject);
			numText.GetComponent<Text>().text = "Life: " + Life;
			sound.PlaySEByname("item-01");
		}
    }

    

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.layer == 4)
		{
			buf = 0.5f;
		}
		else if (hit.gameObject.tag == "treasure")
		{
			timeUpText.GetComponent<Text>().text = "FINISH!!";
			Finished = true;
		}
		else if (hit.gameObject.tag == "Weapon")
		{

			Life--;
			hit.gameObject.GetComponent<Collider>().enabled = false;
				if (Life <= -1)
				{
					GameOver.SetActive(true);
				}

		}
	}
}
