using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameBoard : IUpdateble
{
    private readonly BallsMatrix _ballsMatrix;
    private readonly Slingshot _slingshot;
    private readonly FeederBalls _feederBalls;
    private readonly BallCollisionHandler _ballCollisionHandler;
    private readonly BallCollection _ballCollection;
    private BallFiller _ballFiller;
    public event Action OnBallsThrowOut;
    
    public BallCollisionHandler BallCollisionHandler => _ballCollisionHandler;
    public BallCollection BallCollection => _ballCollection;

    public GameBoard(BallsPlacesData ballsPlacesData, IBallFactory ballFactory,
        IBallsStaticDataService staticDataService, IInputService inputService)
    {
        Transform ballContainer = Object.FindObjectOfType<FeederBallsContainer>().GetTransform();
        
        _ballCollisionHandler = new BallCollisionHandler();

        _ballCollection = new BallCollection(_ballCollisionHandler);

        _ballFiller = new BallFiller(ballFactory);
        _ballCollection.AddThrowBalls(_ballFiller.GetBalls(10, ballContainer));


        BallRadiusDetector ballRadiusDetector = new BallRadiusDetector(staticDataService);

        _ballsMatrix = new BallsMatrix(ballsPlacesData, ballFactory, _ballCollection, 
            FindBallMatrixPosition(), ballRadiusDetector.GetRadius());


        _feederBalls = new FeederBalls(_ballCollection);


        _slingshot = new Slingshot(inputService);
        _slingshot.OnBallFired += delegate(Ball ball) { _ballCollisionHandler.SetBall(ball); };
    }

    public void Start()
    {
        TryGiveBallSlingshot();
    }

    private Transform FindBallMatrixPosition() => 
        GameObject.FindObjectOfType<BallMatrixTransform>().GetTransform();

    public void UpdateState(float dt)
    {
        _slingshot.UpdateState(dt);

        if (_feederBalls.IsPrepareBall == false)
            CheckAllBallsUsed();
        else
            TryGiveBallSlingshot();
    }

    private void TryGiveBallSlingshot()
    {
        if (_slingshot.IsBallNotSet && _ballCollisionHandler.IsBallNotSet)
            _slingshot.SetBall(_feederBalls.GetBall(_slingshot.TargetForNewBall));
    }

    private void CheckAllBallsUsed()
    {
        if(_slingshot.IsBallNotSet)
            OnBallsThrowOut?.Invoke();
    }

    public void AllBallsFeel()
    {
        _slingshot.DeactivateViewBand();
        _ballCollection.FellBalls();
    }
}