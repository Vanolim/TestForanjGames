using System;
using UnityEngine;

public class MainScene : LogicScene
{
    private MainSceneUIHandler _UIHandler;
    
    public event Action OnSelectedExitApplication;

    public MainScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler) : base(hubFactory,
        disposeHandler)
    {

    }

    protected override IUIHandler InitHub(Camera camera)
    {
        _UIHandler = new MainSceneUIHandler(CreateHub(camera));
        _UIHandler.OnChosenExitApplication += CompleteApplication;
        return _UIHandler;
    }

    private MainSceneUI CreateHub(Camera camera)
    {
        MainSceneUI ui = HubFactory.CreateMainHub().GetComponent<MainSceneUI>();
        ui.Init(camera);
        return ui;
    }

    private void CompleteApplication()
    {
        CompleteScene();
        OnSelectedExitApplication?.Invoke();
    }
}