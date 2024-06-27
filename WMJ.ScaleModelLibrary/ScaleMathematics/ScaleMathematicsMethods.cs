using WMJ.ScaleModelLibrary.MetricSystem;
using WMJ.ScaleModelLibrary.FractionSystem;
using WMJ.ScaleModelLibrary.TrigMathematics;
//using Microsoft.VisualBasic;

namespace WMJ.ScaleModelLibrary.ScaleMathematics;

public static partial class ScaleMathematics
{
    /// <summary>
    /// Returns in millimetres the scaled measurement
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="measurement"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns the Metric measurements for the given scale
    /// </summary>
    /// <param name="metrics"></param>
    /// <param name="measurement"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns the Imperial measurements for the given scale
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="measurement"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static ScaleImperialMeasurementsModel ScaleImperialMeasurements(Metrics metric, double measurement, double scale)
    {
        ScaleImperialMeasurementsModel scaleImperialModel = new();

        var results = ScaledMillimetres(metric, measurement, scale);

        switch (metric)
        {
            case Metrics.Feet:
                scaleImperialModel.Feet = measurement;
                scaleImperialModel.Inches = MetricConversion.FeetToInches(measurement);
                scaleImperialModel.FractionNumerator = 0;
                scaleImperialModel.FractionDenominator = 0;
                scaleImperialModel.ScaledMillimetres = results;
                scaleImperialModel.ScaledInches = MetricConversion.MillimetresToInches(results);
                scaleImperialModel.ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(scaleImperialModel.ScaledInches);
                scaleImperialModel.Scale = scale;
                break;
            case Metrics.Inches:
                scaleImperialModel.Feet = MetricConversion.InchesToFeet(measurement);
                scaleImperialModel.Inches = measurement;
                scaleImperialModel.FractionNumerator = 0;
                scaleImperialModel.FractionDenominator = 0;
                scaleImperialModel.ScaledMillimetres = results;
                scaleImperialModel.ScaledInches = MetricConversion.MillimetresToInches(results);
                scaleImperialModel.ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(scaleImperialModel.ScaledInches);
                scaleImperialModel.Scale = scale;
                break;

        }

        return scaleImperialModel;
    }

