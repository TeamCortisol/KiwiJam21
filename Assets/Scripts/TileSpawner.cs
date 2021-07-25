using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    public float timeBetweenSpawn;
    private float spawnTime;
    private Screen _screenGameplayMod;

    private void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(_screenGameplayMod.ActionKey))
        {
            if (Time.time > spawnTime)
            {
                SpawnTile();
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
    }

    private void SpawnTile()
    {
        float randomX = Random.Range(0.1f, 2);
        GameObject newTile = Instantiate(tile, transform.position, Quaternion.identity);
        newTile.transform.localScale += new Vector3(randomX,0,0);
    }
}
