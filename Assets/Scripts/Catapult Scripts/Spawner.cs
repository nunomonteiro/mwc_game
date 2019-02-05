using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject projectilePrefab;

    public void SpawnProjectile()
    {
        GameObject spawn = Instantiate(projectilePrefab, transform.position, Quaternion.identity, GameManager.Instance.GetGameInstanceRoot()) as GameObject;
    }
}
