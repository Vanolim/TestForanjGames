using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpringJoint2D _springJoint2D;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BallMovement _ballMovement;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private BallView _ballView;
    [SerializeField] private BallSameTypeDetector _ballSameTypeDetector;

    private bool _isMatrixBall = false;

    public bool IsHasBreakThroughPotential { get; private set; }

    public BallView BallView => _ballView;
    public BallMovement BallMovement => _ballMovement;
    public BallSameTypeDetector BallSameTypeDetector => _ballSameTypeDetector;

    public event Action<Ball> OnBurst;
    public event Action<Ball> OnCollided;
    

    public void InitBallType(BallType type) => _ballSameTypeDetector.InitBallType(type);

    public void Move(IReadOnlyList<Vector2> dataTrajectoryPoints, float force) => 
        _ballMovement.MoveThroughDots(dataTrajectoryPoints, force);

    public void SetBreakthroughPotential(bool value) =>
        IsHasBreakThroughPotential = value;

    public void Burst()
    {
        _ballView.DeactivateSpriteRenderer();
        _collider.enabled = false;
        OnBurst?.Invoke(this);
        _ballView.PlayDeathEffect();
        StartCoroutine(DeadWaiting());
    }

    public void SetMatrixActivate()
    {
        _ballMovement.OnReachedEndPoint -= SetMatrixActivate;
        _collider.enabled = true;
        _ballView.ActivateSpriteRenderer();
        ActivateSpringJoint();
        _rb.isKinematic = false;
        _rb.gravityScale = 1;
        _isMatrixBall = true;
    }

    private void ActivateSpringJoint()
    {
        _springJoint2D.connectedAnchor = transform.position;
        _springJoint2D.enabled = true;
    }

    public void ShotActivation()
    {
        _collider.enabled = true;
        _rb.isKinematic = false;
        _rb.gravityScale = 0;
    }

    public void Fell()
    {
        _collider.enabled = false;
        _springJoint2D.enabled = false;
        _rb.isKinematic = false;
        _rb.gravityScale = 1;
    }

    public void SetMatrixActivationAfterArrivingPoint() => _ballMovement.OnReachedEndPoint += SetMatrixActivate;

    private IEnumerator DeadWaiting()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);
        yield return waitTime;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.TryGetComponent(out Ball ball))
        {
            OnCollided?.Invoke(ball);
        }
    }

    private void CheckIsMatrixBall()
    {
        if(_isMatrixBall)
            Fell();
    }
}