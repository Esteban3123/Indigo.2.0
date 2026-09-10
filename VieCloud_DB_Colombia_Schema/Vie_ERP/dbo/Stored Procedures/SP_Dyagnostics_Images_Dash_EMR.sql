
CREATE PROCEDURE [dbo].[SP_Dyagnostics_Images_Dash_EMR]
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
    FROM [ViewDiagnosticImagesEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera de forma paginada los registros de imágenes diagnósticas registrados en el EMR, filtrando por centro de atención y profesional de salud. Los resultados se ordenan por fecha de solicitud descendente, mostrando las solicitudes más recientes primero. Consume la vista `ViewDiagnosticImagesEMR` como fuente de datos consolidada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada de solicitudes de imágenes diagnósticas asociadas a un profesional dentro de un centro de atención, ordenadas por fecha de solicitud descendente, para visualización en el dashboard de EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y el profesional deben existir/coincidir con registros en la vista de imágenes diagnósticas para retornar datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los resultados siempre se filtran por centro de atención y profesional; Los resultados siempre se ordenan por fecha de solicitud en orden descendente (más recientes primero); Siempre se aplica paginación con valores normalizados (página ≥1, tamaño ≥1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Imágenes diagnósticas; Centro de atención; Profesional de salud; Solicitud de ayuda diagnóstica; EMR (Historia clínica electrónica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewDiagnosticImagesEMR: Cuando coinciden CodigoCentroAtencion y CodigoProfesional con los parámetros, retorna las filas paginadas (OFFSET/FETCH) ordenadas por FechaSolicitud DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Página solicitada menor a 1 → Se normaliza la página a 1; si Tamaño de página menor a 1 → Se normaliza el tamaño a 20 registros por página', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewDiagnosticImagesEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dyagnostics_Images_Dash_EMR';
-- GO
