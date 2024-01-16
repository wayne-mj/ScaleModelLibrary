namespace Test_WMJ.ScaleModelLibrary;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    /****************************************************************************************************************
     
    Scale Mathematics Tests

    *****************************************************************************************************************/

    [Test]                                              // NMRA S-1.2 Standards for Scale models           
    [TestCase(Metrics.Metres, 0.3048, 87.1, 3.5)]       // define HO scale as 3.5mm to the foot
    [TestCase(Metrics.Centimetres, 30.48, 87.1, 3.5)]   // or 1:87.1 using this as a known reference
    [TestCase(Metrics.Millimetres, 304.8, 87.1, 3.5)]   // it can be used a test case for the ScaleMathematics
    public void TestScaleMetricMeasurements(Metrics metrics, double measurement, double scale, double expectedMillimetres)
    {
        MetricConversion.DecimalPrecision = 3;
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
        MetricConversion.DecimalPrecision = 3;
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

    /****************************************************************************************************************

    Metric Conversion Tests

    *****************************************************************************************************************/

    [Test]
    [TestCase(-1, ExpectedResult = 0)]
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(1, ExpectedResult = 1)]
    [TestCase(10, ExpectedResult = 4)]
    [TestCase(int.MaxValue, ExpectedResult = 4)]
    public int TestDecimalPrecisionConfiguration(int precision)
    {
        MetricConversion.DecimalPrecision = precision;
        //Console.WriteLine($"DecimalPrecision = {MetricConversion.DecimalPrecision}");
        return MetricConversion.DecimalPrecision;
    }

    [Test]
    [TestCase(-1, ExpectedResult = InchFractions.OneSixtyFourth)]
    [TestCase(0, ExpectedResult = InchFractions.OneSixtyFourth)]    
    [TestCase(1, ExpectedResult = InchFractions.One)]
    [TestCase(16, ExpectedResult = InchFractions.OneSixteenth)]
    [TestCase(128, ExpectedResult = InchFractions.OneSixtyFourth)]
    [TestCase(int.MaxValue, ExpectedResult = InchFractions.OneSixtyFourth)]
    public InchFractions TestInchPrecisionConfiguration(int inch)
    {
        MetricConversion.InchPrecision = (InchFractions)inch;
        //Console.WriteLine($"InchPrecision = {MetricConversion.InchPrecision}");
        return MetricConversion.InchPrecision;
    }   

    [Test]
    public void TestFeetToMillimetresConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.FeetToMilliMetres(Definitions.intFeet);
        //Console.WriteLine($"FeetToMillimetresConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.dblInchesInMillimetres * Definitions.intFeetToInches).Within(0.01));
    }

    [Test]
    public void TestFeetToInchesConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.FeetToInches(Definitions.intFeet);
        //Console.WriteLine($"FeetToInchesConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.intFeetToInches).Within(0.01));
    }

    [Test]
    public void TestInchesToMillimetresConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.InchesToMillimetres(Definitions.intInches);
        //Console.WriteLine($"InchesToMillimetresConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.dblInchesInMillimetres).Within(0.01));
    }

    [Test]
    public void TestMetreToMillimetreConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.MetresToMillimetres(Definitions.intMetres);
        //Console.WriteLine($"MetreToMillimetreConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.intMetresInMillimetres).Within(0.01));
    }

    [Test]
    [TestCase(0.1, ExpectedResult = 1)]
    [TestCase(1, ExpectedResult = 10)]
    [TestCase(10, ExpectedResult = 100)]
    public double TestCentimetreToMillimetreConversion(double centimetres)
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.CentimetresToMillimetres(centimetres);
        //Console.WriteLine($"CentimetreToMillimetreConversion = {result}");
        return result;
    }

    [Test]
    [TestCase(1, ExpectedResult = 0.1)]
    [TestCase(10, ExpectedResult = 1)]
    [TestCase(100, ExpectedResult = 10)]    
    public double TestMillimetresToCentimetresConversion(double millimetres)
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.MillimetresToCentimetres(millimetres);
        //Console.WriteLine($"MillimetresToCentimetresConversion = {result}");
        return result;
    }

    [Test]
    public void TestMillimetresToMetresConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.MillimetresToMetres(Definitions.intMetresInMillimetres);
        //Console.WriteLine($"MillimetresToMetresConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.intMetres).Within(0.01));
    }

    [Test]
    public void TestMetresToFeetConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.MetresToFeet(Definitions.intMetres);
        //Console.WriteLine($"MetresToFeetConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.dblMetresInFeet).Within(0.01));
    }

    [Test]
    public void TestCentimetresToInchesConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.CentimetresToInches(Definitions.dblInchesInCentimetres);
        //Console.WriteLine($"CentimetresToInchesConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.intInches).Within(0.01));
    }

    [Test]
    public void TestMillimetresToInchesConversion()
    {
        MetricConversion.DecimalPrecision = 2;
        var result = MetricConversion.MillimetresToInches(Definitions.dblInchesInMillimetres);
        //Console.WriteLine($"MillimetresToInchesConversion = {result}");
        Assert.That(result, Is.EqualTo(Definitions.intInches).Within(0.01));
    }

    /****************************************************************************************************************

    Inch Conversion Tests

    *****************************************************************************************************************/

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

    /****************************************************************************************************************

    Fraction System Tests

    *****************************************************************************************************************/

    [Test]
    [TestCase(1,2,1,3,0,5,6,"OK")]
    [TestCase(1,2,1,0,0,0,0,"Divide by zero error")]
    [TestCase(1,2,1,int.MaxValue,0,0,0,"Integer overflow")]
    [TestCase(1,int.MaxValue,1,int.MaxValue,0,2,int.MaxValue,"OK")]
    public void TestFractionAdd(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator, int units, int numerator, int denominator, string status)
    {
        var result = Fractions.FractionAdd(firstNumerator, firstDenominator, secondNumerator, secondDenominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Units, Is.EqualTo(units));
            Assert.That(result.Numerator, Is.EqualTo(numerator));
            Assert.That(result.Denominator, Is.EqualTo(denominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }

    [Test]
    [TestCase(1,2,1,3,0,1,6,"OK")]
    [TestCase(1,2,1,0,0,0,0,"Divide by zero error")]
    [TestCase(1,2,1,int.MaxValue,0,0,0,"Integer overflow")]
    [TestCase(1,int.MaxValue,1,int.MaxValue,0,0,0,"OK")]
    public void TestFractionSubtract(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator, int units, int numerator, int denominator, string status)
    {
        var result = Fractions.FractionSubtract(firstNumerator, firstDenominator, secondNumerator, secondDenominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Units, Is.EqualTo(units));
            Assert.That(result.Numerator, Is.EqualTo(numerator));
            Assert.That(result.Denominator, Is.EqualTo(denominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }    
    
    [Test]
    [TestCase(1,2,1,3,0,1,6,"OK")]
    [TestCase(1,2,1,0,0,0,0,"Divide by zero error")]
    [TestCase(1,2,1,int.MaxValue,0,0,0,"Integer overflow")]
    [TestCase(1,int.MaxValue,1,int.MaxValue,0,0,0,"Integer overflow")]
    public void TestFractionMultiply(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator, int units, int numerator, int denominator, string status)
    {
        var result = Fractions.FractionMultiply(firstNumerator, firstDenominator, secondNumerator, secondDenominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Units, Is.EqualTo(units));
            Assert.That(result.Numerator, Is.EqualTo(numerator));
            Assert.That(result.Denominator, Is.EqualTo(denominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }

    [Test]
    [TestCase(1,2,1,3,0,3,2,"OK")]
    [TestCase(1,2,1,0,0,0,0,"Divide by zero error")]
    [TestCase(1,2,1,int.MaxValue,0,int.MaxValue,2,"OK")]
    [TestCase(1,int.MaxValue,1,int.MaxValue,0,int.MaxValue,int.MaxValue,"OK")]    
    public void TestFractionDivide(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator, int units, int numerator, int denominator, string status)
    {
        var result = Fractions.FractionDivide(firstNumerator, firstDenominator, secondNumerator, secondDenominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Units, Is.EqualTo(units));
            Assert.That(result.Numerator, Is.EqualTo(numerator));
            Assert.That(result.Denominator, Is.EqualTo(denominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }

    [Test]
    [TestCase(6,15,0,2,5,"OK")]
    [TestCase(6,0,0,0,0,"Divide by zero error")]    
    [TestCase(4,3,1,1,3,"OK")]
    [TestCase(6,int.MaxValue-1,0,1,357913941,"OK")]                  //      These two tests are time consuming
    [TestCase(6,int.MaxValue,0,0,0,"Integer overflow")]              //      They add about 5 seconds to the test run
    public void TestLowestCommonDenominator(int Numerator, int Denominator, int units, int numerator, int denominator, string status)
    {
        var result = Fractions.LowestCommonDenominator(Numerator, Denominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Units, Is.EqualTo(units));
            Assert.That(result.Numerator, Is.EqualTo(numerator));
            Assert.That(result.Denominator, Is.EqualTo(denominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }

    [Test]
    [TestCase(1,1,2,3,2,"OK")]
    [TestCase(1,1,3,4,3,"OK")]
    [TestCase(1,1,4,5,4,"OK")]
    [TestCase(65535,512,65535,0,0,"Integer overflow")]  
    [TestCase(65535,512,0,0,0,"Divide by zero error")]
    public void TestImproperFraction(int TestUnits, int TestNumerator, int TestDenominator, int ExpectedNumerator, int expectedDenominator, string status)
    {
        var result = Fractions.ImproperFraction(TestUnits, TestNumerator, TestDenominator);
        Assert.Multiple(() =>
        {
            Assert.That(result.Numerator, Is.EqualTo(ExpectedNumerator));
            Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
            Assert.That(result.Status, Is.EqualTo(status));
        });
    }
}