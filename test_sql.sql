--1. Сотрудник с максимальной з/п
SELECT id, name FROM employee WHERE salary = (SELECT MAX(salary) FROM employee)
--2. Максимальная длина цепочки руководителей
WITH RECURSIVE tree AS (
  SELECT e.id, e.chief_id, 1 AS level FROM employee e WHERE chief_id IS NULL 
  UNION ALL
  SELECT e.id, e.chief_id, tree.level + 1
  FROM employee e
  JOIN tree  ON e.chief_id = tree.id
)
SELECT MAX(level) AS max_level FROM tree;
--3. Отдел с максимальной суммарной з/п
WITH department_salaries AS 
(SELECT department_id, sum(salary) AS salary FROM employee GROUP BY department_id)
SELECT department_id FROM department_salaries ds
WHERE ds.salary = (SELECT max(salary) FROM department_salaries);
--4. Сотрудник, имя которого начинается на Р, а заканчивается на н. 
--Логично, что в БД с данными сотрудников имя содержится в графе name вместе с фамилией, поэтому запрос пишем для случаев вроде "Иванов Роман"
SELECT * FROM employee WHERE name LIKE "% Р%н"