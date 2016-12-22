namespace PT.Trial.Common
{
    public interface ICalcService
    {
        Number GetNextNumber(Number current);
        Number GetPrevNumber(Number current);
    }
}