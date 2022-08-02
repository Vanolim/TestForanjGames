using UnityEngine;

[CreateAssetMenu(fileName = "BallData", menuName = "StaticData/NewBall")]
public class BallStaticData : ScriptableObject
{
    public BallType Type;
    public Ball Prefab;
}