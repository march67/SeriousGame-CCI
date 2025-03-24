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
decreaseAllPlayersAllStatsBy50 - text
-> final("DecreaseAllPlayersAllStatsBy50")

=== chosen2 ===
decreaseAllPlayersAllStatsBy50 - text
-> final("DecreaseAllPlayersAllStatsBy50")

=== chosen3 ===
increaseAllPlayersAllStatsBy100 - text
-> final("IncreaseAllPlayersAllStatsBy100")

=== chosen4 ===
increaseAllPlayersAllStatsBy100 - text
-> final("IncreaseAllPlayersAllStatsBy100")

=== final(value) ===
~ functionToCall = value
-> END
