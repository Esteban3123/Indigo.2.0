
-- Obtener un usuario por ID
CREATE PROC sp_AP_Obtener
@IdUsuario INT
AS
BEGIN
	SELECT * FROM AP_usuario WHERE IdUsuario = @IdUsuario
END