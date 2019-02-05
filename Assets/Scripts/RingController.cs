using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RingController : MonoBehaviour {

    public int scoreValue;
    GameManager gameManager;
    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        gameManager.AddScore(scoreValue);
    }

    public void StoneHasPassed(){
        
    }
}
