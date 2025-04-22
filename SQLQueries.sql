-- CREATE DATABASE CoursesApiDb;

USE CoursesApiDb;

SELECT * FROM Users;

-- INSERT INTO Users(UserName,Name,Lastname) VALUES ('narto','Santiago','Jimenez');
-- INSERT INTO Users(UserName,Name,Lastname) VALUES ('nicosan','Nicolas','Sosa');

-- DELETE FROM Users;

SELECT * FROM Details;

-- INSERT INTO Details(Email,Address,Birthdate,UserIdFk) VALUES ('nicosan12@hotmail.com', 'Calle 100 b 30-34', '2000-02-18', 1);

-- DELETE FROM Details;

SELECT * FROM Courses;

/*INSERT INTO Courses(Code,Name,Description)
							VALUES ('dbe45d44-9726-4657-9307-4986aa5ca512', 'Java', 'Curso de Java POO'),
								   ('31fd2eb5-fc22-4834-8ea3-0beb4814d06f', 'C#', 'Curso de C# .NET Core'),
								   ('58f13487-e49f-4fcb-bdce-13a06c6fd40e', 'JavaScript', 'Sintaxis básica JavaScript');*/

SELECT * FROM Lessons;

/*INSERT INTO Lessons (Name,InstructorName,Hours,CourseId) VALUES
														 ('Sintaxis básica Java', 'Nicolas Sosa', 10, 1),
														 ('Programación POO', 'Angie Zárate', 40, 1),
														 ('Java Swing', 'Duvan Vargas', 120, 1),
														 ('Sintaxis básica C#', 'Sergio Blanco', 15, 2),
														 ('Programación POO', 'Byron Vergara', 45, 2);*/
