using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float JumpForce = 100f;
    [SerializeField] bool CanJumpInAir = true;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private int _numberOfTimesGotHit = 0;    
    private Screen _screenGameplayMod;
    private GameEvent _onDeathEvent;

    private void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
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
            // another hack: keep doing this until it is not null
            return;  // don't even bother to do the rest of Update()
        }

        if (Input.GetKeyDown(_screenGameplayMod.ActionKey) && CanJump())
        {
            _rigidbody.AddForce(JumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out _))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Enemy>(out _))
        {
            Die();
        }
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

    private bool CanJump() => CanJumpInAir || _collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
}
