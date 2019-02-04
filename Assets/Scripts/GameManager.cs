using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils;

public enum GameState {
    MAIN_MENU,
    GAME,
    END_SCREEN,
    LEADERBOARD,
}

public class GameManager : Singleton<GameManager> {

    public int TotalTime;

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private RewardsController _rewardsController;

    private GameState _state;
    private int _gameStartTime;
    private int _latestScore;

    public GameObject _gamePrefab;
    private GameObject _gameInstance;

	// Use this for initialization
    protected override void Awake() {
        base.Awake(); 
        _uiManager.BuildSlider(TotalTime); //FIX THIS BETTER
        _uiManager.GoToMainMenu();
	}
	
	// Update is called once per frame
	void Update () {
        if (_uiManager.TimeHasEnded() && _state != GameState.END_SCREEN)
        {
            EndGame();
        }
    }

    void ChangeState(GameState state) {
        _state = state;
    }

    public void StartGame()
    {
        _uiManager.GoToGame();
        _gameInstance = (GameObject)Instantiate(_gamePrefab, _gamePrefab.transform.position,_gamePrefab.transform.rotation);
        _uiManager.BuildSlider(TotalTime); 
        _uiManager.StartTimer();
        ChangeState(GameState.GAME);

        _rewardsController.caughtRing1 = false;
        _rewardsController.caughtRing2 = false;
        _rewardsController.caughtRing3 = false;
        _rewardsController.timeLeft = TotalTime;
    }

    public void EndGame() {
        Destroy(_gameInstance);

        _rewardsController.timeLeft = (int)_uiManager.GetTimeLeft();

        //FIX REMOVE THIS FROM HERE
        int timeScore = ((int)_rewardsController.timeLeft) * 100; //TODO get from proper place!
        int advantage1Score = _rewardsController.caughtRing1 ? 1500 : 0; //TODO get from proper place!
        int advantage2Score = _rewardsController.caughtRing2 ? 1000 : 0; //TODO get from proper place!
        int advantage3Score = _rewardsController.caughtRing3 ? 2000 : 0; //TODO get from proper place!

        int totalScore = timeScore + advantage1Score + advantage2Score + advantage3Score;
        _latestScore = totalScore;

        ChangeState(GameState.END_SCREEN);
        _uiManager.GoToEndScreen();
    }

    public void GoToScoreSubmission() {
        _uiManager.GoToLeaderboard(_latestScore);
    }

    public void GoBack() {
        //TODO don't setup menu again
        _uiManager.GoBackToEndScreen();
    }

    public ScoreManager GetScoreManager() {
        return _scoreManager;
    }

    public UIManager GetUIManager()
    {
        return _uiManager;
    }

    public void WentThroughRing(GameObject ring)
    {
        if (ring.name.Contains("ring1")) {
            _rewardsController.caughtRing1 = true;   
        } else if (ring.name.Contains("ring2"))
        {
            _rewardsController.caughtRing2 = true;
        } else if (ring.name.Contains("ring3"))
        {
            _rewardsController.caughtRing3 = true;
        }
    }

    public RewardsController GetRewardsController() {
        return _rewardsController;
    }
}
