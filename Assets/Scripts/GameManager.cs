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
    public int TotalAttempts;
    public int scoreFromRing1;
    public int scoreFromRing2;
    public int scoreFromRing3;
    public int scoreFromTime;
    public GameObject _gamePrefab;

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private RewardsController _rewardsController;

    private GameState _state;
    private int _gameStartTime;
    private int _latestScore;
    private bool _touchedBarrier;
    private Spawner _spawner;

    private GameObject _gameInstance;
    private int _amountAttempts;
    private Attempt _currentAttempt;

	// Use this for initialization
    protected override void Awake() {
        base.Awake(); 
        //_uiManager.StartTimer(TotalTime); //FIX THIS BETTER
        _uiManager.GoToMainMenu();
	}
	
	// Update is called once per frame
	void Update () {
        if (_uiManager.TimeHasEnded() && _state != GameState.END_SCREEN)
        {
            EndTurn();
        }
    }

    void ChangeState(GameState state) {
        _state = state;
    }

    public void StartCountdown() {
        _uiManager.GoToPreGame();
    }

    public void StartGame()
    {
        _rewardsController.Reset();

        _amountAttempts = TotalAttempts;
        _uiManager.GoToGame(TotalAttempts);
        _uiManager.StartTimer(TotalTime);

        _gameInstance = (GameObject)Instantiate(_gamePrefab, _gamePrefab.transform.position, _gamePrefab.transform.rotation);
        _spawner = _gameInstance.GetComponentInChildren<Spawner>();

        StartTurn();

        ChangeState(GameState.GAME);
    }

    void StartTurn() {
        _spawner.SpawnProjectile();
        _currentAttempt = new Attempt();

        _touchedBarrier = false;
    }

    public void EndTurn() {
        _uiManager.LostAttempt();

        _amountAttempts--;
        //TODO update UI with attempt number
        _rewardsController.AddNewAttempt(_currentAttempt);

        if (_amountAttempts <= 0)
            EndGame();
        else {
            StartTurn();
        }


    }

    void EndGame() {
        Destroy(_gameInstance);

        _rewardsController.timeLeft = (int)_uiManager.GetTimeLeft();

        int timeScore = ((int)_rewardsController.timeLeft) * scoreFromTime;
        int advantage1Score = _rewardsController.ScoreForRing(1);
        int advantage2Score = _rewardsController.ScoreForRing(2);
        int advantage3Score = _rewardsController.ScoreForRing(3);

        int totalScore = timeScore + advantage1Score + advantage2Score + advantage3Score;
        _latestScore = totalScore;

        ChangeState(GameState.END_SCREEN);
        _uiManager.GoToEndScreen();
    }

    public void GoToScoreSubmission() {
        ChangeState(GameState.LEADERBOARD);
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

    public void OnTouchedBarrier() {
        _touchedBarrier = true;
    }

    public void WentThroughRing(GameObject ring)
    {
        //Don't take into account further touches after having touched a barrier
        if (_touchedBarrier)
            return;

        if (ring.name.Contains("ring1")) {
            _currentAttempt.caughtRing1 = true;   
        } else if (ring.name.Contains("ring2"))
        {
            _currentAttempt.caughtRing2 = true;
        } else if (ring.name.Contains("ring3"))
        {
            _currentAttempt.caughtRing3 = true;
        }
    }

    public RewardsController GetRewardsController() {
        return _rewardsController;
    }

    public Transform GetGameInstanceRoot() {
        if (_gameInstance != null)
            return _gameInstance.transform;
        return null;
    }

    public void ClearLeaderboardEntries() {
        if (_scoreManager != null) {
            _scoreManager.DeleteScoresFile();
        }

        //Check if we're in the leaderboard screen and immediately update it
        if (_state == GameState.LEADERBOARD) {
            GetUIManager().RefreshLeaderboard();
        }
    }
}
