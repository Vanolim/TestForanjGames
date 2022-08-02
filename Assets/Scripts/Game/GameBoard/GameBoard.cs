using UnityEngine;

public class GameBoard
{
    private BallsMatrix _ballsMatrix;

    public GameBoard(BallsPlacesData ballsPlacesData, IBallFactory ballFactory, IBallsStaticDataService staticDataService)
    {
        IBallRadiusDetector ballRadiusDetector = new BallRadiusDetector(staticDataService);
        _ballsMatrix = new BallsMatrix(ballsPlacesData, ballFactory, FindBallMatrixPosition(), ballRadiusDetector.GetRadius());
    }

    private Transform FindBallMatrixPosition() => 
        GameObject.FindObjectOfType<BallMatrixTransform>().GetTransform();
}