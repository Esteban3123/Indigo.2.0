CREATE PROC GESTOR_GUARDAR_EMPLEADOS
@cNombre varchar(50),
@cDireccion varchar(50),
@dFecha_Nacimiento date,
@cTelefono varchar(50),
@nSalario money,
@nIdDepartamento int,
@nIdCargo int
AS
	INSERT INTO TBL_empleados(
		nombre_empleado,
		direccion_empleado,
		fecha_nacimiento_empleado,
		telefono_empelado,
		salario_empleado,
		id_departamento,
		id_cargo,
		activo_empleado)

	VALUES(
		@cNombre,
		@cDireccion,
		@dFecha_Nacimiento,
		@cTelefono,
		@nSalario,
		@nIdDepartamento,
		@nIdCargo,
		1)

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo empleado en la tabla de empleados, marcándolo como activo por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proporcionarse los datos personales y laborales del empleado (nombre, dirección, fecha de nacimiento, teléfono, salario, departamento y cargo); El departamento y cargo referenciados deben existir si hay integridad referencial definida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo empleado creado por este procedimiento queda registrado como activo (activo_empleado = 1).; No se valida duplicidad ni existencia previa antes del INSERT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; departamento; cargo; salario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.TBL_empleados: Siempre inserta un nuevo registro de empleado con activo_empleado=1 (alta activa por defecto).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GESTOR_GUARDAR_EMPLEADOS';
-- GO
