namespace Savanna.SavannaRandomNumbers
{
    /// <summary>
    /// Interface for getting random numbers further used for faking random numbers for UnitTesting
    /// </summary>
    public interface ISavannaRandomNumbers
    {
        int GetRandomNumber(int range);

        int GetRandomNumber(int from, int to);
    }
}