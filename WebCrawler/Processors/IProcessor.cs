namespace Processors;

public interface IProcessor
{
    Task<Country[]> ListCountryAsync();
}
