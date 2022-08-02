using System;
using System.Collections.Generic;

public class DisposeHandler : IDisposeHandler
{
    private readonly List<IDisposable> _disposables = new List<IDisposable>();
    
    public void Register(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

    public void UnRegister(IDisposable disposable)
    {
        _disposables.Remove(disposable);
    }

    public void AllUnRegister()
    {
        for (int i = 0; i < _disposables.Count; i++)
        {
            _disposables.Remove(_disposables[i]);
        }
    }

    public void Dispose(IDisposable disposable)
    {
        if (_disposables.Contains(disposable) == false)
            throw new InvalidOperationException();
        
        disposable.Dispose();
    }

    public void AllDispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}