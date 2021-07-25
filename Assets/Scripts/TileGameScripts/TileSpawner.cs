using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    public float timeBetweenSpawn;
    private float spawnTime;
    private Screen _screenGameplayMod;

    private void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        StartCoroutine(SpawnTile());
    }

    private IEnumerator SpawnTile()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            float randomX = Random.Range(0.1f, 2);
            var prefabIdx = Random.Range(0, tiles.Length);
            var newTile = Instantiate(tiles[prefabIdx], transform.position, Quaternion.identity);
            newTile.transform.localScale += new Vector3(randomX,0,0);
        }
    }
}
