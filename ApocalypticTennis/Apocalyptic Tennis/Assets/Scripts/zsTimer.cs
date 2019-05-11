using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zsTimer : MonoBehaviour {

    public float TimerToPlay = 60f;
    public GameObject TextGameObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Subtract time
        TimerToPlay -= Time.deltaTime;
        //Check if time is up
        if (TimerToPlay <= 0f) {
            //Enable
            TextGameObject.SetActive(true);
        }
	}
}
