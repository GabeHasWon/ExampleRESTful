using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTful.Controllers;

[ApiController]
[Route("[controller]")]
public class JugStateController(ILogger<JugStateController> logger) : ControllerBase
{
    private readonly ILogger<JugStateController> _logger = logger;

    /// <summary>
    /// Forwards to <see cref="Get(DeliverState)"/>.
    /// </summary>
    /// <param name="state">The state of the problem to solve.</param>
    [HttpPost(Name = "SendJugState")]
    public IActionResult DeliverInfo([FromQuery] DeliverState state) => RedirectToRoute("SolveJugState", state);

    /// <summary>
    /// Using the given parameters, figures out if there is a solution, the steps to get there, and returns the result.
    /// </summary>
    /// <param name="state">Determines the left and right jug capacities, and the target amount.</param>
    /// <returns>List of <see cref="JugState"/>s, in order of completion.</returns>
    [HttpGet(Name = "SolveJugState")]
    public IEnumerable<JugState> Get([FromQuery] DeliverState state)
    {
        int leftCap = state.Left;
        int rightCap = state.Right;
        int target = state.Target;

        // Any of the parameters is zero;
        // Solution is either already complete or impossible.
        if (leftCap <= 0 || rightCap <= 0 || target <= 0)
            return [JugState.AnyZeroOrLess];

        // Guarantee leftCap is the smaller of the caps
        if (leftCap > rightCap)
            (rightCap, leftCap) = (leftCap, rightCap);

        // No solution is possible.
        // Jugs are either too small to hold the target amount,
        // or the jugs can't get to the value desired.
        int modulo = target % FindGCD(leftCap, rightCap);
        if (target > rightCap || modulo != 0)
            return [JugState.Invalid];

        // Determine the amount of steps required for both orders;
        // then return the states for the shorter one.
        // If leftFirst == rightFirst, rightFirst is returned.
        int leftFirst = SolveProblem(leftCap, rightCap, target, true, out var leftStates);
        int rightFirst = SolveProblem(rightCap, leftCap, target, false, out var rightStates);
        return leftFirst < rightFirst ? leftStates : rightStates;
    }

    /// <summary>
    /// Solves the two jugs problem given the parameters, returning the steps needed and the states for each step.<br/>
    /// When this method runs, all parameters are already validated to be solvable. As such, this method does not handle errors.<br/>
    /// Refer to <see cref="Get(DeliverState)"/> for conditions that would throw or misuse this method.
    /// </summary>
    /// <param name="leftCap">Capacity of the left jug.</param>
    /// <param name="rightCap">Capacity of the right jug.</param>
    /// <param name="target">Target amount to solve for.</param>
    /// <param name="flipLeft">Whether to flip the names of X and Y according to original input.</param>
    /// <param name="states">The <see cref="JugState"/> for every step in a solution.</param>
    /// <returns>The number of steps needed to solve the problem.</returns>
    private static int SolveProblem(int leftCap, int rightCap, int target, bool flipLeft, out List<JugState> states)
    {
        // Used for the bucket names
        const string Bucket_X = "left";
        const string Bucket_Y = "right";
        
        // Initialize values
        states = [];
        int from = leftCap;
        int to = 0;
        int step = 1;

        string fromName = flipLeft ? Bucket_X : Bucket_Y;
        string toName = flipLeft ? Bucket_Y : Bucket_X;

        // Add the initial fill state
        states.Add(new JugState()
        {
            Step = step,
            Descriptor = $"Fill bucket {fromName}",
            Left = to,
            Right = from,
        });

        // Stop when the target is reached.
        while (from != target && to != target)
        {
            // Maximum amount that can be poured
            int pourAmount = Math.Min(from, rightCap - to);

            // Transfer
            to += pourAmount;
            from -= pourAmount;
            step++;

            // Determine if the algorithm is finished.
            bool isDone = from == target || to == target;

            // Add transfer state
            states.Add(new JugState()
            {
                Step = step,
                Descriptor = $"Transfer from {fromName} to {toName}{(isDone ? " (DONE)" : "")}",
                Left = to,
                Right = from,
            });

            if (isDone)
                break; // We have achieved our target

            // If first jug becomes empty, fill it
            if (from == 0)
            {
                from = leftCap;
                step++;

                states.Add(new JugState()
                {
                    Step = step,
                    Descriptor = $"Fill bucket {fromName}",
                    Left = to,
                    Right = from,
                });
            }

            // If second jug becomes full, empty it
            if (to == rightCap)
            {
                to = 0;
                step++;

                states.Add(new JugState()
                {
                    Step = step,
                    Descriptor = $"Empty bucket {toName}",
                    Left = to,
                    Right = from,
                });
            }
        }

        return step;
    }

    /// <summary>
    /// Recursively determines the greatest common denominator (GCD) of two integers.
    /// </summary>
    /// <returns>The GCD of two integers.</returns>
    public static int FindGCD(int a, int b)
    {
        // Catch edge case
        if (a == 0)
            return b;

        // Repeat the recursion to find the GCD
        return FindGCD(b % a, a);
    }
}
