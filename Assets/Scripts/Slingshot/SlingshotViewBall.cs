using UnityEngine;

public class SlingshotViewBall
{
    private readonly float _ballZoneRadius;
    private readonly Vector2 _ballZoneCenter;
    private readonly IInputService _inputService;
    private Ball _viewBall;

    public bool IsBallOutsideZone { get; private set; }
    public Vector2 GetBallPosition => _viewBall.transform.position;
    public Vector2 BallZoneCenter => _ballZoneCenter;

    public SlingshotViewBall(IInputService inputService, float ballZoneRadius, Vector2 ballZoneCenter)
    {
        _inputService = inputService;
        _ballZoneCenter = ballZoneCenter;
        _ballZoneRadius = ballZoneRadius;
    }
    
    public Vector2 GetBallDirection() => _ballZoneCenter - (Vector2)_viewBall.transform.position;

    public void SetBall(Ball ball) => _viewBall = ball;
    
    public void MoveViewBall(float dt)
    {
        Vector2 nextMovePosition = new Vector2(_inputService.WorldPosition.x, _inputService.WorldPosition.y);
        float distanceBall = Vector2.Distance(_viewBall.transform.position, _ballZoneCenter);
        
        if (distanceBall >= _ballZoneRadius)
        {
            IsBallOutsideZone = true;
            float addedLimitBallZone = 0.5f;
            float limitZoneRadius = _ballZoneRadius + addedLimitBallZone;
            float limitationFactor = Mathf.InverseLerp(limitZoneRadius, _ballZoneRadius, distanceBall);
            
            if (IsNextPointOutsideLimitZone(nextMovePosition, limitZoneRadius) == false)
                MoveBall(dt, nextMovePosition, limitationFactor);
        }
        else
        {
            IsBallOutsideZone = false;
            MoveBall(dt, nextMovePosition);
        }
    }

    public Vector2 GetBallDirectionAngle(float angle)
    {
        Vector2 direction;
          
        Vector3 newRotation = new Vector3(0, 0, _viewBall.transform.eulerAngles.z + angle);
        _viewBall.transform.eulerAngles = newRotation;
        direction =  _viewBall.transform.right;

        ResetBallRotation();
        return direction;
    }

    private void ResetBallRotation()
    {
        Vector2 ballDirection = GetBallDirection();
        float angle1 = Mathf.Atan2(ballDirection.y, ballDirection.x);
        _viewBall.transform.rotation = Quaternion.Euler(0f, 0f, angle1 * Mathf.Rad2Deg);
    }

    private void MoveBall(float dt, Vector2 nextPosition, float limitation = 1f)
    {
        float ballViewSpeed = 10f;
        _viewBall.transform.position =
            Vector3.Lerp(_viewBall.transform.position, nextPosition, dt * ballViewSpeed * limitation);
    }
    
    private bool IsNextPointOutsideLimitZone(Vector2 nextPoint, float limitZoneRadius)
        => Vector2.Distance(_ballZoneCenter, nextPoint) >= limitZoneRadius;
}