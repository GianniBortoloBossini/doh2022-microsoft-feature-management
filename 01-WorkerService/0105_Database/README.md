# 0601 - Worker service with feature flags definition from database

- Creazione il worker service `dotnet new worker -n Doh2022.WorkerService.Database`
- Aggiunta package `dotnet add package Microsoft.FeatureManagement`
- Avvio applicazione `dotnet run`

## Demo
- Vale tutto quanto abbiamo detto per la demo `0101 - Worker service with in-memory feature flags`
- Configurazione spostata nel database SQLite `features.db`
- Nel `Program.cs` viene registrato il servizio
- Non serve più leggere la configurazione: la registrazione del servizio tramite `services.AddFeatureManagement();` recupera la configurazione dal database `features.db`