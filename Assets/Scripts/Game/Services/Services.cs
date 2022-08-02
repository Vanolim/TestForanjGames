public class Services
{
    public ICoroutineRunner CoroutineRunner { get; }
    public ISceneLoader SceneLoader { get; }
    public HubSceneFactory HubSceneFactory { get; }
    public IDisposeHandler DisposeHandler { get; }
    public IAssetsProvider AssetsProvider { get; }

    public Services(ICoroutineRunner coroutineRunner)
    {
        CoroutineRunner = coroutineRunner;
        SceneLoader = new SceneLoader(CoroutineRunner);
        AssetsProvider = new AssetsProvider();
        HubSceneFactory = new HubSceneFactory(AssetsProvider);
        DisposeHandler = new DisposeHandler();
    }
}