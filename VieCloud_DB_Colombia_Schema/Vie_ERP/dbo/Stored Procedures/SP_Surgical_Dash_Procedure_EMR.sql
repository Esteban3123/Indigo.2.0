
CREATE PROCEDURE [dbo].[SP_Surgical_Dash_Procedure_EMR]
(
    @AttentionCenter CHAR(20),
    @Profesional CHAR(20),
    @Page int,
    @PageSize int
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    IF @Page < 1 SET @Page = 1;
    IF @PageSize < 1 SET @PageSize = 20;
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    SELECT *
    FROM [ViewSurgicalProcedureEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consulta paginada sobre la vista `ViewSurgicalProcedureEMR` que retorna procedimientos quirúrgicos registrados en el EMR, filtrando por centro de atención y profesional, ordenados por fecha de solicitud descendente. Implementa paginación mediante OFFSET/FETCH con valores predeterminados de página 1 y tamaño 20 si los parámetros son inválidos. Está orientada a alimentar un dashboard quirúrgico con los procedimientos asociados a un profesional específico en un centro determinado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, paginado y ordenado por fecha de solicitud descendente, el listado de procedimientos quirúrgicos del EMR correspondientes a un centro de atención y profesional dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben suministrarse los identificadores de centro de atención y profesional para filtrar; La vista de procedimientos quirúrgicos EMR debe existir y ser consultable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Resultados siempre filtrados por centro de atención y profesional indicados; Resultados ordenados por fecha de solicitud descendente (más recientes primero); Paginación con valores mínimos garantizados (Page≥1, PageSize≥1, default 20); Offset calculado como (Page-1)*PageSize', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento quirúrgico; Centro de atención; Profesional; Solicitud (fecha); EMR (Historia Clínica Electrónica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewSurgicalProcedureEMR: Cuando se filtra por centro de atención y profesional, se retorna la página solicitada de procedimientos ordenados por FechaSolicitud DESC usando OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Page < 1 → Se normaliza Page a 1; si PageSize < 1 → Se normaliza PageSize a 20', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewSurgicalProcedureEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Surgical_Dash_Procedure_EMR';
-- GO
