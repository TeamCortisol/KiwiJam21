using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float MinimumSpawnDelay = 0.1f;
    [SerializeField] float SpawnDelay = 3f;
    [SerializeField] float RandomPos = 5f;
    [SerializeField] GameObject[] Meteors;

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
            var verticalSpawnDistance = Random.Range(-RandomPos, RandomPos);
            
            var prefabIdx = Random.Range(0, Meteors.Length);
            Instantiate(Meteors[prefabIdx], new Vector3(transform.position.x , transform.position.y + verticalSpawnDistance), Quaternion.identity);
        }
    }
}
