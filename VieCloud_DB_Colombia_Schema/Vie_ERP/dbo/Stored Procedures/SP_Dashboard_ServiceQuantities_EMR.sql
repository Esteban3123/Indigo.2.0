
CREATE PROCEDURE [dbo].[SP_Dashboard_ServiceQuantities_EMR]
(
    @AttentionCenter VARCHAR(10),
    @Profesional CHAR(20)
)
    
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        /* Imágenes Diagnósticas */
        (
            SELECT COUNT(*)
            FROM dbo.ViewDiagnosticImagesEMR
            WHERE CodigoCentroAtencion = @AttentionCenter 
            AND CodigoProfesional = @Profesional
        ) AS TotalImagenes,

        /* Laboratorios */
        (
            SELECT COUNT(*)
            FROM dbo.ViewLaboratoriesEMR
            WHERE CodigoCentroAtencion = @AttentionCenter
            AND CodigoProfesional = @Profesional
        ) AS TotalLaboratorios,

        /* Patologías */
        (
            SELECT COUNT(*)
            FROM dbo.ViewPathologiesEMR
            WHERE CodigoCentroAtencion = @AttentionCenter
            AND CodigoProfesional = @Profesional
        ) AS TotalPatologias,

        /* Interconsultas */
        (
            SELECT COUNT(*)
            FROM dbo.ViewInterconsultationsEMR
            WHERE CodigoCentroAtencion = @AttentionCenter
            AND CodigoProfesional = @Profesional
        ) AS TotalInterconsultas,

        /* Procedimientos Quirúrgicos */
        (
            SELECT COUNT(*)
            FROM dbo.ViewSurgicalProcedureEMR
            WHERE CodigoCentroAtencion = @AttentionCenter
            AND CodigoProfesional = @Profesional
        ) AS TotalProcedimientosQ,

        /* Procedimientos No Quirúrgicos */
        (
            SELECT COUNT(*)
            FROM dbo.ViewNonSurgicalProcedureEMR
            WHERE CodigoCentroAtencion = @AttentionCenter
            AND CodigoProfesional = @Profesional
        ) AS TotalProcedimientosNoQ

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento del dashboard del módulo EMR que consolida en una sola fila los conteos de servicios clínicos solicitados por un profesional en un centro de atención específico. Agrega totales de imágenes diagnósticas, laboratorios, patologías, interconsultas, procedimientos quirúrgicos y no quirúrgicos consultando seis vistas del EMR. Está diseñado para alimentar indicadores de volumen de servicios en una pantalla de resumen gerencial o clínico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los totales de servicios clínicos (imágenes, laboratorios, patologías, interconsultas, procedimientos quirúrgicos y no quirúrgicos) asociados a un profesional en un centro de atención, para alimentar un dashboard de la historia clínica electrónica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las vistas EMR consultadas deben existir y exponer las columnas CodigoCentroAtencion y CodigoProfesional.; Se deben proporcionar el código del centro de atención y el código del profesional para filtrar los conteos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los conteos se calculan con el mismo par de filtros (centro de atención + profesional), garantizando coherencia entre las métricas del dashboard.; El procedimiento es de solo lectura: no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Imágenes diagnósticas; Laboratorios; Patologías; Interconsultas; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Centro de atención; Profesional; Historia clínica electrónica (EMR)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna una única fila con seis conteos (TotalImagenes, TotalLaboratorios, TotalPatologias, TotalInterconsultas, TotalProcedimientosQ, TotalProcedimientosNoQ) filtrados por centro de atención y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewDiagnosticImagesEMR; dbo.ViewLaboratoriesEMR; dbo.ViewPathologiesEMR; dbo.ViewInterconsultationsEMR; dbo.ViewSurgicalProcedureEMR; dbo.ViewNonSurgicalProcedureEMR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Dashboard_ServiceQuantities_EMR';
-- GO
