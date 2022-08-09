using System;
using UnityEngine;

public class Slingshot : IUpdateble
{
    private readonly ShotTrajectory _shotTrajectory;
    private readonly SlingshotViewBall _slingshotViewBall;
    private SlingshotView _view;
    private Ball _ball;
    private bool _isReachedMaximumStretch;
    private bool _isBallActive; 
    private float _tensionForce;
    private float _divergenceAngle;
    private float _dt;

    public event Action<Ball> OnBallFired;
    public Vector2 TargetForNewBall => _view.Hook.transform.position;
    public bool IsBallNotSet => !_ball;

    public Slingshot(IInputService inputService, SlingshotView _slingshotView)
    {
        _view = _slingshotView;
        InitView();
        
        _slingshotViewBall = new SlingshotViewBall(inputService, _view.BallZone.radius, _view.Hook.position);
        _shotTrajectory = new ShotTrajectory(_view.ShotView, _view.Border, _slingshotViewBall);
    }

    private void InitView()
    {
        _view.BandView.Init();
        _view.BandView.Activate();
    }

    public void SetBall(Ball ball)
    {
        _ball = ball;
        _slingshotViewBall.SetBall(_ball);

        _ball.BallView.OnPressShootButton += SetBallActive;
        _ball.BallView.OnReleasedShootButton += Shoot;
    }

    public void UpdateState(float dt)
    {
        _dt = dt;
        if (_ball != null)
        {
            _view.BandView.Activate();
            if (_isBallActive)
            {
                _isReachedMaximumStretch = _slingshotViewBall.IsBallOutsideZone;
                DisplayViewTrajectory();

                float distanceStretch = GetDistanceStretch();
                AddTensionForce(distanceStretch);
                
                _slingshotViewBall.MoveViewBall(dt);
            }
            else
            {
                _shotTrajectory.HideView();
            }
            
            _view.BandView.SetPosition(_ball.gameObject.transform.position);
        }
        else
        {
            _view.BandView.Deactivate();
        }
    }


    private void DisplayViewTrajectory()
    {
        if (_isReachedMaximumStretch)
        {
            _shotTrajectory.HideMainTrajectoryView();
            _shotTrajectory.IncreaseDivergenceAngle(_dt);
            _shotTrajectory.SetAdditionalTrajectoryView(_tensionForce);
            _shotTrajectory.SeemAdditionalTrajectoryView();
        }
        else
        {
            _shotTrajectory.SetMainTrajectoryView(_tensionForce);
            _shotTrajectory.HideAdditionalTrajectoryView();
            _shotTrajectory.SeemMainTrajectoryView();
            _shotTrajectory.ResetIncreaseDivergenceAngle();
        }
    }

    private float GetDistanceStretch() => 
        Vector2.Distance(_view.Hook.position, _ball.gameObject.transform.position);

    private void SetBallActive() => _isBallActive = true;

    private void AddTensionForce(float distanceStretch)
    {
        float tensionForceFactor = 0.5f;
        _tensionForce = distanceStretch * tensionForceFactor;
    }

    private void Shoot()
    {
        if (_isReachedMaximumStretch)
        {
            _ball.Move(_shotTrajectory.GetRandomAngleTrajectory(_tensionForce), _tensionForce);
            _ball.SetBreakthroughPotential(true);
        }
        else
        {
            _ball.Move(_shotTrajectory.GetTrajectory(_tensionForce), _tensionForce);
            _ball.SetBreakthroughPotential(false);
        }

        _ball.ShotActivation();
        Ball ball = _ball;
        Restart();
        OnBallFired?.Invoke(ball);
    }

    private void Restart()
    {
        _view.ShotView.HideMain();
        _view.ShotView.HideAdditional();
        ResetTensionForce();
        _isBallActive = false;

        ResetBall();
    }

    private void ResetTensionForce() => _tensionForce = 0;

    private void ResetBall()
    {
        _ball.BallView.OnPressShootButton -= SetBallActive;
        _ball.BallView.OnReleasedShootButton -= Shoot;
        _ball = null;
    }
    
    public void DeactivateViewBand()
    {
        _view.BandView.gameObject.SetActive(false);
    }
}
