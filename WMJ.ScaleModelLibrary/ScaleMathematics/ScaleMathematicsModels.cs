using WMJ.ScaleModelLibrary.MetricSystem;

namespace WMJ.ScaleModelLibrary.ScaleMathematics;

public class ScaleMetricModel
{
    public MetricMeasurementModel? ScaleMetricMeasurement { get; set; }
    public double Scale { get; set; }
}

public class ScaleImperialModel
{
    public ImperialMeasurementModel? ScaleImperialMeasurement { get; set; }
    public double Scale { get; set; }
}

public class ScaleMetricMeasurementsModel
{
    public int Id { get; set; } = 0;
    public double Metres { get; set; } = 0;
    public double Centimetres { get; set; } = 0;
    public double Millimetres { get; set; } = 0;
    public double Scale { get; set; } = 0;
    public double ScaledMillimetres { get; set; } = 0;
}