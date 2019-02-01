using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSubmissionPopup : MonoBehaviour {

    [SerializeField]
    private Text _txtScore;

    [SerializeField]
    private InputField _inputName;

    [SerializeField]
    private InputField _inputMail;

    [SerializeField]
    private Toggle _wantsToKnowMore;

    private int _score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupForScore(int score)
    {
        _score = score;
        _txtScore.text = score.ToString();
    }

    public void OnSumbitButtonPressed() {
        GameManager.Instance.GetScoreManager().PostNewScore(_inputName.text, _inputMail.text, _score, _wantsToKnowMore.isOn);
    }

    public void OnCloseButtonPressed() {
        gameObject.SetActive(false);
    }
}
