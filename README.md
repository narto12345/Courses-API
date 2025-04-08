# Courses-API

## Tabla de Contenido

- [Introducción](#introducción)
- [Modelo de datos](#modelo-de-datos)
  - [Explicación de entidades y sus relaciones](#explicación-de-entidades-y-sus-relaciones)
  - [Entidades](#entidades)
  - [Explicación](#explicación)
- [¿Cómo levantar el proyecto?](#cómo-levantar-el-proyecto)
  - [Requisitos](#requisitos)
- [Servicios Web](#servicios-web)
  - [1. User](#1-user)
    - [1.1 Obtener todos los usuarios](#11-obtener-todos-los-usaurios)
    - [1.2 Obtener usuario por id](#12-obtener-usaurio-por-id)
    - [1.3 Crear usuario](#13-crear-usuario)
    - [1.4 Actualizar usuario](#14-actualizar-usuario)
    - [1.5 Eliminar usuario](#15-eliminar-usuario)
  - [2. Detail](#2-detail)
    - [2.1 Obtener todos los detalles](#21-obtener-todos-los-detalles)
    - [2.2 Obtener detalle por id](#22-obtener-detalle-por-id)
    - [2.3 Crear un detalle a un usuario](#23-crear-un-detalle-a-un-usuario)
    - [2.4 Actualizar un detalle de un usuario](#24-actualizar-un-detalle-de-un-usuario)
    - [2.5 Eliminar un detalle de un usuario](#25-eliminar-un-detalle-de-un-usuario)

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

| Parámetro | Tipo   | Descripción                     | Obligatorio |
| --------- | ------ | ------------------------------- | ----------- |
| userId    | number | Identificador único del usuario | Si          |

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

#### 1.4 Actualizar usuario

Este servicio utiliza el estandar **RFC 6902** o también conocido como JSON Patch, que basicamente nos indica cómo debe ser la estructura del cuerpo
de la petición HTTP que va a indicar los cambios que deseamos hacer sobre una entidad, ejemplo:

```json
[
  { "op": "replace", "path": "/lastname", "value": "Gomez" },
  { "op": "replace", "path": "/name", "value": "Duvan" }
]
```

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deeen realizar modificaciones, por lo tanto
no es necesario especificar todos los atributos de la entidad.

```
PATCH https://localhost:7081/api/users{userId}
```

- **Request Body**

```json
[{ "op": "replace", "path": "/userName", "value": "BironTaslima" }]
```

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolvera un cuerpo, si no que solamente un estado 204 No Content.

#### 1.5 Eliminar usuario

- **Endpoint**

```
DELETE https://localhost:7081/api/users/{userId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                     | Obligatorio |
| --------- | ------ | ------------------------------- | ----------- |
| userId    | number | Identificador único del usuario | Si          |

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolvera un cuerpo, si no que solamente un estado 204 No Content.

### 2. Detail

#### 2.1 Obtener todos los detalles

- **Endpoint**

GET https://localhost:7081/api/details

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
[
  {
    "id": 5,
    "email": "vares_gamer@hotmail.com",
    "address": "calle 80",
    "birthdate": "1999-10-13"
  },
  {
    "id": 1002,
    "email": "biron@email.com",
    "address": "calle 80",
    "birthdate": "2000-02-02"
  }
]
```

| Atributo  | Tipo   | Descripción                                 |
| --------- | ------ | ------------------------------------------- |
| id        | number | Identificador único del detalle del usuario |
| email     | text   | Dirreción de correo electrónico             |
| address   | text   | Dirreción física                            |
| birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd` |

#### 2.2 Obtener detalle por id

- **Endpoint**

```
GET https://localhost:7081/api/details/{detailId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                                         | Obligatorio |
| --------- | ------ | --------------------------------------------------- | ----------- |
| detailId  | number | Identificador único del detalle del usuario usuario | Si          |

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
{
  "id": 5,
  "email": "vares_gamer@hotmail.com",
  "address": "calle 80",
  "birthdate": "1999-10-13"
}
```

| Atributo  | Tipo   | Descripción                                 |
| --------- | ------ | ------------------------------------------- |
| id        | number | Identificador único del detalle del usuario |
| email     | text   | Dirreción de correo electrónico             |
| address   | text   | Dirreción física                            |
| birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd` |

#### 2.3 Crear un detalle a un usuario

- **Endpoint**

```
POST https://localhost:7081/api/details
```

- **Request Body**

```json
{
  "email": "biron@email.com",
  "address": "calle 80",
  "birthdate": "2000-02-02",
  "UserIdFk": 3
}
```

| Parámetro | Tipo   | Descripción                                                         | Obligatorio |
| --------- | ------ | ------------------------------------------------------------------- | ----------- |
| email     | text   | Dirreción de correo electrónico                                     | No          |
| address   | text   | Dirreción física                                                    | Si          |
| birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd`                         | No          |
| UserIdFk  | number | Clave foránea que indica a qué usuario va a pertenecer este detalle | Si          |

- **Respuesta exitosa (Ejemplo) 201 Created**

```json
{
  "id": 1003,
  "email": "biron@email.com",
  "address": "calle 80",
  "birthdate": "2000-02-02"
}
```

| Atributo  | Tipo   | Descripción                                 |
| --------- | ------ | ------------------------------------------- |
| id        | number | Identificador único del detalle del usuario |
| email     | text   | Dirreción de correo electrónico             |
| address   | text   | Dirreción física                            |
| birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd` |

#### 2.4 Actualizar un detalle de un usuario

Este servicio utiliza el estandar **RFC 6902** o también conocido como JSON Patch, que basicamente nos indica cómo debe ser la estructura del cuerpo
de la petición HTTP que va a indicar los cambios que deseamos hacer sobre una entidad, ejemplo:

```json
[
  { "op": "replace", "path": "/lastname", "value": "Gomez" },
  { "op": "replace", "path": "/name", "value": "Duvan" }
]
```

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deeen realizar modificaciones, por lo tanto
no es necesario especificar todos los atributos de la entidad.

```
PATCH https://localhost:7081/api/details/{detailId}
```

- **Request Body**

```json
[
  { "op": "replace", "path": "/address", "value": "nueva dirección" },
  { "op": "replace", "path": "/email", "value": "nuevo@correo.com" }
]
```

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolvera un cuerpo, si no que solamente un estado 204 No Content.

#### 2.5 Eliminar un detalle de un usuario

- **Endpoint**

```
DELETE https://localhost:7081/api/details/{detailId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                                 | Obligatorio |
| --------- | ------ | ------------------------------------------- | ----------- |
| detailId  | number | Identificador único del detalle del usuario | Si          |

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolvera un cuerpo, si no que solamente un estado 204 No Content.
