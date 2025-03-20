-> main


=== main ===
Mathieu2 a commis une faute grave sur le dépôt git, ce qui a allongé le temps de déploiement de deux jours. Il semble comprendre les conséquences de sa faute, mais sans en être préoccupé.
    + [Le sermonner pour lui faire comprendre qu'il ne doit plus faire cette erreur]
        -> choix("1")
    
    + [Le faire passer une demi-journée à ré-apprendre le versionning git]
        Il ne comprend pas l'intérêt de la formation, il connait git et son erreur est dûe à un manque d'attention. Il perd en motivation, il ne contribuera pas à l'avancée du projet pour le reste de la journée.
        -> choix("2")
        
    + Ne rien lui dire
        Le silence de votre part l'angoisse, il s'attend au pire
        Son stresse augmente.
        -> choix("3")
        
    + Réaliser une réunion de courte durée avec l'ensemble de l'équipe pour en discuter
        Il prend conscience de son erreur, et profite de l'occasion pour présenter ses excuses auprès de tout le monde.
        L'ensemble de l'équipe gagne en motivation.
        -> choix("4")

=== choix(nb) ===
choix {nb}
-> END
