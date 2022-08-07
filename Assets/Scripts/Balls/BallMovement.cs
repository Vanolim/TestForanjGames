using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private IReadOnlyList<Vector2> _trajectoryDataPoints;
    private Vector2 _pointTo;
    private bool _isMove;
    private int _pointCount;
    private float _speed;

    private const int SPEED_FACTOR = 15;

    private void Update()
    {
        if (_isMove)
        {
            Move();
        }
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, _pointTo) <= 0.0001f)
        {
            _pointTo = GetNexPoint(_pointCount);
            _pointCount++;
        }

        transform.position = Vector2.MoveTowards(transform.position, _pointTo, Time.deltaTime * _speed);
    }

    public void StartMoving(IReadOnlyList<Vector2> dataTrajectoryPoints, float force)
    {
        _trajectoryDataPoints = dataTrajectoryPoints;
        _isMove = true;
        
        _speed = force * SPEED_FACTOR;
        _pointCount = 0;
        _pointTo = GetNexPoint(_pointCount);
        _pointCount++;
    }

    private Vector2 GetNexPoint(int time)
    {
        if (time >= _trajectoryDataPoints.Count)
        {
            return _trajectoryDataPoints[^1];
        }
        return _trajectoryDataPoints[time];
    }
}