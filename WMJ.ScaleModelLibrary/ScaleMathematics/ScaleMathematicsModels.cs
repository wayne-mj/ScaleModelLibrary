using WMJ.ScaleModelLibrary.MetricSystem;

namespace WMJ.ScaleModelLibrary.ScaleMathematics;

// public class ScaleMetricModel
// {
//     public MetricMeasurementModel? ScaleMetricMeasurement { get; set; }
//     public double Scale { get; set; }
// }

// public class ScaleImperialModel
// {
//     public ImperialMeasurementModel? ScaleImperialMeasurement { get; set; }
//     public double Scale { get; set; }
// }

public class ScaleMetricMeasurementsModel
{
    public int Id { get; set; } = 0;
    public double Metres { get; set; } = 0;
    public double Centimetres { get; set; } = 0;
    public double Millimetres { get; set; } = 0;
    public double Scale { get; set; } = 0;
    public double ScaledMillimetres { get; set; } = 0;
}

public class ScaleImperialMeasurementsModel
{
    public int Id { get; set; } = 0;
    public double Feet { get; set; } = 0;
    public double Inches { get; set; } = 0;
    public int FractionNumerator { get; set; } = 0;
    public int FractionDenominator { get; set; } = 0;
    public double Scale { get; set; } = 0;
    public double ScaledMillimetres { get; set; } = 0;
    public double ScaledInches { get; set; } = 0;
    public ClosestImperialFractionModel ScaledClosestImperialFraction { get; set; } = new();
}

public class MultiScaleModel
{
    public int Id {get; set;} = 0;
    public double Scale { get; set; } = 0;
    public string ScaleDescription { get; set; } = string.Empty;
    public List<ScaleImperialMeasurementsModel> ScaledTable { get; set; } = new();
}