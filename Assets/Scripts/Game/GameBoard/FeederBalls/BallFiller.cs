using System.Collections.Generic;
using UnityEngine;

public class BallFiller
{
    private readonly IBallFactory _ballFactory;
    
    public BallFiller(IBallFactory ballFactory)
    {
        _ballFactory = ballFactory;
    }

    public List<Ball> GetBalls(int countBall, Transform container)
    {
        List<Ball> balls = new List<Ball>();

        for (int i = 0; i < countBall; i++)
        {
            balls.Add(_ballFactory.CreateBall(GetRandomBallType(), container));
        }

        return balls;
    }

    private BallType GetRandomBallType() => 
        EnumExtensions.GetRandomEnumValue<BallType>();
}