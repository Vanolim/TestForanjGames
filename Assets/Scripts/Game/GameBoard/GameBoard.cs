using UnityEngine;

public class GameBoard : IUpdateble
{
    private BallsMatrix _ballsMatrix;
    private readonly Slingshot _slingshot;

    public GameBoard(BallsPlacesData ballsPlacesData, IBallFactory ballFactory, 
        IBallsStaticDataService staticDataService, IInputService inputService)
    {
        BallRadiusDetector ballRadiusDetector = new BallRadiusDetector(staticDataService);
        _ballsMatrix = new BallsMatrix(ballsPlacesData, ballFactory, FindBallMatrixPosition(), ballRadiusDetector.GetRadius());


        Ball ball = GameObject.FindGameObjectWithTag("Test").GetComponent<Ball>();

        _slingshot = new Slingshot(inputService);
        _slingshot.SetBall(ball);
    }

    private Transform FindBallMatrixPosition() => 
        GameObject.FindObjectOfType<BallMatrixTransform>().GetTransform();

    public void UpdateState(float dt)
    {
        _slingshot.UpdateState(dt);
    }
}