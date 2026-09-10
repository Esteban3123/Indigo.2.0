CREATE PROC GESTOR_LISTAR_EMPLEADOS
@cBusqueda varchar (100) = ''
AS
SELECT id_empleado as ID, nombre_empleado as Nombre, fecha_nacimiento_empleado as Fecha_Nacimiento, direccion_empleado as Direccion, telefono_empelado as Telefono,
		salario_empleado as Salario,
		c.nombre_cargo as cargo, d.nombre_departamento as Departamento
FROM TBL_empleados e
INNER JOIN TBL_departamentos d ON e.id_departamento = d.id_departamento
inner join TBL_cargos c ON e.id_cargo = c.id_cargo
where e.activo_empleado = 1 AND
UPPER(e.nombre_empleado) +
UPPER(d.nombre_departamento)+
UPPER(c.nombre_cargo)
LIKE '%' + UPPER (@cBusqueda) + '%';

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista empleados activos junto con su cargo y departamento, permitiendo filtrar por una cadena de búsqueda que se compara contra la concatenación de nombre, departamento y cargo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros relacionados en las tablas de departamentos y cargos para cada empleado (los INNER JOIN excluyen empleados sin estos vínculos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan empleados marcados como activos (activo_empleado = 1).; La búsqueda es case-insensitive porque se aplica UPPER tanto a los datos como al criterio.; Empleados sin departamento o sin cargo asignado no aparecen en el resultado por el uso de INNER JOIN.; Si el parámetro de búsqueda viene vacío, se devuelven todos los empleados activos (LIKE ''%%'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; cargo; departamento; salario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] TBL_empleados: Devuelve únicamente empleados con activo_empleado = 1 y cuyo nombre, departamento o cargo (concatenados y en mayúsculas) contengan la cadena de búsqueda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.TBL_empleados; dbo.TBL_departamentos; dbo.TBL_cargos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_LISTAR_EMPLEADOS';
-- GO
