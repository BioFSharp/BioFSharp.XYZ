# BioFSharp.XYZ

![Logo](./img/Logo_large.png)

you can write markdown in these comment brackets `(** ... *)`

```fsharp
// and actual F# content in a literate script file
let a = 42
```

include evaluations like this:

```
42
```

Include the compiled binaries of your project to use in the docs like this.
Make sure to load any external references via nuget.

```fsharp
#r "nuget: BioFSharp, 2.0.0"
#I "../src/BioFSharp.XYZ/bin/Release/netstandard2.0"
#r "BioFSharp.XYZ.dll"

open BioFSharp.XYZ

let o = BioTalk.helloBio "Kev"
```

```
"Hello Kev the average mass of your name as AminoAcid code is 356.42"
```

Read more about documentation with fsdocs here: [https://fsprojects.github.io/FSharp.Formatting/](https://fsprojects.github.io/FSharp.Formatting/)
