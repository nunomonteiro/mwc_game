using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject birdPrefab;

    public GameObject spawn;

    private void Start()
    {
        spawnNext();
    }

    //void FixedUpdate()
    //{
    //    if(Vector2.Distance(spawn.transform.position,transform.position) > 3f)
    //    {
    //        spawnNext();
    //    }
    //}

    void spawnNext()
    {
        spawn = Instantiate(birdPrefab, transform.position, Quaternion.identity) as GameObject;
    }
}
