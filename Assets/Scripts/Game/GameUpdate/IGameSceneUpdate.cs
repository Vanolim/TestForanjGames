public interface IGameSceneUpdate
{
    public void Register(IUpdateble updateble);
    public void UnRegister(IUpdateble updateble);
}