using UnityEngine;

public interface IBallFactory
{
    public Ball CreateBall(BallType ballType, Transform container);
}