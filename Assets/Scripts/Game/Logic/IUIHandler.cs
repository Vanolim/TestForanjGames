using System;

public interface IUIHandler : IDisposable
{
    public event Action<Scenes> OnChosenNewScene;
}