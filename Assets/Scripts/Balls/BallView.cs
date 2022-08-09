using System;
using TMPro;
using UnityEngine;

public class BallView : MonoBehaviour
{
    [SerializeField] private ShootButtonEvent _shootButtonEvent;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public event Action OnPressShootButton;
    public event Action OnReleasedShootButton;

    private void OnEnable()
    {
        _shootButtonEvent.OnDown += ReportEventPress;
        _shootButtonEvent.OnUp += ReportEventReleased;
    }

    public void EnableViewText() => _text.enabled = true;

    public void DisableViewText() => _text.enabled = false;

    public void SetValueTest(string value) => _text.text = value;
    
    public float GetRadiusSprite() => _spriteRenderer.size.x / 2;
    
    private void ReportEventPress() => OnPressShootButton?.Invoke();
    private void ReportEventReleased() => OnReleasedShootButton?.Invoke();

    public void PlayDeathEffect() => _deathEffect.Play();

    public void ActivateSpriteRenderer() => _spriteRenderer.enabled = true;
    public void DeactivateSpriteRenderer() => _spriteRenderer.enabled = false;

    private void OnDisable()
    {
        _shootButtonEvent.OnDown -= ReportEventPress;
        _shootButtonEvent.OnUp -= ReportEventReleased;
    }
}