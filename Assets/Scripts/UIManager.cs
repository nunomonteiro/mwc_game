using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private ScreenState _state;
    private GameObject _activeScreen;

    private void Awake()
    {
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

    public void GoToMainMenu() {
        ChangeState(ScreenState.MAIN_MENU);
    }

    public void GoToGame()
    {
        ChangeState(ScreenState.GAME);
    }

    public void GoToEndScreen() {
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

    public void OnScoreSuccessfullySubmitted()
    {
        _endScreen.OnScoreSuccessfullySubmitted();
        _leaderboardScreen.OnScoreSuccessfullySubmitted();
    }

}
