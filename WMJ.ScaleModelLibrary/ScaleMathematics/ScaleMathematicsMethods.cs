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

    public static ScaleMetricMeasurementsModel ScaleMetricMeasurements(Metrics metrics, double measurement, double scale)
    {
        ScaleMetricMeasurementsModel scaleMetricModel = new();

        var results = ScaledMillimetres(metrics, measurement, scale);
        
        switch (metrics)
        {
            case Metrics.Metres:
                scaleMetricModel.Metres = measurement;
                scaleMetricModel.Centimetres = MetricConversion.MetresToCentimetres(measurement);
                scaleMetricModel.Millimetres = MetricConversion.MetresToMillimetres(measurement);
                scaleMetricModel.ScaledMillimetres = results;
                scaleMetricModel.Scale = scale;
                break;
            case Metrics.Centimetres:
                scaleMetricModel.Metres = MetricConversion.CentimetresToMetres(measurement);
                scaleMetricModel.Centimetres = measurement;
                scaleMetricModel.Millimetres = MetricConversion.CentimetresToMillimetres(measurement);
                scaleMetricModel.ScaledMillimetres = results;
                scaleMetricModel.Scale = scale;
                break;
            case Metrics.Millimetres:
                scaleMetricModel.Metres = MetricConversion.MillimetresToMetres(measurement);
                scaleMetricModel.Centimetres = MetricConversion.MillimetresToCentimetres(measurement);
                scaleMetricModel.Millimetres = measurement;
                scaleMetricModel.ScaledMillimetres = results;
                scaleMetricModel.Scale = scale;
                break;
        }        

        return scaleMetricModel;

    }
}