namespace Test_InchFeature;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(0.5, 0, 1, 2, 0, 1 ,2 , 0, 1,2)]
    [TestCase(0.25, 0, 1, 4, 0, 1, 4, 0, 1, 4)]
    [TestCase(0.26, 0, 1, 4, 0, 13, 50, 0, 17, 64)]
    [TestCase(0.27, 0, 17, 64, 0, 27, 100, 0, 9, 32)]
    [TestCase(1.4562, 1, 29, 64, 1, 4561, 10000, 1, 15, 32)]
    [TestCase(1.999999, 2, 0, 1, 2, 0, 1, 2, 1, 64)]
    public void TestClosestInch(double inches, 
    int expectedLowerUnits, int expectedLowerNumerator, int expectedLowerDenominator,
    int expectedUnits, int expectedNumerator, int expectedDenominator,
    int expectedUpperUnits, int expectedUpperNumerator, int expectedUpperDenominator)
    {
        var result = MetricConversion.ClosestInchFraction(inches);
        Assert.Multiple(() =>
        {
            Assert.That(result.LowerImperialFraction?.Units, Is.EqualTo(expectedLowerUnits));
            Assert.That(result.LowerImperialFraction?.Numerator, Is.EqualTo(expectedLowerNumerator));
            Assert.That(result.LowerImperialFraction?.Denominator, Is.EqualTo(expectedLowerDenominator));

            Assert.That(result.ImperialFraction?.Units, Is.EqualTo(expectedUnits));
            Assert.That(result.ImperialFraction?.Numerator, Is.EqualTo(expectedNumerator));
            Assert.That(result.ImperialFraction?.Denominator, Is.EqualTo(expectedDenominator));

            Assert.That(result.UpperImperialFraction?.Units, Is.EqualTo(expectedUpperUnits));
            Assert.That(result.UpperImperialFraction?.Numerator, Is.EqualTo(expectedUpperNumerator));
            Assert.That(result.UpperImperialFraction?.Denominator, Is.EqualTo(expectedUpperDenominator));
        });
    }

    [Test]
    [TestCase(0.5, 0, 1, 2, 0, 1, 2, 0,1,2, 0.5, 0.5)]
    [TestCase(0.25, 0, 1, 4, 0, 1, 4, 0, 1, 4, 0.25, 0.25)]
    [TestCase(0.26, 0, 1, 4, 0, 13, 50, 0, 17, 64, 0.25, 0.2656)]   
    [TestCase(0.27, 0, 17, 64, 0, 27, 100, 0, 9, 32, 0.2656, 0.2812)]
    [TestCase(1.4562, 1, 29, 64, 1, 4561, 10000, 1, 15, 32, 1.4531, 1.4688)]  // This test fails as a perfect example of a rounding error
                                                                                // the computed value is 1.46887999 which should be rounded to 1.4688001
                                                                                // but it does not round up correctly.
    public void TestClosestInchWithInchReturns(double inches,
    int expectedLowerUnits, int expectedLowerNumerator, int expectedLowerDenominator,
    int expectedUnits, int expectedNumerator, int expectedDenominator,
    int expectedUpperUnits, int expectedUpperNumerator, int expectedUpperDenominator,
    double lowerInch, double upperInch)
    {
        var result = MetricConversion.ClosestInchFraction(inches);
        Assert.Multiple(() =>
        {
            Assert.That(result.LowerImperialFraction?.Units, Is.EqualTo(expectedLowerUnits));
            Assert.That(result.LowerImperialFraction?.Numerator, Is.EqualTo(expectedLowerNumerator));
            Assert.That(result.LowerImperialFraction?.Denominator, Is.EqualTo(expectedLowerDenominator));

            Assert.That(result.ImperialFraction?.Units, Is.EqualTo(expectedUnits));
            Assert.That(result.ImperialFraction?.Numerator, Is.EqualTo(expectedNumerator));
            Assert.That(result.ImperialFraction?.Denominator, Is.EqualTo(expectedDenominator));

            Assert.That(result.UpperImperialFraction?.Units, Is.EqualTo(expectedUpperUnits));
            Assert.That(result.UpperImperialFraction?.Numerator, Is.EqualTo(expectedUpperNumerator));
            Assert.That(result.UpperImperialFraction?.Denominator, Is.EqualTo(expectedUpperDenominator));

            Assert.That(result.lowerInches, Is.EqualTo(lowerInch));
            Assert.That(result.upperInches, Is.EqualTo(upperInch));
        });
    }
    
}