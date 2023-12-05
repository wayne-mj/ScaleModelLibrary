namespace MetricSystem_Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    [TestCase(-1, ExpectedResult = 0)]
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(1, ExpectedResult = 1)]
    [TestCase(10, ExpectedResult = 5)]
    [TestCase(int.MaxValue, ExpectedResult = 5)]
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
}