using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRetryButtonPressed() {
        GameManager.Instance.StartGame();   
    }

    public void OnSubmitButtonPressed() {
        GameManager.Instance.GoToScoreSubmission();
    }
}
