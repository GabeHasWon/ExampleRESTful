## How to Use
1. Copy source to your machine with Git.
2. Open the ExampleRESTful.sln file.
2. Run the project. This requires ASP.NET to be installed on the local machine.
3. The project will open a browser window, similar to https://localhost:7013/swagger/index.html.
4. Click on the POST window and click Try It Out near the right side of the screen.
5. Enter whatever inputs you want, and click run.
6. Under Responses, the JSON output will be displayed. The request URL is also displayed, which will be similar to https://localhost:7013/JugState?Left=6&Right=2&Target=4.

## Endpoints
This project exposes only one endpoint: JugState.
This endpoint takes in 3 integers from the query string; Left, Right and Target.
Using these, the program will return a JSON output of each step solving the Water Jug Problem given the parameters.

## Algorithm

The algorithm is split into two parts: validation and execution.
The validation checks for any of the following invalid conditions:
	Right is > left, and switches them around if not
	Any of the parameters are <= 0, such that the problem is either impossible or already solved
	Right is < target, which means neither jug can hold the target amount
	The Greatest Common Denominator of left and right % target != 0, which means left/right can't be swapped to meet the target

If the algorithm is invalid, it'll return a state with default parameters aside from the descriptor, which describes the error found.
If the algorithm is valid, it proceeds to the execution stage. At this point, all values are guaranteed to work without looping, crashing or erring.

First, create a left and right store. The left store is always filled to capacity as the first step. The right one is empty.

Repeat the following steps as long as either store is not == the target:
	Move as much liquid from the left store to right store, add the transfer state to the list and increment step
	If either store == target, break and return the steps needed
	When the left store is empty, refill it; add the fill state to list and increment step
	When the right store is full, empty it; add the empty state to list and increment step

This will repeat until the given problem is solved. The algorithm returns the step count, and the states needed, in order, to solve the problem.
This will be run for both sides; so the left and right stores will handle both values at one point.
Finally, the algorithm returns the states according to which one has less steps.

## Examples
For brevity, refer to EXAMPLES.md for some examples.