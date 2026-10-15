
CREATE PROCEDURE sp_EditarCategoria(
    @IdCategoria int,
    @Descripcion varchar(50),
    @Estado bit,
    @Resultado int OUTPUT,
    @Mensaje varchar(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 1
    SET @Mensaje = ''

    IF NOT EXISTS (
        SELECT * FROM AP_categoria 
        WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria
    )
    BEGIN
        UPDATE AP_categoria
        SET Descripcion = @Descripcion,
            Estado = @Estado
        WHERE IdCategoria = @IdCategoria
    END
    ELSE
    BEGIN
        SET @Resultado = 0
        SET @Mensaje = 'Ya existe otra categoría con la misma descripción.'
    END
END