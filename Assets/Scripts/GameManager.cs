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

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private ScoreManager _scoreManager;

    private GameState _state;
    private float _gameStartTime;
    private int _latestScore;

	// Use this for initialization
    protected override void Awake() {
        base.Awake(); 
        _uiManager.GoToMainMenu();
	}
	
	// Update is called once per frame
	void Update () {
        if ((_state == GameState.GAME) && (Time.time - _gameStartTime > 3f) && (_state != GameState.END_SCREEN)) {
            EndGame(987);
        }
	}

    void ChangeState(GameState state) {
        _state = state;
    }

    public void StartGame()
    {
        _uiManager.GoToGame();
        _gameStartTime = Time.time;
        ChangeState(GameState.GAME);
    }

    public void EndGame(int score) {
        ChangeState(GameState.END_SCREEN);
        _latestScore = score;
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
}
