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
    private Text _txtLivesScore;
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

        int timeScore = ((int)rewardsController.timeLeft) * GameManager.Instance.scoreFromTime;
        int advantage1Score = rewardsController.ScoreForRing(1);
        int advantage2Score = rewardsController.ScoreForRing(2);
        int advantage3Score = rewardsController.ScoreForRing(3);
        int livesScore = GameManager.Instance.GetAttemptsLeft() * GameManager.Instance.scoreFromLives;

        _txtTimeScore.text = timeScore.ToString();
        _txtLivesScore.text = livesScore.ToString();

        SolveScoreForAdvantage(advantage1Score, _imgAdvantage1, _txtAdvantage1Score);
        SolveScoreForAdvantage(advantage2Score, _imgAdvantage2, _txtAdvantage2Score);
        SolveScoreForAdvantage(advantage3Score, _imgAdvantage3, _txtAdvantage3Score);

        _txtFinalScore.text = (timeScore + advantage1Score + advantage2Score + advantage3Score + livesScore).ToString();
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
