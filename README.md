# Notes
Список всех nuget packages
   * FluentValidation                                    11.10.0     11.10.0
   * FluentValidation.AspNetCore                         11.3.0      11.3.0
   * FluentValidation.DependencyInjectionExtensions      11.10.0     11.10.0
   * MediatR                                             12.4.1      12.4.1
   * MediatR.Extensions.FluentValidation.AspNetCore      5.1.0       5.1.0
   * Microsoft.EntityFrameworkCore                       8.0.8       8.0.8
   * Microsoft.EntityFrameworkCore.Design                8.0.8       8.0.8
   * Microsoft.OpenApi                                   1.6.21      1.6.21
   * Moq                                                 4.20.72     4.20.72
   * Npgsql.EntityFrameworkCore.PostgreSQL               8.0.4       8.0.4
   * Swashbuckle.AspNetCore                              6.7.3       6.7.3
   * xunit                                               2.9.1       2.9.1
* В файле appsettings.json необходимо установить свою строку подключения к бд PostgreSQL.
* Для настройки миграций `dotnet ef migrations add {Имя}`
* Для инициализации базы данных `dotnet ef database update`
* Запуск производится в MS VS (http, https) или в powershell `dotnet run`
* Не реализованы unit-test.
