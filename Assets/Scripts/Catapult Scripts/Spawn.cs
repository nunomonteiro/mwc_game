using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject birdPrefab;

    public GameObject spawnPosition;
  
    private void Start()
    {
        spawnNext();
    }

    void FixedUpdate()
    {

    }

    void spawnNext()
    {
        Instantiate(birdPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerExit2D(Collider2D co)
    {

    }

    bool sceneMoving()
    {
        // Find all Rigidbodies, see if any is still moving a lot
        Rigidbody2D[] bodies = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
        foreach (Rigidbody2D rb in bodies)
            if (rb.velocity.sqrMagnitude > 5)
                return true;
        return false;
    }
}
