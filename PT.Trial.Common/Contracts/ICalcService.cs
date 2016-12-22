namespace PT.Trial.Common.Contracts
{
    public interface ICalcService
    {
        Number GetNextNumber(Number current);
        Number GetPrevNumber(Number current);
    }
}