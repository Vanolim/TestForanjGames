using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private NameLogicScene _nameLogicScene;
    
    private Services _services;
    private Game _game;

    private void Awake()
    {
        _services = new Services(this);
        _game = new Game(_services, _nameLogicScene);
        _game.LoadScene(Scenes.Main);
        
        DontDestroyOnLoad(this);
    }
}
