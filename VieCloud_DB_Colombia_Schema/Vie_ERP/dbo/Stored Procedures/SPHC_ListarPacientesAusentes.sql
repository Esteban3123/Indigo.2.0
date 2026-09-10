
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentes]
@UnidadFuncional char(10),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT A.IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,
A.IPCODPACI AS Paciente,RTRIM(A.IPNOMCOMP) AS NombreCompleto,A.CODCENATE AS CentroAtencion,
A.UFUCODIGO AS UnidadFuncional,CASE A.CONESTADO WHEN '2' THEN 'Clasificacion Triage' WHEN '6' THEN 'Atencion Inicial Urgencias' END AS Estado ,
A.FECPRILLA AS FechaPrimerLlamadoT,A.FECSEGLLA AS FechaSegundoLlamadoT,A.FECTERLLA AS FechaTercerLlamadoT,
B.FECPRILLA AS FechaPrimerLlamadoA,B.FECSEGLLA AS FechaSegundoLlamadoA,B.FECTERLLA AS FechaTercerLlamadoA,
cast('' AS datetime) AS PrimerLlamado,cast('' AS datetime) AS SegundoLlamado,cast('' AS datetime) AS TercerLlamado,A.OBVAUSENT AS Observacion,A.FECAUSENT AS FechaAusente, C.NOMMEDICO AS  ProfesionalAusenta
FROM ADCONTURG A
LEFT OUTER JOIN ADTRIAGEU B ON A.CODCONCEC =B.CODCONCEC 
INNER JOIN INPROFSAL c on a.PROAUSENT=c.CODPROSAL 
WHERE CONESTADO IN ('2','6') AND A.FECTERLLA is not null AND A.UFUCODIGO= @UnidadFuncional 
AND A.FECAUSENT BETWEEN @FechaInicial AND @FechaFinal
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que fueron marcados como ausentes en urgencias dentro de un rango de fechas y para una unidad funcional específica. Combina el registro de contactos y llamados de urgencias (ADCONTURG) con los datos del triage inicial (ADTRIAGEU) para mostrar los tres intentos de llamado tanto del proceso de convocatoria como del triage, e incorpora el nombre del profesional que registró la ausencia desde el maestro de profesionales (INPROFSAL). Solo incluye pacientes que completaron los tres intentos de llamado y cuyo estado corresponde a ''Clasificación Triage'' o ''Atención Inicial de Urgencias''. Es utilizado para el control y seguimiento de pacientes que no respondieron a los llamados en sala de espera de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes marcados como ausentes en urgencias/triage dentro de un rango de fechas y para una unidad funcional, incluyendo datos de llamados y profesional que registró la ausencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registrado el tercer llamado (FECTERLLA no nulo); El estado de la consulta debe ser ''2'' (Clasificación Triage) o ''6'' (Atención Inicial Urgencias); La fecha de ausencia debe estar dentro del rango solicitado; Debe existir un profesional asociado al campo PROAUSENT en INPROFSAL (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan pacientes que ya tienen tercer llamado registrado; Solo se consideran estados de Triage (2) o Atención Inicial de Urgencias (6); Cada fila se identifica por la combinación de código de paciente y fecha de ausencia; Los campos PrimerLlamado/SegundoLlamado/TercerLlamado se devuelven vacíos (placeholders datetime)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente ausente; Triage; Atención inicial de urgencias; Llamados al paciente (primero, segundo, tercero); Unidad funcional; Centro de atención; Profesional de salud; Consulta de urgencias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT con datos del paciente, llamados de triage y atención, observación y profesional que marcó la ausencia, cuando CONESTADO IN (''2'',''6'') AND FECTERLLA IS NOT NULL AND UFUCODIGO=@UnidadFuncional AND FECAUSENT BETWEEN rango', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONESTADO = ''2'' → Etiqueta el estado como ''Clasificacion Triage''; si CONESTADO = ''6'' → Etiqueta el estado como ''Atencion Inicial Urgencias'' else No se incluye en el resultado (filtro WHERE CONESTADO IN (''2'',''6''))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentes';
-- GO
