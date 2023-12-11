namespace FractionSystem_Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

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
    // [TestCase(6,int.MaxValue-1,0,1,357913941,"OK")]                  //      These two tests are time consuming
    // [TestCase(6,int.MaxValue,0,0,0,"Integer overflow")]              //      They add about 5 seconds to the test run
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