# Courses-API

## Tabla de Contenido

- [1. Introducción](#1.-introduccion)
- [2. Modelo relacional](#modelo-relacional)
- [2.1 Explicación de entidades y sus relaciones](##explicacion-de-entidades-y-sus-relaciones)

## 1. Introducción

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


## 2. Modelo Relacional

Como podemos apreciar el siguiente diagrama ilustra la relación entre las diferentes
entidades del modelo de datos:


![Modelo relacional de la base de datos](./Public/RelationModel.drawio.svg)

### 2.1 Explicación de entidades y sus relaciones

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

- **User N:N Course (Relación de muchos a muchos):**  Un usuario puede estar relacionado a uno o
  más cursos, y de la misma manera un curso puede estar relacionado a uno a más usuarios. Esta relación
  de muchos a muchos requiere de una tabla intermediria que se encargue de alojar las multiples relaciones,
  esta la tabla es Registration o en español Inscripción.
  