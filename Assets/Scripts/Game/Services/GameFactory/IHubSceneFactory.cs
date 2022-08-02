using UnityEngine;

public interface IHubSceneFactory
{
    public GameObject CreateMainHub();
    public GameObject CreateGameHub();
    public GameObject CreateAboutGameHub();
}