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
    private GameObject _mainScreen;
    [SerializeField]
    private GameObject _gameScreen;
    [SerializeField]
    private GameObject _endScreen;
    [SerializeField]
    private GameObject _leaderboardScreen;

    private ScreenState _state;
    private GameObject _activeScreen;

    private void ChangeState(ScreenState state) {
        if (_activeScreen != null) {
            _activeScreen.SetActive(false);
        }

        switch(state) {
            case ScreenState.MAIN_MENU:
                _activeScreen = _mainScreen;
                break;
            case ScreenState.GAME:
                _activeScreen = _gameScreen;
                break;
            case ScreenState.END_SCREEN:
                _activeScreen = _endScreen;
                break;
            case ScreenState.LEADERBOARD:
                _activeScreen = _leaderboardScreen;
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
        ChangeState(ScreenState.END_SCREEN);
    }

    public void GoBackToEndScreen()
    {
        ChangeState(ScreenState.END_SCREEN);
    }

    public void GoToLeaderboard() {
        ChangeState(ScreenState.LEADERBOARD);
    }
}
