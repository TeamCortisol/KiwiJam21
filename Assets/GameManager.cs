using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject WinUI;
    public static GameManager Instance;


    [SerializeField] float GameLengthTime = 240f;
    private float gameTimeLeft;

    public bool IsGameComplete { get; private set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        gameTimeLeft = GameLengthTime;
        IsGameComplete = false;

    }

    private void Update()
    {
        gameTimeLeft -= Time.deltaTime;
        if (gameTimeLeft <= 0)
        {
            Debug.Log("You win!");
            IsGameComplete = true;
            WinUI.SetActive(true);
        }

        // TODO: HANDLE RESTART

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
