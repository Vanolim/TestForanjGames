public interface IStaticDataService
{
    public void LoadBalls();
    public void LoadTextData();
    public BallStaticData ForBalls(BallType typeId);
    public LoadTextData ForLoadTextData();
}