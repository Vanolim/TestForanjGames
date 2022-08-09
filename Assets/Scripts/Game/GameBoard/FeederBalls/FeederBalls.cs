using UnityEngine;

public class FeederBalls
{
    private readonly BallCollection _ballCollection;
    private Ball _prepareBall;

    public bool IsPrepareBall => _prepareBall;
    
    public FeederBalls(BallCollection ballCollection)
    {
        _ballCollection = ballCollection;
    }

    public Ball GetBall(Vector2 target)
    {
        Ball ball;

        if (_prepareBall)
        {
            ball = _prepareBall;
            _prepareBall = null;
        }
        else
            ball = _ballCollection.GetBallThrowBall();

        if (_ballCollection.CurrentCountBallsThrow != 0)
        {
            PrepareNextBall();
        }

        ball.BallView.DisableViewText();
        ball.ActivateView();
        ball.transform.parent = null;
        MoveBallToTarget(ball, target);
        return ball;
    }

    private void PrepareNextBall()
    {
        _prepareBall = _ballCollection.GetBallThrowBall();
        _prepareBall.ActivateView();
        SetNumberRemainingBalls(_prepareBall);
    }

    private void MoveBallToTarget(Ball ball, Vector2 target)
    {
        ball.transform.position = target;
    }

    private void SetNumberRemainingBalls(Ball ball)
    {
        ball.BallView.SetValueTest((_ballCollection.CurrentCountBallsThrow + 1).ToString());
        ball.BallView.EnableViewText();
    }
}