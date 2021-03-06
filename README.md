# Implementação de uma API rest em .NET Core 2.1

# Integrantes
> Alan Felipe Jantz e Matheus Mahnke

Documentação da api com:
> Swagger

Banco de dados:
> Sql server

Ferramenta ORM:
> Entity Framework Core

# Como rodar o projeto?
Com SqlServer developer e dotnet core 2.1
> Execute "dotnet run" em \WebAPI,
Atente-se à connectionString no AppSettings.json.
O banco é gerado ao iniciar a aplicação.

Porta padrão: 8080

# Como se autenticar?
1. Primeiramente deve cadastrar o usuario em  /RestAPIFurb/Usuarios
2. Irá retornar um usuário com um campo token
```json
{
    "id": 1,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTk5MjEwMjAsImV4cCI6MTU2MDUyNTgyMCwiaWF0IjoxNTU5OTIxMDIwfQ._5P1>K9HhZ86tRpowGx_QlGD6pcns8TEgNpVD5SMi4Z0",
    "email": "exemplo@exemplo.ex"
}
```
3. Adicione o token no header etecedido por "Bearer ". Ex:
```json
  {
    "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTk5MjEwMjAsImV4cCI6MTU2MDUyNTgyMCwiaWF0IjoxNTU5OTIxMDIwfQ._5P1K9HhZ86tRpowGx_QlGD6pcns8TEgNpVD5SMi4Z0"
  }
```

# Como rodar os testes?
>Execute "dotnet test" em \FurbAPI.Integration.Tests
