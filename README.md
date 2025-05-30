# Courses-API (Documentación)

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
  - [3. Course](#3-course)
    - [3.1 Obtener todos los cursos](#31-obtener-todos-los-cursos)
    - [3.2 Obtener curso por id](#32-obtener-curso-por-id)
    - [3.3 Obtener curso por id con usuarios inscritos](#33-obtener-curso-por-id-con-usuarios-inscritos)
    - [3.4 Crear un curso](#34-crear-un-curso)
    - [3.5 Inscribir usuario a curso](#35-inscribir-usuario-a-curso)
    - [3.6 Actualizar un curso](#36-actualizar-un-curso)
    - [3.7 Eliminar un curso](#37-eliminar-un-curso)
    - [3.8 Eliminar inscrición de un usuario en un curso](#38-eliminar-inscrición-de-un-usuario-en-un-curso)
  - [4. Lesson](#4-lesson)
    - [4.1 Obtener todas las lecciones](#41-obtener-todas-las-lecciones)
    - [4.2 Obtener lección por id](#42-obtener-lección-por-id)
    - [4.3 Crear una lección a un curso](#43-crear-una-lección-a-un-curso)
    - [4.4 Actualizar una lección](#44-actualizar-una-lección)
    - [4.5 Eliminar una lección](#45-eliminar-una-lección)

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
- UsersCourses (Tabla de unión)

### Explicación

- **User 1:1 Detail (Relación de uno a uno):** Un usuario puede o no tener relacionado un único
  detalle, y a su vez un detalle solo puede tener asociado un único usuario.

- **Course 1:N Lesson (Relación de uno a muchos):** Un curso puede tener asociado una o más
  lecciones y a su vez una lección solo puede estar relacionada a un único curso.

- **User N:N Course (Relación de muchos a muchos):** Un usuario puede estar relacionado a uno o
  más cursos, y de la misma manera un curso puede estar relacionado a uno o más usuarios. Esta relación
  de muchos a muchos requiere de una tabla intermediaria que se encargue de alojar las multiples relaciones,
  esta tabla es UsersCourses.

## ¿Cómo levantar el proyecto?

### Requisitos

- Git
- .Net 8
- ASP.NET Core 8
- Contar con una instancia local de un servidor de base de datos Microsft SQL Server
- Un Administrador para la base de datos de SQL Server (Esto es opcional, pero esta herramienta facilita el manejo de la base da datos, para esta explicación se usará Microsoft SQL Server Management Studio)
- Visual Studio o Visual Studio Code (Para esta explicación se usará Visual Studio)

### 1. Crear base de datos

Siga los siguientes pasos para crear la base de datos en un instancia local con la ayuda de SQL Server Management Studio:

Abra SQL Server Management Studio e inicie sesión en su instancia local:

![Tutorial-levantar-1](./Public/1.png)

Seleccionamos “New Query”:

![Tutorial-levantar-2](./Public/2.png)

El aplicativo deberá abrir un archivo de texto.

Ejecute el siguiente script para crear la base de datos requerida:

```
CREATE DATABASE CoursesApiDb;
```

![Tutorial-levantar-3](./Public/3.png)

Deberá apreciar un mensaje de éxito que indica que la creación de la base de datos se realizó correctamente.

![Tutorial-levantar-4](./Public/4.png)

**Nota: Antes de continuar asegúrese de tener instalado ASP.NET en su Visual Studio:**

![Tutorial-levantar-5](./Public/5.png)

### 2. Clonar código fuente del proyecto

Abra una interfaz de línea de comandos (CLI) y ubíquese en el directorio en que desea clonar el código fuente del proyecto:

Ejecute el siguiente comando para clonar el proyecto:

```
git clone https://github.com/narto12345/Courses-API.git
```

Git descargará los archivos fuentes del proyecto, y deberá poder visualizar lo siguiente en su gestor de archivos:

![Tutorial-levantar-6](./Public/6.png)

A continuación, abra el archivo “Courses-API.sln” que abrirá automáticamente la solución del proyecto en Visual Studio:

![Tutorial-levantar-7](./Public/7.png)

### 3. Configurar credenciales de la base de datos

Abra el archivo “appsettings.Development.json” y configure los datos de acuerdo con su instancia local de la base de datos SQL Server:

![Tutorial-levantar-8](./Public/8.png)

![Tutorial-levantar-9](./Public/9.png)

### 4. Migrar modelo de datos

Abra la Package Manager Console de Visual Studio:

![Tutorial-levantar-10](./Public/10.png)

### Migración de base de datos

Los siguiente comandos son utilizados para realizar una migración Code First, que consiste en que a partir de un modelo de datos representado por clases de C#,
se construyan las tablas en el motor de bases de datos configurado:

Nota: Estos comandos aplican únicamente para la Package Manager Console de Visual Studio.

- **Crear migración**

```
Add-Migration **Nombre de la migración**
```

- **Implementar migración en la base de datos**

```
Update-Database
```

Una vez se haya efectuado la migración, compile la solución:

![Tutorial-levantar-11](./Public/11.png)

Ejecute el aplicativo:

![Tutorial-levantar-12](./Public/12.png)

Cuando el sistema abra una consola similar a la siguiente, el aplicativo se encontrará encendido y en pleno funcionamiento en modo desarrollo:

![Tutorial-levantar-13](./Public/13.png)

Ahora que el API se encuentra arriba, podrá probar los diversos servicios web que expone.

En el directorio raíz de este proyecto encontrará un archivo llamado “Courses-API.postman_collection.json” el cual representa una colección de POSTMAN con todos endpoints mapeados, y de igual importancia en el README.md encontrará una documentación detallada del cómo usar cada uno de los servicios.

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

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deseen realizar modificaciones, por lo tanto
no es necesario especificar todos los atributos de la entidad.

```
PATCH https://localhost:7081/api/users{userId}
```

- **Request Body**

```json
[{ "op": "replace", "path": "/userName", "value": "BironTaslima" }]
```

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

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

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

### 2. Detail

#### 2.1 Obtener todos los detalles

- **Endpoint**

```
GET https://localhost:7081/api/details
```

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

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deseen realizar modificaciones, por lo tanto
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

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

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

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

### 3. Course

#### 3.1 Obtener todos los cursos

- **Endpoint**

```
GET https://localhost:7081/api/courses
```

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
[
  {
    "id": 1,
    "code": "dbe45d44-9726-4657-9307-4986aa5ca512",
    "name": "Java",
    "description": "Fundamentos de POO",
    "lessons": [
      {
        "id": 1,
        "name": "Sintaxis básica Java",
        "instructorName": "Nicolas Sosa",
        "hours": 10,
        "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
      },
      {
        "id": 2,
        "name": "Programación POO",
        "instructorName": "Angie Zárate",
        "hours": 40,
        "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
      }
    ]
  },
  {
    "id": 3,
    "code": "58f13487-e49f-4fcb-bdce-13a06c6fd40e",
    "name": "JavaScript",
    "description": "Sintaxis básica JavaScript",
    "lessons": []
  }
]
```

| Atributo              | Tipo   | Descripción                                              |
| --------------------- | ------ | -------------------------------------------------------- |
| id                    | number | Identificador único del curso                            |
| code                  | guid   | Código en formato GUID                                   |
| name                  | text   | Nombre de curso                                          |
| description           | text   | Descripción del curso                                    |
| lessons               | array  | Arreglo que contiene las lecciones que componen el curso |
| lesson.id             | number | Identificador único de la lección                        |
| lesson.name           | text   | Nombre de la lección                                     |
| lesson.instructorName | text   | Nombre del instructor de la lección                      |
| lesson.hours          | number | Número de horas de la lección                            |
| lesson.courseCode     | guid   | Código del curso asociado a la lección                   |

#### 3.2 Obtener curso por id

- **Endpoint**

```
GET https://localhost:7081/api/courses/{courseId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                   | Obligatorio |
| --------- | ------ | ----------------------------- | ----------- |
| courseId  | number | Identificador único del curso | Si          |

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
{
  "id": 1,
  "code": "dbe45d44-9726-4657-9307-4986aa5ca512",
  "name": "Java",
  "description": "Fundamentos de POO",
  "lessons": [
    {
      "id": 1,
      "name": "Sintaxis básica Java",
      "instructorName": "Nicolas Sosa",
      "hours": 10,
      "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
    }
  ]
}
```

| Atributo              | Tipo   | Descripción                                              |
| --------------------- | ------ | -------------------------------------------------------- |
| id                    | number | Identificador único del curso                            |
| code                  | guid   | Código en formato GUID                                   |
| name                  | text   | Nombre de curso                                          |
| description           | text   | Descripción del curso                                    |
| lessons               | array  | Arreglo que contiene las lecciones que componen el curso |
| lesson.id             | number | Identificador único de la lección                        |
| lesson.name           | text   | Nombre de la lección                                     |
| lesson.instructorName | text   | Nombre del instructor de la lección                      |
| lesson.hours          | number | Número de horas de la lección                            |
| lesson.courseCode     | guid   | Código del curso asociado a la lección                   |

#### 3.3 Obtener curso por id con usuarios inscritos

- **Endpoint**

```
GET https://localhost:7081/api/courses/{courseId}/users
```

- **Path Params**

| Parámetro | Tipo   | Descripción                   | Obligatorio |
| --------- | ------ | ----------------------------- | ----------- |
| courseId  | number | Identificador único del curso | Si          |

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
{
  "id": 1,
  "code": "dbe45d44-9726-4657-9307-4986aa5ca512",
  "name": "Java",
  "description": "Fundamentos de POO",
  "users": [
    {
      "id": 1,
      "userName": "narto",
      "fullName": "Santiago Jimenez",
      "detail": {
        "id": 4,
        "email": "nicosan12@hotmail.com",
        "address": "Calle 100 b 30-34",
        "birthdate": "2000-02-18"
      }
    },
    {
      "id": 2,
      "userName": "nicosan",
      "fullName": "Nicolas Sosa",
      "detail": null
    }
  ]
}
```

| Atributo               | Tipo   | Descripción                                 |
| ---------------------- | ------ | ------------------------------------------- |
| id                     | number | Identificador único del curso               |
| code                   | guid   | Código en formato GUID                      |
| name                   | text   | Nombre de curso                             |
| description            | text   | Descripción del curso                       |
| users                  | array  | Arreglo de usuarios inscritos en el curso   |
| users.id               | number | Identificador único del usuario             |
| users.userName         | text   | Nombre de usuario                           |
| users.fullName         | text   | Nombre completo del usuario                 |
| users.detail           | object | Objeto del detalle del usuario              |
| users.detail.id        | number | Identificador único del detalle del usuario |
| users.detail.email     | text   | Dirreción de correo electrónico             |
| users.detail.address   | text   | Dirreción física                            |
| users.detail.birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd` |

#### 3.4 Crear un curso

- **Endpoint**

```
POST https://localhost:7081/api/courses
```

- **Request Body**

```json
{
  "name": "Curso de Futbol",
  "description": "Curso básico de futbol"
}
```

| Parámetro   | Tipo | Descripción           | Obligatorio |
| ----------- | ---- | --------------------- | ----------- |
| name        | text | Nombre del curso      | Si          |
| description | text | Descripción del curso | No          |

- **Respuesta exitosa (Ejemplo) 201 Created**

```json
{
  "id": 9,
  "code": "21aecbd8-c0e5-4f8a-8154-0e50ac3d1262",
  "name": "Curso de Futbol",
  "description": "Curso básico de futbol",
  "lessons": []
}
```

| Atributo    | Tipo   | Descripción                                                                                |
| ----------- | ------ | ------------------------------------------------------------------------------------------ |
| id          | number | Identificador único del curso                                                              |
| code        | guid   | Código del curso                                                                           |
| name        | text   | Nombre del curso                                                                           |
| description | text   | Descripción del curso                                                                      |
| lessons     | array  | Lecciones del curso (Esta propiedad siempre está vacía, ya que se acaba de crear el curso) |

#### 3.5 Inscribir usuario a curso

- **Endpoint**

```
POST https://localhost:7081/api/courses/{courseId}/{userId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                                                        | Obligatorio |
| --------- | ------ | ------------------------------------------------------------------ | ----------- |
| courseId  | number | Identificador único del curso al que se desea inscribir un usuario | Si          |
| userId    | number | Identificador único del usuario al que se desea inscribir al curso | Si          |

- **Respuesta exitosa (Ejemplo) 201 Created**

```json
{
  "id": 9,
  "code": "Curso de Futbol",
  "name": "Curso de Futbol",
  "description": "Curso básico de futbol",
  "users": [
    {
      "id": 1,
      "userName": "narto",
      "fullName": "Santiago Jimenez",
      "detail": {
        "id": 4,
        "email": "nicosan12@hotmail.com",
        "address": "Calle 100 b 30-34",
        "birthdate": "2000-02-18"
      }
    }
  ]
}
```

| Atributo               | Tipo   | Descripción                                 |
| ---------------------- | ------ | ------------------------------------------- |
| id                     | number | Identificador único del curso               |
| code                   | guid   | Código en formato GUID                      |
| name                   | text   | Nombre de curso                             |
| description            | text   | Descripción del curso                       |
| users                  | array  | Arreglo de usuarios inscritos en el curso   |
| users.id               | number | Identificador único del usuario             |
| users.userName         | text   | Nombre de usuario                           |
| users.fullName         | text   | Nombre completo del usuario                 |
| users.detail           | object | Objeto del detalle del usuario              |
| users.detail.id        | number | Identificador único del detalle del usuario |
| users.detail.email     | text   | Dirreción de correo electrónico             |
| users.detail.address   | text   | Dirreción física                            |
| users.detail.birthdate | date   | Fecha de nacimiento en formato `yyyy-mm-dd` |

#### 3.6 Actualizar un curso

Este servicio utiliza el estandar **RFC 6902** o también conocido como JSON Patch, que basicamente nos indica cómo debe ser la estructura del cuerpo
de la petición HTTP que va a indicar los cambios que deseamos hacer sobre una entidad, ejemplo:

```json
[
  { "op": "replace", "path": "/lastname", "value": "Gomez" },
  { "op": "replace", "path": "/name", "value": "Duvan" }
]
```

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deseen realizar modificaciones, por lo tanto
no es necesario especificar todos los atributos de la entidad.

```
PATCH https://localhost:7081/api/courses/{courseId}
```

- **Request Body**

```json
[
  {
    "op": "replace",
    "path": "/description",
    "value": "Curso avanzado de futbol"
  }
]
```

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

#### 3.7 Eliminar un curso

- **Endpoint**

```
DELETE https://localhost:7081/api/courses/{courseId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                   | Obligatorio |
| --------- | ------ | ----------------------------- | ----------- |
| courseId  | number | Identificador único del curso | Si          |

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

#### 3.8 Eliminar inscrición de un usuario en un curso

- **Endpoint**

```
DELETE https://localhost:7081/api/courses/{courseId}/users/{userId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                                          | Obligatorio |
| --------- | ------ | ---------------------------------------------------- | ----------- |
| courseId  | number | Identificador único del curso                        | Si          |
| userId    | number | Identificador único del usuario inscrito en el curso | Si          |

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

### 4. Lesson

#### 4.1 Obtener todas las lecciones

- **Endpoint**

```
GET https://localhost:7081/api/lessons
```

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
[
  {
    "id": 1,
    "name": "Sintaxis básica Java",
    "instructorName": "Nicolas Sosa",
    "hours": 10,
    "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
  },
  {
    "id": 2,
    "name": "Programación POO",
    "instructorName": "Angie Zárate",
    "hours": 40,
    "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
  },
  {
    "id": 3,
    "name": "Java Swing",
    "instructorName": "Duvan Vargas",
    "hours": 120,
    "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
  }
]
```

| Atributo       | Tipo   | Descripción                                    |
| -------------- | ------ | ---------------------------------------------- |
| id             | number | Identificador único de la lección              |
| name           | text   | Nombre de la lección                           |
| instructorName | text   | Nombre de instructor que imparte la lección    |
| hours          | number | Horas que compone la lección                   |
| courseCode     | guid   | Código del curso del que hace parte la lección |

#### 4.2 Obtener lección por id

- **Endpoint**

```
GET https://localhost:7081/api/courses/{lessonId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                       | Obligatorio |
| --------- | ------ | --------------------------------- | ----------- |
| lessonId  | number | Identificador único de la lección | Si          |

- **Respuesta exitosa (Ejemplo) 200 Ok**

```json
{
  "id": 1,
  "name": "Sintaxis básica Java",
  "instructorName": "Nicolas Sosa",
  "hours": 10,
  "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
}
```

| Atributo       | Tipo   | Descripción                                    |
| -------------- | ------ | ---------------------------------------------- |
| id             | number | Identificador único de la lección              |
| name           | text   | Nombre de la lección                           |
| instructorName | text   | Nombre de instructor que imparte la lección    |
| hours          | number | Horas que compone la lección                   |
| courseCode     | guid   | Código del curso del que hace parte la lección |

#### 4.3 Crear una lección a un curso

- **Endpoint**

```
POST https://localhost:7081/api/lessons
```

- **Request Body**

```json
{
  "name": "ciencias de halo",
  "instructorName": "Jefe",
  "hours": 3,
  "courseId": 1
}
```

| Parámetro      | Tipo   | Descripción                                      | Obligatorio |
| -------------- | ------ | ------------------------------------------------ | ----------- |
| name           | text   | Nombre de la lección                             | Si          |
| instructorName | text   | Nombre de instructor que imparte la lección      | Si          |
| hours          | number | Número de horas que compone la lección           | Si          |
| courseId       | number | Id único del curso al que se agregará la lección | Si          |

- **Respuesta exitosa (Ejemplo) 201 Created**

```json
{
  "id": 15,
  "name": "ciencias de halo",
  "instructorName": "Jefe",
  "hours": 3,
  "courseCode": "dbe45d44-9726-4657-9307-4986aa5ca512"
}
```

| Atributo       | Tipo   | Descripción                                    |
| -------------- | ------ | ---------------------------------------------- |
| id             | number | Identificador único de la lección              |
| name           | text   | Nombre de la lección                           |
| instructorName | text   | Nombre de instructor que imparte la lección    |
| hours          | number | Horas que compone la lección                   |
| courseCode     | guid   | Código del curso del que hace parte la lección |

#### 4.4 Actualizar una lección

Este servicio utiliza el estandar **RFC 6902** o también conocido como JSON Patch, que basicamente nos indica cómo debe ser la estructura del cuerpo
de la petición HTTP que va a indicar los cambios que deseamos hacer sobre una entidad, ejemplo:

```json
[
  { "op": "replace", "path": "/lastname", "value": "Gomez" },
  { "op": "replace", "path": "/name", "value": "Duvan" }
]
```

Como puede observar el servicio requiere un de arreglo donde especifiquemos los cambios en las propiedades que se deseen realizar modificaciones, por lo tanto
no es necesario especificar todos los atributos de la entidad.

```
PATCH https://localhost:7081/api/lessons/{lessonId}
```

```json
[
  { "op": "replace", "path": "/name", "value": "Sintaxis básica Java 8" },
  { "op": "replace", "path": "/instructorName", "value": "Santaigo Jimenez" }
]
```

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la moficiación se haya ejecutado satifactoriamente el sistema no devolverá un cuerpo, si no que solamente un estado 204 No Content.

#### 4.5 Eliminar una lección

- **Endpoint**

```
DELETE https://localhost:7081/api/lessons/{lessonId}
```

- **Path Params**

| Parámetro | Tipo   | Descripción                       | Obligatorio |
| --------- | ------ | --------------------------------- | ----------- |
| lessonId  | number | Identificador único de la lección | Si          |

- **Respuesta exitosa (Ejemplo) 204 No Content**

Cuando la eliminación se haya ejecutado satifactoriamente el sistema no devolvera un cuerpo, si no que solamente un estado 204 No Content.
