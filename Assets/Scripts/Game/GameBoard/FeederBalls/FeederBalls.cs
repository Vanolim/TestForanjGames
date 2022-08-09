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
        {
            ball = _ballCollection.GetThrowBall();
        }

        TryPrepareNextBall();

        PrepareBallForGet(target, ball);
        return ball;
    }

    private void PrepareNextBallForSet()
    {
        _prepareBall = _ballCollection.GetThrowBall();
        _prepareBall.BallView.ActivateSpriteRenderer();
        SetNumberRemainingBalls(_prepareBall);
    }

    private void PrepareBallForGet(Vector2 target, Ball ball)
    {
        ball.BallView.DisableViewText();
        ball.BallView.ActivateSpriteRenderer();
        ball.transform.parent = null;
        MoveBallToTarget(ball, target);
    }

    private void MoveBallToTarget(Ball ball, Vector2 target) => ball.transform.position = target;

    private void SetNumberRemainingBalls(Ball ball)
    {
        ball.BallView.SetValueTest((_ballCollection.CurrentCountBallsThrow + 1).ToString());
        ball.BallView.EnableViewText();
    }

    private void TryPrepareNextBall()
    {
        if (_ballCollection.CurrentCountBallsThrow != 0) 
            PrepareNextBallForSet();
    }
}