using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour {

    [SerializeField]
    private Text _txtPlace;
    [SerializeField]
    private Text _txtName;
    [SerializeField]
    private Text _txtScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupWithScoreEntry(int place, CSVScoreEntry entry) {
        _txtPlace.text = place.ToString();
        _txtName.text = entry.name;
        _txtScore.text = entry.score.ToString();
    }
}
