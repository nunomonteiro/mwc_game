using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScreen : MonoBehaviour {

    [SerializeField]
    private GameObject _submissionPopupObj;
    private ScoreSubmissionPopup _submissionPopup;

	// Use this for initialization
	void Awake () {
        _submissionPopup = _submissionPopupObj.GetComponent<ScoreSubmissionPopup>();
        _submissionPopup.onScoreSuccessfullySubmited.AddListener(GameManager.Instance.GetUIManager().OnScoreSuccessfullySubmitted);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupForScore(int score) {
        if (_submissionPopup == null)
            _submissionPopup = _submissionPopupObj.GetComponent<ScoreSubmissionPopup>();

        _submissionPopupObj.SetActive(true);
        _submissionPopup.SetupForScore(score);
    }

    public void OnBackButtonPressed() {
        GameManager.Instance.GoBack();
    }

    public void OnScoreSuccessfullySubmitted() {
        //TODO update scores with new score
    }
}
