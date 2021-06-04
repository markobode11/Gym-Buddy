# Gym Buddy

Full stack application to learn about training in the gym.

## Running the app.

1. Inside the WebApp directory in backend run command "dotnet run" - backend starts to run and simple ASP.NET pages version launches.
2. Inside the frontend directory run command "npm install".
3. Inside the frontend directory run command "npm start" - frontend client starts to run.

## Admin user

All edit, delete and create functionalities are for the admin user. Also user banning and role management is for admin only.

**Default admin creds:**
```
Email: admin@ttu.ee
Password: Foobar1.
```

## Data init

Data init is provided in the DAL.App.EF folder.

Data init toggle is in appsettings.json.

## Swagger

API is documented with Swagger. Reachable upon running backend.
```
https://localhost:yourport/swagger/
```

## Technologies
* .NET 5
* ASP.NET
* EF Core
* Swagger
* JWT Authentication
* MS SQL
* React.js with typescript

## Author
* **Marko Bode** - [markobode11](https://github.com/markobode11)
