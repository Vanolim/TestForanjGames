using UnityEngine;

public struct BorderPoints
{
    private Border _border;

    private Vector2 _topLeftPointBorder;
    private Vector2 _downLeftPointBorder;
    private Vector2 _topRightPointBorder;
    private Vector2 _downRightPointBorder;

    private float _angleTopLeftPointBorder;
    private float _angleDownLeftPointBorder;
    private float _angleTopRightPointBorder;
    private float _angleDownRightPointBorder;

    public void InitPoints(Border border)
    {
        _border = border;
        
        _topLeftPointBorder = GetPointPosition(true, false);
        _downLeftPointBorder = GetPointPosition(true, true);
        _topRightPointBorder = GetPointPosition(false, false);
        _downRightPointBorder = GetPointPosition(false, true);

        GetAnglePointsBorder();
    }

    private void GetAnglePointsBorder()
    {
        _angleTopLeftPointBorder = GetAngle(_topLeftPointBorder);
        _angleDownLeftPointBorder = GetAngle(_downLeftPointBorder);
        _angleTopRightPointBorder = GetAngle(_topRightPointBorder);
        _angleDownRightPointBorder = GetAngle(_downRightPointBorder);
    }

    private Vector2 GetPointPosition(bool isPointLeft, bool isPointDown)
    {
        BoxCollider2D borderBox = _border.Box;
        
        float x = borderBox.size.x / 2;
        float y = borderBox.size.y / 2;

        if (isPointLeft)
            x *= -1;

        if (isPointDown)
            y *= -1;

        return (new Vector2(x, y) + (Vector2)borderBox.transform.position);
    }

    private float GetAngle(Vector2 point)
    {
        Vector2 targetDir = point - (Vector2)_border.Box.transform.position;
        float angle = Mathf.Atan2(targetDir.y,  targetDir.x) * Mathf.Rad2Deg;
        if (angle < 0f) 
            angle += 360f;

        return angle;
    }

    public bool IsTargetInBorder(Vector2 targetPosition) => _border.Box.bounds.Contains(targetPosition);

    public bool IsTargetInDown(Vector2 targetPosition)
    {
        Vector2 targetDir = targetPosition - (Vector2)_border.Box.transform.position;

        float angle = Mathf.Atan2(targetDir.y,  targetDir.x) * Mathf.Rad2Deg;
        if (angle < 0f) 
            angle += 360f;

        return angle >= _angleDownLeftPointBorder && angle <= _angleDownRightPointBorder;
    }

    public Vector2 GetNormal(Vector2 target)
    {
        Vector2 targetDir = target - (Vector2)_border.Box.transform.position;

        float angle = Mathf.Atan2(targetDir.y,  targetDir.x) * Mathf.Rad2Deg;
        if (angle < 0f) 
            angle += 360f;
        
        
        if(angle >= _angleTopLeftPointBorder && angle <= _angleDownLeftPointBorder)
            return Vector2.right.normalized;
        if (angle >= _angleDownLeftPointBorder && angle <= _angleDownRightPointBorder)
            return Vector2.up.normalized;
        if (angle >=_angleTopRightPointBorder && angle <= _angleTopLeftPointBorder)
            return Vector2.down.normalized;
        
        return Vector2.left.normalized;
        
    }
}