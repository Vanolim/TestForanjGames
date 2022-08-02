using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class SceneContextUI : MonoBehaviour
{
    private Canvas _canvas;
    
    public void Init(Camera camera)
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = camera;
    }
}