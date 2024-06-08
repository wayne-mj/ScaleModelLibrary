using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace WMJ.ScaleModelLibrary.FractionSystem;

public static class Fractions
{
    /// <summary>
    /// Method to add two fractions
    /// </summary>
    /// <param name="firstNumerator"></param>
    /// <param name="firstDenominator"></param>
    /// <param name="secondNumerator"></param>
    /// <param name="secondDenominator"></param>
    /// <returns></returns>
    public static FractionModel FractionAdd(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator)
    {
        FractionModel fraction;
        long lNumerator;
        long lDenominator;

        // Check for divide by zero
        if (firstDenominator == 0 || secondDenominator == 0)
        {
            fraction = new()
            {
                Status = "Divide by zero error"
            };
            return fraction;
        }

        // Check if the fractions have the same denominator
        if (firstDenominator != secondDenominator)
        {
            // Cross multiply
            lNumerator = (long)firstNumerator * (long)secondDenominator + (long)secondNumerator * (long)firstDenominator;
            lDenominator = (long)firstDenominator * (long)secondDenominator;

            // Check for integer overflow
            if (lNumerator > int.MaxValue || lDenominator > int.MaxValue)
            {
                fraction = new()
                {
                    Status = "Integer overflow"
                };
                return fraction;
            }
            else
            {
                fraction = new()
                {
                    Numerator = (int)lNumerator,//(firstNumerator * secondDenominator) + (secondNumerator * firstDenominator),
                    Denominator = (int)lDenominator, //firstDenominator * secondDenominator,
                    Status = "OK"
                };
            }
        }
        else
        {
            // Otherwise just add the fractions
            lNumerator = (long)firstNumerator + (long)secondNumerator;
            lDenominator = (long)firstDenominator;

            // Check for integer overflow
            if (lNumerator > int.MaxValue || lDenominator > int.MaxValue)
            {
                fraction = new()
                {
                    Status = "Integer overflow"
                };
                return fraction;
            }
            else
            {

                fraction = new()
                {
                    Numerator = (int)lNumerator,//firstNumerator + secondNumerator,
                    Denominator = (int)lDenominator, //firstDenominator,
                    Status = "OK"
                };
            }
        }

        return fraction;
    }

    /// <summary>
    /// Method to subtract two fractions
    /// </summary>
    /// <param name="firstNumerator"></param>
    /// <param name="firstDenominator"></param>
    /// <param name="secondNumerator"></param>
    /// <param name="secondDenominator"></param>
    /// <returns></returns>
    public static FractionModel FractionSubtract(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator)
    {
        FractionModel fraction;
        long lNumerator;
        long lDenominator;

        // Check for divide by zero
        if (firstDenominator == 0 || secondDenominator == 0)
        {
            fraction = new()
            {
                Status = "Divide by zero error"
            };
            return fraction;
        }

        // Check if the fractions have the same denominator
        if (firstDenominator != secondDenominator)
        {
            // Cross multiply
            lNumerator = (long)firstNumerator * (long)secondDenominator - (long)secondNumerator * (long)firstDenominator;
            lDenominator = (long)firstDenominator * (long)secondDenominator;

            // Check for integer overflow
            if (lNumerator > int.MaxValue || lDenominator > int.MaxValue || lNumerator < int.MinValue || lDenominator < int.MinValue)
            {
                fraction = new()
                {
                    Status = "Integer overflow"
                };
                return fraction;
            }
            else
            {
                fraction = new()
                {
                    Numerator = (int)lNumerator,
                    Denominator = (int)lDenominator,
                    Status = "OK"
                };
            }
        }
        else
        {
            // Otherwise just add the fractions
            fraction = new()
            {
                Numerator = firstNumerator - secondNumerator,
                Denominator = firstDenominator,
                Status = "OK"
            };


        }

        if (fraction.Numerator == 0)
        {
            fraction.Denominator = 0;
        }


        return fraction;
    }

    /// <summary>
    /// Method to multiply two fractions
    /// </summary>
    /// <param name="firstNumerator"></param>
    /// <param name="firstDenominator"></param>
    /// <param name="secondNumerator"></param>
    /// <param name="secondDenominator"></param>
    /// <returns></returns>
    public static FractionModel FractionMultiply(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator)
    {
        FractionModel fraction;
        long lNumerator;
        long lDenominator;

        // Check for divide by zero
        if (firstDenominator == 0 || secondDenominator == 0)
        {
            fraction = new()
            {
                Status = "Divide by zero error"
            };

            return fraction;

        }
        else
        {
            // Multiply the fractions
            lNumerator = (long)firstNumerator * (long)secondNumerator;
            lDenominator = (long)firstDenominator * (long)secondDenominator;

            // Check for integer overflow
            if (lNumerator > int.MaxValue || lDenominator > int.MaxValue)
            {
                fraction = new()
                {
                    Status = "Integer overflow"
                };
                return fraction;
            }
            else
            {
                fraction = new()
                {
                    Numerator = (int)lNumerator,
                    Denominator = (int)lDenominator,
                    Status = "OK"
                };
            }
        }

        return fraction;
    }

    /// <summary>
    /// Method to divide two fractions
    /// </summary>
    /// <param name="firstNumerator"></param>
    /// <param name="firstDenominator"></param>
    /// <param name="secondNumerator"></param>
    /// <param name="secondDenominator"></param>
    /// <returns></returns>
    public static FractionModel FractionDivide(int firstNumerator, int firstDenominator, int secondNumerator, int secondDenominator)
    {
        // When dividing fractions, invert the second fraction and multiply
        // Why repeat the code when I already have it written?
        if (firstDenominator == 0 || secondDenominator == 0)
        {
            return new FractionModel
            {
                Status = "Divide by zero error"
            };
        }
        else
        {
            return FractionMultiply(firstNumerator, firstDenominator, secondDenominator, secondNumerator);
        }
    }

