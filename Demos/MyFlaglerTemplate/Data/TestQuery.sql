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


--SELECT Query (SHIFT+TAB for identation)
SELECT 
    p.PersonID,
    p.Name,
    p.ID AS CampusID,
    p.Email,
    p.PersonType,
    s.Major, s.GPA, s.IsFullTime, s.EnrollmentDate,
    pr.Department, pr.ResearchArea, pr.IsTerminalDegree,
    st.Position, st.Division, st.IsAdministrative
FROM Persons p
LEFT JOIN Students s ON p.PersonID = s.PersonID
LEFT JOIN Professors pr ON p.PersonID = pr.PersonID
LEFT JOIN Staff st ON p.PersonID = st.PersonID
ORDER BY p.PersonID ASC;