    /// <summary>
    /// Returns a List Table of Feet for a given scale
    /// </summary>
    /// <param name="feet"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static List<ScaleImperialMeasurementsModel> FeetTable (double feet, double scale)
    {
        List<ScaleImperialMeasurementsModel> scaleImperialModels = new();

        ScaleImperialMeasurementsModel _holder = new()
        {
            Id = 0,
            Feet = feet,
            Inches = 0,
            Scale = scale,
            ScaledMillimetres = ScaledMillimetres(Metrics.Feet, feet, scale),
            ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Feet, feet, scale)),
            ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Feet, feet, scale)))
        };

        scaleImperialModels.Add(_holder);

        for (int i = 1; i < 12 ; i++)
        {
            _holder = new()
            {
                Id = i,
                Feet = feet,
                Inches = i,
                Scale = scale,
                ScaledMillimetres = ScaledMillimetres(Metrics.Inches, MetricConversion.FeetToInches(feet) + i, scale),
                ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, MetricConversion.FeetToInches(feet) + i, scale)),
                ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, MetricConversion.FeetToInches(feet) + i, scale)))
            };

            scaleImperialModels.Add(_holder);
        }

        _holder = new()
        {
            Id = scaleImperialModels.Count,
            Feet = feet + 1,
            Inches = 0,
            Scale = scale,
            ScaledMillimetres = ScaledMillimetres(Metrics.Feet, feet +1, scale),
            ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Feet, feet+1, scale)),
            ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Feet, feet+1, scale)))
        };

        scaleImperialModels.Add(_holder);

        return scaleImperialModels;
    }

    /// <summary>
    /// Returns a List Table of Inches for a given scale
    /// </summary>
    /// <param name="inch"></param>
    /// <param name="fraction"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static List<ScaleImperialMeasurementsModel> InchTable(int inch, InchFractions fraction, double scale)
    {
        List<ScaleImperialMeasurementsModel> scaleImperialModels = new();

        ScaleImperialMeasurementsModel holder = new()
        {
            Id =0,
            Feet = 0,
            Inches = inch,
            FractionNumerator = 0,
            FractionDenominator = 0,
            Scale = scale,
            ScaledMillimetres = ScaledMillimetres(Metrics.Inches, inch, scale),
            ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch, scale)),
            ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch, scale)))
        };

        scaleImperialModels.Add(holder);

        for (int i = 1; i < (int)fraction; i++)
        {
            var simplified = Fractions.LowestCommonDenominator(i, (int)fraction);

            holder = new()
            {
                Id = i,
                Feet = 0,
                Inches = inch,
                FractionNumerator = simplified.Numerator,
                FractionDenominator = simplified.Denominator,
                Scale = scale,
                ScaledMillimetres = ScaledMillimetres(Metrics.Inches, inch + (double)i / (double)fraction, scale),
                ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch + (double)i / (double)fraction, scale)),
                ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch + (double)i / (double)fraction, scale)))
            };
            scaleImperialModels.Add(holder);
        }

        holder = new()
        {
            Id = scaleImperialModels.Count,
            Feet = 0,
            Inches = inch + 1,
            FractionNumerator = 0,
            FractionDenominator = 0,
            Scale = scale,
            ScaledMillimetres = ScaledMillimetres(Metrics.Inches, inch + 1, scale),
            ScaledInches = MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch + 1, scale)),
            ScaledClosestImperialFraction = MetricConversion.ClosestInchFraction(MetricConversion.MillimetresToInches(ScaledMillimetres(Metrics.Inches, inch + 1, scale)))
        };

        scaleImperialModels.Add(holder);
        
        return scaleImperialModels;
    }

    /// <summary>
    /// Returns a List Table of Feet and Inches for a given list of scales
    /// </summary>
    /// <param name="feet"></param>
    /// <param name="ScaleList"></param>
    /// <returns></returns>
    public static List<MultiScaleModel> MultiScaleTable(double feet, List<double>ScaleList)
    {
        List<MultiScaleModel> multiScaleTable = new();
        int Count = 0;

        foreach (double scale in ScaleList)
        {
            MultiScaleModel multiScaleModel = new()
            {
                Id = Count,
                Scale = scale,
                ScaleDescription = $"1:{scale}",
                ScaledTable = FeetTable(feet, scale)
            };

            multiScaleTable.Add(multiScaleModel);
            Count++;                        
        }

        return multiScaleTable;
    }

    /// <summary>
    /// Create the plot points for an arc
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="arc"></param>
    /// <returns></returns>
    public static List<PlotXY> GeneratePlotPoints(double radius, int arc)
    {
        List<PlotXY> plotPoints = [];

        for (int i = 0 ; i <= arc; i++)
        {
            plotPoints.Add(new PlotXY
            {
                X = Math.Round(Trigonometry.Adj_hyp_ang_deg(radius, i),1),
                Y = Math.Round(Trigonometry.Opp_hyp_ang_deg(radius, i),1)
            });
        }

        return plotPoints;
    }

    /// <summary>
    /// Create the plot points for an arc with an increment
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="arc"></param>
    /// <param name="increment"></param>
    /// <returns></returns>
    public static List<PlotXY> GeneratePlotPoints(double radius, int arc, int increment)
    {
        List<PlotXY> plotPoints = [];

        for (int i = 0 ; i <= arc; i+=increment)
        {
            plotPoints.Add(new PlotXY
            {
                X = Math.Round(Trigonometry.Adj_hyp_ang_deg(radius, i),1),
                Y = Math.Round(Trigonometry.Opp_hyp_ang_deg(radius, i),1)
            });
        }

        return plotPoints;
    }

    /// <summary>
    /// Create a table for to represent a helix in one revolution
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="support"></param>
    /// <param name="increment"></param>
    /// <returns></returns>
    public static List<Helix> HelixTable (double radius, double support, double increment)
    {
        List<Helix> helixTable = [];
        double[] height = new double[9];
        int count = 0;        
        double degrees;

        degrees = 45;

        for (int i =0 ; i <= 360 ; i += (int)degrees)
        {            
            if (count > 0)
            {
                height[count] = height[count -1] + increment;
            }
            else
            {
                height[count] += increment;
            }
            helixTable.Add(new Helix
            {
                height = height[count],
                degree = i,
                radius = radius,
                support = support,
            });
            count ++;
        }

        count = 0;

        for (int i = (int)degrees; i <= 360 + (int)degrees; i += (int)degrees)
        {
            double denominator = (2.0 * Trigonometry.pi * radius) / (360.0 / (double)i);
            double _gradient = height[count] /  denominator;
            helixTable[count].gradient = Math.Round(_gradient * 100, 2);
            count++;
        }

        for (int i = 0; i < helixTable.Count; i++)
        {
            helixTable[i].clearance = helixTable[helixTable.Count-1].height - helixTable[0].height - support;
        }


        return helixTable;
    }
}