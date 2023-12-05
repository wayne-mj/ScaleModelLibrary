namespace WMJ.ScaleModelLibrary.MetricSystem;

/// <summary>
/// Represents the fractions of an inch
/// </summary>
public enum InchFractions
{
    One = 1,
    OneHalf = 2,
    OneFourth = 4,
    OneEighth = 8,
    OneSixteenth = 16,
    OneThirtySecond = 32,
    OneSixtyFourth = 64
}

/// <summary>
/// Represents the metric system
/// </summary>
public enum Metrics
{
    Feet, Inches, Metres, Centimetres, Millimetres
}

/// <summary>
/// Model for Imperial measurements (feet, inches, fractions)
/// </summary>
public class ImperialMeasurementModel
{
    public int Id { get; set; } = 0;
    public int Feet { get; set; } = 0;   
    public int Inches { get; set; } = 0;
    public int Numerator { get; set; } = 0;     // 0 in this case means no fraction
    public int Denominator { get; set; } = 0;   // 0 in this case means no fraction
}

public class MetricMeasurementModel
{
    public int Id { get; set; } = 0;
    public double Metres { get; set; } = 0;
    public double Centimetres { get; set; } = 0;
    public double Millimetres { get; set; } = 0;
}