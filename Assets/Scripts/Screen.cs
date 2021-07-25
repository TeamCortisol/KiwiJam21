using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] public KeyCode ActionKey = KeyCode.Q;
    [SerializeField] public Color PlayerColor;
    [SerializeField] public GameEvent PlayerDeathEvent;

    public float CurrentDifficulty;

    private GlobalState _globalState;

    // Start is called before the first frame update
    void Start()
    {
        _globalState = FindObjectOfType<GlobalState>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDifficulty = _globalState.Difficulty;
    }

    public void DestroyGame()
    {
        var game = GetComponentInChildren<SubGame>();
        if (game != null)
        {
            Destroy(game.gameObject);
        }
    }

    public GameObject CreateGame(SubGame subGame)
    {
        var newGame = Instantiate(subGame);
        newGame.transform.parent = transform;
        newGame.transform.localPosition = Vector3.zero;

        return newGame.gameObject;
    }
}
