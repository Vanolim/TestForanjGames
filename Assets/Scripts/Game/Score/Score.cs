using System;

public class Score
{
    private readonly BallSameTypeDetectorHandler _ballSameTypeDetectorHandler;
    private int _value;

    private const int NUMBER_SCORE_FOR_ONE_BALL = 10;
    
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

    public Score(BallSameTypeDetectorHandler ballSameTypeDetectorHandler)
    {
        _ballSameTypeDetectorHandler = ballSameTypeDetectorHandler;
        _ballSameTypeDetectorHandler.OnBallsSameTypeFound += AddScore;
    }

    private void AddScore()
    {
        int countFindBallsSameType = _ballSameTypeDetectorHandler.CountFindBallsSamyType;
        Value += (int)Math.Pow(NUMBER_SCORE_FOR_ONE_BALL, countFindBallsSameType);
    }
}