using System;

public class GameSceneUIHandler : IUIHandler
{
    public GameSceneUI GameSceneUI { get; private set; }
    
    public event Action<Scenes> OnChosenNewScene;

    public GameSceneUIHandler(GameSceneUI gameSceneUI)
    {
        GameSceneUI = gameSceneUI;

        GameSceneUI.OnChosenMain += TriggerMainScene;
    }

    public void Dispose()
    {
        GameSceneUI.OnChosenMain -= TriggerMainScene;
    }
    
    private void TriggerMainScene() => OnChosenNewScene?.Invoke(Scenes.Main);
}