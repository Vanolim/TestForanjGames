using System;

public interface ISceneLoader
{
      public void Load(string nextScene, Action onLoaded = null);
}