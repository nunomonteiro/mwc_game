using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScreen : MonoBehaviour {

    [SerializeField]
    private GameObject _submissionPopupObj;
    private ScoreSubmissionPopup _submissionPopup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupForScore(int score) {
        _submissionPopupObj.SetActive(true);
        _submissionPopup.SetupForScore(score);
    }

    public void OnBackButtonPressed() {
        GameManager.Instance.GoBack();
    }
}
