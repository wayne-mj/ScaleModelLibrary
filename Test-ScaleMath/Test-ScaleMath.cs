namespace Test_ScaleMath;

public class Tests
{
    [SetUp]
    public void Setup()
    {
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
}