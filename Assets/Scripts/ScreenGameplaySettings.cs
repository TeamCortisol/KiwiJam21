using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameplaySettings : MonoBehaviour
{
    [SerializeField] public KeyCode ActionKey = KeyCode.Q;
    [SerializeField] public Color PlayerColor;

    public float CurrentSpeed { get; private set; }

    private GlobalState _globalState;

    // Start is called before the first frame update
    void Start()
    {
        _globalState = FindObjectOfType<GlobalState>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpeed = _globalState.Difficulty * 10;
    }
}
