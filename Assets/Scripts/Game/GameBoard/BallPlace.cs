using UnityEngine;

public class BallPlace
{
    private Ball _ball;
    private Vector2 _position;

    public Ball Ball => _ball;

    public void SetBall(Ball ball)
    {
        _ball = ball;
        _ball.SetPosition(_position);
    }

    public void SetPosition(Vector2 position) => _position = position;
}