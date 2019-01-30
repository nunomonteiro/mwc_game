using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private SendToGoogle _sendToGoogle;
    private CSVWriter _csvWriter;

	// Use this for initialization
	void Awake () {
        _sendToGoogle = GetComponent<SendToGoogle>();
        _csvWriter = GetComponent<CSVWriter>();

        Invoke("Test", 3);
	}
	
    void Test() {
        PostNewScore("Nuno Monteiro", "mail@mail.com", 999, true);
        PostNewScore("Bruno Varela", "mail2@mail.com", 998, false);
        PostNewScore("Mikie Ribeiro", "mail3@mail.com", 997, false);
    }

    public void PostNewScore(string name, string mail, int score, bool wantsMore) {

        string[] values = new string[4];
        values[0] = name;
        values[1] = mail;
        values[2] = score.ToString();
        values[3] = wantsMore ? "YES" : "NO";

        _csvWriter.AddNewEntry(values);

        //TODO only do this if we know that there's internet available
        //_sendToGoogle.Send(name, mail, wantsMore ? "YES" : "NO", score.ToString());

        //TODO add score to local list to showcase top winners
    }
}
