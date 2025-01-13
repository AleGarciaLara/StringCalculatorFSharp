open System 

let Add (input: string) = 
  if input = "" then 0 
  else 
    try 
      let parts = input.Split(',')
      let num1 = int parts.[0]
      let num2 = int parts.[1]
      num1 + num2 
    with 
      | _ -> 0 

Console.WriteLine"Welcome to the String Calculator! What's your name?"

let name = Console.ReadLine() // we get the user's name

Console.WriteLine($"Hello {name}! Type 2 numbers separated by a comma so we can sum them:")
let input = Console.ReadLine() // we get the numbers 

let result = Add input

Console.WriteLine($"Result: {result}")