    /// <summary>
    /// Method to find the lowest common denominator of a fraction
    /// </summary>
    /// <param name="Numerator"></param>
    /// <param name="Denominator"></param>
    /// <returns></returns>
    // public static FractionModel LowestCommonDenominator(int Numerator, int Denominator)
    // {
    //     FractionModel fraction = new();

    //     try
    //     {
    //         int HighestCommonDenominator = 1;
    //         int[] values = new int[2];

    //         // Check for divide by zero or if either value is zero
    //         if (Numerator > 0 || Denominator > 0)
    //         {
    //             for (int _HighestCommonDenominator = 1; _HighestCommonDenominator <= Denominator; _HighestCommonDenominator++)
    //             {
    //                 if ((Numerator % _HighestCommonDenominator == 0) && (Denominator % _HighestCommonDenominator == 0))
    //                 {
    //                     HighestCommonDenominator = _HighestCommonDenominator;
    //                 }
    //             }

    //             values[0] = Numerator / HighestCommonDenominator;
    //             values[1] = Denominator / HighestCommonDenominator;


    //             if (values[0] > values[1])
    //             {
    //                 int Units = Math.Abs(values[0] / values[1]);
    //                 int newNumerator = values[0] - (Units * values[1]);
    //                 fraction.Units = Units;
    //                 fraction.Numerator = newNumerator;
    //                 fraction.Denominator = values[1];
    //             }
    //             else
    //             {
    //                 fraction.Units = 0;
    //                 fraction.Numerator = values[0];
    //                 fraction.Denominator = values[1];
    //             }

    //             fraction.Status = "OK";
    //         }
    //         else
    //         {
    //             fraction.Status = "Divide by zero error";
    //             //fraction.Units = 0;
    //             //fraction.Numerator = 0;
    //             //fraction.Denominator = 0;
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         // Something else has gone wrong here
    //         // Chances are it's an integer overflow
    //         fraction.Status = ex.Message;
    //     }

    //     return fraction;
    // }

    public static FractionModel LowestCommonDenominator(int Numerator, int Denominator)
    {
        FractionModel fraction = new();

        try
        {
            int HighestCommonDenominator = 1;
            int[] values = new int[2];

            // Check for divide by zero or if either value is zero
            if (Numerator == 0 || Denominator == 0)
            {
                return new FractionModel
                {
                    Status = "Divide by zero error"
                };
                
            }
            // Check for integer overflow
            // It is more important for the denominator to be checked
            // as the loop below can wrap around to int.MinValue or become
            // zero suddenly if the denominator is int.MaxValue causing an unexpected
            // divide by zero error.
            else if (Denominator == int.MaxValue)
            {
                return new FractionModel
                {
                    Status = "Integer overflow"
                };
            }
            else
            {
                // Replaced the loop with a recursive function
                // for (int _HighestCommonDenominator = 1; _HighestCommonDenominator <= Denominator; _HighestCommonDenominator++)
                // {
                //     if ((Numerator % _HighestCommonDenominator == 0) && (Denominator % _HighestCommonDenominator == 0))
                //     {
                //         HighestCommonDenominator = _HighestCommonDenominator;
                //     }
                // }

                HighestCommonDenominator = GCDFunction(Numerator, Denominator);

                values[0] = Numerator / HighestCommonDenominator;
                values[1] = Denominator / HighestCommonDenominator;


                if (values[0] > values[1])
                {
                    int Units = Math.Abs(values[0] / values[1]);
                    int newNumerator = values[0] - (Units * values[1]);
                    fraction.Units = Units;
                    fraction.Numerator = newNumerator;
                    fraction.Denominator = values[1];
                }
                else
                {
                    fraction.Units = 0;
                    fraction.Numerator = values[0];
                    fraction.Denominator = values[1];
                }

                fraction.Status = "OK";
            }            
        }
        catch (Exception ex)
        {
            // Something else has gone wrong here
            // other than a divide by zero error or integer overflow,
            // which should have been caught above.
            fraction.Status = ex.Message;
        }

        return fraction;
    }

    /// <summary>
    /// Method to reduce a fraction to its lowest terms
    /// </summary>
    private static int GCDFunction(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        else if (a == 0)
        {
            return b;
        }
        else
        {
            return GCDFunction(b, a % b);
        }
    }


    /// <summary>
    /// Method to create an improper fraction from a whole number and a fraction
    /// </summary>
    /// <param name="iUnit"></param>
    /// <param name="iNumerator"></param>
    /// <param name="iDenominator"></param>
    /// <returns></returns>
    public static FractionModel ImproperFraction(int iUnit, int iNumerator, int iDenominator)
    {
        FractionModel fraction = new();
        
        long lNumerator = (long)(iUnit * iDenominator) + iNumerator;

        if (iDenominator == 0)
        {
            fraction = new()
            {
                Status = "Divide by zero error"
            };
        }        
        else if (iUnit > 0 && iNumerator > 0 && lNumerator < int.MaxValue && lNumerator > 0)
        {
            fraction = new()
            {
                Numerator = (iUnit * iDenominator) + iNumerator,
                Denominator = iDenominator,
                Status = "OK"
            };
        }
        else
        {
            fraction = new()
            {
                Status = "Integer overflow"
            };
        }

        return fraction;
    }
}