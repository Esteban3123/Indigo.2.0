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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra una nueva categoría garantizando unicidad por descripción y devuelve el identificador generado o un mensaje de error.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La descripción no debe existir previamente en AP_categoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten dos categorías con la misma Descripcion; El resultado es 0 cuando la operación falla por duplicidad; El mensaje queda vacío cuando la inserción es exitosa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Categoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_categoria: Cuando no existe un registro con la misma Descripcion, se inserta la nueva categoría y se retorna el SCOPE_IDENTITY() como resultado; [RETURN_RESULT] dbo.AP_categoria: Cuando ya existe la descripción, se devuelve mensaje ''No se puede repetir la descripción de una categoría'' y Resultado queda en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe categoría con la misma Descripcion → Inserta el registro y asigna el ID generado al resultado else No inserta y devuelve mensaje de descripción duplicada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_categoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RegistrarCategoria';
-- GO
