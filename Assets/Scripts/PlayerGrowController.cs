using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowController : MonoBehaviour
{
    public float growSpeed = 0.5f;
    public float maxGrowScale = 10f;

    public TextMesh playerNumberText;

    private Vector3 originalScale;
    private int _numberOfTimesGotHit = 0;
    private Screen _screenGameplayMod;
    public float scaleMultiplier = 0.3f;
    
    private void Start()
    {
        playerNumberText.text = "";

        originalScale = transform.localScale;

        _screenGameplayMod = GetComponentInParent<Screen>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _screenGameplayMod.PlayerColor;
    }

    void Update()
    {
        // grow
        transform.localScale *= 1 + growSpeed * Time.deltaTime;

        // update number
        var currentScale = Mathf.RoundToInt(transform.localScale.x / scaleMultiplier);
        UpdateText(currentScale);

        if (currentScale > maxGrowScale)
        {
            transform.localScale = originalScale;
        }

        // handle input
        if (Input.GetKeyDown(_screenGameplayMod.ActionKey))
        {
            Debug.Log("currentScale " + currentScale);
            if (NumberSpawner.Instance.CurrentNumber == currentScale)
            {
                Debug.Log("Ring HIT!");

                // reset
                transform.localScale = originalScale;
                playerNumberText.text = "";

                // generate new number
                NumberSpawner.Instance.GenerateNewNumber();
            }
            else { 
                // TODO: handle wrong press
                
            }
        }
    }

    private void UpdateText(int currentScale)
    {
        if (currentScale == 0)
            playerNumberText.text = "";
        else
            playerNumberText.text = currentScale.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _numberOfTimesGotHit++;
        Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }


}
