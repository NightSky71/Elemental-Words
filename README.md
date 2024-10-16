<h1>Elemental Words</h1>

I'll be using the readme as a place that I can detail my thought process as I develop a solution to solve this problem.

<h2>The Problem</h2>
 
Link to the codewars page here: https://www.codewars.com/kata/56fa9cd6da8ca623f9001233

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

<h2>First Thoughts</h2>
So before I actually write anything I've had a think about the problem and came up with the following ideas:

- Use a dictionary or hash set to store the periodic symbols and periodic element names
- Find a match with either an element symbol length of 1, 2 or 3 and then recursively 

Looking at the example `snack`

```
                                          snack
                    S/                     Sn|                     #\
                  nack                     ack
      N/         Na|        #\
     ack          ck

```

https://github.com/therealarfu/Periodic-Table-JSON/blob/main/tables/Types/Types.json



