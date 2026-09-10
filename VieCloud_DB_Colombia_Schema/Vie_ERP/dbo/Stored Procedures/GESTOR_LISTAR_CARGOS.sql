CREATE PROC GESTOR_LISTAR_CARGOS

AS
	SELECT id_cargo,nombre_cargo FROM TBL_cargos
	WHERE activo_cargo = 1;