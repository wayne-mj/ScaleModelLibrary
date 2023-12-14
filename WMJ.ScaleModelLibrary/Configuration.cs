using WMJ.ScaleModelLibrary.MetricSystem;
namespace WMJ.ScaleModelLibrary;

public static class Configuration
{
    private static int decimalPrecision = 2;
    private static int inchPrecision = 64;

    // Maximum decimal precision is 5 to prevent integer overflow
    private static readonly int _MaxDecimalPrecision = 4; //5;    
    private static readonly int _baseDenominator = (int)Math.Pow(10, _MaxDecimalPrecision);

    public static int MaxDecimalPrecision 
    { 
        get { return _MaxDecimalPrecision; }
    }

    public static int baseDenominator 
    {
        get { return _baseDenominator; }
    }

    /// <summary>
    /// Sets the resolution of the inch fractions (1/64, 1/32, 1/16, 1/8, 1/4, 1/2). Default is 64.
    /// </summary>
    public static InchFractions InchPrecision
    {
        get { return (InchFractions)inchPrecision; }
        set
        {
            if (Enum.IsDefined(typeof(InchFractions), value))
            {
                inchPrecision = (int)value;
            }
            else
            {
                inchPrecision = (int)InchFractions.OneSixtyFourth;
            }
        }
    }

    /// <summary>
    /// Sets the decimal precision (0.00001, 0.0001, 0.001, 0.01, 0.1, 1) Maximum of 5 decimal places.  Default is 2.
    /// </summary>
    public static int DecimalPrecision
    {
        get { return decimalPrecision; }
        set
        {
            if (value > MaxDecimalPrecision)
                value = MaxDecimalPrecision;
            decimalPrecision = value;

            if (value <= 0)
                decimalPrecision = 0;
        }
    }
}