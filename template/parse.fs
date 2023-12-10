module day_xx.parse

type ParseError = InvalidInputData

type Input = string list

let parse: string seq -> Result<Input, ParseError> = List.ofSeq >> Ok