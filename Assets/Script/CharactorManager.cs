using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactorManager : MonoBehaviour {
    
    private CharacterController characterController;

    private SoundController sound;

    //宝石の当たり判定
    private BoxCollider jewelCollider;
    
    private Vector3 velocity;

    //重力(LeftShiftをおすと飛び上がる)
    private Vector3 Gravity = new Vector3(0f, -1f, 0f);
    [SerializeField]
    //動くスピード
    private float walkSpeed;
    //走った時の倍率
    private float run;

    //獲得した宝石の数
    public static int jewelNum;

    //宝石の数を表記するText
    private GameObject numText;

    //残り時間を表記するText
    private GameObject timeText;

    //時間切れの時に表示する文字
    private GameObject timeUpText;

    //制限時間
    private float timeLimit;

  

    // Use this for initialization
    void Start () {
        jewelCollider = GetComponent<BoxCollider>();
        characterController = GetComponent<CharacterController>();
        jewelNum = 0;
        numText = GameObject.Find("numText");
        timeText = GameObject.Find("timeText");
        timeUpText = GameObject.Find("timeUpText");
        timeLimit = 50.0f;
        run = 1.5f;
       
    }
	
	// Update is called once per frame
	void Update () {
        //カメラの方向からxz平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward,new Vector3(1,0,1)).normalized;

        if (Input.GetKey(KeyCode.LeftShift)){
            Gravity = new Vector3(0f, 2f, 0f);
        }
        else{
            Gravity = new Vector3(0,-1f,0);
        }
        //velocity = playerが動く方向
        velocity = cameraForward * Input.GetAxis("Vertical")+ Camera.main.transform.right *Input.GetAxis("Horizontal") + Gravity;
        

        //制限時間のカウントダウン
        timeLimit -= Time.deltaTime;

       
            if (velocity - Gravity != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                characterController.Move(velocity * walkSpeed * Time.deltaTime * run);
            }
            else
            {
                characterController.Move(velocity * walkSpeed * Time.deltaTime);
            }
            transform.rotation = Quaternion.LookRotation(velocity - Gravity);
            
            //ここまで
        }
        

        if (timeLimit <= 0) {
            timeLimit = 0;
            timeUpText.GetComponent<Text>().text = "Finish!!";

            Invoke("FinishGame", 2);
            
        }

        timeText.GetComponent<Text>().text = timeLimit.ToString("F2");
       
       // Debug.Log(velocity);
      
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Jewel") {
            jewelNum++;
            Debug.Log(jewelNum);
            Destroy(other.gameObject);
            numText.GetComponent<Text>().text = "Jewel " + jewelNum;
            sound.GetJewel();
        }
    }
    public void FinishGame()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
