# RecipeBook (Recipe Management System)

## Overview

This project aims to simplify recipe management.

## Technology Stack

- **Primary Language:** C#

- **Framework:** ASP.NET Core

- **Object-Relational Mapping (ORM):** Entity Framework Core

- **Mapping:** AutoMapper

- **Message Broker:** RabbitMQ

- **Containerization:** Docker

- **Database:** PostgreSQL

## Project Structure

The project structure is organized to ensure clear separation of concerns and maintainability. It includes various layers and components to handle different aspects of the application.

![image](https://github.com/Edo1337/RecipeBook/assets/59211066/c7cabecd-0e0b-4448-a80d-a09f858c5684)

## Example API Calls

Here are a few examples of API calls that can be made to interact with the system:

1. **Get Recipe by ID**
   ```
    GET /api/v1/Recipe/{id}
    {
        "id": 1
    }
   ```

2. **Create a New Recipe**
   ```
    POST /api/v1/Recipe
    {
        "name": "string",
        "description": "string",
        "userId": 0
    }
   ```

3. **Update a Recipe**
   ```
    PUT /api/v1/Recipe
    {
        "id": 0,
        "name": "string",
        "description": "string"
    }
   ```

## Conclusion

This Recipe Management System combines a comprehensive API, robust technology stack, and community-driven development approach to provide a scalable and flexible solution. It is ready for integration and further development to meet the evolving needs of users.




# RU

# Книга рецептов

## Обзор

Цель этого проекта — упростить управление рецептами.

## Технологический стек

- **Основной язык:** C#

- **Фреймворк:** ASP.NET Core

- **Объектно-реляционное отображение (ORM):** Entity Framework Core

- **Маппинг:** AutoMapper

- **Брокер сообщений:** RabbitMQ

- **Контейнеризация:** Docker

- **База данных:** PostgreSQL

## Структура проекта

Структура проекта организована таким образом, чтобы обеспечить четкое разделение задач и удобство обслуживания. Она включает в себя различные слои и компоненты для обработки различных аспектов приложения.

![image](https://github.com/Edo1337/RecipeBook/assets/59211066/c7cabecd-0e0b-4448-a80d-a09f858c5684)

## Примеры вызовов API

Вот несколько примеров вызовов API, которые можно выполнить для взаимодействия с системой:

1. **Получить рецепт по идентификатору**
```
GET /api/v1/Recipe/{id}
{
"id": 1
}
```

2. **Создать новый рецепт**
```
POST /api/v1/Recipe
{
"name": "string",
"description": "string",
"userId": 0
}
```

3. **Обновить рецепт**
```
PUT /api/v1/Recipe
{
"id": 0,
"name": "string",
"description": "string"
}
```

## Заключение

Эта система управления рецептами объединяет в себе комплексный API, надежный стек технологий и подход к разработке, основанный на сообществе, для предоставления масштабируемого и гибкого решения. Она готова к интеграции и дальнейшей разработке для удовлетворения меняющихся потребностей пользователей.
