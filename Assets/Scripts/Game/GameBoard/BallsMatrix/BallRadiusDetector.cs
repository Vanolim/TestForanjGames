public class BallRadiusDetector
{
       private readonly IStaticDataService _staticDataService;
       private const BallType BALL_TYPE_FOR_SET_VALUE = BallType.Blue;
       
       public BallRadiusDetector(IStaticDataService staticData)
       {
              _staticDataService = staticData;
       }

       public float GetRadius() =>
              _staticDataService.ForBalls(BALL_TYPE_FOR_SET_VALUE).Ball.BallView.GetRadiusSprite();
}