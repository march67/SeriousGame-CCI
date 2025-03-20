INCLUDE ../globals.ink

-> main

=== main ===
Minusha0 ?
    + [Charmander]
        -> chosen1
    + [Charmander]
        -> chosen2
    + [Charmander]
        -> chosen3
    + [Charmander]
        -> chosen4
        
=== chosen1 ===
Choosen 1
-> final("10")

=== chosen2 ===
Choosen 2
-> final("20")

=== chosen3 ===
Choosen 3
-> final("30")

=== chosen4 ===
Choosen 4
-> final("40")

=== final(value) ===
VAR nb = 600
~ pokemon_name = value
-> END
