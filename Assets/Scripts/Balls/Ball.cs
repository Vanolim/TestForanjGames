using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    
    public void SetPosition(Vector2 position) 
        => transform.localPosition = position;
}