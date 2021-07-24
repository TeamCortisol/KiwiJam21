using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumberChecker : MonoBehaviour
{
    private int _numberOfTimesGotHit = 0;
    private ScreenGameplaySettings _screenGameplayMod;
    //public NumberSpawner spawner;
    public float scaleMultiplier = 0.3f;
    public float tolerance = 0.5f;

    void Start()
    {
        //spawner = GetComponent<NumberSpawner>();

        _screenGameplayMod = GetComponentInParent<ScreenGameplaySettings>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _screenGameplayMod.PlayerColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_screenGameplayMod.ActionKey))
        {
            var currentScale = (transform.localScale.x / scaleMultiplier);
            Debug.Log("currentScale "+ currentScale);
            if (NumberSpawner.Instance.CurrentNumber >= currentScale - tolerance
                && NumberSpawner.Instance.CurrentNumber <= currentScale + tolerance)
            {
                Debug.Log("Ring HIT!");

                //reset
                NumberSpawner.Instance.GenerateNewNumber();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _numberOfTimesGotHit++;
        Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }
}
