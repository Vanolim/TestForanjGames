using UnityEngine;

public class GameScene : LogicScene
{
    private GameSceneUIHandler _UIHandler;
    private GameBoard _gameBoard;
    private IBallFactory _ballFactory;
    
    public GameScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler) : base(hubFactory,
        disposeHandler)
    {

    }

    public override void Start()
    {
        base.Start();
        BallsPlacesData ballsPlacesData = new BallsPlacesData();
        
        IBallsStaticDataService ballsStaticDataService = new BallsStaticDataService();
        ballsStaticDataService.LoadBalls();

        _ballFactory = new BallFactory(ballsStaticDataService);
        _gameBoard = new GameBoard(ballsPlacesData, _ballFactory, ballsStaticDataService);
    }

    protected override IUIHandler InitHub(Camera camera)
    {
        _UIHandler = new GameSceneUIHandler(CreateHub(camera));
        return _UIHandler;
    }

    private GameSceneUI CreateHub(Camera camera)
    {
        GameSceneUI ui = HubFactory.CreateGameHub().GetComponent<GameSceneUI>();
        ui.Init(camera);
        return ui;
    }
}