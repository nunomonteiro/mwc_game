using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour {

    public GameObject effect;

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Spawn Effect, then remove Script
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
