using WMJ.ScaleModelLibrary.FractionSystem;

namespace WMJ.ScaleModelLibrary.MetricSystem;

public static partial class MetricConversion
{
    public static ClosestImperialFractionModel ClosestInchFraction(double inches)
    {
        // Required variables
        ClosestImperialFractionModel closestImperialFractionModel;
        int below = 0; int above = 0;

        // Using MaxDecimalPrecision to prevent the possibility of an integer overflow
        inches = Math.Round(inches, Configuration.MaxDecimalPrecision);

        // If there is a Unit value, hold onto it
        int localInchUnit = (int)Math.Floor(inches);

        // Remove the Unit value from the inches
        double localInches = inches - localInchUnit;
        
        // Convert the decimal portion of the inches to a fraction
        double localNumerator = Math.Floor(localInches * Configuration.baseDenominator);
        double localDenominator = Configuration.baseDenominator;

        // First part, using the current unit, numerator and denominator, find the lowest common denominator
        if (localInchUnit > 0)
        {
            localNumerator += localInchUnit * Configuration.baseDenominator;
        }

        var simplifiedFraction = Fractions.LowestCommonDenominator((int)localNumerator, (int)localDenominator);

        closestImperialFractionModel = new ClosestImperialFractionModel
        {
            ImperialFraction = simplifiedFraction
        };

        // Second part, using the current unit, numerator and denominator, find the closest imperial fraction
        // lower than the current value
        for (int i = 1; i < (int)InchFractions.OneSixtyFourth; i++)
        {
            //double _math = (double)i / (double)InchPrecision;
            double _math = (double)i / (double)InchFractions.OneSixtyFourth;
            if (_math <= (double)localInches)
            {
                below = i;
            }
            else
            {
                break;
            }
        }

        if (localInchUnit > 0)
        {
            below += localInchUnit * (int)InchFractions.OneSixtyFourth;
        }

        var simplifiedBelow = Fractions.LowestCommonDenominator(below, (int)Configuration.InchPrecision);

        closestImperialFractionModel.LowerImperialFraction = simplifiedBelow;

        // Third part, using the current unit, numerator and denominator, find the closest imperial fraction
        // higher than the current value
        for (int i = (int)InchFractions.OneSixtyFourth; i > 0; i--)
        {
            //double _math = (double)i / (double)InchPrecision;
            double _math = (double)i / (double)InchFractions.OneSixtyFourth;
            if (_math >= (double)localInches)
            {
                above = i;
            }
            else
            {
                break;
            }
        }

        if (localInchUnit > 0)
        {
            above += localInchUnit * (int)InchFractions.OneSixtyFourth;
        }

        var simplifiedAbove = Fractions.LowestCommonDenominator(above, (int)Configuration.InchPrecision);

        closestImperialFractionModel.UpperImperialFraction = simplifiedAbove;

        return closestImperialFractionModel;
    }
}