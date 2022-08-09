using System;

public class Score
{
    private int _value;

    private const int NUMBER_SCORE_FOR_SAME_TYPE_COLIDER = 300;
    private const int NUMBER_SCORE_FOR_BURST_ANOTHER_BALL = 200;

    public event Action<int> OnScoreIncreased;

    public int Value
    {
        get => _value;
        private set
        {
            _value = value;
            OnScoreIncreased?.Invoke(_value);
        }
    }

    public Score(BallCollisionHandler ballCollisionHandler)
    {
        ballCollisionHandler.OnBallSameTypeCollided += AddScoreForSameTypeCollider;
        ballCollisionHandler.OnBallBurstAnotherBall += AddScoreForBurstAnotherBall;
    }

    private void AddScoreForSameTypeCollider() => Value += NUMBER_SCORE_FOR_SAME_TYPE_COLIDER;
    private void AddScoreForBurstAnotherBall() => Value += NUMBER_SCORE_FOR_BURST_ANOTHER_BALL;
}