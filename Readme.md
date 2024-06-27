## Scale Model Library

### Background

This library was originally a collection of spread sheets that were created to convert from imperial measurements to metric for modelling and is a rewrite/rework of my previous library that I had worked on.

The spread sheet included tables for HO, OO, and user scale:
- Ruler measurements with increments from 1/16th to 1/4 of an inch between 0 and 12 inches,
- Lookup table for range between 0' 0" to 10' 11 63/64"
- A generated lookup tables up to 36", 48" and 96"

Other spread sheets were added as the need arose including some based on trig points for planning where track curves were to be located, proof formula if a locomotive or piece of rolling stock was going to be supported by a curve or series of curves, calculation requirements for a helix, and the odd will it just plain fit table.

### MetricSystem

Primarily created to convert from the Imperial system to the Metric system this contains a collection of methods and classes to aide in this effort.  It also includes known metric definitions required for comparison and verification.

The limit is **4 decimal places.**  Beyond that, errors can occur.  Most common error is an integer overflow, especially if two or more number when multiplied exceed 2^31.  The next is a floating point error that can have unexpected consequences.  By reducing the precision to what can be measured by ruler or calipers reduces the risk of these errors cropping up.

```c#
ClosestImperialFractionModel ClosestInchFraction(double inches)
```

Taking in the inch value in decimal notation, the method will return the fraction notation.  The return ```ClosestImperialFractionModel``` holds the closest inch fraction notation below the current value, and above the current value.

For example, if the current decimal value is 0.26, then the lowest fraction notation will be 1/4 and the highest will be 17/64 with the current being 13/50 which is the simplified of 2600/10000 (0.26*(10^4)/(10^4))

This is the only method that relies directly on MaxDecimalPrecision for its precision.

```c#
int DecimalPrecision
```

This property allows for the setting of how many digits to the right of the decimal point will be displayed when performing conversions.  The default is 2, and the maximum is 4.

```c#
InchFractions InchPrecision
```

This property ranges from 1/64" to 1/2".

The remaining methods are macros to convert between Imperial to Imperial, Imperial to Metric and Metric to Metric measurements.

### Updates

I have written some updated routines, and modified the code to improve performance.  Additionally, code that I have written in Fortran has been ported across and incorporated into the library.

The mathematics was far easier to implement in Fortran than it was in C#.  Trying to work with List<> in .Net seemed cumbersome and slow compared to arrays in Fortran.  Even coding Linked Lists in Fortran made sense.

A simple test for integer overflow is one line in Fortran, even rounding.  But in .Net, it got complicated fast.

Even floating point errors - The compiler offers a warning that the parentheses are redundant for C# **NO THEY ARE NOT!** Too many times I have had my math break because I didn't put in the parentheses, especially when I am working with fractions.