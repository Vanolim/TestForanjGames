using System.Collections.Generic;
using UnityEngine;

public class ParabolicTrajectory
{
      private List<Vector2> _trajectoryDataPoints;
      private bool _isChangeTrajectory;
      private BorderPoints _borderPoints;

      private int _numberLastPointConsideredLineLenght;
      private float _lineLength;
      private const float STEP = 0.001f;

      public ParabolicTrajectory(Border border)
      {
            _borderPoints = new BorderPoints();
            _borderPoints.InitPoints(border);
      }

      public List<Vector2> GetTrajectory(Vector2 initialPoint, Vector2 direction, float force, float maxLenght = 0)
      {
            _trajectoryDataPoints = new List<Vector2>();
            var pointPosition = initialPoint;
            _numberLastPointConsideredLineLenght = 0;
            _lineLength = 0;
            
            while (true)
            {
                  float time = (_trajectoryDataPoints.Count - 1) * STEP;
                  
                  if (_borderPoints.IsTargetInBorder(pointPosition) == false)
                  {
                        if(_borderPoints.IsTargetInDown(pointPosition))
                              break;
                        
                        direction = ChangeDirectionReflection(direction);
                        initialPoint = _trajectoryDataPoints[^2];
                  }
                  
                  pointPosition = initialPoint + (direction.normalized * force * time) + 0.5f * Physics2D.gravity * (time * time) / 2f;

                  if (maxLenght != 0)
                  {
                        IncreaseTrajectoryLineLength(pointPosition, initialPoint);

                        if (_lineLength >= maxLenght && _numberLastPointConsideredLineLenght == 0)
                        {
                              _numberLastPointConsideredLineLenght = _trajectoryDataPoints.Count;
                              return _trajectoryDataPoints;
                        }
                  }
                  
                  initialPoint = pointPosition;
                  _trajectoryDataPoints.Add(pointPosition);
            }
            return _trajectoryDataPoints;
      }

      private Vector2 ChangeDirectionReflection(Vector2 currentDirection) => 
            Vector2.Reflect(currentDirection, _borderPoints.GetNormal(_trajectoryDataPoints[^2]));

      private void IncreaseTrajectoryLineLength(Vector2 pointTo, Vector2 initialPoint) =>
            _lineLength += Vector2.Distance(pointTo, initialPoint);
}