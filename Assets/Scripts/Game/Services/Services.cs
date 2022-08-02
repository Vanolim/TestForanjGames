public class Services
{
    public ICoroutineRunner CoroutineRunner { get; }
    public ISceneLoader SceneLoader { get; }
    public HubSceneFactory HubSceneFactory { get; }
    public IDisposeHandler DisposeHandler { get; }

    public Services(ICoroutineRunner coroutineRunner)
    {
        CoroutineRunner = coroutineRunner;
        SceneLoader = new SceneLoader(CoroutineRunner);
        HubSceneFactory = new HubSceneFactory(new AssetsProvider());
        DisposeHandler = new DisposeHandler();
    }
}