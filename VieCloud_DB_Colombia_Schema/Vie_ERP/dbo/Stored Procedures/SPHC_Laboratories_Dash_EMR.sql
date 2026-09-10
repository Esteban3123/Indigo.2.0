
CREATE PROCEDURE [dbo].[SPHC_Laboratories_Dash_EMR]
(
    @AttentionCenter VARCHAR(20),
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
    FROM [ViewLaboratoriesEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera solicitudes de laboratorio desde la vista `ViewLaboratoriesEMR`, filtrando por centro de atención y profesional de salud, ordenadas por fecha de solicitud descendente. Implementa paginación basada en offset/fetch para controlar el volumen de resultados retornados. Está orientado a alimentar un dashboard de laboratorios dentro de un módulo de Historia Clínica Electrónica (EMR).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada de laboratorios solicitados, filtrados por centro de atención y profesional, ordenados por fecha de solicitud descendente, para tablero clínico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe la vista ViewLaboratoriesEMR con columnas CodigoCentroAtencion, CodigoProfesional y FechaSolicitud; Se debe proveer código de centro de atención y código de profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La página mínima es 1 y el tamaño mínimo de página es 20 cuando se reciben valores inválidos; El offset se calcula como (Page-1)*PageSize; Los resultados siempre se ordenan por FechaSolicitud descendente; Solo se exponen laboratorios del centro y profesional indicados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Laboratorios; Centro de atención; Profesional de salud; Solicitud de laboratorio; EMR (Historia clínica electrónica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewLaboratoriesEMR: Devuelve registros donde CodigoCentroAtencion=@AttentionCenter y CodigoProfesional=@Profesional, ordenados por FechaSolicitud DESC, con paginación OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Page < 1 → Se normaliza @Page a 1; si @PageSize < 1 → Se normaliza @PageSize a 20 (tamaño por defecto)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewLaboratoriesEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Dash_EMR';
-- GO
