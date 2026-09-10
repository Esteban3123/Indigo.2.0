
CREATE PROCEDURE [dbo].[SP_Non_Surgical_Procedures_EMR]
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
    FROM [ViewNonSurgicalProcedureEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera de forma paginada los procedimientos no quirúrgicos registrados en el módulo EMR, filtrando por centro de atención y profesional de salud. Consulta la vista `ViewNonSurgicalProcedureEMR` ordenando los resultados por fecha de solicitud descendente, devolviendo el bloque de filas correspondiente a la página solicitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada de procedimientos no quirúrgicos solicitados en un centro de atención y por un profesional, ordenados por fecha de solicitud descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La vista ViewNonSurgicalProcedureEMR debe existir y estar accesible; Se requieren código de centro de atención y código de profesional para filtrar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La paginación nunca usa valores menores a 1 (página mínima 1, tamaño mínimo 20 por defecto); Los resultados siempre se ordenan por FechaSolicitud en orden descendente (más recientes primero); Solo se retornan procedimientos del centro de atención y profesional indicados; El offset se calcula como (Page - 1) * PageSize', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimientos no quirúrgicos; Centro de atención; Profesional de salud; Fecha de solicitud; EMR (Expediente Médico Electrónico)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewNonSurgicalProcedureEMR: Cuando CodigoCentroAtencion = @AttentionCenter y CodigoProfesional = @Profesional, devuelve registros ordenados por FechaSolicitud DESC con paginación OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Page < 1 → Se normaliza @Page = 1 (primera página por defecto); si @PageSize < 1 → Se normaliza @PageSize = 20 (tamaño de página por defecto)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewNonSurgicalProcedureEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Non_Surgical_Procedures_EMR';
-- GO
