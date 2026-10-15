create proc GESTOR_DESACTIVAR_EMPLEADOS
@nId_Empleado int
as
	update TBL_empleados set activo_empleado = 0
	where id_empleado = @nId_Empleado