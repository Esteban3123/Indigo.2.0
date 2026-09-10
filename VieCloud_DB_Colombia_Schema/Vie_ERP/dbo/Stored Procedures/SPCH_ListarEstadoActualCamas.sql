CREATE PROCEDURE [dbo].[SPCH_ListarEstadoActualCamas]
@centroAtencion varchar(20),
@unidadFuncional varchar(1000)
as
begin
SELECT 
	tmp.[CODCENATE],
	tmp.[CENTROATENCION],
	tmp.[UFUCODIGO],
	rtrim(tmp.[UNIDADFUN]) UNIDADFUN,
	tmp.[CODICAMAS],
	rtrim(tmp.[DescripcionCama]) DescripcionCama,
	tmp.[ESTADOCAMA],
	tz.FECHAINICIAL,
	tmp.IPCODPACI,
	rtrim(p.IPNOMCOMP) PACIENTE,
	p.CODENTIDA [CodigoEntidad],
	rtrim(i.NOMENTIDA) [ENTIDAD],
	CH.Id [CodigoAislamiento],
	rtrim(CH.Nombre) [AISLAMIENTO],
	dp.coddiagno [CodigoDiagnostico],
	rtrim(DI.NOMDIAGNO) [DIAGNOSTICO],
	TMP.[CodigoBloqueo],
	Q.DESTIPBLO TIPOBLOQUEO,
	TMP.[CodigoMedico],
	RTRIM(S.NOMMEDICO) [NombreMedico],
	TMP.[CodigoEspecialidad],
	RTRIM(SP.DESESPECI) DescripcionEspecialidad
