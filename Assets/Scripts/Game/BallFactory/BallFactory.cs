using System.IO;
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
        BallStaticData enemyData = _ballStaticData.ForBalls(ballType);
        Ball ball = Object.Instantiate(enemyData.Prefab, container, false);
        return ball;
    }
}