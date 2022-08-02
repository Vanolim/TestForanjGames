using System;
using UnityEngine;

public class MainSceneUIHandler : IUIHandler
{
    private readonly MainSceneUI _sceneContextUI;

    public event Action<Scenes> OnChosenNewScene;
    public event Action OnChosenExitApplication;

    public MainSceneUIHandler(MainSceneUI mainSceneUI)
    {
        _sceneContextUI = mainSceneUI;

        _sceneContextUI.OnChosenNewGame += TriggerNewGame;
        _sceneContextUI.OnChosenAboutGame += TriggerAboutGame;
        _sceneContextUI.OnChosenExit += TriggerExitApplication;
    }

    public void Dispose()
    {
        _sceneContextUI.OnChosenNewGame -= TriggerNewGame;
        _sceneContextUI.OnChosenAboutGame -= TriggerAboutGame;
        _sceneContextUI.OnChosenExit -= TriggerExitApplication;
    }

    private void TriggerNewGame() => OnChosenNewScene?.Invoke(Scenes.Game);
    private void TriggerAboutGame() => OnChosenNewScene?.Invoke(Scenes.AboutGame);
    private void TriggerExitApplication() => OnChosenExitApplication?.Invoke();
}