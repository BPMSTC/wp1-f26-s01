# Web Programming 1 — .NET 10 / Azure starter

Run locally with Docker Desktop: `docker compose up --build`. Open http://localhost:8080.
The default page and `/health` work without a database. `/health/db` requires a connection string.

For database work, create a local `.env` file (ignored by Git):

```dotenv
WP1_CONNECTION_STRING="Server=tcp:YOUR_SERVER.database.windows.net,1433;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

Use your individual credentials from the instructor. In SSMS, select SQL Server authentication,
enter the server, and specify your database in connection properties. Do not connect to `master`.
TLS encryption must be enabled and the server certificate trusted normally (do not bypass validation).

Each student works in their assigned repository. A push to `main` builds using Docker Compose,
pushes to that student's Azure Container Registry repository, and updates their existing Container App.
GitHub authenticates to Azure through OIDC; there is no stored Azure client secret.
The cloud database connection string remains in an Azure Container App secret, not GitHub or this repo.

The Azure deployment uses a preconfigured Container App. Compose defines the local app and image build;
GitHub Actions updates that app's image. Additional Compose services are NOT automatically deployed.
The instructor's earlier Compose-import pilot tested the basic deployment route.

The runtime SQL account can read/write tables. Your personal database account owns your own database.
Schema migrations belong in an explicit development/instructor step, not application startup.

This starter exposes health checks and a landing page only. Add authentication and authorization before
adding private features. Never commit credentials or real student/patient/customer data.

Resources expire after December 20, 2026. Export any database work you need before then.
