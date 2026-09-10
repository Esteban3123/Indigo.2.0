
CREATE PROCEDURE [dbo].[SPREP_HC_Interconsultations_Dash_EMR]
(
    @AttentionCenter varchar (20),
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
    FROM [ViewInterconsultationsEMR]
    WHERE CodigoCentroAtencion = @AttentionCenter
    AND CodigoProfesional = @Profesional
    ORDER BY FechaSolicitud DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
    
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consulta paginada sobre la vista `ViewInterconsultationsEMR` que retorna las interconsultas registradas en la Historia Clínica Electrónica (EMR), filtradas por centro de atención y profesional solicitante, ordenadas por fecha de solicitud descendente. Está diseñada para alimentar un dashboard clínico, soportando navegación por páginas con valores predeterminados de 20 registros por página.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar de forma paginada las interconsultas del EMR para un profesional dentro de un centro de atención, ordenadas por fecha de solicitud descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la vista ViewInterconsultationsEMR con columnas CodigoCentroAtencion, CodigoProfesional y FechaSolicitud; Se requiere el código del centro de atención y del profesional para filtrar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven interconsultas del centro de atención y profesional indicados; Resultados ordenados por fecha de solicitud descendente (más recientes primero); Paginación basada en OFFSET/FETCH con valores mínimos garantizados (página≥1, tamaño≥1); Tamaño de página por defecto = 20 cuando se recibe valor inválido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Centro de atención; Profesional; Solicitud médica; EMR (Historia Clínica Electrónica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando CodigoCentroAtencion y CodigoProfesional coinciden con los parámetros, se devuelve la página solicitada de interconsultas ordenadas por FechaSolicitud DESC aplicando OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Página solicitada menor a 1 → Se normaliza la página a 1; si Tamaño de página menor a 1 → Se normaliza el tamaño de página a 20 por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewInterconsultationsEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_Dash_EMR';
-- GO
