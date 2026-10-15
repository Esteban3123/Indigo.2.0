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