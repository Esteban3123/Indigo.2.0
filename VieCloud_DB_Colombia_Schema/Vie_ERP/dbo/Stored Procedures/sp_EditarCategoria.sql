
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

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza la descripción y estado de una categoría existente, garantizando que no se duplique la descripción con otra categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La categoría identificada debe existir para que el UPDATE tenga efecto; La descripción no debe estar siendo usada por otra categoría distinta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Resultado se inicializa en 1 (éxito) y solo cambia a 0 ante duplicidad de descripción; No se permite tener dos categorías distintas con la misma Descripcion; Nunca se modifica el IdCategoria del registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Categoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_categoria: Cuando no existe otra categoría con la misma Descripcion (distinta al IdCategoria), se actualizan Descripcion y Estado del registro indicado; [RETURN_RESULT] dbo.AP_categoria: Cuando ya existe otra categoría con la misma Descripcion, se retorna Resultado=0 y Mensaje=''Ya existe otra categoría con la misma descripción.'' sin modificar datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe otra categoría (distinto IdCategoria) con la misma Descripcion → Ejecuta UPDATE sobre AP_categoria con la nueva descripción y estado else Aborta la operación, asigna Resultado=0 y mensaje de duplicidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_categoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_EditarCategoria';
-- GO
