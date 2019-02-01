﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct CSVScoreEntry {
    public string name;
    public int score;
}

public class ScoreManager : MonoBehaviour {

    private SendToGoogle _sendToGoogle;
    private CSVWriter _csvWriter;
    private List<CSVScoreEntry> _sortedScores;

	// Use this for initialization
	void Awake () {
        _sendToGoogle = GetComponent<SendToGoogle>();
        _csvWriter = GetComponent<CSVWriter>();
        //Invoke("Test", 3);

        LoadScores();
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

    void LoadScores() {
        _sortedScores = new List<CSVScoreEntry>();

        //TODO check for internet connection and get the scores from google sheets 

        //Check for file existence and load the stored scores;
        if (System.IO.File.Exists(_csvWriter.GetPath()))
        {
            StreamReader reader = new StreamReader(_csvWriter.GetPath());    

            reader.ReadLine(); //Discard first line because it's the csv columns names
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                ProcessScoreLine(line);
            }

            _sortedScores.Sort((x, y) => y.score.CompareTo(x.score));
        }
    }

    public void UpdateScores() {
        LoadScores();
    }

    void ProcessScoreLine(string line) {
        string[] parts = line.Split(',');

        //index 1 is name and index 3 is score
        CSVScoreEntry entry = new CSVScoreEntry();
        entry.name = parts[1];
        entry.score = Convert.ToInt32(parts[3]);

        Debug.Log("Adding csv score entry with name: " + entry.name + " score: " + entry.score);
        _sortedScores.Add(entry);
    }

    public List<CSVScoreEntry> GetSortedScores() {
        return _sortedScores;
    }
}
