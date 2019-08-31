using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactorManager : MonoBehaviour {

    private CharacterController characterController;

    private SoundController sound;
    [SerializeField]
    private GameObject pauseUI;

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

    private bool CountFinish = false;

    // Use this for initialization
    void Start() {

        characterController = GetComponent<CharacterController>();
        jewelNum = 0;
        numText = GameObject.Find("numText");
        timeText = GameObject.Find("timeText");
        timeUpText = GameObject.Find("timeUpText");
        timeLimit = 50.0f;
        run = 1.5f;

        sound = GameObject.Find("sound").GetComponent<SoundController>();

        StartCoroutine(CountDown());

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

        CountFinish = true;
    }

    // Update is called once per frame
    void Update() {
        //カメラの方向からxz平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Gravity = new Vector3(0f, 2f, 0f);
        }
        else {
            Gravity = new Vector3(0, -1f, 0);
        }
        //velocity = playerが動く方向
        velocity = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal") + Gravity;

        if (CountFinish)
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


            if (timeLimit <= 0)
            {
                timeLimit = 0;
                timeUpText.GetComponent<Text>().text = "Finish!!";

                Invoke("FinishGame", 2);

            }

            timeText.GetComponent<Text>().text = timeLimit.ToString("F2");

            // Debug.Log(velocity);
        }
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Jewel") {
            jewelNum++;
           
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
