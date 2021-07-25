using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSpawner : MonoBehaviour
{
    [SerializeField] float InitialSpawnDelay = 3f;
    [SerializeField] float InitialSpawnDelayRandomness = 1f;
    [SerializeField] GameObject[] Meteors;

    private Screen _screenGameplayMod;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        var initialSpawnDelay = Random.Range(InitialSpawnDelay - InitialSpawnDelayRandomness, 
            InitialSpawnDelay + InitialSpawnDelayRandomness);
        StartCoroutine(SpawnMeteor(initialSpawnDelay));
    }

    private IEnumerator SpawnMeteor(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            
            var prefabIdx = Random.Range(0, Meteors.Length);
            var cactus = Instantiate(Meteors[prefabIdx], transform.position, Quaternion.identity);
            var scaleUp = 1.8f * _screenGameplayMod.CurrentDifficulty;
            cactus.transform.localScale = new Vector3(
                cactus.transform.localScale.x, 
                cactus.transform.localScale.y + scaleUp, 
                cactus.transform.localScale.z);
            cactus.transform.localPosition = new Vector3(
                cactus.transform.localPosition.x, 
                cactus.transform.localPosition.y + 0.5f * scaleUp, 
                cactus.transform.localPosition.z);
            cactus.transform.parent = transform;
        }
    }
}
