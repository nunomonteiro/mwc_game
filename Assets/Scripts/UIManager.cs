﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenState{
    MAIN_MENU,
    GAME,
    END_SCREEN,
    LEADERBOARD,
}

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _mainScreenObj;

    [SerializeField]
    private GameObject _gameScreenObj;

    [SerializeField]
    private GameObject _endScreenObj;
    private EndScreen _endScreen;

    [SerializeField]
    private GameObject _leaderboardScreenObj;
    private LeaderboardScreen _leaderboardScreen;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private GameObject _attemptPrefab;

    [SerializeField]
    private Transform _attemptsParent;

    private List<GameObject> _attemptsUI;

    private int _gameTime;
    private bool _startedTimer
;
    private ScreenState _state;
    private GameObject _activeScreen;
    private float _timestampTimeStart;
    private int _remainingAttempts;
    private int _totalAttempts;

    private void Awake()
    {
        _attemptsUI = new List<GameObject>();
        _endScreen = _endScreenObj.GetComponent<EndScreen>();
        _leaderboardScreen = _leaderboardScreenObj.GetComponent<LeaderboardScreen>();

        //disable all screens
        _mainScreenObj.SetActive(false);
        _gameScreenObj.SetActive(false);
        _endScreenObj.SetActive(false);
        _leaderboardScreenObj.SetActive(false);
    }

    private void ChangeState(ScreenState state) {
        if (_activeScreen != null) {
            _activeScreen.SetActive(false);
        }

        switch(state) {
            case ScreenState.MAIN_MENU:
                _activeScreen = _mainScreenObj;
                break;
            case ScreenState.GAME:
                _activeScreen = _gameScreenObj;
                break;
            case ScreenState.END_SCREEN:
                _activeScreen = _endScreenObj;
                break;
            case ScreenState.LEADERBOARD:
                _activeScreen = _leaderboardScreenObj;
                break;
        }
        _activeScreen.SetActive(true);

    }

    private void Update()
    {
        if (_startedTimer)
        {
            _slider.value -= Time.deltaTime;
        }
    }

    public void GoToMainMenu() {
        ChangeState(ScreenState.MAIN_MENU);
    }

    public void GoToGame(int totalAttempts)
    {
        //Remove previous ones
        foreach(Transform child in _attemptsParent) {
            Destroy(child.gameObject);
        }
            
        _attemptsUI.Clear();

        _totalAttempts = totalAttempts;
        _remainingAttempts = totalAttempts;

        //Create attempt prefabs and add them to attempts parent
        for (int i = 0; i < totalAttempts; i++) {
            GameObject attempt = Instantiate(_attemptPrefab, _attemptsParent) as GameObject;
            _attemptsUI.Add(attempt);
        }
        ChangeState(ScreenState.GAME);
    }

    public void LostAttempt() {
        //Hide another life
        _attemptsUI[_totalAttempts - _remainingAttempts].SetActive(false);
        _remainingAttempts--;
    }

    void LeaveGame() {
        _startedTimer = false;
    }

    public void GoToEndScreen() {
        LeaveGame();
        _endScreen.Setup();
        ChangeState(ScreenState.END_SCREEN);
    }

    public void GoBackToEndScreen()
    {
        ChangeState(ScreenState.END_SCREEN);
    }

    public void GoToLeaderboard(int score) {
        _leaderboardScreen.SetupForScore(score);
        ChangeState(ScreenState.LEADERBOARD);
    }

    public void StartTimer(int time)
    {
        _gameTime = time;
        _slider.maxValue = _gameTime;
        _slider.value = _gameTime;
        _timestampTimeStart = Time.time;
        _startedTimer = true;
    }

    public bool TimeHasEnded()
    {
        return _slider.value <= 0 && _startedTimer;
    }

    public float GetTimeLeft() {
        return _slider.value;
    }

    public void OnScoreSuccessfullySubmitted()
    {
        _endScreen.OnScoreSuccessfullySubmitted();
        _leaderboardScreen.OnScoreSuccessfullySubmitted();

    }

}
