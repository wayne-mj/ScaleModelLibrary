using WMJ.ScaleModelLibrary.MetricSystem;
using WMJ.ScaleModelLibrary.FractionSystem;

namespace WMJ.ScaleModelLibrary.ScaleMathematics;

public static partial class ScaleMathematics
{
    private static double ScaledMillimetres(Metrics metric, double measurement, double scale)
    {
        double result = 0;

        switch (metric)
        {
            case Metrics.Feet:
                result = Math.Round(MetricConversion.FeetToMilliMetres(measurement) / scale, MetricConversion.DecimalPrecision);
                break;
            case Metrics.Inches:
                result = Math.Round(MetricConversion.InchesToMillimetres(measurement) / scale, MetricConversion.DecimalPrecision);
                break;
            case Metrics.Metres:
                result = Math.Round(MetricConversion.MetresToMillimetres(measurement) / scale, MetricConversion.DecimalPrecision);
                break;
            case Metrics.Centimetres:
                result = Math.Round(MetricConversion.CentimetresToMillimetres(measurement) / scale, MetricConversion.DecimalPrecision);
                break;
            case Metrics.Millimetres:
                result = Math.Round(measurement / scale, MetricConversion.DecimalPrecision);
                break;
        }

        return result;
    }

    public static ScaleMetricModel ScaleMetricMeasurements(Metrics metrics, double measurement, double scale)
    {
        var results = ScaledMillimetres(metrics, measurement, scale);

        ScaleMetricModel scaleMetricModel = new()
        {
            ScaleMetricMeasurement = new MetricMeasurementModel
            {
                Metres = MetricConversion.MillimetresToMetres(results),
                Centimetres = MetricConversion.MillimetresToCentimetres(results),
                Millimetres = results
            },
            Scale = scale            
        };

        return scaleMetricModel;

    }
}