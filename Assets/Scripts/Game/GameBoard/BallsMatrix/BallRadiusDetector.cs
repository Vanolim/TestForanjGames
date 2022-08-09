public class BallRadiusDetector
{
       private readonly IBallsStaticDataService _ballsStaticDataService;
       private const BallType BALL_TYPE = BallType.Blue;
       
       public BallRadiusDetector(IBallsStaticDataService ballStaticData)
       {
              _ballsStaticDataService = ballStaticData;
       }

       public float GetRadius() => 
              _ballsStaticDataService.ForBalls(BALL_TYPE).Ball.SpriteRenderer.size.x / 2;
}