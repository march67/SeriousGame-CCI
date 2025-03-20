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
-> final("decreaseAllPlayersAllStatsBy50")

=== chosen2 ===
decreaseAllPlayersAllStatsBy50 - text
-> final("decreaseAllPlayersAllStatsBy50")

=== chosen3 ===
increaseAllPlayersAllStatsBy100 - text
-> final("increaseAllPlayersAllStatsBy100")

=== chosen4 ===
increaseAllPlayersAllStatsBy100 - text
-> final("increaseAllPlayersAllStatsBy100")

=== final(value) ===
~ functionToCall = value
-> END
