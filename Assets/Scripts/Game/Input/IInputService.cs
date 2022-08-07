using UnityEngine;

public interface IInputService : IUpdateble
{
       public void Init();
       public Vector2 WorldPosition { get; }
}