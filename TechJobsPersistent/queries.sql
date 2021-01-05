--Part 1
--Column 1 is Id and it's type is int
--Column 2 is Name and it's type is longtext
--Column 3 is EmployerID and it's type is int
--Part 2
SELECT name
from techjobs.employers
where location = "St. Louis";
--Part 3
SELECT skills.name, description
FROM skills
WHERE id in (SELECT SkillId from jobskills)
ORDER BY skills.name;
