create proc GESTOR_ACTUALIZAR_EMPLEADOS
@nId_Empleado int,
@cNombre varchar(50),
@cDireccion varchar(50),
@dFecha_Nacimiento date,
@cTelefono varchar(50),
@nSalario money,
@nIdDepartamento int,
@nIdCargo int

as
	update TBL_empleados set nombre_empleado = @cNombre,
	direccion_empleado = @cDireccion,
	fecha_nacimiento_empleado = @dFecha_Nacimiento,
	telefono_empelado = @cTelefono,
	salario_empleado = @nSalario,
	id_departamento = @nIdDepartamento,
	id_cargo = @nIdCargo

where id_empleado = @nId_Empleado

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos personales, salariales y la asignación de departamento y cargo de un empleado existente identificado por su id.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en TBL_empleados con id_empleado igual al identificador suministrado; de lo contrario la operación no afecta filas.; Los identificadores de departamento y cargo deben ser válidos para mantener integridad referencial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se modifica el empleado cuyo identificador coincide con el parámetro recibido (filtro por id_empleado).; La actualización reemplaza simultáneamente datos personales (nombre, dirección, fecha de nacimiento, teléfono), salario y asignación organizacional (departamento y cargo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; departamento; cargo; salario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.TBL_empleados: Cuando id_empleado coincide con el identificador recibido, se sobrescriben nombre, dirección, fecha de nacimiento, teléfono, salario, departamento y cargo del empleado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_ACTUALIZAR_EMPLEADOS';
-- GO
