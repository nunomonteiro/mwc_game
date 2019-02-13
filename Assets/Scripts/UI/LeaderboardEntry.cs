﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardEntry : MonoBehaviour {

    public Color top5placesTextColor;
    public Color[] top5PlacesCellColors;
    [SerializeField]
    private Image _cell;
    [SerializeField]
    private TextMeshProUGUI _txtPlace;
    [SerializeField]
    private TextMeshProUGUI _txtName;
    [SerializeField]
    private TextMeshProUGUI _txtScore;
    [SerializeField]
    private TextMeshProUGUI _txtAppcoins;
    [SerializeField]
    private GameObject _appcoinsContainer;

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

        if (place > 5)
        {
            _appcoinsContainer.SetActive(false);
            _cell.color = new Color(0, 0, 0, 0);
        } else {
            _cell.color = top5PlacesCellColors[place - 1];
            _txtPlace.color = top5placesTextColor;
            _appcoinsContainer.SetActive(true);
            _txtAppcoins.text = GameManager.Instance.GetAppcoinsRewardForPlace(place).ToString();
        }
            
    }
}
