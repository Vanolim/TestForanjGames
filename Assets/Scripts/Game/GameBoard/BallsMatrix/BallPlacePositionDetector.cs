using System;
using UnityEngine;

public class BallPlacePositionDetector
{
      private readonly float _countColumnsMatrix;
      private readonly float _countRowsMatrix;
      private readonly float _ballRadius;
      private float _x;
      private float _y;
      
      public BallPlacePositionDetector(float countColumnsMatrix, float countRowsMatrix, float ballRadius)
      {
            _countColumnsMatrix = countColumnsMatrix;
            _countRowsMatrix = countRowsMatrix;
            _ballRadius = ballRadius;

            InitPosition();
      }

      private void InitPosition()
      {
            SetXToStartPosition();
            _y = ((Convert.ToSingle(_countRowsMatrix) * (_ballRadius * 2) / 2) - _ballRadius);
      }

      public void GetToNextRaw()
      {
            SetXToStartPosition();
            _y -= _ballRadius * 2;
      }

      private void SetXToStartPosition() => 
            _x = -1 * ((Convert.ToSingle(_countColumnsMatrix) * (_ballRadius * 2) / 2) - _ballRadius);

      public Vector2 GetPosition()
      {
            var position = new Vector2(_x, _y);
            _x += (_ballRadius * 2);
            return position;
      }
}