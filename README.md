# Test Technique - Senior Software Developer

## Contexte
Cette API de réservation d'activités a été développée rapidement pour un prototype. Votre mission est d'analyser le code, identifier les problèmes potentiels et proposer des améliorations pour rendre l'application robuste et prête pour la production.

## Architecture
Le projet suit une architecture CLEAN avec les couches suivantes :
- **Domain** : Entités et interfaces métier
- **Application** : Cas d'usage, services, DTOs
- **Infrastructure** : Implémentations (repositories en mémoire)  
- **Controllers** : Points d'entrée API

## Fonctionnalités
- Gestion des activités (CRUD)
- Système de réservation avec gestion de capacité
- Statistiques et rapports
- Stockage en mémoire (pas de base de données)

## Votre mission

### 1. Analyse et identification (40 min)
Analyser le code et identifier les problèmes suivants :
- ⚠️ **Problèmes architecturaux** : Violations des principes SOLID, Clean Architecture, séparation des responsabilités
- ⚠️ **Erreurs critiques** : Bugs qui peuvent planter l'application (async/await, thread-safety)
- ⚠️ **Erreurs majeures** : Problèmes de sécurité, performance, ou logique métier
- ⚠️ **Erreurs mineures** : Mauvaises pratiques, code smell

### 2. Correction et amélioration (100 min)
Corriger les erreurs identifiées en respectant :
- Les principes SOLID et Clean Architecture
- Les bonnes pratiques C# et ASP.NET Core
- La séparation appropriée des responsabilités
- La robustesse et la maintenabilité

### 3. Documentation (40 min)
- Lister les problèmes identifiés par ordre de priorité
- Expliquer les solutions apportées
- Proposer des améliorations architecturales supplémentaires

## Critères d'évaluation

### Architecture et Design (40% du score)
- ✅ Identification et correction des violations SOLID
- ✅ Respect de la Clean Architecture
- ✅ Séparation appropriée des responsabilités
- ✅ Proposition d'améliorations structurelles

### Qualité technique (35% du score)
- ✅ Correction des erreurs critiques (async/await, thread-safety, race conditions)
- ✅ Gestion robuste d'erreurs et exceptions
- ✅ Code maintenable et testable
- ✅ Validation des données appropriée

### Bonnes pratiques (25% du score)
- 🎯 Pipeline de validation centralisé
- 🎯 Middleware d'erreur global
- 🎯 Logging structuré et monitoring
- 🎯 Tests unitaires par couche
- 🎯 Patterns architecturaux (CQRS, Domain Services, Unit of Work)

## Livrables
1. Code corrigé avec commits explicites
2. Document listant les problèmes identifiés et les corrections
3. Suggestions d'amélioration architecturale

## Temps estimé
**3-4 heures** au total (niveau Senior Developer avec focus architecture)

## Comment démarrer
1. Analyser la structure du projet et l'architecture
2. Tester l'API avec Swagger (endpoints disponibles)
3. Identifier les problèmes par ordre de criticité
4. Corriger progressivement en testant

## API Documentation
Interface Swagger UI interactive : `http://localhost:5278/swagger`

Pour démarrer l'application :
```bash
dotnet run
```

## Données de test
Le projet contient des données pré-chargées :
- 2 activités (Randonnée, Cours de cuisine)
- Capacités et réservations existantes pour tester la logique métier

Bonne chance ! 🚀