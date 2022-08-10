using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameBoard : IUpdateble, IDisposable
{
    private readonly BallsMatrix _ballsMatrix;
    private readonly Slingshot _slingshot;
    private readonly FeederBalls _feederBalls;
    private readonly BallCollisionHandler _ballCollisionHandler;
    private readonly BallCollection _ballCollection;
    private BallFiller _ballFiller;
    public BallSameTypeDetectorHandler BallSameTypeDetectorHandler { get; }

    private const int COUNT_THROW_BALLS = 20;
    public event Action OnBallsThrowOut;
    
    public BallCollection BallCollection => _ballCollection;

    public GameBoard(BallsPlacesData ballsPlacesData, IBallFactory ballFactory,
        IStaticDataService staticDataService, IInputService inputService)
    {
        Transform ballContainer = FindBallContainer();

        _ballCollisionHandler = new BallCollisionHandler();

        _ballCollection = new BallCollection(_ballCollisionHandler);

        _ballFiller = new BallFiller(ballFactory);
        _ballCollection.SetThrowBalls(_ballFiller.GetBalls(COUNT_THROW_BALLS, ballContainer));

        _ballsMatrix = new BallsMatrix(ballsPlacesData, ballFactory, _ballCollection, 
            FindBallMatrixPosition(), new BallRadiusDetector(staticDataService).GetRadius());

        _feederBalls = new FeederBalls(_ballCollection);
        
        _slingshot = new Slingshot(inputService, FindSlingshotView());
        _slingshot.OnBallFired += SetShotBallCollisionHandler;

        BallSameTypeDetectorHandler = new BallSameTypeDetectorHandler(_ballCollisionHandler);
    }

    private Transform FindBallContainer() => Object.FindObjectOfType<FeederBallsContainer>().GetTransform();
    private SlingshotView FindSlingshotView() => Object.FindObjectOfType<SlingshotView>();

    public void Start() => TryGiveBallSlingshot();

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

    private void SetShotBallCollisionHandler(Ball ball) => _ballCollisionHandler.SetBall(ball);

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

    public void Dispose()
    {
        _ballCollection.Dispose();
        _slingshot.OnBallFired -= SetShotBallCollisionHandler;
        BallSameTypeDetectorHandler.Dispose();
    }
}