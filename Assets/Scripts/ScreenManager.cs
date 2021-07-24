using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] List<GameObject> GameScreenPrefabs;
    
    [SerializeField] Screen TopLeft;
    [SerializeField] Screen TopRight;
    [SerializeField] Screen BottomLeft;
    [SerializeField] Screen BottomRight;
    [SerializeField] float ScreenRespawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        CreateNewScreen(TopLeft.transform);
        CreateNewScreen(TopRight.transform);
        CreateNewScreen(BottomLeft.transform);
        CreateNewScreen(BottomRight.transform);

        EventManager.Subscribe(GameEvent.TopLeftDeath, _ => 
        {
            Debug.Log("Top Left Died");
            StartCoroutine(DestroyAndRecreate(TopLeft));
        });
        EventManager.Subscribe(GameEvent.TopRightDeath, _ => 
        {
            Debug.Log("Top Right Died");
            StartCoroutine(DestroyAndRecreate(TopRight));
        });
        EventManager.Subscribe(GameEvent.BottomLeftDeath, _ => 
        {
            Debug.Log("Bottom Left Died");
            StartCoroutine(DestroyAndRecreate(BottomLeft));
        });
        EventManager.Subscribe(GameEvent.BottomRightDeath, _ => 
        {
            Debug.Log("Bottom Right Died");
            StartCoroutine(DestroyAndRecreate(BottomRight));
        });
    }

    private IEnumerator DestroyAndRecreate(Screen destroyScreen)
    {
        destroyScreen.DestroyGame();
        yield return new WaitForSeconds(ScreenRespawnTime);
        CreateNewScreen(destroyScreen.transform);
    }

    private void CreateNewScreen(Transform parentTransform)
    {
        var randomGame = GameScreenPrefabs[Random.Range(0, GameScreenPrefabs.Count)];
        var newScreen = Instantiate(randomGame);
        newScreen.transform.parent = parentTransform;
        newScreen.transform.localPosition = Vector3.zero;
    }
}
