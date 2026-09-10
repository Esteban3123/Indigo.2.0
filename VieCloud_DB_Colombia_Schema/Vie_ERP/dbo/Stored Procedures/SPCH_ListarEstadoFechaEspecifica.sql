-- =============================================
-- Author:		<Johan Sebastian Carranza Ramos, Developer Junior>
-- Create date: <20 Agosto de 2019>
-- Description:	<Procedimiento almacenado que me lista el hisotrico de las camas para mostrarlo en GridView>
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarEstadoFechaEspecifica]( 
	@Centro varchar(20),
	@Unidad varchar(1000), --Vienen varias unidades funcionales!!
	@FechaInicial DateTime,
	@FechaFinal DateTime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--INSERT INTO @TABLA
	SELECT 
		Az.CODCENATE,
		RTRIM(B.NOMCENATE) AS CENTROATENCION,
		Az.UFUCODIGO,
		RTRIM(C.UFUDESCRI) AS UNIDADFUN,
		Az.CODICAMAS,
		RTRIM(E.DESCCAMAS) AS DescripcionCama,
		Az.ESTADOCAMA AS ESTADOCAMA,
		Az.FECHAINICIAL, 
		Az.FECHAFINAL,
		Az.IPCODPACI,
		RTRIM(D.IPNOMCOMP) AS PACIENTE,
		D.CODENTIDA AS CodigoEntidad,
		RTRIM(J.NOMENTIDA) AS ENTIDAD,
		L.Id AS CodigoAislamiento,
		RTRIM(L.Nombre) AS AISLAMIENTO,		
		CASE
		WHEN Az.FECHAFINAL IS NULL THEN Q.CODDIAGNO 
			ELSE O.CODDIAGNO
			END AS CodigoDiagnostico,
		CASE
			WHEN Az.FECHAFINAL IS NULL THEN RTRIM(Q.NOMDIAGNO)
			ELSE RTRIM(O.NOMDIAGNO)
		END AS DIAGNOSTICO,
		F.CODTIPBLO AS CodigoBloqueo,
		RTRIM(M.DESTIPBLO) AS TIPOBLOQUEO,
		H.CODPROSAL AS CodigoMedico, 
		RTRIM(H.NOMMEDICO) AS NombreMedico, 
		I.CODESPECI AS CodigoEspecialidad,
		RTRIM(I.DESESPECI) AS DescripcionEspecialidad		
	FROM CHCAMATRAZA AS Az with (nolock) 
		INNER JOIN 	ADCENATEN AS B  with (nolock) ON Az.CODCENATE = B.CODCENATE  
		INNER JOIN	INUNIFUNC AS C  with (nolock) ON Az.UFUCODIGO = C.UFUCODIGO  
		INNER JOIN	CHCAMASHO AS E with (nolock) ON Az.CODICAMAS = E.CODICAMAS  
		LEFT OUTER JOIN	INPACIENT AS D with (nolock) ON Az.IPCODPACI = D.IPCODPACI  
		LEFT OUTER JOIN	CHREGEBLO AS F with (nolock) ON Az.IDCHREGEBLO = F.ID
		LEFT OUTER JOIN CHREGESTA AS G with (nolock) ON E.CODICAMAS = G.CODICAMAS
			AND (Az.FECHAINICIAL = G.FECFINEST OR( ABS ( DATEDIFF( minute , G.FECINIEST,  Az.FECHAINICIAL ) ) <= 5  OR G.FECFINEST = '1900-01-01 00:00:00.000') )
			AND Az.IPCODPACI IS NOT NULL
			AND Az.IPCODPACI = G.IPCODPACI
		LEFT OUTER JOIN INPROFSAL AS H with (nolock) ON G.CODPROSAL = H.CODPROSAL
		LEFT OUTER JOIN INESPECIA AS I with (nolock) ON G.CODESPECI = I.CODESPECI
		LEFT OUTER JOIN INENTIDAD AS J with (nolock) ON D.CODENTIDA = J.CODENTIDA
		LEFT OUTER JOIN CHREGAISL AS K with (nolock)  ON  E.CODICAMAS = K.CODICAMAS
			AND D.IPCODPACI = K.IPCODPACI 
			AND G.NUMINGRES = K.NUMINGRES 
		LEFT OUTER JOIN CHTIPOSAISLAMIENTOS AS L with (nolock) ON K.CODAISLAM = L.Id
		LEFT OUTER JOIN CHTIPBLOQ AS M with (nolock) ON F.CODTIPBLO = M.CODTIPBLO
		LEFT OUTER JOIN ADINGRESO AS N with (nolock) ON G.NUMINGRES = N.NUMINGRES 
		LEFT OUTER JOIN INDIAGNOS AS O with (nolock) ON Az.CODDIAGNO = O.CODDIAGNO
		LEFT OUTER JOIN INDIAGNOP AS P with (nolock) ON D.IPCODPACI = P.IPCODPACI 
			AND N.NUMINGRES = P.NUMINGRES 
			AND P.CODDIAPRI = 1
		LEFT OUTER JOIN INDIAGNOS AS Q with (nolock) ON P.CODDIAGNO = Q.CODDIAGNO
		--WHERE	
		--Az.CODCENATE = @Centro
		--AND Az.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))
		--AND (Az.FECHAINICIAL BETWEEN @FechaInicial AND @FechaFinal OR Az.FECHAFINAL BETWEEN @FechaInicial AND @FechaFinal)
	union
	SELECT 
	tmp.[CODCENATE],
	tmp.[CENTROATENCION],
	tmp.[UFUCODIGO],
	rtrim(tmp.[UNIDADFUN]) UNIDADFUN,
	tmp.[CODICAMAS],
	rtrim(tmp.[DescripcionCama]) DescripcionCama,
	tmp.[ESTADOCAMA],
	tz.FECHAINICIAL,
	tz.FECHAFINAL,
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
				(select ipcodpaci from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as IPCODPACI,
				(select NUMINGRES from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as INGRESO,
				(select CODPROSAL from chregesta where codicamas = A.CODICAMAS and REGESTADO = 1) as [CodigoMedico],
				(select chregeblo.CODTIPBLO from chregeblo where CODICAMAS = A.CODICAMAS and chregeblo.FECFINBLO is null) as [CodigoBloqueo],
				(select CODESPECI from CHREGESTA where CODICAMAS = A.CODICAMAS and REGESTADO = 1) as [CodigoEspecialidad]
			from
				chcamasho as a with (nolock)
				inner join adcenaten as b with (nolock) on a.codcenate = b.codcenate
				inner join inunifunc as c with (nolock) on a.ufucodigo = c.ufucodigo	
			WHERE a.CODCENATE = @centro
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
	left outer join CHCAMATRAZA tz with (nolock) on TMP.IPCODPACI = tz.IPCODPACI and TMP.CODICAMAS = tz.codicamas and tz.FECHAFINAL is null
	

	ORDER BY Az.FECHAINICIAL ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el estado histórico y actual de las camas hospitalarias para una sede y un rango de fechas específico. Combina dos fuentes: el historial de trazabilidad de camas (CHCAMATRAZA) para el período solicitado, y el estado vigente de cada cama (CHCAMASHO) con su ocupación en curso, unificando ambos mediante UNION. Para cada cama devuelve: centro de atención, unidad funcional, descripción de la cama, estado, fechas de ocupación o bloqueo, datos del paciente (cédula, nombre, entidad aseguradora), diagnóstico principal (CIE-10), tipo de bloqueo o mantenimiento, médico tratante y especialidad, así como el tipo de aislamiento activo. Se utiliza para visualizar en pantalla (GridView) el tablero de camas hospitalarias — quién ocupa cada cama, desde cuándo, con qué diagnóstico y en qué estado — en un momento o período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el histórico y estado actual de camas hospitalarias (con paciente, entidad, aislamiento, diagnóstico, bloqueo, médico y especialidad) para visualizar trazabilidad por centro de atención y unidades funcionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADCENATEN y ser provisto para filtrar la rama de estado actual de camas.; Las unidades funcionales pueden venir como cadena delimitada para ser separada por dbo.splitstring (filtrado actualmente comentado).; Las tablas maestras (centros, unidades funcionales, camas, pacientes, entidades, diagnósticos, profesionales, especialidades, aislamientos, tipos de bloqueo) deben estar disponibles para los joins.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El diagnóstico principal del paciente se identifica por CODDIAPRI = 1 en INDIAGNOP.; El estado actual de una cama corresponde a CHREGESTA.REGESTADO = 1.; Un bloqueo se considera vigente cuando FECFINBLO IS NULL; un aislamiento vigente cuando FECFINAIS IS NULL; una traza vigente cuando FECHAFINAL IS NULL.; Se tolera un desfase de hasta 5 minutos al emparejar la traza de cama con el registro de estado.; FECFINEST = ''1900-01-01 00:00:00.000'' se interpreta como registro de estado aún abierto.; Todas las consultas usan WITH (NOLOCK), aceptando lecturas sucias para reportería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Trazabilidad/Histórico de cama; Estado de cama; Centro de atención; Unidad funcional; Paciente; Ingreso hospitalario; Entidad (aseguradora); Aislamiento; Diagnóstico principal; Bloqueo de cama; Profesional de la salud / Médico tratante; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Une dos conjuntos: (1) trazas históricas de CHCAMATRAZA con sus joins de paciente/diagnóstico/bloqueo/médico, y (2) estado actual de cada cama de CHCAMASHO del centro indicado, ordenados por FECHAINICIAL ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Az.FECHAFINAL IS NULL (traza de cama aún abierta) → Toma el diagnóstico desde INDIAGNOP/INDIAGNOS (Q) usando el diagnóstico principal del ingreso (CODDIAPRI=1) else Toma el diagnóstico directamente desde CHCAMATRAZA → INDIAGNOS (O); si Coincidencia de registro de estado G con la traza: Az.FECHAINICIAL = G.FECFINEST, o diferencia ≤ 5 minutos entre G.FECINIEST y Az.FECHAINICIAL, o G.FECFINEST = ''1900-01-01'' → Asocia la traza de cama con el registro de estado (CHREGESTA) para obtener médico, especialidad e ingreso, exigiendo además que IPCODPACI no sea nulo y coincida; si En la rama actual: chregesta.REGESTADO = 1 → Selecciona paciente, ingreso, médico y especialidad vigentes de la cama; si En la rama actual: chregeblo.FECFINBLO IS NULL → Considera el bloqueo vigente de la cama; si CHREGAISL.FECFINAIS IS NULL (rama actual) → Considera el aislamiento vigente del paciente en esa cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMATRAZA; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPACIENT; dbo.CHREGEBLO; dbo.CHREGESTA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INENTIDAD; dbo.CHREGAISL; dbo.CHTIPOSAISLAMIENTOS; dbo.CHTIPBLOQ; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEstadoFechaEspecifica';
-- GO
