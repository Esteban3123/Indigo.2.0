create PROC sp_AP_Editar
@IdUsuario INT,
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(50),
@Clave VARCHAR(50)
AS
BEGIN
	UPDATE AP_usuario
	SET
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave
	WHERE IdUsuario = @IdUsuario
END