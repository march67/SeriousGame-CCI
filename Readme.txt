Pré-requis :
Docker Desktop
Visual Studio
Unity Editor version 
Être alphabète

Cloner le dépôt avec "git clone https://github.com/march67/2D-interface-project.git"

Se déplacer sur la branche "development" qui est la branche la plus stable car pas encore de release pour le main
utiliser la commande "git switch -c development origin/development"

Dans le dossier /docker
- Réaliser une copie de .env.example et nommer la copie en .env
- Si nécessaire, changer le port

Dans le dossier /Apirest/APIRest-2D-interface-project
- Réaliser une copie de appsettings.json.example et nommer la copie en appsettings.json
- Si nécessaire, modifier la section "ConnectionStrings" pour la faire correspondre à la configuration de la BDD du .env


Allumer Docker Desktop
Démarrer le conteneur en allant dans le dossier /Docker
Commandes pour exécuter et vérifier le contenu de la BDD
- docker-compose up -d
- docker ps
- docker exec -it [nameOfContainerUnderThePSCommand] psql -U test -d test
- par défaut le password = test 
- pour vérifier le contenu de la BDD : "\dt"

Construire la BDD avec l'ORM Entity Framework Core
Avec Visual Studio allumer le projet de l'API Rest
Ouvrir la console Tools > NuGet Package Manager > Package Manager Console
Réaliser dans l'ordre les commandes suivantes
- dotnet ef migrations add CloningProject
- dotnet ef migrations remove
- dotnet ef database update

Normalement à ce stade la BDD devrait être construite, faire \dt dans le contenur de PostgreSQL pour voir
si la table Users est présente

Maintenant pour tester l'application
- S'assurer que la BDD est en cours
- S'assurer que l'API Rest est en cours

