using System;
using UnityEngine;

public class BallSameTypeDetector : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    
    private BallType _ballType;

    public BallType BallType => _ballType;

    public event Action<Ball> OnFound;

    public void InitBallType(BallType type) => _ballType = type;

    public void CheckCollidersAround() => _collider.enabled = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Ball ball))
        {
            if (ball.BallSameTypeDetector.BallType == _ballType) 
                OnFound?.Invoke(ball);
        }
    }

    public void Deactivate() => _collider.enabled = false;
}