open System 

let Add (input: string) = 
  if input = "" then 0 
  else 
    try 
      let parts = input.Split(',') // split string
      let allParts = 
          parts
          |> Array.collect (fun x -> x.Split('\\', 'n'))
          |> Array.filter (fun x -> not (String.IsNullOrWhiteSpace(x)))

      let numbers = allParts |> Array.map int // convert the input to numbers

      // check negative numbers
      let negatives = numbers |> Array.filter (fun x -> x < 0)
      if Array.length negatives > 0 then 
        let negativesString = String.Join(", ", negatives)
        raise (new Exception($"Negatives not allowed: {negativesString}"))

      else 
          numbers |> Array.sum // sum the numbers 

    with 
      | ex ->
           printfn "%s" ex.Message // if the input is not a number, return 0
           0

Console.WriteLine"Welcome to the String Calculator! What's your name?"

let name = Console.ReadLine() // we get the user's name

Console.WriteLine($"Hello {name}! Let's sum! \nType numbers separated by a comma. \n(You can use newline characters as well.)")
let input = Console.ReadLine() // we get the numbers 

let result = Add input

Console.WriteLine($"Result: {result}")

