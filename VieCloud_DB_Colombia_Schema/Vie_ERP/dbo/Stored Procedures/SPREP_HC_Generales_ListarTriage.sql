
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_ListarTriage]
(
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT 'Numero Reporte: ' + TRIANUMER + ' - ' + RTRIM(CAST(TRIAGECLA AS CHAR)) + ' - ' + CASE CAST(TRIAGECLA AS CHAR) WHEN '1' THEN 'EMERGENCIA' WHEN '2' THEN 'URGENCIA MEDICA' WHEN '3' THEN 'URGENCIA DIFERIDA' WHEN '4' THEN 'NO URGENTE' END AS 'NIVEL CALIFICACION TRIAGE'

FROM ADTRIAGEU WITH(NOLOCK)
WHERE NUMINGRES=@NumeroIngreso 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los registros de triage de urgencias asociados a un número de ingreso específico. Para cada valoración inicial registrada en urgencias, muestra el número de reporte y la clasificación de prioridad en lenguaje legible: Emergencia, Urgencia Médica, Urgencia Diferida o No Urgente. Se usa para visualizar en la historia clínica el nivel de triage asignado al paciente al momento de su llegada a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_ListarTriage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la clasificación de triage asociada a un ingreso, formateando el número de reporte y el nivel de urgencia en texto descriptivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de triage en ADTRIAGEU asociado al número de ingreso recibido para retornar filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La clasificación de triage se traduce a 4 niveles fijos: 1=EMERGENCIA, 2=URGENCIA MEDICA, 3=URGENCIA DIFERIDA, 4=NO URGENTE.; La consulta usa NOLOCK, por lo que no bloquea ni garantiza lecturas consistentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage; Clasificación de urgencia; Ingreso del paciente; Emergencia; Urgencia médica; Urgencia diferida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADTRIAGEU: Cuando NUMINGRES coincide con el ingreso solicitado, retorna una cadena con el número de reporte de triage y la descripción del nivel de clasificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRIAGECLA = ''1'' → Etiqueta el nivel como ''EMERGENCIA''; si TRIAGECLA = ''2'' → Etiqueta el nivel como ''URGENCIA MEDICA''; si TRIAGECLA = ''3'' → Etiqueta el nivel como ''URGENCIA DIFERIDA''; si TRIAGECLA = ''4'' → Etiqueta el nivel como ''NO URGENTE'' else No se asigna descripción textual al nivel (NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTRIAGEU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_ListarTriage';
-- GO
