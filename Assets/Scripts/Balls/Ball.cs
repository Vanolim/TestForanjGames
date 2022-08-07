using System;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpringJoint2D _springJoint2D;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ShootButtonEvent _shootButtonEvent;
    [SerializeField] private BallMovement _ballMovement;

    public event Action OnPressShootButton;
    public event Action OnReleasedShootButton;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Rigidbody2D RB => _rb;
    

    private void OnEnable()
    {
        _shootButtonEvent.OnDown += ReportEventPress;
        _shootButtonEvent.OnUp += ReportEventReleased;
    }

    public void SetPosition(Vector2 position) 
        => transform.localPosition = position;

    public void ActivateSpringJoint() => _springJoint2D.enabled = true;
    public void DeactivateSpringJoint() => _springJoint2D.enabled = false;
    public void ActivateRB() => _rb.isKinematic = false;
    public void DeactivateRB() => _rb.isKinematic = true;

    private void ReportEventPress() => OnPressShootButton?.Invoke();

    private void ReportEventReleased() => OnReleasedShootButton?.Invoke();

    private void OnDisable()
    {
        _shootButtonEvent.OnDown -= ReportEventPress;
        _shootButtonEvent.OnUp -= ReportEventReleased;
    }

    public void Move(IReadOnlyList<Vector2> dataTrajectoryPoints, float force) => 
        _ballMovement.StartMoving(dataTrajectoryPoints, force);
}