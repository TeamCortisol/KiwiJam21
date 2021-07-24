using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float SpawnDelay = 10f;
    [SerializeField] float MinimumSpawnDelay = 0.1f;
    [SerializeField] GameObject Meteor;

    private Screen _screenGameplayMod;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        StartCoroutine(SpawnMeteor(SpawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnMeteor(float delay)
    {
        while (true)
        {
            var nextSpawnTime = Mathf.Max(MinimumSpawnDelay, delay * (1 - _screenGameplayMod.CurrentDifficulty));
            yield return new WaitForSeconds(nextSpawnTime);
            var horizontalSpawnDistance = Random.Range(-5, 5);
            Instantiate(Meteor, new Vector3(transform.position.x + horizontalSpawnDistance, transform.position.y), Quaternion.identity);
        }
    }
}
