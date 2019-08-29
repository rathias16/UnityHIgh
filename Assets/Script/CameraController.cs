using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject Player;
    private float rotatespeed = 3.0f;
    public static bool reverse = true;
    Vector3 PlayerPos;
    float mouseInputY;
    float mouseInputX;
    // Use this for initialization
    void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += Player.transform.position - PlayerPos;

        PlayerPos = Player.transform.position;
        if (Input.GetMouseButton(1)){
            
            // マウスの移動量
            if (reverse == true) {
                mouseInputX = Input.GetAxis("Mouse X");
                mouseInputY = Input.GetAxis("Mouse Y");
            }else{
                mouseInputX = -Input.GetAxis("Mouse X");
                mouseInputY = -Input.GetAxis("Mouse Y");
            }
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(PlayerPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            transform.RotateAround(PlayerPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
    }
}
