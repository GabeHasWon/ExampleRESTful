using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTful.Controllers;

[ApiController]
[Route("[controller]")]
public class JugStateController(ILogger<JugStateController> logger) : ControllerBase
{
    private readonly ILogger<JugStateController> _logger = logger;

    [HttpGet(Name = "SolveJugState")]
    public IEnumerable<JugState> Get([FromQuery] int leftCap, [FromQuery] int rightCap, [FromQuery] int target)
    {
        // Guarantee leftCap is the smaller of the caps
        if (leftCap > rightCap)
            (rightCap, leftCap) = (leftCap, rightCap);

        // No solution is possible.
        // Jugs are either too small to hold the target amount,
        // or the jugs can't get to the value desired.
        if (target > rightCap || ExtendedEuclidian(leftCap, rightCap, out _, out _) % target == 0)
        {
            return [new JugState()
            {
                Descriptor = "No solution possible."
            }];
        }

        // Determine the amount of steps required for both orders;
        // then return the states for the shorter one.
        // If leftFirst == rightFirst, rightFirst is returned.
        int leftFirst = Transfer(leftCap, rightCap, target, out var leftStates);
        int rightFirst = Transfer(rightCap, target, target, out var rightStates);
        return leftFirst < rightFirst ? leftStates : rightStates;
    }

    private static int Transfer(int leftCap, int rightCap, int target, out List<JugState> states)
    {
        // Initialize values
        states = [];
        int from = leftCap;
        int to = 0;
        int step = 1;

        // Add the initial fill state
        states.Add(new JugState()
        {
            Step = step,
            Descriptor = "Fill",
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

            // Add transfer state
            states.Add(new JugState()
            {
                Step = step,
                Descriptor = $"Transfer",
                Left = to,
                Right = from,
            });

            if (from == target || to == target)
                break; // We have achieved our target

            // If first jug becomes empty, fill it
            if (from == 0)
            {
                from = leftCap;
                step++;

                states.Add(new JugState()
                {
                    Step = step,
                    Descriptor = "Fill",
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
                    Descriptor = "Empty",
                    Left = to,
                    Right = from,
                });
            }
        }

        return step;
    }

    /// <summary>
    /// Determines the greatest common denominator (GCD) of two integers, alongside updating the integer coefficients for both.
    /// </summary>
    /// <returns>The GCD of two integers.</returns>
    public static int ExtendedEuclidian(int a, int b, out int x, out int y)
    {
        // Catch edge case
        if (a == 0)
        {
            x = 0;
            y = 1;
            return b;
        }

        // Repeat the recursion to find the GCD, and find the new coefficients along the way
        int gcd = ExtendedEuclidian(b % a, a, out int newX, out int newY);

        // Finalize the coefficients
        x = newY - b / a * newX;
        y = newX;
        return gcd;
    }
}
