using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowController : MonoBehaviour
{
    public float InitialGrowSpeed = 0.5f;
    private float _currentGrowSpeed;
    public float maxGrowScale = 10f;

    public TextMesh playerNumberText;
    private SpriteRenderer _spriteRenderer;

    private Vector3 originalScale;
    private int _numberOfTimesGotHit = 0;
    private Screen _screen;
    public float scaleMultiplier = 0.3f;

    [Space]
    public Color ringColor = Color.red;
    public Color PlayerSuccessColor = Color.green;
    public SpriteRenderer[] ringSprites;

    private bool isGrowing = true;

    private int _currentNumber;
    private Color _playerFadedColor;
    
    private void Start()
    {
        _currentGrowSpeed = InitialGrowSpeed;
        playerNumberText.text = "";

        originalScale = transform.localScale;

        _screen = GetComponentInParent<Screen>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _playerFadedColor = new Color(_screen.PlayerColor.r, _screen.PlayerColor.g, _screen.PlayerColor.b, 0.2f);

        SetRingColor();
    }

    private void SetRingColor()
    {
        foreach (var ring in ringSprites)
            ring.color = Color.clear;

        _currentNumber = NumberSpawner.Instance.GenerateNewNumber();
        ringSprites[_currentNumber - 1].color = ringColor;

    }

    void Update()
    {
        // grow
        if (isGrowing)
            transform.localScale *= 1 + _currentGrowSpeed * Time.deltaTime;
        else
            transform.localScale *= 1 - _currentGrowSpeed * Time.deltaTime;

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
                // destroy screen
                EventManager.Emit(_screen.PlayerDeathEvent);
                return;
            }
        }

        // handle input
        if (Input.GetKeyDown(_screen.ActionKey))
        {
            Debug.Log("currentScale " + currentScale);
            if (IsPlayerOnRing())
            {
                StartCoroutine(PlayerWin());
            }
            else { 
                // handle wrong press
                EventManager.Emit(_screen.PlayerDeathEvent);
            }
        }

        UpdatePlayerColor();
    }

    private void UpdateText(int currentScale)
    {
        if (currentScale == 0)
            playerNumberText.text = "";
        else
            playerNumberText.text = currentScale.ToString();
    }

    private IEnumerator PlayerWin()
    {
        Debug.Log("Ring HIT!");
        _currentGrowSpeed = 0;
        _spriteRenderer.color = PlayerSuccessColor;

        yield return new WaitForSeconds(3);

        // reset
        transform.localScale = originalScale;
        playerNumberText.text = "";
        _currentGrowSpeed = InitialGrowSpeed;

        // generate new number
        SetRingColor();
    }

    private bool IsPlayerOnRing() => _currentNumber == Mathf.RoundToInt(transform.localScale.x / scaleMultiplier);

    private void UpdatePlayerColor()
    {
        if (_currentGrowSpeed == 0)
        {
            return;
        }
        _spriteRenderer.color = IsPlayerOnRing() ? _screen.PlayerColor : _playerFadedColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _numberOfTimesGotHit++;
        Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }
}
