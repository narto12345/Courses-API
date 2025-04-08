# Courses-API

## Tabla de Contenido

1. [Introducción](#introducción)
2. [Modelo de datos](#modelo-de-datos)
3. [¿Cómo levantar el proyecto?](#cómo-levantar-el-proyecto)
4. [Modelo de datos](#servicos-web)

## Introducción

Courses-API es una aplicación backend que se encarga de gestionar un sistema
integral de cursos, esta permite realizar diversas operaciones como:

- Crear un curso.
- Añadir lecciones al curso.
- Crear usuarios.
- Registrar usuarios a los cursos.
- Entre otras.

Este proyecto está elaborado con .Net 8 LTS, está compuesto por un API REST contruida
sobre ASP.NET Core y Entity Framework que se conecta a una base de datos Microsoft SQL Server.

A continuación, encontrará una explicación detallada de sus diversos apartados
tanto funcionales como técnicos.

## Modelo de datos

Como podemos apreciar el siguiente diagrama ilustra la relación entre las diferentes
entidades del modelo de datos:

![Modelo relacional de la base de datos](./Public/RelationModel.drawio.svg)

### Explicación de entidades y sus relaciones

### Entidades

- User (Usuario)
- Detail (Detalle)
- Course (Curso)
- Lesson (Lección)
- Registration (Inscripción)

### Explicación

- **User 1:1 Detail (Relación de uno a uno):** Un usuario puede o no tener relacionado un único
  detalle, y a su vez un detalle solo puede tener asociado un único usuario.

- **Course 1:N Lesson (Relación de uno a muchos):** Un curso puede tener asociado una o más
  lecciones y a su vez una lección solo puede estar relacionada a un único curso.

- **User N:N Course (Relación de muchos a muchos):** Un usuario puede estar relacionado a uno o
  más cursos, y de la misma manera un curso puede estar relacionado a uno o más usuarios. Esta relación
  de muchos a muchos requiere de una tabla intermediaria que se encargue de alojar las multiples relaciones,
  esta la tabla es Registration o en español Inscripción.

## ¿Cómo levantar el proyecto?

### Requisitos

- .Net 8
- ASP.NET Core 8
- Contar con una instancia local de un servidor de base de datos Microsft SQL Server
- Un Administrador para la base de datos de SQL Server (Esto es opcional, pero esta herramienta facilita el manejo de la base da datos, para esta explicación se usará Microsoft SQL Server Management Studio)
- Visual Studio o Visual Studio Code (Para esta explicación se usará Visual Studio)

## Servicos Web

### 1. User

#### 1.1 Obtener todos los usaurios

- **Endpoint**

```
GET https://localhost:7081/api/users
```

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
[
  {
    "id": 3,
    "userName": "bironWow",
    "fullName": "Byron Vergara",
    "detail": null
  },
  {
    "id": 4,
    "userName": "varesGamerYT",
    "fullName": "Duvan Vargas",
    "detail": {
      "id": 5,
      "email": "vares_gamer@hotmail.com",
      "address": "calle 80",
      "birthdate": "1999-10-13"
    }
  }
]
```

| Atributo         | Tipo   | Descripción                                             |
| ---------------- | ------ | ------------------------------------------------------- |
| id               | number | Identificador único del usuario                         |
| userName         | text   | Nombre de usuario                                       |
| fullName         | text   | Nombre completo real del usuario                        |
| detail.id        | number | Identificador único del detalle del usuario             |
| detail.email     | text   | Dirreción de correo electrónico del detalle del usuario |
| detail.address   | text   | Dirreción de física electronico del detalle del usuario |
| detail.birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd`             |

#### 1.2 Obtener usaurio por id

- **Endpoint**

```
GET https://localhost:7081/api/users/{userId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción               | Obligatorio |
| --------- | ------ | ------------------------- | ----------- |
| userId    | number | Identificador del usuario | Si          |

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
{
  "id": 4,
  "userName": "varesGamerYT",
  "fullName": "Duvan Vargas",
  "detail": {
    "id": 5,
    "email": "vares_gamer@hotmail.com",
    "address": "calle 80",
    "birthdate": "1999-10-13"
  }
}
```

| Atributo         | Tipo   | Descripción                                             |
| ---------------- | ------ | ------------------------------------------------------- |
| id               | number | Identificador único del usuario                         |
| userName         | text   | Nombre de usuario                                       |
| fullName         | text   | Nombre completo real del usuario                        |
| detail.id        | number | Identificador único del detalle del usuario             |
| detail.email     | text   | Dirreción de correo electrónico del detalle del usuario |
| detail.address   | text   | Dirreción de física electronico del detalle del usuario |
| detail.birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd`             |

#### 1.3 Crear usuario

- **Endpoint**

```
POST https://localhost:7081/api/users
```

- **Request Body**

```json
{
  "username": "narto",
  "name": "Nicolas",
  "lastname": "Sosa"
}
```

| Parámetro | Tipo | Descripción             | Obligatorio |
| --------- | ---- | ----------------------- | ----------- |
| username  | text | Nombre de usuario       | Si          |
| name      | text | Nombre real del usuario | Si          |
| lastname  | text | Apellido del usuario    | No          |

- **Respuesta exitosa (Ejemplo) 201 Created**

```json
{
  "id": 1004,
  "userName": "narto",
  "fullName": "Nicolas Sosa",
  "detail": null
}
```

| Atributo | Tipo   | Descripción                                                                                                                       |
| -------- | ------ | --------------------------------------------------------------------------------------------------------------------------------- |
| id       | number | Identificador único del usuario                                                                                                   |
| userName | text   | Nombre de usuario                                                                                                                 |
| fullName | text   | Nombre completo real del usuario                                                                                                  |
| detail   | object | objeto relacionado al detalle del usuario (Por defecto cuando creamos un usario este valor será nulo, ya que se crea sin detalle) |
