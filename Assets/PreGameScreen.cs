using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameScreen : MonoBehaviour {

    [SerializeField]
    private Text _text;

	// Use this for initialization
	void Start () {
        StartCoroutine("ShowGameCountdown");
	}
	
    IEnumerator ShowGameCountdown() {
        _text.text = "3";
        yield return new WaitForSeconds(1);
        _text.text = "2";
        yield return new WaitForSeconds(1);
        _text.text = "1";
        yield return new WaitForSeconds(1);
        _text.text = "Unleash your apps!";
        yield return new WaitForSeconds(2);

        GameManager.Instance.StartGame();
        yield break;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
