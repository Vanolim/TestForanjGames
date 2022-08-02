using UnityEngine;
using Application = UnityEngine.Application;

public class Game
{
    private readonly Services _services;
    private readonly NameLogicScene _nameLogicScene;
    
    private MainScene _mainScene;
    private GameScene _gameScene;
    private AboutGameScene _aboutGameScene;
    
    public Game(Services services, NameLogicScene nameLogicScene)
    {
        _services = services;
        _nameLogicScene = nameLogicScene;
    }

    public void LoadScene(Scenes scene, IChangingScene initialScene = null)
    {
        if (initialScene != null)
            UnsubscribeSceneChange(initialScene);
        
        switch (scene)
        {
            case Scenes.Main:
                LoadMainScene();
                break;
            case Scenes.Game:
                LoadGameScene();
                break;
            case Scenes.AboutGame:
                LoadAboutGameScene();
                break;
        }
    }

    private void LoadMainScene()
    {
        _mainScene = new MainScene(_services.HubSceneFactory, _services.DisposeHandler);
        SubscribeSceneChange(_mainScene);
        _mainScene.OnSelectedExitApplication += ExitGame;
        
        _services.SceneLoader.Load(_nameLogicScene.MainScene, _mainScene.Start);
    }

    private void LoadGameScene()
    {
        _gameScene = new GameScene(_services.HubSceneFactory, _services.DisposeHandler);
        SubscribeSceneChange(_gameScene);
        
        _services.SceneLoader.Load(_nameLogicScene.GameScene, _gameScene.Start);
    }

    private void LoadAboutGameScene()
    {
        _aboutGameScene = new AboutGameScene(_services.HubSceneFactory, _services.DisposeHandler);
        SubscribeSceneChange(_aboutGameScene);
        
        _services.SceneLoader.Load(_nameLogicScene.AboutGameScene, _aboutGameScene.Start);
    }

    private void SubscribeSceneChange(IChangingScene changingScene) => changingScene.OnSelectedNewScene += LoadScene;
    private void UnsubscribeSceneChange(IChangingScene changingScene) => changingScene.OnSelectedNewScene -= LoadScene;

    private void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}