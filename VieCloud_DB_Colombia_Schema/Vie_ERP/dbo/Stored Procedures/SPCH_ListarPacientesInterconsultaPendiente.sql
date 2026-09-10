CREATE PROCEDURE [dbo].[SPCH_ListarPacientesInterconsultaPendiente]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@Especialidad1 char(3),
@Especialidad2 char(3),
@Especialidad3 char(3)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT DISTINCT  RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.UFUDESCRI) AS UFUDESCRI,AD.UFUAACTHOS AS UFUCODIGO,A.IPCODPACI AS Identificacion,A.NUMINGRES AS Ingreso, RTRIM(IPNOMCOMP) AS Paciente,RTRIM(ca.NUMCAMHOS) + ' - ' + RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(CA.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(CA.CODCLACAM) AS 'Clase de Cama',dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,CAST(0 AS BIT) AS Resultado,A.NUMEFOLIO,A.CODSERIPS,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta, 
AD.ESCADOWNT , AD.ESCARASS, AD.ESCVASPAC, AD.ESCAPAPAC, AD.ESCNORPAC, dbo.PuntajeEscalaDownTon(A.NUMINGRES, A.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(A.NUMINGRES, A.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(A.NUMINGRES, A.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(A.NUMINGRES, A.IPCODPACI) as PUNTAJEAPACHE, dbo.PuntajeEscalaNorton(A.NUMINGRES, A.IPCODPACI) as PUNTAJENORTON, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
IIF(B.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI
FROM dbo.HCORDINTE  A with(nolock) INNER JOIN 
dbo.ADINGRESO AD with(nolock) ON A.NUMINGRES = AD.NUMINGRES INNER JOIN 
dbo.INUNIFUNC D with(nolock) ON AD.UFUAACTHOS=D.UFUCODIGO INNER JOIN
dbo.INPacient B with(nolock) ON A.IPCODPACI=B.IPCODPACI INNER JOIN 
dbo.INCUPSIPS J with(nolock) ON A.CODSERIPS=J.CODSERIPS LEFT OUTER JOIN 
dbo.CHCAMASHO CA with(nolock) ON AD.CODCAMACT=CA.CODICAMAS LEFT OUTER JOIN
DBO.INESPECIA E with(nolock) ON AD.CODESPTRA=E.CODESPECI
WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS='1' AND A.CODESPECI IN (@Especialidad1,@Especialidad2,@Especialidad3)

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados que tienen órdenes de interconsulta pendientes, filtrando por centro de atención y hasta tres especialidades médicas destino. Combina las órdenes internas de servicios (interconsultas activas) con el ingreso hospitalario del paciente, su ubicación actual en cama, la unidad funcional donde está internado y la especialidad requerida. Para cada paciente devuelve datos de identificación y nombre, número de ingreso, cama y clase de habitación, además de puntajes de escalas clínicas de riesgo (Downton, RASS, VAS, Apache, Norton), indicador de recomendación de traslado y si pertenece a población PAPSIVI, permitiendo al equipo asistencial gestionar la cola de interconsultas pendientes por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados con órdenes de interconsulta pendientes en un centro de atención, filtrados por hasta tres especialidades, incluyendo datos clínicos, ubicación de cama, escalas de valoración y marcadores de población especial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir y coincidir con el código enviado.; Las especialidades enviadas deben corresponder a códigos válidos de interconsulta.; Debe existir relación entre el ingreso y un servicio CUPS-IPS activo (ESTSERIPS=''1'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera órdenes de interconsulta cuyo servicio IPS está activo (ESTSERIPS=''1'').; Filtra exclusivamente por el centro de atención recibido.; Restringe a un máximo de tres especialidades por consulta.; Aplica DISTINCT para evitar duplicados de pacientes en el resultado.; La unión con cama y especialidad de tratamiento es opcional (LEFT JOIN), por lo que pacientes sin cama o sin especialidad asignada también aparecen.; Resultado y MuestraAlerta siempre se devuelven en falso/vacío y la Alerta siempre como ''Normal'' (valores fijos no calculados).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta pendiente; Paciente hospitalizado; Ingreso; Especialidad médica; Unidad funcional; Cama hospitalaria; Clase de habitación; Clase de cama; Aislamiento; Escalas de valoración (Down-Ton, RASS, VAS, APACHE, Norton); Recomendación de paciente; Población PAPSIVI; Servicio CUPS-IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna un conjunto de resultados con pacientes cuyas órdenes de interconsulta están en estado activo (ESTSERIPS=''1'') en el centro de atención indicado y pertenecen a alguna de las hasta tres especialidades suministradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en RecommendPatient con Status=1 para el paciente e ingreso → Marca Recomendacion = 1 (bit verdadero) else Marca Recomendacion = 0; si INPacient.PoblacionPAPSIVI = 1 → Marca EsPoblacionPAPSIVI = 1 else Marca EsPoblacionPAPSIVI = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.PuntajeEscalaNorton', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPacient; dbo.INCUPSIPS; dbo.CHCAMASHO; dbo.INESPECIA; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesInterconsultaPendiente';
-- GO
