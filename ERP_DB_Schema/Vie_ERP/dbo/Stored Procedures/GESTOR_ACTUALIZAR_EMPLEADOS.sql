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