using System;

public interface IChangingScene
{
    public event Action<Scenes, IChangingScene> OnSelectedNewScene;       
}