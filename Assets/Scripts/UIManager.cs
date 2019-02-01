using System.Collections;
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
    private GameObject _mainScreen;
    [SerializeField]
    private GameObject _gameScreen;
    [SerializeField]
    private GameObject _endScreen;
    [SerializeField]
    private GameObject _leaderboardScreen;
    [SerializeField]
    private Slider _slider;



    private int _gameTime;
    private bool _startTime;
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

    private void Update()
    {
        if (_startTime)
        {
            StartCoroutine(_timeChanger());
        }
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

    public void BuildSlider(int time)
    {
        _gameTime = time;
        _slider.maxValue = _gameTime;
        _slider.value = _gameTime;
    }


    IEnumerator _timeChanger()
    {
        _startTime = false;
        yield return new WaitForSeconds(1f);
        _slider.value -= 1f;
        if(_slider.value > 0)
        {
            _startTime = true;
        }
        else
        {
            _startTime = false;
        }

    }

    public bool TimeHasEnded()
    {
        return _slider.value <= 0;
    }

    public void StartTimer()
    {
        _startTime = true;
    }

}
