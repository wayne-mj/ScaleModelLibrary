namespace WMJ.ScaleModelLibrary.MetricSystem;

public static class Definitions
{
    /// <summary>
    /// Basic definitions for the metric system
    /// </summary>
    
    public static int intMetres = 1;
    public static int intMetresInCentimetres = intMetres * 100;
    public static int intMetresInMillimetres = intMetresInCentimetres * 10;
    public static double dblMetresInFeet = intMetres * 3.28084;

    public static int intFeet = 1;
    public static int intFeetToInches = intFeet * 12;
    
    public static int intInches = 1;
    public static double dblInchesInMillimetres = intInches * 25.4;
    public static double dblInchesInCentimetres = dblInchesInMillimetres / 10;

}