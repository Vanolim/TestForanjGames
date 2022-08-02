public interface IDisposeHandler
{
    public void Register(IDisposable disposable);
    public void UnRegister(IDisposable disposable);
    public void AllUnRegister();
    public void Dispose(IDisposable disposable);
    public void AllDispose();
}