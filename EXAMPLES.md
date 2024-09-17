### Left: 2, Right: 9, Target: 5
Output:
[
  {
    "step": 1,
    "left": 0,
    "right": 9,
    "descriptor": "Fill bucket right"
  },
  {
    "step": 2,
    "left": 2,
    "right": 7,
    "descriptor": "Transfer from right to left"
  },
  {
    "step": 3,
    "left": 0,
    "right": 7,
    "descriptor": "Empty bucket left"
  },
  {
    "step": 4,
    "left": 2,
    "right": 5,
    "descriptor": "Transfer from right to left (DONE)"
  }
]

### Left: 15, Right: 3, Target: 4
Output:
[
  {
    "step": 0,
    "left": 0,
    "right": 0,
    "descriptor": "No solution possible."
  }
]

### Left: 28, Right: 3, Target: 13
Output: 
[
  {
    "step": 1,
    "left": 0,
    "right": 28,
    "descriptor": "Fill bucket right"
  },
  {
    "step": 2,
    "left": 3,
    "right": 25,
    "descriptor": "Transfer from right to left"
  },
  {
    "step": 3,
    "left": 0,
    "right": 25,
    "descriptor": "Empty bucket left"
  },
  {
    "step": 4,
    "left": 3,
    "right": 22,
    "descriptor": "Transfer from right to left"
  },
  {
    "step": 5,
    "left": 0,
    "right": 22,
    "descriptor": "Empty bucket left"
  },
  {
    "step": 6,
    "left": 3,
    "right": 19,
    "descriptor": "Transfer from right to left"
  },
  {
    "step": 7,
    "left": 0,
    "right": 19,
    "descriptor": "Empty bucket left"
  },
  {
    "step": 8,
    "left": 3,
    "right": 16,
    "descriptor": "Transfer from right to left"
  },
  {
    "step": 9,
    "left": 0,
    "right": 16,
    "descriptor": "Empty bucket left"
  },
  {
    "step": 10,
    "left": 3,
    "right": 13,
    "descriptor": "Transfer from right to left (DONE)"
  }
]

### Left: -1, Right: 30, Target: 2
Output:
[
  {
    "step": 0,
    "left": 0,
    "right": 0,
    "descriptor": "Target amount, left jug or right jug are equal to 0 or negative. Problem is impossible or already solved."
  }
]