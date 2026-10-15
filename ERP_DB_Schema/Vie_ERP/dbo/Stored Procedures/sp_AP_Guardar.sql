-- Guardar nuevo usuario
CREATE PROC sp_AP_Guardar
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(50),
@Clave VARCHAR(50)
AS
BEGIN
	INSERT INTO AP_usuario (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
	VALUES (@Documento, @NombreCompleto, @Correo, @Clave, 2, 0)
END