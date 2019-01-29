using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private SendToGoogle _sendToGoogle;

	// Use this for initialization
	void Awake () {
        _sendToGoogle = GetComponent<SendToGoogle>();
	}
	
    public void PostNewScore(string name, string mail, int score, bool wantsMore) {
        _sendToGoogle.Send(name, mail, wantsMore ? "YES" : "NO", score.ToString());

        //TODO add score to local list to showcase top winners
        //TODO if the TV has no internet this has to write to a local file
    }
}
