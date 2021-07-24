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
    private Screen _screen;
    public float scaleMultiplier = 0.3f;

    private bool isGrowing = true;
    
    private void Start()
    {
        playerNumberText.text = "";

        originalScale = transform.localScale;

        _screen = GetComponentInParent<Screen>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _screen.PlayerColor;
    }

    void Update()
    {
        // grow
        if (isGrowing)
            transform.localScale *= 1 + growSpeed * Time.deltaTime;
        else
            transform.localScale *= 1 - growSpeed * Time.deltaTime;

        // update number
        var currentScale = Mathf.RoundToInt(transform.localScale.x / scaleMultiplier);
        UpdateText(currentScale);

        if (currentScale > maxGrowScale)
        {
            //shrink
            isGrowing = false;
            
        }
        else if (currentScale == 0)
        {
            if (!isGrowing)
            {
                // reset
                isGrowing = true;
                // TODO: destroy screen ?
            }
        }

        // handle input
        if (Input.GetKeyDown(_screen.ActionKey))
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
                EventManager.Emit(_screen.PlayerDeathEvent);
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
