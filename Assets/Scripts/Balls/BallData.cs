using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "StaticData/NewBall")]
public class BallData : ScriptableObject
{
    public Balls Type;
    public Ball Prefab;
}