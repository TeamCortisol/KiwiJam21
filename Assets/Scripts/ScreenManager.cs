using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] List<SubGame> GameScreenPrefabs;
    [SerializeField] Screen TopLeft;
    [SerializeField] Screen TopRight;
    [SerializeField] Screen BottomLeft;
    [SerializeField] Screen BottomRight;
    [SerializeField] float ScreenRespawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        TopLeft.CreateGame(GetRandomGame());
        TopRight.CreateGame(GetRandomGame());
        BottomLeft.CreateGame(GetRandomGame());
        BottomRight.CreateGame(GetRandomGame());

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

    private void Update()
    {
        //TopLeft.enabled = !GameManager.Instance.IsGameComplete;
        //TopRight.enabled = !GameManager.Instance.IsGameComplete;
        //BottomLeft.enabled = !GameManager.Instance.IsGameComplete;
        //BottomRight.enabled = !GameManager.Instance.IsGameComplete;
    }

    private IEnumerator DestroyAndRecreate(Screen screen)
    {
        screen.DestroyGame();
        yield return new WaitForSeconds(ScreenRespawnTime);
        if (!GameManager.Instance.IsGameComplete)
        {
            screen.CreateGame(GetRandomGame());
        }
    }

    private SubGame GetRandomGame() => GameScreenPrefabs[Random.Range(0, GameScreenPrefabs.Count)];
}
