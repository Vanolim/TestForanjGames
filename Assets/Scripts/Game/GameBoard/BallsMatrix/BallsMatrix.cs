using System;
using UnityEngine;

public class BallsMatrix
{
    private readonly BallsPlacesData _ballsPlacesData;
    private readonly IBallFactory _ballFactory;
    private BallPlacePositionDetector _ballPlacePositionDetector;
    private readonly Transform _centerMatrix;
    private readonly BallCollection _ballCollection;
    private BallPlace[,] _ballsPlaces;

    private readonly float _ballRadius;

    public BallsMatrix(BallsPlacesData ballsPlacesData, IBallFactory ballFactory, BallCollection ballCollection,
        Transform centerMatrix, float ballRadius)
    {
        _ballCollection = ballCollection;
        _ballFactory = ballFactory;
        _ballsPlacesData = ballsPlacesData;
        _centerMatrix = centerMatrix;
        _ballRadius = ballRadius;

        Init(ballsPlacesData);
    }

    private void Init(BallsPlacesData ballsPlacesData)
    {
        int countRowsMatrix = ballsPlacesData.CountRowsMatrix;
        int countColumnsMatrix = ballsPlacesData.CountColumnsMatrix;

        _ballPlacePositionDetector = new BallPlacePositionDetector
            (countRowsMatrix, countColumnsMatrix, _ballRadius);

        InitMatrix(countRowsMatrix, countColumnsMatrix);
    }

    private void InitMatrix(int countRowsMatrix, int countColumnsMatrix)
    {
        _ballsPlaces = new BallPlace[countRowsMatrix, countColumnsMatrix];

        for (int i = 0; i < countRowsMatrix; i++)
        {
            for (int j = 0; j < countColumnsMatrix; j++)
            {
                PlaceType placeType = _ballsPlacesData.PlacesData[i, j];
                    
                var newBallPlace = new BallPlace();
                _ballsPlaces[i, j] = newBallPlace;

                newBallPlace.SetPosition(_ballPlacePositionDetector.GetPosition());
                
                if (BallPlaceIsNull(placeType) == false)
                {
                    Ball ball = _ballFactory.CreateBall(GetBallType(placeType), _centerMatrix);
                    newBallPlace.SetBall(ball);
                    ball.SetMatrixActivate();
                    _ballCollection.AddMatrixBall(ball, IsTopRaw(i));
                }
            }
            
            _ballPlacePositionDetector.GetToNextRaw();
        }
    }

    private bool IsTopRaw(int i) => i == 0;
    
    private bool BallPlaceIsNull(PlaceType placeType) => placeType == PlaceType.None;

    private BallType GetBallType(PlaceType placeType)
    {
        switch (placeType)
        {
            case PlaceType.Blue:
                return BallType.Blue;
            case PlaceType.Green:
                return BallType.Green;
            case PlaceType.Red:
                return BallType.Red;
            case PlaceType.Yellow:
                return BallType.Yellow;
            default:
                throw new InvalidOperationException();
        }
    }
}