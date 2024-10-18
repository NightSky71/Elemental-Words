<h1>Elemental Words</h1>

This readme details the problem, my approach to solving it, the solution, and a couple of closing thoughts.

<h2>The Problem</h2>

Each element of the periodic table has a symbol associated with it that can be used to construct a word. For example the word `"yes"` which can be created using Yttrium (`Y`) and Einsteinium (`Es`).

Some words can have multiple solutions the word `snack` for example can be made from 3 distinct sequences of periodic element symbols.

```
----------------------------------------------------
|       1        |       2        |       3        |
|---------------------------------------------------
| Sulfur    (S)  | Sulfur    (S)  | Tin       (Sn) |
| Nitrogen  (N)  | Sodium    (Na) | Actinium  (Ac) |
| Actinium  (Ac) | Carbon    (C)  | Potassium (K)  |
| Potassium (K)  | Potassium (K)  |                |
----------------------------------------------------
```

- Write a standalone solution that can be accept a word and output each sequence of periodic element symbols that can create that word.
- Must be case insensitive
- Periodic Element Symbols can have length 1, 2 or 3

Link to the codewars page here: https://www.codewars.com/kata/56fa9cd6da8ca623f9001233

<h2>First Thoughts</h2>
So before I actually write anything I've had a think about the problem and came up with the following ideas:

- Use a dictionary to store the periodic symbols and periodic element names
  - I found a JSON resource with all 118 elements from here https://github.com/therealarfu/Periodic-Table-JSON/blob/main/tables/Types/Types.json. 
  - _Note this is more up-to-date list of elements since elements 113 to 118 have been named and do have elemental symbols, this also changes some of criteria since now all known elements have 1 or 2 characters in their symbols_
- For the algorithm:
  - find a match in the first characters of the string with either an element symbol length of 1 or 2 
  - If a match is found, create a substring from the word by removing the matching element characters
    - Then take this remaining string then recursively work with a smaller and smaller sub string performing the same operation.
  - If a match isn't found then return


<h2>The Program</h2>

- A project called `ElementalWords` which compiles to a simple console application. It contains a service called `ElementalWordsService` that performs the elemental words conversion.
- Test project called `ElementalWords.Tests.Unit` which I used to validate the `ElementalWordsService`


<h2>Closing Thoughts</h2>

Overall I'm pretty happy with the solution it's fairly readable and can cope with many different conditions. 

There is a potential optimisation that can be made with substrings that crop up more than once.
- For example the elmental words that make up `ack` in `snack` will be calculated twice. 
So perhaps the algorithm can record the elemental words for the substring `ack` into a dictionary to used again later

