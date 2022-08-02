using UnityEngine;

public class NameLogicScene : MonoBehaviour
{
    [SerializeField] private string _mainScene;
    [SerializeField] private string _gameScene;
    [SerializeField] private string _aboutGameScene;

    public string MainScene => _mainScene;
    public string GameScene => _gameScene;
    public string AboutGameScene => _aboutGameScene;
}