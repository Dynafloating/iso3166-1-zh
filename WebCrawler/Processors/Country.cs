namespace Processors;

public class Country
{
    public string Name { get; set; } = string.Empty;
    public string TwoLetterCode { get; set; } = string.Empty;
    public string ThreeLetterCode { get; set; } = string.Empty;
    public string NumericCode { get; set; } = string.Empty;
    public string TraditionalChineseName { get; set; } = string.Empty;
    public string SimplifiedChineseName { get; set; } = string.Empty;
    public bool Independent { get; set; }
}
