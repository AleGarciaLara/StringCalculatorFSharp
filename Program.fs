open System 

let Add (input: string) = 
  if input = "" then 0 
  else 
      try 
          let processedInput = input.Replace("\\n", "\n")
          let numbers = 
              if processedInput.StartsWith("//[") then 
                  //get everything between the first [ and \n 
                  let delimitersSection = processedInput.Substring(2, processedInput.IndexOf("\n") - 2)
                  //extract delimiter (skip first 3 char)
                  let delimiters = 
                      delimitersSection.Split([|'['|], StringSplitOptions.RemoveEmptyEntries)
                      |> Array.map (fun d -> d.TrimEnd(']'))
                  //get numbers part after \n
                  let numbersString = processedInput.Substring(processedInput.IndexOf("\n") + 1)

                  //split by all delimiters and convert to int
                  let mutable parts = [| numbersString|]
                  for delimiter in delimiters do
                      parts <- 
                          parts 
                          |> Array.collect (fun part ->
                              part.Split([|delimiter|], StringSplitOptions.RemoveEmptyEntries))
                  parts |> Array.map int
                  //split string by delimiter and convert to int

              // check if input starts with // 

              else if processedInput.StartsWith("//") then
                  let delimiter = processedInput.[2] //get custom delimiter 
                  let numbersString = processedInput.[4..] // get the numbers part
                  numbersString.Split(delimiter) 
                  |> Array.map int //convert to int
              else
                  processedInput.Split([| ',' ; '\n' |], StringSplitOptions.RemoveEmptyEntries)
                  |> Array.map int

      // check negative numbers
          let negatives = numbers |> Array.filter (fun x -> x < 0)
          if Array.length negatives > 0 then 
            let negativesString = String.Join(", ", negatives)
            raise (new Exception($"Negatives not allowed: {negativesString}"))

      //ignore big numbers 
          let validNums = numbers |> Array.filter(fun x -> x <= 1000)
          validNums |> Array.sum
      with 
      | ex ->
          printfn "%s" ex.Message // if the input is not a number, return 0
          0

Console.WriteLine"Welcome to the String Calculator! What's your name?"

let name = Console.ReadLine() // we get the user's name

Console.WriteLine($"Hello {name}! Let's sum! \nType numbers separated by a comma. \nYou can use newline characters as well. \nYou can also use custom delimiters of any length in these formats: \n1. //[delimiter]new line character[numbers...]. \n2. //[delim1][delim2]new line character[numbers...].")
let input = Console.ReadLine() // we get the numbers 

let result = Add input

Console.WriteLine($"Result: {result}")

