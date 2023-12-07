namespace WMJ.ScaleModelLibrary.FractionSystem;

/// <summary>
/// Model to represent a Fraction including an Improper Fraction.  Status is used to indicate the error state of the fraction - typically divide by zero or integer overflow.
/// </summary>
public class FractionModel
{
    public int Units { get; set; } = 0;
    public int Numerator { get; set; } = 0;
    public int Denominator { get; set; } = 0;
    public string Status { get; set; } = string.Empty;
}