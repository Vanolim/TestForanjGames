public class BallsPlacesData
{
    private readonly char[,] _fileData;
    private PlaceType[,] _placesData;
    private int _countRowsMatrix;
    private int _countColumnsMatrix;

    public PlaceType[,] PlacesData => _placesData;
    public int CountRowsMatrix => _countRowsMatrix;
    public int CountColumnsMatrix => _countColumnsMatrix;

    public BallsPlacesData()
    {
        TextFileParser textFileParser = new TextFileParser();
        _fileData = textFileParser.GetFileData();

        InitializeData();
    }

    private void InitializeData()
    {
        _countRowsMatrix = _fileData.GetUpperBound(0) + 1;
        _countColumnsMatrix = _fileData.Length / _countRowsMatrix;

        _placesData = new PlaceType[_countRowsMatrix, _countColumnsMatrix];
        
        for (int i = 0; i < _countRowsMatrix; i++)
        {
            for (int j = 0; j < _countColumnsMatrix; j++)
            {
                _placesData[i,j] = SetPlaceData(_fileData[i,j]);
            }
        }
    }

    private PlaceType SetPlaceData(char data)
    {
        switch (data)
        {
            case 'b':
                return PlaceType.Blue;
            case 'r':
                return PlaceType.Red;
            case 'y':
                return PlaceType.Yellow;
            case 'g':
                return PlaceType.Green;
            default:
                return PlaceType.None;
        }
    }
}