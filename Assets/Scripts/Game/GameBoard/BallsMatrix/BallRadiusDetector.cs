public class BallRadiusDetector
{
       private readonly IBallsStaticDataService _ballsStaticDataService;
       private const BallType BALL_TYPE_FOR_SET_VALUE = BallType.Blue;
       
       public BallRadiusDetector(IBallsStaticDataService ballStaticData)
       {
              _ballsStaticDataService = ballStaticData;
       }

       public float GetRadius() =>
              _ballsStaticDataService.ForBalls(BALL_TYPE_FOR_SET_VALUE).Ball.BallView.GetRadiusSprite();
}