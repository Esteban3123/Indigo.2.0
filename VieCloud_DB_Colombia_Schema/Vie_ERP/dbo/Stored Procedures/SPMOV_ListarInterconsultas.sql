
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPMOV_ListarInterconsultas] 
	-- Add the parameters for the stored procedure here
@CentroAtencion Char(10),
@CodigoProfesional Char(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  SELECT RTRIM(A.IPCODPACI) AS Identificacion,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso,CASE WHEN AD.UFUAACTHOS IS NULL THEN AD.UFUCODIGO ELSE AD.UFUAACTHOS END AS CodigoUnidadFuncional, 
	CASE WHEN RTRIM(D.UFUDESCRI) IS NULL THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO=AD.UFUCODIGO) ELSE RTRIM(D.UFUDESCRI) END AS DescripcionUnidadFuncional,
	RTRIM(DESCCAMAS) AS Cama,
	dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,'2 - Solicitudes Valoracion de Interconsultas' AS Tipo, H.DESESPECI AS Especialidad,TRIANUMER AS Consecutivo 
	, CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
	, B.ZONAPARTADA,
	L.RIESGOAGRE,
	(SELECT count(*) FROM dbo.ADPOBESPEPAC WHERE IPCODPACI = B.IPCODPACI) AS POBESPECIAL,
	AD.VIVESOLO,
	(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = AD.NUMINGRES) AS ACOMPANANTES,
	CONVERT(BIT,0) AS Riesgo
	,B.IPSEXOPAC AS Sexo  
	FROM dbo.HCORDINTE  A INNER JOIN 
	dbo.ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES LEFT OUTER JOIN
	dbo.INUNIFUNC D ON AD.UFUAACTHOS=D.UFUCODIGO INNER JOIN
	dbo.INPacient B ON A.IPCODPACI=B.IPCODPACI LEFT OUTER JOIN
	dbo.CHCAMASHO CA ON AD.CODCAMACT=CA.CODICAMAS INNER JOIN 
	dbo.INESPECIA H ON A.CODESPECI=H.CODESPECI LEFT OUTER JOIN
	dbo.ADTRIAGEU E ON A.IPCODPACI= E.IPCODPACI AND A.NUMINGRES=E.NUMINGRES LEFT OUTER JOIN 
	dbo.ADACTIVID L ON B.CODACTIVI = L.codactivi
	WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS='1' AND A.CODPROSAL = @CodigoProfesional
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las interconsultas pendientes de valoración asignadas a un profesional de salud en un centro de atención específico. Para cada solicitud muestra: identificación y nombre del paciente, número de ingreso, unidad funcional y cama actual, especialidad solicitada, número de triage, tipo de documento (para identificar afiliación ADRES/SOAT), zona apartada, riesgo agregado, si el paciente vive solo, cantidad de acompañantes y población especial. Combina las órdenes médicas internas (HCORDINTE) con el ingreso activo del paciente (ADINGRESO), sus datos demográficos (INPACIENT), la cama hospitalaria asignada (CHCAMASHO), la especialidad requerida (INESPECIA), el triage de urgencias (ADTRIAGEU) y la actividad de admisión asociada (ADACTIVID). Se usa en el módulo de hospitalización o urgencias para que el médico consultor vea las interconsultas que debe resolver.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarInterconsultas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarInterconsultas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de valoración de interconsultas activas asignadas a un profesional dentro de un centro de atención, incluyendo datos del paciente, ubicación, especialidad y banderas de riesgo/población especial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y el código del profesional deben existir y coincidir con órdenes de interconsulta registradas.; Las órdenes deben tener estado de servicio IPS = ''1'' (activas/vigentes) para ser consideradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan interconsultas con ESTSERIPS = ''1''.; Toda fila se rotula con el tipo fijo ''2 - Solicitudes Valoracion de Interconsultas''.; El campo Riesgo siempre se devuelve como BIT 0 (placeholder, sin cálculo).; Pacientes con tipo de documento 6 o 7 se consideran ASMS.; La unidad funcional de hospitalización (UFUAACTHOS) prevalece sobre la unidad funcional de admisión (UFUCODIGO) cuando está presente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Paciente; Ingreso hospitalario; Unidad funcional; Cama hospitalaria; Aislamiento; Especialidad médica; Triage de urgencias; Población especial; Riesgo de agresividad; ASMS (tipo de documento); Acompañantes; Zona apartada; Vive solo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDINTE: Devuelve interconsultas donde CODCENATE = centro recibido, CODPROSAL = profesional recibido y ESTSERIPS = ''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AD.UFUAACTHOS IS NULL → Se usa AD.UFUCODIGO como código de unidad funcional else Se usa AD.UFUAACTHOS como código de unidad funcional; si RTRIM(D.UFUDESCRI) IS NULL → Se obtiene la descripción de la unidad funcional consultando INUNIFUNC por AD.UFUCODIGO else Se usa la descripción ya unida D.UFUDESCRI; si B.IPTIPODOC IN (6,7) → Se marca el paciente como ASMS = 1 else ASMS = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoAislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPacient; dbo.CHCAMASHO; dbo.INESPECIA; dbo.ADTRIAGEU; dbo.ADACTIVID; dbo.ADPOBESPEPAC; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInterconsultas';
-- GO
