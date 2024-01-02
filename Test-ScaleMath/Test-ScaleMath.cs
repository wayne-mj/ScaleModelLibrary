namespace Test_ScaleMath;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        MetricConversion.DecimalPrecision = 3;
    }

    
    [Test]                                              // NMRA S-1.2 Standards for Scale models           
    [TestCase(Metrics.Metres, 0.3048, 87.1, 3.5)]       // define HO scale as 3.5mm to the foot
    [TestCase(Metrics.Centimetres, 30.48, 87.1, 3.5)]   // or 1:87.1 using this as a known reference
    [TestCase(Metrics.Millimetres, 304.8, 87.1, 3.5)]   // it can be used a test case for the ScaleMathematics
    public void TestScaleMetricMeasurements(Metrics metrics, double measurement, double scale, double expectedMillimetres)
    {
        var result = ScaleMathematics.ScaleMetricMeasurements(metrics, measurement, scale);
        Assert.That(result.ScaledMillimetres, Is.EqualTo(expectedMillimetres).Within(0.01));
    }

    [Test]                                                                  // NMRA S-1.2 Standards for Scale models
    [TestCase(Metrics.Feet, 1, 87.1, 3.5, 0.138, 1,8,69,500,9,64)]          // define HO scale as 3.5mm to the foot
    [TestCase(Metrics.Inches, 12, 87.1, 3.5, 0.138, 1,8,69,500,9,64)]       // or 1:87.1 using this as a known reference
    public void TestScaleImperialMeasurements(Metrics metrics, double measurement, double scale, double expectedMillimetres, double expectedInches,
    int lowerNumerator, int lowerDenominator,
    int middleNumerator, int middleDenominator,
    int upperNumerator, int upperDenominator)
    {
        var result = ScaleMathematics.ScaleImperialMeasurements(metrics, measurement, scale);
        Assert.Multiple(() =>
        {
            Assert.That(result.ScaledMillimetres, Is.EqualTo(expectedMillimetres).Within(0.01));
            Assert.That(result.ScaledInches, Is.EqualTo(expectedInches).Within(0.01));
            Assert.That(result.ScaledClosestImperialFraction.LowerImperialFraction?.Numerator, Is.EqualTo(lowerNumerator));
            Assert.That(result.ScaledClosestImperialFraction.LowerImperialFraction?.Denominator, Is.EqualTo(lowerDenominator));
            Assert.That(result.ScaledClosestImperialFraction.ImperialFraction?.Numerator, Is.EqualTo(middleNumerator));
            Assert.That(result.ScaledClosestImperialFraction.ImperialFraction?.Denominator, Is.EqualTo(middleDenominator));
            Assert.That(result.ScaledClosestImperialFraction.UpperImperialFraction?.Numerator, Is.EqualTo(upperNumerator));
            Assert.That(result.ScaledClosestImperialFraction.UpperImperialFraction?.Denominator, Is.EqualTo(upperDenominator));
        });
    }
}