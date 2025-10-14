# 🚌 Fumobilite

<div align="center">
  <img src="Fumoblilite.Interface/Resources/logo.png" alt="Logo Fumobilite" width="200"/>
  
  **Application de gestion et consultation de réseau de transport en commun**
  
  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
  ![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-blue)
  ![MySQL](https://img.shields.io/badge/MySQL-Database-orange)
</div>

---

## 📋 Table des matières

- [À propos](#-à-propos)
- [Fonctionnalités](#-fonctionnalités)
- [Architecture](#-architecture)
- [Prérequis](#-prérequis)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Utilisation](#-utilisation)
- [Structure du projet](#-structure-du-projet)
- [Technologies utilisées](#-technologies-utilisées)

---

##  À propos

**Fumobilite** est une application Windows Forms complète de gestion et de consultation de réseaux de transport en commun. Elle permet aux utilisateurs de consulter les lignes, les horaires et les arrêts, ainsi que de rechercher des itinéraires. Les administrateurs peuvent gérer l'ensemble du réseau via une interface intuitive.

### Objectifs du projet

- Faciliter la consultation des horaires de transport en commun
- Permettre la recherche d'itinéraires entre deux arrêts
- Offrir des outils de gestion pour les administrateurs
- Proposer une interface utilisateur moderne et conviviale

---

##  Fonctionnalités

###  Pour les utilisateurs

- **Consultation du réseau** : Visualisation des lignes et arrêts disponibles
- **Consultation des horaires** : Affichage des horaires de passage par ligne et arrêt
- **Recherche d'itinéraire** : Calcul d'itinéraires optimaux entre deux points
- **Interface intuitive** : Navigation simple avec contrôles utilisateur dédiés

###  Pour les administrateurs

- **Gestion des lignes** : Création, modification et suppression de lignes
- **Gestion des arrêts** : Administration des arrêts du réseau
- **Gestion des horaires** : Configuration des horaires de passage
- **Gestion des utilisateurs** : Administration des comptes et permissions
- **Historique des actions** : Traçabilité des modifications effectuées

###  Système d'authentification

- Connexion sécurisée avec gestion des rôles
- Inscription de nouveaux utilisateurs
- Différenciation des droits (administrateur/utilisateur)

---

##  Architecture

Le projet suit une architecture en couches (Layered Architecture) avec une séparation claire des responsabilités :

```
┌─────────────────────────────────────┐
│   Couche Présentation (Interface)  │
│     - Windows Forms                 │
│     - UserControls                  │
└─────────────────┬───────────────────┘
                  │
┌─────────────────▼───────────────────┐
│   Couche Métier (Système)           │
│     - Services                       │
│     - Modèles                        │
│     - Interfaces                     │
└─────────────────┬───────────────────┘
                  │
┌─────────────────▼───────────────────┐
│   Couche Données (SQL)               │
│     - Repositories                   │
│     - Gestion Base de Données        │
└─────────────────────────────────────┘
```

---

##  Prérequis

- **Windows** : 7/8/10/11
- **.NET Framework** : 4.7.2 ou supérieur
- **MySQL** : 5.7 ou supérieur
- **Visual Studio** : 2019 ou supérieur (pour le développement)

---

##  Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/clepix21/Fumobilite.git
cd Fumobilite
```

### 2. Restaurer les packages NuGet

Ouvrir la solution dans Visual Studio et restaurer les packages :
- Clic droit sur la solution → **Restaurer les packages NuGet**

Ou via la ligne de commande :
```bash
nuget restore Fumobilite.sln
```

### 3. Configurer la base de données

Créer une base de données MySQL et noter les paramètres de connexion :
- Serveur (ex: `localhost`)
- Utilisateur (ex: `root`)
- Mot de passe
- Nom de la base de données (ex: `fumobilite`)

### 4. Compiler le projet

```bash
msbuild Fumobilite.sln /p:Configuration=Release
```

Ou via Visual Studio : **Build** → **Build Solution** (F6)

---

## Configuration

### Configuration de la base de données

Modifier les paramètres de connexion dans `Program.cs` :

```csharp
// Configuration de la base de données MySQL
string serveur = "localhost";      // Adresse du serveur MySQL
string login = "votre_utilisateur"; // Nom d'utilisateur MySQL
string mdp = "votre_mot_de_passe";  // Mot de passe MySQL
string bd = "fumobilite";           // Nom de la base de données
```

**Note** : L'application créera automatiquement les tables nécessaires au premier lancement si elles n'existent pas.

---

## Utilisation

### Lancement de l'application

1. Exécuter `Fumoblilite.Interface.exe` depuis le dossier `bin/Release` ou `bin/Debug`
2. L'application vérifie automatiquement la connexion à la base de données
3. La fenêtre principale s'affiche après une connexion réussie

### Première connexion

- **Mode invité** : Accès limité aux fonctionnalités de consultation
- **Créer un compte** : Inscription avec nom d'utilisateur et mot de passe
- **Connexion administrateur** : Accès complet aux fonctionnalités de gestion

### Navigation

L'interface est organisée en contrôles utilisateur (UserControls) accessibles via le menu principal :

- 🌐 **Consultation du réseau**
- 🚏 **Consultation des lignes**
- 🕐 **Consultation des horaires**
- 🗺️ **Recherche d'itinéraire**
- ⚙️ **Gestion** (administrateurs uniquement)


---

## Technologies utilisées

### Langage et Framework
- **C#** 
- **.NET Framework 4.7.2** 
- **Windows Forms** 

### Base de données
- **MySQL**
- **MySql.Data** : Connecteur MySQL pour .NET

### Outils de développement
- **Visual Studio** 
- **Git** 
- **NuGet** 

### Patterns et principes
- **Repository Pattern** : Abstraction de l'accès aux données
- **Layered Architecture** : Séparation en couches
- **Dependency Injection** : Injection de dépendances
- **Single Responsibility Principle** : Responsabilité unique

---


## Contact et Support

Pour toute question ou suggestion concernant le projet :

- **GitHub** : [github.com/clepix21/Fumobilite](https://github.com/clepix21/Fumobilite)
- **Issues** : [Signaler un problème](https://github.com/clepix21/Fumobilite/issues)


## Mentions légales

**Logo et ressources graphiques** : Le logo utilisé dans ce projet provient du jeu vidéo *Final Fantasy XIII* développé et publié par Square Enix. Tous les droits relatifs à ce logo et aux personnages de Final Fantasy XIII appartiennent à **Square Enix Co., Ltd.** Ce projet est un projet éducatif à but non lucratif et le logo est utilisé à titre de référence uniquement. Aucune affiliation officielle avec Square Enix n'est revendiquée.
