using System;
using UnityEngine;

public class AboutGameUIHandler : IUIHandler
{
    private readonly AboutGameSceneUI _aboutGameSceneUI;

    public event Action<Scenes> OnChosenNewScene;

    public AboutGameUIHandler(AboutGameSceneUI aboutGameUI)
    {
        _aboutGameSceneUI = aboutGameUI;

        _aboutGameSceneUI.OnChosenMain += TriggerMainScene;
        _aboutGameSceneUI.OnChosenLink += OpenLinkVK;
    }

    private void TriggerMainScene() => OnChosenNewScene.Invoke(Scenes.Main);

    private void OpenLinkVK() => Application.OpenURL(LinkVK.LINK);

    public void Dispose()
    {
        _aboutGameSceneUI.OnChosenMain -= TriggerMainScene;
        _aboutGameSceneUI.OnChosenLink -= OpenLinkVK;
    }
}