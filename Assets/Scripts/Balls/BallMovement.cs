using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private IReadOnlyList<Vector2> _trajectoryDataPoints;
    private Vector2 _pointTo;
    private bool _isMove;
    private int _pointCount;
    private float _speed;
    private bool _movingOnDots;

    private const int SPEED_FACTOR = 50;

    public event Action OnReachedEndPoint; 

    private void Update()
    {
        if (_isMove)
        {
            Move();
            
            if (Vector2.Distance(transform.position, _pointTo) <= 0.0001f)
            {
                if (_movingOnDots)
                    TryGetTheNextPoint();
                else
                {
                    OnReachedEndPoint?.Invoke();
                    _isMove = false;
                }
            }
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _pointTo, Time.deltaTime * _speed);
    }

    private void TryGetTheNextPoint()
    {
        _pointTo = GetNexPoint(_pointCount);
        _pointCount++;
    }

    public void MoveThroughDots(IReadOnlyList<Vector2> dataTrajectoryPoints, float force)
    {
        _movingOnDots = true;
        _trajectoryDataPoints = dataTrajectoryPoints;
        _isMove = true;
        
        _speed = force * SPEED_FACTOR;
        SetStartingPoint();
    }

    private void SetStartingPoint()
    {
        _pointCount = 0;
        _pointTo = GetNexPoint(_pointCount);
        _pointCount++;
    }

    public void MoveToPoint(Vector2 target)
    {
        StopMoving();
        _speed = SPEED_FACTOR;
        _movingOnDots = false;
        _pointTo = target;
        _isMove = true;
    }

    public void StopMoving() => _isMove = false;

    private Vector2 GetNexPoint(int time)
    {
        if (time >= _trajectoryDataPoints.Count)
        {
            OnReachedEndPoint?.Invoke();
            _isMove = false;
            _movingOnDots = false;
            return Vector2.zero;
        }
        return _trajectoryDataPoints[time];
    }
}