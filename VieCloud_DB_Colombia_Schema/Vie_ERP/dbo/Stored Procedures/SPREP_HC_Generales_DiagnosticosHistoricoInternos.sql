
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DiagnosticosHistoricoInternos]
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
FROM INDIAGNOI AS A WITH(NOLOCK)
INNER JOIN INDIAGNOS AS B WITH(NOLOCK) ON A.CODDIAGNO=B.CODDIAGNO 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta el historial de diagnósticos clínicos registrados en la historia clínica de un paciente para un ingreso y folio específicos. Combina los diagnósticos ingresados por atención (INDIAGNOI) con el catálogo maestro CIE-10 (INDIAGNOS) para obtener el código y nombre legible de cada diagnóstico. Devuelve, para cada diagnóstico, el código CIE-10, su nombre, si es el diagnóstico principal, las observaciones del médico y el tipo de diagnóstico (impresión diagnóstica, confirmado nuevo o confirmado repetido). Se utiliza en reportes de historia clínica interna para visualizar los diagnósticos asociados a una atención u hospitalización del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos registrados para un paciente en una atención interna (ingreso/folio) específica, indicando cuál es principal y el tipo de diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener diagnósticos asociados al ingreso y folio indicados; Los códigos de diagnóstico deben existir en el catálogo maestro de diagnósticos para que aparezcan (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan diagnósticos cuyo código exista en el catálogo maestro; El indicador de diagnóstico principal se expone como BIT; La consulta usa NOLOCK, aceptando lecturas sucias para fines de reporte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de atención; Diagnóstico; Diagnóstico principal; Impresión diagnóstica; Diagnóstico confirmado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve los diagnósticos del paciente filtrados por IPCODPACI, NUMINGRES y NUMEFOLIO, traduciendo TIPDIAGNO (''I'',''C'',''R'') a su descripción y casteando CODDIAPRI a BIT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPDIAGNO = ''I'' → Etiqueta como ''Impresión diagnóstica''; si TIPDIAGNO = ''C'' → Etiqueta como ''Confirmado nuevo''; si TIPDIAGNO = ''R'' → Etiqueta como ''Confirmado repetido'' else NULL en el tipo de diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOI; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DiagnosticosHistoricoInternos';
-- GO
