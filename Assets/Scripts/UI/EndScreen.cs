using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {
    [SerializeField]
    private Sprite activeAdvantageSprite;
    [SerializeField]
    private Sprite inactiveAdvantageSprite;

    [SerializeField]
    private GameObject _btnSubmitScore;
    [SerializeField]
    private GameObject _btnScoreSubmitted;

    [SerializeField]
    private Text _txtFinalScore;
    [SerializeField]
    private Text _txtAdvantage1Score;
    [SerializeField]
    private Text _txtAdvantage2Score;
    [SerializeField]
    private Text _txtAdvantage3Score;
    [SerializeField]
    private Text _txtTimeScore;
    [SerializeField]
    private Image _imgAdvantage1;
    [SerializeField]
    private Image _imgAdvantage2;
    [SerializeField]
    private Image _imgAdvantage3;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SolveScoreForAdvantage(int advantageScore, Image advantageImage, Text advantageText) {
        if (advantageScore > 0)
            advantageImage.sprite = activeAdvantageSprite;
        else
            advantageImage.sprite = inactiveAdvantageSprite;
        
        advantageText.text = advantageScore.ToString();

    }

    public void Setup()
    {
        _btnSubmitScore.SetActive(true);
        _btnScoreSubmitted.SetActive(false);

        RewardsController rewardsController = GameManager.Instance.GetRewardsController();

        int timeScore = ((int)rewardsController.timeLeft) * 100; //TODO get from proper place!
        int advantage1Score = rewardsController.caughtRing1 ? 1500 : 0; //TODO get from proper place!
        int advantage2Score = rewardsController.caughtRing2 ? 1000 : 0; //TODO get from proper place!
        int advantage3Score = rewardsController.caughtRing3 ? 2000 : 0; //TODO get from proper place!

        _txtTimeScore.text = timeScore.ToString();

        SolveScoreForAdvantage(advantage1Score, _imgAdvantage1, _txtAdvantage1Score);
        SolveScoreForAdvantage(advantage2Score, _imgAdvantage2, _txtAdvantage2Score);
        SolveScoreForAdvantage(advantage3Score, _imgAdvantage3, _txtAdvantage3Score);

        _txtFinalScore.text = (timeScore + advantage1Score + advantage2Score + advantage3Score).ToString();
    }

    public void OnRetryButtonPressed() {
        GameManager.Instance.StartGame();   
    }

    public void OnSubmitButtonPressed() {
        GameManager.Instance.GoToScoreSubmission();
    }

    public void OnScoreSuccessfullySubmitted() {
        _btnSubmitScore.SetActive(false);
        _btnScoreSubmitted.SetActive(true);
    }   
}
