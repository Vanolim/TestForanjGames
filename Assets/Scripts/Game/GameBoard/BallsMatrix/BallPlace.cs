using UnityEngine;

public class BallPlace
{
    private Ball _ball;
    private Vector2 _position;

    public void SetBall(Ball ball)
    {
        _ball = ball;
        _ball.transform.localPosition = _position;
    }

    public void SetPosition(Vector2 position) => _position = position;
}