using System;
using UnityEngine;

public class GameSceneUIHandler : IUIHandler
{
    public GameSceneUIHandler(GameSceneUI gameSceneUI)
    {
        
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public event Action<Scenes> OnChosenNewScene;
}