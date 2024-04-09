# Informations globale sur le projet

Ce projet est réalisé dans le cadre de l'UE LIFPROJET à l'université Lyon 1 dont voici le site: https://www.univ-lyon1.fr/
Nos noms sont Romain Soares, Angel Munoz et Anthony Bove. Voici nos numéros étudiants respectifs: p2100189 - p2103110 - p2006692
Voici le lien du projet hebergé sous gitHub,dont son identifiant est BDBCorps3 : https://github.com/Kyutsune/BDBCorps3
Le nom du projet est donc BDBattle dont vous trouverez des explications précises plus bas

# BDBattle

BDBattle est une modélisation distribuée d'un jeu stratégique 
Dans ce jeu on peut choisir deux modes de combats.
Dans le premier on va choisir le nombre d'unités alliés (de type mélé ou distance) et ennemis (de type mélé ou distance).
Les unités vont ensuite se battre sur un terrain avec des statistiques propres prédéfinis.
Le deuxième mode est une bataille qui fonctionne par régiments. En effet comme le premier mode on va indiquer le nombre d'unités. Elle vont ensuite former des régiments puis se battre en groupe contre les unités ennemis.

Le code est écrit en C# et compiler sur le logiciel Unity.

## Organisation des fichiers/répertoires de l'archive

L'organisation des fichiers est la suivante.

Projet_unity/Assets/Animations : Dossier contenant l'ensemble des fichiers d'animations

Projet_unity/Assets/Material : Dossier contenant les couleurs appliquées aux personnages

Projet_unity/Assets/Ressources/Prefabs : Dossier contenant les préfabriqués des unités

Projet_unity/Assets/Scenes : Dossier contenant les différentes scènes de notre jeu

Projet_unity/Assets/Script : Dossier contenant les classes de base du jeu

Projet_unity/Assets/Spritesheet : Dossier des images qui ont été utilisées pour l'UI des menus

Projet_unity/Assets/Terrain : Dossier contenant les données et informations du terrain

## Compilation et Execution du projet

Compilation sous Unity: il suffit de cliquer sur le bouton "Play".

Il est aussi possible de créer un fichier executable à partir d'Unity. Pour se faire il suffit de cliquer sur le volet "File" et
de se rendre sur "Build Settings". Il faut choisir comme Platfrome "Windows, Mac, Linux" et enfin cliquer sur "Build".
Une fois le Dossier exporté on peut lancer l'executable "Projet/Projet test.exe".


## Touches de jeu

- Pour se déplacer dans les différents menus on utilise la souris.
- Pour entrer le nombre d'unités on utilise les touches du clavier.
- une fois en jeu on peut déplacer la caméra avec les touches directionelles, on utilise espace pour monter, shift pour déscendre, et enfin clic droit pour faire une rotation de la caméra.


## Nous contacter

Developpeur:

Romain Soares: romain.soares@etu.univ-lyon1.fr
Angel Munoz: angel.munoz@etu.univ-lyon1.fr
Anthony Bove: anthony.bove@etu.univ-lyon1.fr