FROM (	
			select 
				a.codcenate [CODCENATE],
				rtrim(b.nomcenate) as [CENTROATENCION],
				a.ufucodigo [UFUCODIGO],
				rtrim(c.ufudescri) as [UNIDADFUN],
				a.codicamas [CODICAMAS], 
				a.desccamas [DescripcionCama],
				a.estadcama [ESTADOCAMA],				
				(select Top 1 ipcodpaci from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as IPCODPACI,
				(select Top 1 NUMINGRES from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as INGRESO,
				(select Top 1 CODPROSAL from chregesta  with (nolock) where codicamas = A.CODICAMAS and REGESTADO = 1) as [CodigoMedico],
				(select Top 1 CODESPECI from CHREGESTA  with (nolock) where CODICAMAS = A.CODICAMAS and REGESTADO = 1) as [CodigoEspecialidad],
				(select Top 1 chregeblo.CODTIPBLO from chregeblo  with (nolock) where CODICAMAS = A.CODICAMAS and chregeblo.FECFINBLO is null) as [CodigoBloqueo]
			from
				chcamasho as a with (nolock)
				inner join adcenaten as b with (nolock) on a.codcenate = b.codcenate
				inner join inunifunc as c with (nolock) on a.ufucodigo = c.ufucodigo	
			WHERE a.CODCENATE = @centroAtencion and a.ufucodigo IN (SELECT Value FROM dbo.splitstring(@unidadFuncional))
		) as TMP 
	left join INPACIENT P with (nolock) on P.IPCODPACI =  TMP.IPCODPACI
	left join INENTIDAD i with (nolock) on p.CODENTIDA = i.CODENTIDA
	LEFT OUTER JOIN CHREGAISL AS K with (nolock)  ON  TMP.[CODICAMAS] = K.CODICAMAS AND K.IPCODPACI = P.IPCODPACI and K.FECFINAIS is null
	LEFT JOIN CHTIPOSAISLAMIENTOS CH with (nolock) ON K.CODAISLAM = CH.Id
	left join indiagnop dp with (nolock) on tmp.IPCODPACI = dp.IPCODPACI and dp.CODDIAPRI = 1 and dp.NUMINGRES = tmp.INGRESO
	LEFT JOIN INDIAGNOS DI WITH (NOLOCK) ON DP.CODDIAGNO = DI.CODDIAGNO
	LEFT JOIN CHTIPBLOQ Q WITH (NOLOCK) ON TMP.[CodigoBloqueo] = Q.CODTIPBLO
	LEFT JOIN INPROFSAL S WITH (NOLOCK) ON TMP.[CodigoMedico] = S.CODPROSAL
	LEFT JOIN INESPECIA SP WITH (NOLOCK) ON TMP.[CodigoEspecialidad] = SP.CODESPECI
	left outer join CHCAMATRAZA tz with (nolock) on TMP.CODICAMAS = tz.codicamas and tz.FECHAFINAL is null 
where 	
	tmp.[UFUCODIGO] IN (SELECT Value FROM dbo.splitstring(@unidadFuncional))
order by
	tmp.[CODICAMAS]
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el estado actual de todas las camas hospitalarias para un centro de atención y una o varias unidades funcionales (salas o servicios) específicas. Por cada cama retorna su ubicación (sede y unidad), descripción, estado operativo (libre, ocupada, bloqueada), el paciente internado con su nombre, entidad aseguradora, diagnóstico principal (CIE-10), tipo de aislamiento vigente, motivo de bloqueo (limpieza, mantenimiento, reserva), médico tratante y especialidad. Integra información del maestro de camas (CHCAMASHO), el registro activo de estancias del paciente (CHREGESTA), los bloqueos vigentes (CHREGEBLO), datos del paciente (INPACIENT), entidad aseguradora (INENTIDAD), diagnósticos (INDIAGNOP/INDIAGNOS), aislamientos (CHREGAISL/CHTIPOSAISLAMIENTOS) y profesionales de salud (INPROFSAL/INESPECIA). Es el procedimiento principal del censo de camas en tiempo real, utilizado para visualizar la ocupación del piso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEstadoActualCamas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el estado actual de las camas hospitalarias de un centro de atención y unidades funcionales, incluyendo paciente ocupante, entidad, aislamiento, diagnóstico principal, bloqueo, médico tratante y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en adcenaten; Las unidades funcionales deben venir como cadena delimitada parseable por dbo.splitstring; Las unidades funcionales deben existir en inunifunc', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera una ocupación activa por cama (TOP 1 con REGESTADO=1); Solo se considera un bloqueo vigente por cama (FECFINBLO IS NULL); Solo se considera un aislamiento vigente por cama+paciente (FECFINAIS IS NULL); Solo se considera un diagnóstico principal por ingreso (CODDIAPRI=1); Solo se considera una traza de cama vigente (FECHAFINAL IS NULL); Las camas siempre se listan aunque no tengan paciente, aislamiento, diagnóstico, bloqueo, médico o especialidad (uso de LEFT JOIN); El filtro de unidad funcional se aplica tanto en la subconsulta interna como en el WHERE externo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Centro de atención; Unidad funcional; Estado de cama; Paciente; Ingreso hospitalario; Entidad (aseguradora/responsable); Aislamiento; Diagnóstico principal; Bloqueo de cama; Médico tratante; Especialidad; Trazabilidad de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cama del centro de atención filtrado, cuyo ufucodigo esté en la lista de unidades funcionales recibidas, ordenadas por código de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si chregesta.REGESTADO = 1 para la cama → Se considera al paciente, ingreso, médico y especialidad como ocupantes/atendientes activos de la cama; si chregeblo.FECFINBLO IS NULL para la cama → Se toma ese registro como bloqueo vigente y se reporta su tipo; si CHREGAISL.FECFINAIS IS NULL y coincide cama+paciente → Se reporta el aislamiento vigente del paciente en esa cama; si indiagnop.CODDIAPRI = 1 y mismo NUMINGRES del paciente → Se reporta como diagnóstico principal del ingreso; si CHCAMATRAZA.FECHAFINAL IS NULL → Se toma esa traza como vigente y se reporta su FECHAINICIAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.chcamasho; dbo.adcenaten; dbo.inunifunc; dbo.chregesta; dbo.chregeblo; dbo.INPACIENT; dbo.INENTIDAD; dbo.CHREGAISL; dbo.CHTIPOSAISLAMIENTOS; dbo.indiagnop; dbo.INDIAGNOS; dbo.CHTIPBLOQ; dbo.INPROFSAL; dbo.INESPECIA; dbo.CHCAMATRAZA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoActualCamas';
-- GO
