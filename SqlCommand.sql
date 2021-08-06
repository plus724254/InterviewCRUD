SET NOCOUNT ON

GO
CREATE DATABASE [CourseSelection]

GO
CREATE TABLE [dbo].[Student](
	[Number] CHAR(5) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(20) NULL,
	[Birthday] DATETIME NULL,
	[Email] NVARCHAR(50) NULL,
)

GO

CREATE TABLE [dbo].[Course](
	[Number] CHAR(4) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(20) NULL,
	[Credit] INT NULL,
	[Place] NVARCHAR(20) NULL,
	[TeacherName] NVARCHAR(20) NULL,
)
GO

CREATE TABLE [dbo].[CourseSelect](
	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
	[StudentNumber] CHAR(5) NOT NULL,
	[CourseNumber] CHAR(4) NOT NULL,
	FOREIGN KEY (StudentNumber) REFERENCES Student(Number),
	FOREIGN KEY (CourseNumber) REFERENCES Course(Number),
)
GO