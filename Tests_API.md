# Exemples de requêtes pour tester l'API

## Démarrer l'application
```bash
dotnet run
```
L'API sera accessible sur : `http://localhost:5278`

## 📋 Documentation Swagger
Interface Swagger UI interactive : `http://localhost:5278/swagger`

Swagger offre :
- ✅ Documentation complète de tous les endpoints
- ✅ Interface "Try it out" pour tester directement
- ✅ Exemples de requêtes et réponses
- ✅ Schémas des données
- ✅ Codes de statut HTTP

**Recommandation** : Utiliser Swagger UI pour explorer et tester l'API facilement !

## Activités

### Lister toutes les activités
```bash
curl -X GET "http://localhost:5278/api/activities"
```

### Obtenir une activité par ID
```bash
curl -X GET "http://localhost:5278/api/activities/{id}"
```

### Créer une nouvelle activité
```bash
curl -X POST "http://localhost:5278/api/activities" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Cours de yoga",
    "description": "Séance de yoga relaxante",
    "price": 30.0,
    "maxCapacity": 12,
    "startDate": "2024-03-01T10:00:00",
    "endDate": "2024-03-01T11:30:00"
  }'
```

### Modifier une activité
```bash
curl -X POST "http://localhost:5278/api/activities/{id}/update" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Cours de yoga modifié",
    "description": "Séance de yoga pour débutants",
    "price": 25.0,
    "maxCapacity": 15,
    "startDate": "2024-03-01T10:00:00",
    "endDate": "2024-03-01T11:30:00"
  }'
```

### Supprimer une activité
```bash
curl -X POST "http://localhost:5278/api/activities/{id}/delete"
```

## Réservations

### Lister toutes les réservations
```bash
curl -X GET "http://localhost:5278/api/bookings"
```

### Créer une réservation
```bash
curl -X POST "http://localhost:5278/api/bookings" \
  -H "Content-Type: application/json" \
  -d '{
    "activityId": "GUID_DE_L_ACTIVITE",
    "customerEmail": "test@example.com",
    "customerName": "John Doe",
    "numberOfPeople": 2
  }'
```

### Modifier le statut d'une réservation
```bash
curl -X POST "http://localhost:5278/api/bookings/{id}/status" \
  -H "Content-Type: application/json" \
  -d '1'
```

### Annuler une réservation
```bash
curl -X POST "http://localhost:5278/api/bookings/{id}/cancel"
```

## Statistiques

### Nombre total d'activités
```bash
curl -X GET "http://localhost:5278/api/statistics/total-activities"
```

### Revenus totaux
```bash
curl -X GET "http://localhost:5278/api/statistics/revenue"
```

### Rapport complet
```bash
curl -X GET "http://localhost:5278/api/statistics/report"
```

## Rapports Business

### Rapport business complexe
```bash
curl -X GET "http://localhost:5278/api/reports/business"
```

### Statistiques rapides
```bash
curl -X GET "http://localhost:5278/api/reports/quick-stats"
```

## Scénarios de test recommandés

### Test 1 : Performance et concurrence
1. Créer plusieurs réservations simultanément pour la même activité
2. Observer le comportement de l'application sous charge

### Test 2 : Gestion des erreurs
1. Essayer de créer une activité avec des données invalides
2. Tenter d'accéder à des ressources inexistantes
3. Observer les messages d'erreur retournés

### Test 3 : Logique métier
1. Tester les règles de pricing et de réservation
2. Vérifier la gestion des capacités
3. Analyser le comportement des calculs de revenus

### Test 4 : Validation des données
1. Envoyer des requêtes avec des données manquantes ou invalides
2. Tester les limites des validations

Ces tests vous aideront à identifier les problèmes dans l'application et à comprendre son comportement actuel.