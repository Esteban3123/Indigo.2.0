
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DiagnosticosHistorico]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.CODDIAGNO AS 'CODIGO DEL DIAGNOSTICO', NOMDIAGNO AS 'NOMBRE DEL DIAGNOSTICO', 
	   CAST(CODDIAPRI AS BIT) AS 'CODIGO DEL DIAGNOSTICO PRINCIPAL',RTRIM(OBSDIAGNO) AS 'OBSERVACIONES DEL DIAGNOSTICO',
	   CASE A.TIPDIAGNO WHEN 'I' THEN 'Impresión diagnóstica' WHEN 'C' THEN 'Confirmado nuevo' WHEN 'R' THEN 'Confirmado repetido'  END AS 'TIPO DIAGNOSTICO'
FROM INDIAGNOH A With(Nolock)
INNER JOIN INDIAGNOS B With(Nolock) ON A.CODDIAGNO=B.CODDIAGNO 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el historial de diagnósticos clínicos registrados en la historia clínica de un paciente para un ingreso y folio específicos. Combina los diagnósticos del ingreso (INDIAGNOH) con el catálogo maestro CIE-10 (INDIAGNOS) para devolver el código y nombre del diagnóstico, si es el diagnóstico principal, las observaciones clínicas y el tipo de diagnóstico (impresión diagnóstica, confirmado nuevo o confirmado repetido). Se utiliza en la visualización del historial diagnóstico del paciente dentro de la historia clínica, filtrando por cédula del paciente, número de ingreso y número de folio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reportar el histórico de diagnósticos (código, nombre, indicador de principal, observaciones y tipo) asociados a un paciente para un ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso y folio en el histórico de diagnósticos; El código de diagnóstico debe existir en el catálogo de diagnósticos para ser retornado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna diagnósticos asociados simultáneamente al paciente, ingreso y folio indicados; El indicador de diagnóstico principal se entrega como bit (booleano); Las observaciones se entregan sin espacios finales (RTRIM); Solo se incluyen diagnósticos cuyo código exista en el catálogo maestro de diagnósticos (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; diagnóstico; diagnóstico principal; ingreso; folio; histórico de diagnósticos; impresión diagnóstica; diagnóstico confirmado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOH: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, se retorna el conjunto de diagnósticos históricos enriquecidos con el nombre del catálogo y la descripción del tipo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de diagnóstico = ''I'' → Se etiqueta como ''Impresión diagnóstica''; si Tipo de diagnóstico = ''C'' → Se etiqueta como ''Confirmado nuevo''; si Tipo de diagnóstico = ''R'' → Se etiqueta como ''Confirmado repetido'' else NULL (sin etiqueta)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistorico';
-- GO
