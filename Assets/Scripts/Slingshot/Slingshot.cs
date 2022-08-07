using System;
using UnityEngine;

public class Slingshot : IUpdateble
{
    private SlingshotView _view;
    private readonly ShotTrajectory _shotTrajectory;
    private readonly IInputService _inputService;
    private Ball _ball;
    private bool _isBallActive;
    private bool _isReachedMaximumStretch = false;
    private float _tensionForce;

    private const float TENSION_FORCE_FACTOR = 0.5f;
    private const float BALL_VIEW_SPEED = 10f;
    private const float ADDED_VALUE_LIMIT_BALL_ZONE = 0.5f;
    public Slingshot(IInputService inputService)
    {
        _inputService = inputService;
        InitView();
        
        _shotTrajectory = new ShotTrajectory(_view.ShotView, _view.Border);
    }

    private void InitView()
    {
        _view = FindView();
        _view.Band.Init();
        _view.Band.Activate();
    }

    private SlingshotView FindView() => GameObject.FindObjectOfType<SlingshotView>();

    public void SetBall(Ball ball)
    {
        _ball = ball;

        _ball.OnPressShootButton += SetBallActive;
        _ball.OnReleasedShootButton += Shoot;
    }

    public void UpdateState(float dt)
    {
        if (_ball != null)
        {
            _shotTrajectory.HideView();
            _shotTrajectory.HideAdditionalTrajectoryView();
            if (_isBallActive)
            {
                _shotTrajectory.SeemView();

                SetMainShotTrajectory();

                float distanceStretch = GetDistanceStretch();
                AddTensionForce(distanceStretch);
                
                MoveViewBall(distanceStretch, dt);

                if (_isReachedMaximumStretch)
                {
                    _shotTrajectory.SeemAdditionalTrajectoryView();
                    ActivateAdditionalTrajectoryView();
                }
            }
            
            _view.Band.SetPosition(_ball.gameObject.transform.position);
        }
    }

    private void MoveViewBall(float distanceStretch, float dt)
    {
        Vector2 nextPosition = new Vector2(_inputService.WorldPosition.x, _inputService.WorldPosition.y);
        float ballZoneRadius = _view.BallZone.radius;

        if (distanceStretch >= ballZoneRadius)
        {
            _isReachedMaximumStretch = true;
            float limitZoneRadius = ballZoneRadius + ADDED_VALUE_LIMIT_BALL_ZONE;
            float limitationFactor = Mathf.InverseLerp(limitZoneRadius, ballZoneRadius, distanceStretch);

            if (IsNextPointOutsideLimitZone(nextPosition, limitZoneRadius))
                return;

            MoveBall(dt, nextPosition, limitationFactor);
        }
        else
        {
            _isReachedMaximumStretch = false;
            MoveBall(dt, nextPosition);
        }
    }

    private float GetDistanceStretch() => 
        Vector2.Distance(_view.Hook.position, _ball.gameObject.transform.position);

    private void MoveBall(float dt, Vector2 nextPosition, float limitation = 1f) => 
        _ball.transform.position = Vector3.Lerp(_ball.transform.position, nextPosition, dt * BALL_VIEW_SPEED * limitation);

    private void SetBallActive() => _isBallActive = true;

    private bool IsNextPointOutsideLimitZone(Vector2 nextPoint, float limitZoneRadius)
        => Vector2.Distance(_view.Hook.position, nextPoint) >= limitZoneRadius;

    private void AddTensionForce(float distanceStretch) => 
        _tensionForce = distanceStretch * TENSION_FORCE_FACTOR;

    private void ActivateAdditionalTrajectoryView()
    {
        SetAdditionalShotTrajectory(15);
    }

    private void SetAdditionalShotTrajectory(float angle)
    {
        Vector2 ballDirection = (Vector2)_ball.transform.position - _view.Hook.position;

        Vector2 directionLeft = new Vector2(ballDirection.x * Mathf.Cos(angle) - ballDirection.y * Mathf.Sin(angle),
            ballDirection.x * Mathf.Sin(angle) + ballDirection.y * Mathf.Cos(angle));
        
        Vector2 directionRight = new Vector2(ballDirection.x * Mathf.Cos(-angle) - ballDirection.y * Mathf.Sin(-angle),
            ballDirection.x * Mathf.Sin(-angle) + ballDirection.y * Mathf.Cos(-angle));


        _shotTrajectory.SetAdditionalTrajectory(_ball.transform.position, directionLeft, directionRight,  _tensionForce);
    }

    private Vector2 RotateVector(Vector2 direction, float angle)
    {
        const float PI = 3.141592f;

        float dirAngle = Mathf.Atan2(direction.y, direction.x);

        dirAngle *= 180 / PI;

        float newAngle = (dirAngle + angle) * PI / 180;

        Vector2 newDir = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle) );

        return newDir;
    }

    private void SetMainShotTrajectory()
    {
        Vector2 ballDirection = _view.Hook.position - (Vector2)_ball.transform.position;
        _shotTrajectory.SetTrajectory(_ball.transform.position, ballDirection, _tensionForce);
    }

    private void Shoot()
    {
        _ball.Move(_shotTrajectory.Trajectory, _tensionForce);
        Restart();
    }

    private void Restart()
    {
        _view.ShotView.HideMain();
        _view.ShotView.HideAdditional();
        _view.Band.Deactivate();
        ResetTensionForce();
        _isBallActive = false;

        ResetBall();
    }

    private void ResetTensionForce() => _tensionForce = 0;

    private void ResetBall()
    {
        _ball.OnPressShootButton += SetBallActive;
        _ball.OnReleasedShootButton += Shoot;
        _ball = null;
    }
}
