using System;
using UnityEngine;

public class AboutGameScene : LogicScene
{
    private AboutGameUIHandler _UIHandler;

    public AboutGameScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler) : base(hubFactory, disposeHandler)
    {
    }

    protected override IUIHandler InitHub(Camera camera)
    {
        _UIHandler = new AboutGameUIHandler(CreateHub(camera));
        return _UIHandler;
    }
    
    private AboutGameSceneUI CreateHub(Camera camera)
    {
        AboutGameSceneUI ui = HubFactory.CreateAboutGameHub().GetComponent<AboutGameSceneUI>();
        ui.Init(camera);
        return ui;
    }
}