using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreGameScreen : MonoBehaviour {

    [SerializeField]
    private Sprite[] _numbers;

    [SerializeField]
    private Image _cleanBg;

    [SerializeField]
    private Image _gradientBg;

    [SerializeField]
    private Image _numberImg;

    [SerializeField]
    private TextMeshProUGUI _text;


    // Use this for initialization
	void Start () {
        StartCoroutine("ShowGameCountdown");
	}
	
    IEnumerator ShowGameCountdown() {
        //Setup. gradient background and image on, all the rest off
        _numberImg.gameObject.SetActive(true);
        _gradientBg.gameObject.SetActive(true);
        _cleanBg.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);

        _numberImg.sprite = _numbers[0];
        yield return new WaitForSeconds(1);
        _numberImg.sprite = _numbers[1];
        yield return new WaitForSeconds(1);
        _numberImg.sprite = _numbers[2];
        yield return new WaitForSeconds(1);

        //Setup. gradient background and image off, clean background and text on
        _numberImg.gameObject.SetActive(false);
        _gradientBg.gameObject.SetActive(false);
        _cleanBg.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        GameManager.Instance.StartGame();
        yield break;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
