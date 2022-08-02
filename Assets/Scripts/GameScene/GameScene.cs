using UnityEngine;

public class GameScene : LogicScene
{
    private GameSceneUIHandler _UIHandler;
    
    public GameScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler) : base(hubFactory,
        disposeHandler)
    {

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