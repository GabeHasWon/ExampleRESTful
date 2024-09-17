namespace ExampleRESTful;

/// <summary>
/// Handles a given state of two jugs, how full each are, which step we're on, and the descriptor of the last step taken.
/// </summary>
public class JugState
{
    /// <summary>
    /// Instance used for when an operation doesn't have a valid solution.
    /// </summary>
    public static readonly JugState Invalid = new()
    {
        Descriptor = "No solution possible.",
    };

    /// <summary>
    /// Instance used for when either jug capacity is negative.
    /// </summary>
    public static readonly JugState AnyZeroOrLess = new()
    {
        Descriptor = "Target amount, left jug or right jug are equal to 0 or negative. Problem is impossible or already solved.",
    };

    /// <summary>
    /// Which step this is, starting from 1.<br/>
    /// This is always 0 when an invalid, already solved, or impossible problem is given.
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// Current amount in the left jug.
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// Current amount in the right jug.
    /// </summary>
    public int Right { get; set; }

    /// <summary>
    /// The descriptor of the current step. For example:<code>Transfer from X to Y</code>
    /// </summary>
    public string Descriptor { get; set; } = string.Empty;
}
