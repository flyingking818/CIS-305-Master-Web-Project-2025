-- Drop foreign key tables first
IF OBJECT_ID('dbo.Professors', 'U') IS NOT NULL
    DROP TABLE dbo.Professors;

IF OBJECT_ID('dbo.Students', 'U') IS NOT NULL
    DROP TABLE dbo.Students;

IF OBJECT_ID('dbo.Staff', 'U') IS NOT NULL
    DROP TABLE dbo.Staff;

-- Then drop the base table
IF OBJECT_ID('dbo.Persons', 'U') IS NOT NULL
    DROP TABLE dbo.Persons;