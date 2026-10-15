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