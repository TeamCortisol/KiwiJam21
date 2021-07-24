using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float JumpForce = 100f;

    private Rigidbody2D _rigidbody;
    private int _numberOfTimesGotHit = 0;    
    private Screen _screenGameplayMod;
    private GameEvent _onDeathEvent;

    private void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _onDeathEvent = _screenGameplayMod.PlayerDeathEvent;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = _screenGameplayMod.PlayerColor;
    }

    void Update()
    {
        // hack: start isn't called when this object is instantiated so call it manually here
        if (_screenGameplayMod == null)
        {
            Start();
        }

        if (Input.GetKeyDown(_screenGameplayMod.ActionKey))
        {
            _rigidbody.AddForce(JumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void Die()
    {
        if (_numberOfTimesGotHit == 0)
        {
            _numberOfTimesGotHit++;
            EventManager.Emit(_onDeathEvent);
        }
        //Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }
}
