using UnityEngine;
using Object = UnityEngine.Object;

public class BallFactory : IBallFactory
{
    private readonly IStaticDataService _staticData;
    
    public BallFactory(IStaticDataService staticData)
    {
        _staticData = staticData;
    }

    public Ball CreateBall(BallType ballType, Transform container)
    {
        BallStaticData ballData = _staticData.ForBalls(ballType);
        Ball ball = Object.Instantiate(ballData.Ball, container, false);
        ball.InitBallType(ballType);
        return ball;
    }
}