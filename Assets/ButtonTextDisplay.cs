using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonTextDisplay : MonoBehaviour
{
    private ScreenGameplaySettings _screenGameplayMod;
    private TextMeshProUGUI _textUI;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<ScreenGameplaySettings>();
        _textUI = GetComponent<TextMeshProUGUI>();
        _textUI.text = _screenGameplayMod.ActionKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
