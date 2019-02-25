using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour {

    [SerializeField]
    private GameObject _purchaseBtn;

	// Use this for initialization
	void Start () {
        if (GameManager.Instance.HasPurchasedFullTrajectory()) {
            _purchaseBtn.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayButtonPressed() {
        GameManager.Instance.StartCountdown();
    }
}
