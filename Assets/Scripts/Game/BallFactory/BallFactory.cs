using UnityEngine;
using Object = UnityEngine.Object;

public class BallFactory : IBallFactory
{
    private readonly IBallsStaticDataService _ballStaticData;
    
    public BallFactory(IBallsStaticDataService ballStaticData)
    {
        _ballStaticData = ballStaticData;
    }

    public Ball CreateBall(BallType ballType, Transform container)
    {
        BallStaticData ballData = _ballStaticData.ForBalls(ballType);
        Ball ball = Object.Instantiate(ballData.Ball, container, false);
        ball.InitBallType(ballType);
        return ball;
    }
}