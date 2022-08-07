using UnityEngine;

public class BallRadiusDetector
{
       private readonly IBallsStaticDataService _ballsStaticDataService;
       private const BallType BALL_TYPE = BallType.Blue;
       
       public BallRadiusDetector(IBallsStaticDataService ballStaticData)
       {
              _ballsStaticDataService = ballStaticData;
       }

       public float GetRadius()
       {
              return _ballsStaticDataService.ForBalls(BALL_TYPE).Prefab.SpriteRenderer.size.x / 2;
       }
}