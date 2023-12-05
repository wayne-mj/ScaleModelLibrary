namespace WMJ.ScaleModelLibrary.MetricSystem;

public static class MetricConversion
{

    private static int decimalPrecision = 2;
    private static int inchPrecision = 64;
    private static readonly int MaxDecimalPrecision = 5;    

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

    /// <summary>
    /// Quick metric conversion macros
    /// </summary>
    public static double FeetToMilliMetres(double feet) => Math.Round(FeetToInches(feet) * 25.4, DecimalPrecision);
    public static double FeetToInches(double Feet) => Math.Round((Feet * 12.0), DecimalPrecision);
    //public static double YardToFeet(double Yard) => Math.Round(Yard * 3, DecimalPrecision);
    public static double InchesToMillimetres(double Inches) => Math.Round(Inches * 25.4, DecimalPrecision);

    public static double MetresToMillimetres(double Metres) => Math.Round(Metres * 1000, DecimalPrecision);
    public static double CentimetresToMillimetres(double Centimetres) => Math.Round(Centimetres * 10, DecimalPrecision);
    public static double MillimetresToCentimetres(double Millimetres) => Math.Round(Millimetres / 10, DecimalPrecision);
    public static double MillimetresToMetres(double Millimetres) => Math.Round(MillimetresToCentimetres(Millimetres) / 100, DecimalPrecision);

    public static double MetresToFeet(double Metres) => Math.Round(Metres * 3.28084, DecimalPrecision);
    public static double CentimetresToInches(double Centimetres) => Math.Round(Centimetres / 2.54, DecimalPrecision);
    public static double MillimetresToInches(double Millimetres) => Math.Round(Millimetres / 25.4, DecimalPrecision);
}