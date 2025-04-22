--This is the schema SQL for our tables! :)

-- Drop and recreate the Persons table
IF OBJECT_ID('dbo.Persons', 'U') IS NOT NULL
    DROP TABLE dbo.Persons;

CREATE TABLE dbo.Persons (
    PersonID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL, --N means unicode that support multilingual app.
    ID NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PersonType NVARCHAR(20) NOT NULL, -- 'Professor', 'Student', or 'Staff'
    CreatedAt DATETIME DEFAULT GETDATE()
);


-- Drop and recreate the Professors table
IF OBJECT_ID('dbo.Professors', 'U') IS NOT NULL
    DROP TABLE dbo.Professors;

CREATE TABLE dbo.Professors (
    PersonID INT PRIMARY KEY FOREIGN KEY REFERENCES dbo.Persons(PersonID) ON DELETE CASCADE,
    Department NVARCHAR(100),
    ResearchArea NVARCHAR(255),
    IsTerminalDegree BIT
);

-- Drop and recreate the Students table
IF OBJECT_ID('dbo.Students', 'U') IS NOT NULL
    DROP TABLE dbo.Students;

CREATE TABLE dbo.Students (
    PersonID INT PRIMARY KEY FOREIGN KEY REFERENCES dbo.Persons(PersonID) ON DELETE CASCADE,
    Major NVARCHAR(100),
    GPA FLOAT,
    IsFullTime BIT,
    EnrollmentDate DATE
);

-- Drop and recreate the Staff table
IF OBJECT_ID('dbo.Staff', 'U') IS NOT NULL
    DROP TABLE dbo.Staff;

CREATE TABLE dbo.Staff (
    PersonID INT PRIMARY KEY FOREIGN KEY REFERENCES dbo.Persons(PersonID) ON DELETE CASCADE,
    Position NVARCHAR(100),
    Division NVARCHAR(100),
    IsAdministrative BIT
);