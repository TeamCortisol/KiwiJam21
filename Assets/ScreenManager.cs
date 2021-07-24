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
        EventManager.Subscribe(GameEvent.TopLeftDeath, _ => 
        {
            Debug.Log("Top Left Died");
            StartCoroutine(DestroyAndRecreate(TopLeft, GameEvent.TopLeftDeath));
        });
        EventManager.Subscribe(GameEvent.TopRightDeath, _ => 
        {
            Debug.Log("Top Right Died");
            StartCoroutine(DestroyAndRecreate(TopRight, GameEvent.TopRightDeath));
        });
        EventManager.Subscribe(GameEvent.BottomLeftDeath, _ => 
        {
            Debug.Log("Bottom Left Died");
            StartCoroutine(DestroyAndRecreate(BottomLeft, GameEvent.BottomLeftDeath));
        });
        EventManager.Subscribe(GameEvent.BottomRightDeath, _ => 
        {
            Debug.Log("Bottom Right Died");
            StartCoroutine(DestroyAndRecreate(BottomRight, GameEvent.BottomRightDeath));
        });
    }

    private IEnumerator DestroyAndRecreate(Screen destroyScreen, GameEvent gameEvent)
    {
        destroyScreen.DestroyGame();
        yield return new WaitForSeconds(ScreenRespawnTime);
        var newScreen = Instantiate(GameScreenPrefabs[0]);
        newScreen.transform.parent = destroyScreen.transform;
        newScreen.transform.localPosition = Vector3.zero;
    }
}
