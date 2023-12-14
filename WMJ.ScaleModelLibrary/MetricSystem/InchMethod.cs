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
        inches = Math.Round(inches, MaxDecimalPrecision);

        // If there is a Unit value, hold onto it
        int localInchUnit = (int)Math.Floor(inches);

        // Remove the Unit value from the inches
        double localInches = inches - localInchUnit;
        
        // Convert the decimal portion of the inches to a fraction
        double localNumerator = Math.Floor(localInches * baseDenominator);
        double localDenominator = baseDenominator;

        // First part, using the current unit, numerator and denominator, find the lowest common denominator
        if (localInchUnit > 0)
        {
            localNumerator += localInchUnit * baseDenominator;
        }

        var simplifiedFraction = Fractions.LowestCommonDenominator((int)localNumerator, (int)localDenominator);

        closestImperialFractionModel = new ClosestImperialFractionModel
        {
            ImperialFraction = simplifiedFraction,
            currentInches = inches            
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

        var simplifiedBelow = Fractions.LowestCommonDenominator(below, (int)InchPrecision);

        closestImperialFractionModel.LowerImperialFraction = simplifiedBelow;
        closestImperialFractionModel.lowerInches = (double)simplifiedBelow.Units + (double)simplifiedBelow.Numerator / (double)simplifiedBelow.Denominator;

        // Round the lowerInches to the MaxDecimalPrecision to prevent floating point errors
        closestImperialFractionModel.lowerInches = Math.Round(closestImperialFractionModel.lowerInches, MaxDecimalPrecision);

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

        var simplifiedAbove = Fractions.LowestCommonDenominator(above, (int)InchPrecision);

        closestImperialFractionModel.UpperImperialFraction = simplifiedAbove;
        closestImperialFractionModel.upperInches = (double)simplifiedAbove.Units + (double)simplifiedAbove.Numerator / (double)simplifiedAbove.Denominator;

        // Round the upperInches to the MaxDecimalPrecision to prevent floating point errors
        closestImperialFractionModel.upperInches = Math.Round(closestImperialFractionModel.upperInches, MaxDecimalPrecision);

        return closestImperialFractionModel;
    }
}