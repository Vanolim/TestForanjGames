using System;
using UnityEngine;

public abstract class LogicScene : IChangingScene
{
    private readonly IHubSceneFactory _hubFactory;
    private readonly IDisposeHandler _disposeHandler;
    private IUIHandler _uiHandler;
    private Camera _camera;

    protected IHubSceneFactory HubFactory => _hubFactory;
    protected IDisposeHandler DisposeHandler => _disposeHandler;
    
    public event Action<Scenes, IChangingScene> OnSelectedNewScene;

    protected LogicScene(IHubSceneFactory hubFactory, IDisposeHandler disposeHandler)
    {
        _hubFactory = hubFactory;
        _disposeHandler = disposeHandler;
    }

    public virtual void Start()
    {
        _camera = Camera.main;
        InitUIHandler();
    }

    private void InitUIHandler()
    {
        _uiHandler = InitHub(_camera);
        _uiHandler.OnChosenNewScene += LoadNewScene;

        _disposeHandler.Register(_uiHandler);
    }

    protected abstract IUIHandler InitHub(Camera camera);

    private void LoadNewScene(Scenes newScene)
    {
        CompleteScene();
        OnSelectedNewScene?.Invoke(newScene, this);
    }
    
    protected void CompleteScene()
    {
        _uiHandler.OnChosenNewScene -= LoadNewScene;
        _disposeHandler.AllDispose();
        _disposeHandler.AllUnRegister();
    }
}