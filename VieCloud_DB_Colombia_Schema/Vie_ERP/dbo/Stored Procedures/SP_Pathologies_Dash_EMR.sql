
CREATE PROCEDURE [dbo].[SP_Pathologies_Dash_EMR]
(
    @AttentionCenter CHAR(20),
    @Profesional CHAR(20),
    @Page int,
    @PageSize int
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Page < 1 SET @Page = 1;
    IF @PageSize < 1 SET @PageSize = 20;
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    SELECT *
    FROM [ViewPathologiesEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consulta patologías registradas en el módulo EMR (Historia Clínica Electrónica) con paginación server-side, filtrando por centro de atención y profesional de salud. Consume la vista `ViewPathologiesEMR` y retorna los registros ordenados por fecha de solicitud descendente, permitiendo navegar listados de patologías desde un dashboard clínico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, paginado, el listado de patologías asociadas a un centro de atención y profesional, ordenadas por fecha de solicitud descendente, para visualización en el dashboard del EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la vista ViewPathologiesEMR accesible; Se requiere el código del centro de atención y del profesional para filtrar los resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La página mínima entregada es 1; El tamaño de página mínimo es 20 cuando el valor recibido es inválido; Los resultados siempre se ordenan por fecha de solicitud descendente; Solo se retornan registros del centro de atención y profesional indicados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Patologías; Centro de atención; Profesional; Fecha de solicitud; EMR (Expediente Médico Electrónico)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewPathologiesEMR: Cuando se filtra por CodigoCentroAtencion y CodigoProfesional, se retorna la página solicitada ordenada por FechaSolicitud DESC usando OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Página solicitada menor a 1 → Se normaliza la página a 1; si Tamaño de página menor a 1 → Se normaliza el tamaño de página a 20 por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewPathologiesEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Pathologies_Dash_EMR';
-- GO
