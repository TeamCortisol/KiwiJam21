using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountText : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private GlobalState _globalState;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _globalState = FindObjectOfType<GlobalState>();
    }

    // Update is called once per frame
    void Update()
    {
        _textMesh.text = $"Deaths: {_globalState.NumberOfDeaths}";
    }
}
