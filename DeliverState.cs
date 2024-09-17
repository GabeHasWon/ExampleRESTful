using ExampleRESTful.Controllers;

namespace ExampleRESTful;

/// <summary>
/// Handles a given state of two jugs, how full each are, and the target.<br/>
/// This is used by <see cref="JugStateController.DeliverInfo(DeliverState)"/> to package a query.
/// </summary>
public class DeliverState
{
    /// <summary>
    /// Capacity of the left jug.
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// Capacity of the right jug.
    /// </summary>
    public int Right { get; set; }

    /// <summary>
    /// Target amount to be in either jug.
    /// </summary>
    public int Target { get; set; }
}
