
CREATE PROCEDURE [dbo].[SP_AD_ListarIngresosRelacionadosTriage]
(
@Paciente varchar(25),
@UnidadFuncional char(10),
@NumerHoras int
)
AS
BEGIN
	SET NOCOUNT ON;
--SELECT CAST(0 AS BIT) AS Sel, FECHISPAC AS FechaHistoria,RTRIM(NOMDIAGNO) AS Diagnostico,RTRIM(NOMMEDICO) AS Profesional,RTRIM(DESESPECI) AS Especialidad,A.NUMINGRES AS Ingreso,A.NUMEFOLIO AS Folio
--FROM dbo.HCHISPACA A 
--INNER JOIN dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO 
--INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
--INNER JOIN dbo.INESPECIA D ON C.CODESPEC1=D.CODESPECI 
--WHERE A.IPCODPACI=@paciente AND A.UFUCODIGO=@UnidadFuncional AND TIPHISPAC='I' AND 
--DATEDIFF(hour,FECHISPAC,[Common].[GETDATE]())<=@NumerHoras 
	SELECT 'de Ingreso' as Reingreso, CAST(0 AS BIT) AS Sel, CONVERT(VARCHAR(30),FECHISPAC) AS FechaHistoria,RTRIM(NOMDIAGNO) AS Diagnostico,RTRIM(NOMMEDICO) AS Profesional,RTRIM(DESESPECI) AS Especialidad,A.NUMINGRES AS Ingreso,A.NUMEFOLIO AS Folio, null as NumeroTriage
	FROM dbo.HCHISPACA A 
	INNER JOIN dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO 
	INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INESPECIA D ON C.CODESPEC1=D.CODESPECI 
	INNER JOIN dbo.HCREGEGRE E ON E.NUMINGRES = A.NUMINGRES AND E.NUMEFOLIO = A.NUMEFOLIO
	INNER JOIN dbo.ADINGRESO I ON I.NUMINGRES = A.NUMINGRES 
	WHERE A.IPCODPACI=@Paciente AND DATEDIFF(hour,FECALTPAC,[Common].[GETDATE]())<=@NumerHoras   and i.FECHEGRESO is null and i.UFUEGRHOS IS NULL and I.IINGREPOR <> 2
UNION ALL
	SELECT 'de Triage' as Reingreso, CAST(0 AS BIT) AS Sel, CONVERT(VARCHAR(30),A.TRIAFECHA) AS FechaHistoria,RTRIM(B.TRIANOMCA) AS Diagnostico,RTRIM(NOMMEDICO) AS Profesional,RTRIM(DESESPECI) AS Especialidad,null AS Ingreso,'' AS Folio, A.TRIANUMER as NumeroTriage
	FROM dbo.ADTRIAGEU A 
	INNER JOIN dbo.ADCATTRIU B ON A.TRIACATEG=B.TRIACATEG
	INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INESPECIA D ON C.CODESPEC1=D.CODESPECI 
	WHERE A.IPCODPACI=@Paciente AND A.TRIAGECLA IN (3,4) AND
	DATEDIFF(hour,A.TRIAFECHA,[Common].[GETDATE]())<=@NumerHoras 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que busca los ingresos y registros de triage recientes de un paciente específico, con el fin de detectar posibles reingresos o atenciones previas antes de crear un nuevo episodio de urgencias. Recibe como parámetros la cédula o código del paciente, la unidad funcional y un rango de horas hacia atrás para filtrar la búsqueda. Combina dos fuentes: por un lado, historias clínicas de ingresos activos (sin fecha de egreso ni egreso hospitalario) cruzadas con diagnósticos CIE-10, profesional tratante y especialidad, verificando que el alta haya ocurrido dentro del rango de horas indicado; por otro lado, registros de triage urgente (clasificaciones 3 y 4) del módulo de triaje, también dentro del rango horario. El resultado unificado indica si cada registro proviene ''de Ingreso'' o ''de Triage'', mostrando fecha, diagnóstico, profesional, especialidad, número de ingreso o número de triage, permitiendo al personal asistencial evaluar si el paciente es un reingreso en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos activos y los triages de urgencia recientes de un paciente dentro de una ventana horaria, para detectar posibles reingresos en urgencias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener historias clínicas o registros de triage asociados.; Los diagnósticos, profesionales y especialidades referenciados deben existir en sus catálogos para que aparezcan en el resultado (joins INNER).; Para los ingresos: debe existir registro de egreso (HCREGEGRE) asociado al folio.; La función [Common].[GETDATE]() debe estar disponible para el cálculo de la ventana horaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca devuelve ingresos ya egresados (con FECHEGRESO o UFUEGRHOS informados).; Nunca devuelve ingresos cuyo motivo de ingreso sea 2.; Nunca devuelve triages con clasificación distinta de 3 o 4.; Solo retorna información dentro de la ventana de @NumerHoras horas hacia atrás desde la fecha actual.; El campo Sel siempre se devuelve en 0 (no seleccionado por defecto).; Los registros de tipo Ingreso no llevan NumeroTriage y los de Triage no llevan número de Ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario / urgencias; Egreso hospitalario; Triage de urgencias; Clasificación de triage; Reingreso; Diagnóstico; Profesional de salud; Especialidad médica; Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve registros etiquetados ''de Ingreso'' cuando el ingreso del paciente no tiene fecha de egreso (FECHEGRESO IS NULL), no tiene unidad funcional de egreso (UFUEGRHOS IS NULL), el motivo de ingreso es distinto de 2 y la fecha de alta del folio está dentro de las últimas @NumerHoras horas.; [RETURN_RESULT] resultset: Devuelve registros etiquetados ''de Triage'' cuando el triage del paciente tiene clasificación TRIAGECLA en (3,4) y la fecha del triage está dentro de las últimas @NumerHoras horas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del registro en el UNION ALL → Si proviene de HCHISPACA con ingreso activo: marca ''de Ingreso'' y reporta NUMINGRES/NUMEFOLIO sin NumeroTriage else Si proviene de ADTRIAGEU con clasificación 3 o 4: marca ''de Triage'' y reporta TRIANUMER sin Ingreso; si I.IINGREPOR <> 2 → Solo se incluyen ingresos cuyo motivo/origen de ingreso no sea el código 2; si i.FECHEGRESO IS NULL AND i.UFUEGRHOS IS NULL → Solo se consideran ingresos que aún están abiertos (sin egreso registrado); si A.TRIAGECLA IN (3,4) → Solo se consideran triages con clasificación 3 o 4 (categorías de urgencia específicas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.ADTRIAGEU; dbo.ADCATTRIU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionadosTriage';
-- GO
