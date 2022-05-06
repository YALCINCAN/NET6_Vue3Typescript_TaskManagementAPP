# NET6 with VueJS 3 (Typescript) Composition API (Quasar Framework) Task Management Project

<img alt="TaskManagement" src="assets/taskmanagement.gif"> </img>

**<h2 align="center">Features</p>**

## Backend
- .NET6
- Entity Framework Core – Code First
- Response Wrappers
- CQRS Pattern 
- UnitOfWork Pattern
- Docker
- Automapper
- Repository Pattern
- Net Core Identity with JWT Authentication
- Database Seeding
- Custom Exception Handling Middleware,
- Confirmation Mail
- Logging (Serilog)
- Validation (Fluent Validation) with Aspect (Autofac,Castle.DynamicProxy)
 
## Frontend
- Vue3
- Composition API
- Typescript
- Vuelidate
- Tailwindcss
- Pinia
- Route guards
- Dashboard 

## How To Start .Net API

For api, you must edit the appsettings.json file before typing these commands.
I used postgresql as database 

Docker support added you can start project with docker, first you must look docker compose yaml file and write

```sh
docker compose -f "docker-compose.yml" up -d --build
```

When the project is up, the migrations run automatically, but you can run it manually with the following command.

```sh
dotnet ef database update --context TaskContext --project "DataAccess" --startup-project "WebAPI"
```

After these commands, a database will be created. 


Default User Account : 

```sh
Username : defaultuser
Password : 159357456qW
```


## How To Start Quasar Project


```sh
npm install
quasar dev
```



