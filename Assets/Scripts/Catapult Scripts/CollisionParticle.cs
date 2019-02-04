using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour {

    public GameObject effect;
    private bool _isColliding;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (_isColliding)
            return;

        _isColliding = true;

        // Spawn Effect, then remove Script
        Instantiate(effect, transform.position, Quaternion.identity);
        //Destroy(this.gameObject);
    }

    //void OnCollisionExit2D(Collision2D coll) {
    //    _isColliding = false;
    //}
}
