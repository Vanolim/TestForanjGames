using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpringJoint2D _springJoint2D;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BallMovement _ballMovement;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private BallView _ballView;
    [SerializeField] private ParticleSystem _dead;

    private bool _isHasBreakThroughPotential = false;
    private BallType _ballType;

    public event Action OnPressShootButton;
    public event Action OnReleasedShootButton;
    public event Action<Ball> OnBurst;
    public event Action<Ball> OnCollided;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public BallView BallView => _ballView;
    public BallMovement BallMovement => _ballMovement;
    public bool IsHasBreakThroughPotential => _isHasBreakThroughPotential;
    public BallType BallType => _ballType;

    private void OnEnable()
    {
        _ballView.ShootButtonEvent.OnDown += ReportEventPress;
        _ballView.ShootButtonEvent.OnUp += ReportEventReleased;
    }

    public void InitBallType(BallType type) => _ballType = type;

    public void ActivateView() => _spriteRenderer.enabled = true;
    public void DeactivateView() => _spriteRenderer.enabled = false;
    
    public void ActivateCollider() => _collider.enabled = true;

    public void DeactivateCollider() => _collider.enabled = false;

    public void ActivateSpringJoint()
    {
        _springJoint2D.connectedAnchor = transform.position;
        _springJoint2D.enabled = true;
    }

    public void DeactivateSpringJoint() => _springJoint2D.enabled = false;
    public void ActivateRB() => _rb.isKinematic = false;
    public void DeactivateRB() => _rb.isKinematic = true;

    private void ReportEventPress() => OnPressShootButton?.Invoke();

    private void ReportEventReleased() => OnReleasedShootButton?.Invoke();

    public void Move(IReadOnlyList<Vector2> dataTrajectoryPoints, float force) => 
        _ballMovement.MoveThroughDots(dataTrajectoryPoints, force);

    public void SetBreakthroughPotential(bool value) =>
        _isHasBreakThroughPotential = value;
    

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.TryGetComponent(out Ball ball))
    //     {
    //         OnCollided?.Invoke(ball);
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.TryGetComponent(out Ball ball))
        {
            OnCollided?.Invoke(ball);
        }
    }

    public void Burst()
    {
        DeactivateView();
        DeactivateCollider();
        OnBurst?.Invoke(this);
        _dead.Play();
        StartCoroutine(ParticleWaiting());
    }

    private void OnDisable()
    {
        _ballView.ShootButtonEvent.OnDown -= ReportEventPress;
        _ballView.ShootButtonEvent.OnUp -= ReportEventReleased;
    }

    private IEnumerator ParticleWaiting()
    {
        WaitForSeconds waitTime = new WaitForSeconds(5);
        yield return waitTime;
        Destroy(gameObject);
    }

    public void SetMatrixActivate()
    {
        _ballMovement.OnReachedEndPoint -= SetMatrixActivate;
        ActivateCollider();
        ActivateView();
        ActivateSpringJoint();
        ActivateRB();
        _rb.gravityScale = 1;
    }

    public void ShotActivation()
    {
        ActivateCollider();
        ActivateRB();
        _rb.gravityScale = 0;
    }

    public void SetMatrixActivationAfterArrivingPoint()
    {
        _ballMovement.OnReachedEndPoint += SetMatrixActivate;
    }

    public void Fell()
    {
        DeactivateCollider();
        DeactivateSpringJoint();
        ActivateRB();
        _rb.gravityScale = 1;
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.TryGetComponent(out Border border))
    //         Burst();
    // }
}