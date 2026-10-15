CREATE PROCEDURE SP_RegistrarCategoria(
    @Descripcion varchar(50),
    @Estado bit,
    @Resultado int OUTPUT,
    @Mensaje varchar(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0
    SET @Mensaje = ''

    IF NOT EXISTS (SELECT * FROM AP_categoria WHERE Descripcion = @Descripcion)
    BEGIN
        INSERT INTO AP_categoria (Descripcion, Estado)
        VALUES (@Descripcion, @Estado)

        SET @Resultado = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        SET @Mensaje = 'No se puede repetir la descripción de una categoría'
    END
END