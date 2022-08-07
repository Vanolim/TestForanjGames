using UnityEngine;

public class GameScene : LogicScene
{
    private GameSceneUIHandler _UIHandler;
    private GameBoard _gameBoard;
    private IGameSceneUpdate _gameSceneUpdate;
    private IBallFactory _ballFactory;
    private IInputService _inputService;
    
    public GameScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler) : base(hubFactory,
        disposeHandler)
    {

    }

    public override void Start()
    {
        base.Start();
        FindGameSceneUpdate();
        
        BallsPlacesData ballsPlacesData = new BallsPlacesData();
        
        IBallsStaticDataService ballsStaticDataService = new BallsStaticDataService();
        ballsStaticDataService.LoadBalls();

        _inputService = new InputService();
        _inputService.Init();

        _ballFactory = new BallFactory(ballsStaticDataService);
        _gameBoard = new GameBoard(ballsPlacesData, _ballFactory, ballsStaticDataService, _inputService);
        
        _gameSceneUpdate.Register(_gameBoard);
        _gameSceneUpdate.Register(_inputService);
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

    private void FindGameSceneUpdate()
    {
        _gameSceneUpdate = GameObject.FindObjectOfType<GameSceneUpdate>();
    }
}