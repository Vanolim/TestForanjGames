using UnityEngine;

public class InputService : IInputService, IUpdateble
{
    private Camera _camera;
    
    public Vector2 WorldPosition { get; private set; }

    public void Init() => _camera = Camera.main;

    public void UpdateState(float dt) => GetWorldPosition();

    private void GetWorldPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            WorldPosition = _camera.ScreenToWorldPoint(touch.position);
        }
    }
}