-- Stored Procedure

CREATE PROCEDURE [dbo].[SPREP_ADM_Impresion_Resultados_Laboratorio]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT IPCODPACI, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, FECORDMED, CODSERIPS
FROM AMBORDLAB with(nolock)
WHERE CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional
UNION SELECT IPCODPACI, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, FECORDMED, CODSERIPS
FROM HCORDLABO with(nolock)
WHERE CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el listado consolidado de órdenes de laboratorio clínico para imprimir resultados, combinando tanto las órdenes ambulatorias (AMBORDLAB) como las órdenes registradas en la historia clínica hospitalaria (HCORDLABO). Filtra por centro de atención y unidad funcional, y retorna para cada orden los datos clave: cédula del paciente, número de ingreso, profesional de salud solicitante, fecha de la orden médica y código del servicio o examen (CUPS). Se usa en la impresión o consulta de resultados de laboratorio de pacientes atendidos en un área específica de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida órdenes de laboratorio provenientes de atención ambulatoria y de historia clínica para un centro de atención y unidad funcional, con fines de impresión de resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse el código de centro de atención y la unidad funcional para filtrar las órdenes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las lecturas se hacen con NOLOCK, por lo que pueden incluir datos no confirmados.; Al usar UNION (no UNION ALL), órdenes idénticas presentes en ambas fuentes se reportan una sola vez.; Solo se incluyen órdenes que pertenecen simultáneamente al centro de atención y unidad funcional indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Centro de atención; Unidad funcional; Paciente; Ingreso; Profesional de salud; Servicio IPS; Atención ambulatoria; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AMBORDLAB+HCORDLABO: Devuelve la unión (UNION, eliminando duplicados) de órdenes de laboratorio ambulatorias y de hospitalización filtradas por CODCENATE y UFUCODIGO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDLAB; dbo.HCORDLABO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_Impresion_Resultados_Laboratorio';
-- GO
