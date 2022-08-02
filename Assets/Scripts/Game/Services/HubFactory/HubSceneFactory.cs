using UnityEngine;

public class HubSceneFactory : IHubSceneFactory
{
     private readonly IAssetsProvider _assetsProvider;
     
     public HubSceneFactory(IAssetsProvider assetsProvider)
     {
          _assetsProvider = assetsProvider;
     }
     
     public GameObject CreateMainHub() => _assetsProvider.Instantiate(AssetPath.MainHubPath);
     public GameObject CreateGameHub() => _assetsProvider.Instantiate(AssetPath.GameHubPath);
     public GameObject CreateAboutGameHub() => _assetsProvider.Instantiate(AssetPath.AboutGamePath);
}