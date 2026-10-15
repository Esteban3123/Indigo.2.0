CREATE PROC GESTOR_LISTAR_DEPARTAMENTOS

AS
	SELECT id_departamento,nombre_departamento FROM TBL_departamentos
	WHERE activo_departamento = 1;