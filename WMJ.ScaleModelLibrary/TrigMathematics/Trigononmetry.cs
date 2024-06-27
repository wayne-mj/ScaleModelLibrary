using System.Net.NetworkInformation;

namespace WMJ.ScaleModelLibrary.TrigMathematics;

public static partial class Trigonometry
{
    /// <summary>
    /// The value of pi
    /// </summary>
    public static readonly double pi = 4 * Math.Atan(1);
    public static readonly decimal d_pi = 4 * (decimal)Math.Atan(1);

    /// <summary>
    /// Returns the adjacent using the hypotenuse and angle in degrees
    /// </summary>
    /// <param name="hyp"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double Adj_hyp_ang_deg(double hyp, double degrees) => hyp * Math.Cos(degrees * pi / 180.0);

    /// <summary>
    /// Returns the opposite using the hypotenuse and angle in degrees
    /// </summary>
    /// <param name="hyp"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double Opp_hyp_ang_deg(double hyp, double degrees) => hyp * Math.Sin(degrees * pi / 180.0);

    /// <summary>
    /// Returns the hypotenuse using the adjacent and angle in degrees
    /// </summary>
    /// <param name="adj"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double Hyp_adj_ang_deg(double adj, double degrees) => adj / Math.Cos(degrees * pi / 180.0);

    /// <summary>
    /// Returns the hypotenuse using the opposite and angle in degrees
    /// </summary>
    /// <param name="opp"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double Hyp_opp_ang_deg(double opp, double degrees) => opp / Math.Sin(degrees * pi / 180.0);
}