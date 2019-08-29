using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public GameObject[] Jewel;
	// Use this for initialization
	void Start () {
        Instantiate(Jewel[0],new Vector3(0f,0f,0f),transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
