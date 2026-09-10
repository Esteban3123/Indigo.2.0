

CREATE PROCEDURE [dbo].[SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD]
(                      
@CentroAtencion Char(10),
@UnidadFuncional as varchar(max),
@ListarEgresados as bit, --Obsoleta
@FechaInicialEgre as varchar(max),
@FechaFinalEgre as varchar(max)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '3- Pacientes en la unidad' ELSE '2- Pacientes alta medica' END AS Egreso, 
		CASE J.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END as Alerta, A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
		J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING, 
		J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS,  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE,  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as UnidadFuncionalActual, RTRIM(J.UFUACTPAC) as CodigoUfuActual
		, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS FechaEgreso, IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS Edad,RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as EntidadPaciente
		FROM dbo.CHCAMASHO A WITH(NOLOCK)
		INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON A.CODCENATE=D.CODCENATE 
		INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON A.UFUCODIGO=E.UFUCODIGO 
		INNER JOIN dbo.CHREGESTA C WITH(NOLOCK) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1
		INNER JOIN dbo.CHTIPESTA G WITH(NOLOCK) ON G.CODTIPEST=C.CODTIPEST 
		INNER JOIN dbo.INPacient H WITH(NOLOCK) ON C.IPCODPACI=H.IPCODPACI
		INNER JOIN dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		LEFT OUTER JOIN dbo.HCREGEGRE I WITH(NOLOCK) ON C.NUMINGRES=I.NUMINGRES
		INNER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON C.NUMINGRES=J.NUMINGRES
		INNER JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
		INNER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
		WHERE A.CODCENATE= @CentroAtencion  AND convert(varchar(max),A.UFUCODIGO) IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))  AND ESTADCAMA =2 
			AND NOT EXISTS (
				SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK)
				WHERE PC.AdmisionNumber = C.NUMINGRES 
					AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(C.NUMINGRES)
			)

UNION ALL

		SELECT  '4- Pacientes egresados' AS 'Egreso', CASE J.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END AS 'Alerta', A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS 'Cama',dbo.ClaseHabitacion(A.CODCLAHAB) AS 'ClaseHabitacion',dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS 'Identificacion',C.NUMINGRES AS 'Ingreso', dbo.TipoAislamiento(A.CODAISLAM) AS 'Aislamiento',RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS 'Paciente',CAST(0 AS BIT) AS 'Resultado', A.CAMTRACIR AS 'TrasladoCirugia', A.CAMTRAMED AS 'TrasladoMedicamentos', A.CODCONCEC AS 'Consecutivo',CAST('' as bit) AS 'MuestraAlerta',
		J.CODESPTRA AS 'CodigoEspecialidad',RTRIM(K.DESESPECI) AS 'DescripcionEspecialidad',IFECHAING, J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJERASS', dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEVAS',  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEAPACHE',  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJENORTON', RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as 'UnidadFuncionalActual', RTRIM(J.UFUACTPAC) as 'CodigoUfuActual'
		, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS 'FechaEgreso', IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS 'Edad',RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as 'EntidadPaciente'
		FROM dbo.CHCAMASHO A WITH(NOLOCK) 
		INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON A.CODCENATE=D.CODCENATE 
		INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON A.UFUCODIGO=E.UFUCODIGO 
		LEFT OUTER JOIN dbo.CHREGESTA C WITH(NOLOCK) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 2
		INNER JOIN (
					SELECT max(ID) AS ID, IPCODPACI 
					FROM dbo.chregesta aa WITH(NOLOCK)
					inner join	CHCAMASHO bb WITH(NOLOCK) ON aa.CODICAMAS = bb.CODICAMAS
					where regestado = 2 and bb.CODCENATE = @CentroAtencion and bb.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))
					group by IPCODPACI
				) AS tmp ON tmp.ID = C.ID
		LEFT OUTER JOIN dbo.CHTIPESTA G WITH(NOLOCK) ON G.CODTIPEST=C.CODTIPEST 
		LEFT OUTER JOIN dbo.INPacient H WITH(NOLOCK) ON C.IPCODPACI=H.IPCODPACI
		LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		LEFT OUTER JOIN dbo.HCREGEGRE I WITH(NOLOCK) ON C.NUMINGRES=I.NUMINGRES
		LEFT OUTER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON C.NUMINGRES=J.NUMINGRES
		LEFT OUTER JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
		LEFT OUTER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
		WHERE A.CODCENATE= @CentroAtencion AND convert(varchar(max),A.UFUCODIGO) 
		IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional ))  AND CONVERT(nvarchar, FECHEGRESO) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre  AND SERSUSCEP = 1 
			AND NOT EXISTS (
				SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK)
				WHERE PC.AdmisionNumber = C.NUMINGRES 
				AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(C.NUMINGRES)
			)

	-- Modificado para BUG-4409 - INICIO codigo anadido
UNION ALL

			SELECT '4- Pacientes egresados' AS 'Egreso',
			CASE B.SERSUSCEP
			WHEN 1 THEN 'Alerta'
			ELSE 'Normal'
			END as Alerta,
			'' AS 'Codigo Cama',
			'' AS 'Cama',
			'' AS 'ClaseHabitacion',
			'' AS 'Clase de Cama',
			A.IPCODPACI AS Identificacion,
			A.NUMINGRES AS Ingreso,
			'' AS 'Aislamiento',
			'' AS 'Tipo Estancia',
			RTRIM(C.IPNOMCOMP) AS 'Paciente',
			CAST(0 AS BIT) AS 'Resultado',
			CAST(0 AS BIT) AS 'TrasladoCirugia',
			CAST(0 AS BIT) AS 'TrasladoMedicamentos',
			NULL AS 'Consecutivo',
			CAST('' AS BIT) AS 'MuestraAlerta',
			B.CODESPTRA AS 'CodigoEspecialidad',
			RTRIM(D.DESESPECI) AS 'DescripcionEspecialidad',
			B.IFECHAING, B.ESCADOWNT , B.ESCARASS, B.ESCVASPAC, B.ESCAPAPAC, B.ESCNORPAC,
			dbo.PuntajeEscalaDownTon(B.NUMINGRES, B.IPCODPACI) AS 'PUNTAJEDOWN',
			dbo.PuntajeEscalaRass(B.NUMINGRES, B.IPCODPACI) AS 'PUNTAJERASS', 
			dbo.PuntajeEscalaVas(B.NUMINGRES, B.IPCODPACI) AS 'PUNTAJEVAS',
			dbo.PuntajeEscalaApache(B.NUMINGRES, B.IPCODPACI) AS 'PUNTAJEAPACHE',
			dbo.PuntajeEscalaNorton(B.NUMINGRES, B.IPCODPACI) AS 'PUNTAJENORTON',
			RTRIM(B.UFUACTPAC) + ' - ' + RTRIM(E.UFUDESCRI) AS 'UnidadFuncionalActual',
			RTRIM(B.UFUACTPAC) AS 'CodigoUfuActual',
			CASE WHEN A.FECALTPAC IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(A.FECALTPAC, 'dd/MM/yyyy'))) END AS FechaEgreso,
			C.IPFECNACI AS 'Fecha Nacimiento',
			CAST('' AS CHAR(50)) AS 'Edad',
			RTRIM(F.CODENTIDA) + '-'+ RTRIM(F.NOMENTIDA) as 'EntidadPaciente'
			FROM HCREGEGRE A WITH(NOLOCK)--TIENE EGRESO
			INNER JOIN  dbo.ADINGRESO B WITH(NOLOCK) ON A.NUMINGRES=B.NUMINGRES
			INNER JOIN  dbo.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN  dbo.INESPECIA D WITH(NOLOCK) ON B.CODESPTRA = D.CODESPECI
			INNER JOIN  dbo.INUNIFUNC E WITH(NOLOCK) ON B.UFUCODIGO = E.UFUCODIGO
			INNER JOIN  dbo.INENTIDAD F WITH(NOLOCK) ON C.CODENTIDA = F.CODENTIDA
			LEFT OUTER JOIN  dbo.HCHISPACA G WITH(NOLOCK) ON A.NUMINGRES = G.NUMINGRES AND B.DESTINOPAC IN (9,10,11,12,15,16,17) --DESTINOS DE SALIDA
			WHERE A.CODCENATE=  @CentroAtencion  AND CONVERT(varchar(max),A.UFUCODIGO) IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional )) AND CONVERT(nvarchar, A.FECALTPAC) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre AND SERSUSCEP = 1 
			AND E.UFUTIPUNI = '1' --URGENCIAS
			AND NOT EXISTS (SELECT 1 FROM CHREGESTA PC WITH(NOLOCK) WHERE PC.NUMINGRES = A.NUMINGRES) --NO TENGA CAMA por ese ingreso
			AND NOT EXISTS (SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK) WHERE PC.AdmisionNumber = A.NUMINGRES AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(A.NUMINGRES))

	-- Modificado para BUG-4409 - FIN codigo anadido
UNION ALL

			-- Modificado para BUG-4409
			-- SELECT  '1- Pacientes Urgencias con orden de hospitalizacion' as Egreso, 
			SELECT (CASE INDICAPAC WHEN 13 THEN '3- Pacientes en la unidad' ELSE '1- Pacientes Urgencias con orden de hospitalizacion' END) AS 'Egreso',
			CASE C.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END as Alerta, 0 AS 'Codigo Cama','' as Cama,'Sin Clase' AS ClaseHabitacion,'Sin Clase' AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, '' AS Aislamiento, Historia.Destino AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, '0' AS TrasladoCirugia, '0' AS TrasladoMedicamentos, 0 AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
			C.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING, 
			C.ESCADOWNT , C.ESCARASS, C.ESCVASPAC, C.ESCAPAPAC, C.ESCNORPAC, dbo.PuntajeEscalaDownTon(C.NUMINGRES, C.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(C.NUMINGRES, C.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(C.NUMINGRES, C.IPCODPACI) as PUNTAJEVAS,  dbo.PuntajeEscalaApache(C.NUMINGRES, C.IPCODPACI) as PUNTAJEAPACHE,  dbo.PuntajeEscalaNorton(C.NUMINGRES, C.IPCODPACI) as PUNTAJENORTON, RTRIM(C.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as UnidadFuncionalActual, RTRIM(C.UFUACTPAC) as CodigoUfuActual
			, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS FechaEgreso, IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS Edad,RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as EntidadPaciente
			FROM  dbo.ADINGRESO C with (NOLOCK)
			INNER JOIN dbo.ADcenaten D  with(nolock) ON C.CODCENATE=D.CODCENATE 
			INNER JOIN dbo.INUNIFUNC E with(nolock)  ON C.UFUCODIGO=E.UFUCODIGO 
			INNER JOIN dbo.INPacient H  with(nolock) ON C.IPCODPACI=H.IPCODPACI
			INNER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
			INNER JOIN dbo.INESPECIA K  with(nolock) ON C.CODESPTRA=K.CODESPECI
			INNER JOIN dbo.INUNIFUNC U  with(nolock) ON C.UFUACTPAC = U.UFUCODIGO AND U.UFUTIPUNI = 1
			OUTER APPLY (
				SELECT TOP(1) NUMEFOLIO, IDETIPHIS, 
					CASE INDICAPAC 
						WHEN '2' THEN 'Trasladar a Observacion '
						WHEN '3' THEN 'Trasladar a Hospitalizacion'
						WHEN '4' THEN 'Trasladar a  UCI Adulto' 
						WHEN '5' THEN 'Trasladar a UCI Pediatrica'
						WHEN '6' THEN 'Trasladar a UCI Neonatal'
						WHEN '18' THEN 'Estancia Con la Madre'
						WHEN '19' THEN 'U.Cuidado Intermedio'
						WHEN '20' THEN 'U.Basica'
						WHEN '21' THEN 'Hospitalización Pediatría'
						END as Destino,
						INDICAPAC
						-- BUG-4409: Se añade el 13 en INDICAPAC para el condicional de Continúa en Unidad
				FROM HCHISPACA WITH(NOLOCK) WHERE NUMINGRES = C.NUMINGRES AND IPCODPACI = C.IPCODPACI AND INDICAPAC IN ('2','3','4','5','6','18','19','20','21','13') ORDER BY FECHISPAC
			)As Historia
			WHERE c.CODCENATE= @CentroAtencion   AND C.UFUACTPAC IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional)) AND CONVERT(nvarchar, IFECHAING) BETWEEN  @FechaInicialEgre  AND @FechaFinalEgre 
				AND NOT EXISTS (
					SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK)
					WHERE PC.AdmisionNumber = C.NUMINGRES 
						AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(C.NUMINGRES)
				)
				AND C.IESTADOIN = '' AND C.CODICAMHO IS NULL AND Historia.INDICAPAC is NOT NULL AND E.UFUTIPUNI = 1

UNION ALL

			SELECT DISTINCT '5- Pacientes con ordenes de material de osteosintesis' AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',
			RTRIM(a.NUMCAMHOS) + ' - ' +   RTRIM(DESCCAMAS) AS 'Cama', dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', 
			ORPRO.IPCODPACI AS 'Identificacion',ORPRO.NUMINGRES AS 'Ingreso', dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,'Hospitalizado' AS 'Tipo Estancia',RTRIM(H.IPNOMCOMP) AS 'Paciente',CAST(0 AS BIT) AS 'Resultado', 
			A.CAMTRACIR AS 'TrasladoCirugia', A.CAMTRAMED AS 'TrasladoMedicamentos', A.CODCONCEC AS 'Consecutivo',0 AS 'MuestraAlerta',	J.CODESPTRA AS 'CodigoEspecialidad',RTRIM(K.DESESPECI) AS 'DescripcionEspecialidad',
			IFECHAING, J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEDOWN', dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJERASS', 
			dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEVAS',  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEAPACHE',  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJENORTON', 
			RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as 'UnidadFuncionalActual', RTRIM(J.UFUACTPAC) as 'CodigoUfuActual', 'Sin egreso' AS 'FechaEgreso', H.IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS 'Edad', RTRIM(P.CODENTIDA) + ' - '+ RTRIM(P.NOMENTIDA) as 'EntidadPaciente'
			FROM dbo.HCORDPROQ AS ORPRO 
			INNER JOIN dbo.HCORDPROQD AS QD WITH(NOLOCK) ON ORPRO.AUTO = QD.AUTOPROCED
			INNER JOIN dbo.INPACIENT H WITH(NOLOCK) ON H.IPCODPACI=ORPRO.IPCODPACI 
			INNER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON J.NUMINGRES=ORPRO.NUMINGRES 
			INNER JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
			INNER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
			INNER JOIN dbo.CHCAMASHO A WITH(NOLOCK) ON J.CODCAMACT = A.CODICAMAS
			INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON ORPRO.CODCENATE =D.CODCENATE 
			INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON ORPRO.UFUCODIGO=E.UFUCODIGO 
			LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
			WHERE ORPRO.SOLICITAMATOST IN ('1','2')  AND ORPRO.CODCENATE=  @CentroAtencion AND ORPRO.UFUCODIGO IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional )) AND CONVERT(nvarchar, ORPRO.FECORDMED) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre 
			AND J.FECHEGRESO IS NULL AND NOT EXISTS (SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK) WHERE PC.AdmisionNumber = ORPRO.NUMINGRES AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(ORPRO.NUMINGRES))
end
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de versión anterior (sufijo OLD) que consolida mediante cuatro bloques UNION ALL los pacientes hospitalizados de un centro de atención y unidades funcionales dados, clasificándolos en: pacientes en unidad (con cama activa sin confirmación de autoservicio), pacientes con alta médica, pacientes egresados con alerta de susceptibilidad en rango de fechas, y pacientes de urgencias con orden de hospitalización pendiente. Para cada grupo retorna datos de cama, escalas clínicas (DownTon, RASS, VAS, Apache, Norton), especialidad, entidad aseguradora y unidad funcional actual, excluyendo aquellos ya confirmados en el módulo de autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados/egresados/urgencias para vista de autoservicio, clasificándolos por estado (en unidad, alta médica, egresados, con orden de hospitalización, con orden de osteosíntesis) según centro de atención, unidades funcionales y rango de fechas, excluyendo aquellos ya confirmados en autorizaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADcenaten.; Las unidades funcionales recibidas en cadena deben ser parseables por dbo.splitstring.; Las fechas inicial y final de egreso deben permitir conversión a nvarchar comparable (formato compatible con CONVERT).; Para egresados desde urgencias, la unidad funcional debe ser de tipo urgencias (UFUTIPUNI=''1'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen registros del centro de atención recibido y de las unidades funcionales seleccionadas.; Los pacientes ya confirmados en [Authorization].PatientConfirmation con fecha posterior a la última solicitud son siempre excluidos.; Las camas reportadas en pacientes activos cumplen ESTADCAMA=2 y CHREGESTA.REGESTADO=1.; Los egresados se identifican siempre con CHREGESTA.REGESTADO=2 o con HCREGEGRE.FECALTPAC no nula.; Para ''Pacientes con orden de osteosíntesis'' el ingreso debe estar abierto (FECHEGRESO IS NULL).; El bloque de urgencias con orden de hospitalización exige UFUTIPUNI=1 y CODICAMHO NULL.; Cada paciente egresado del segundo bloque aparece una sola vez (max(ID) por IPCODPACI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna pacientes activos en cama (ESTADCAMA=2, REGESTADO=1) categorizados como ''Pacientes en la unidad'' si HCREGEGRE.NUMINGRES es NULL, o ''Pacientes alta medica'' en caso contrario.; [RETURN_RESULT] RESULTSET: Retorna pacientes egresados (REGESTADO=2, SERSUSCEP=1) cuyo FECHEGRESO está en el rango [@FechaInicialEgre, @FechaFinalEgre], tomando el último registro por paciente (max(ID)).; [RETURN_RESULT] RESULTSET: Retorna pacientes egresados de urgencias (UFUTIPUNI=''1'', SERSUSCEP=1) con FECALTPAC en rango de fechas, que no tengan registro de cama (no existe en CHREGESTA por ese ingreso).; [RETURN_RESULT] RESULTSET: Retorna pacientes en urgencias (UFUTIPUNI=1) con orden de hospitalización: IESTADOIN vacío, sin cama asignada (CODICAMHO IS NULL) y con INDICAPAC en (2,3,4,5,6,18,19,20,21,13); marca como ''Pacientes en la unidad'' si INDICAPAC=13, sino como ''Pacientes Urgencias con orden de hospitalizacion''.; [RETURN_RESULT] RESULTSET: Retorna pacientes con orden de material de osteosíntesis (SOLICITAMATOST IN (''1'',''2'')) cuyo ingreso aún no tiene egreso (FECHEGRESO IS NULL) y FECORDMED dentro del rango.; [RETURN_RESULT] RESULTSET: En todas las consultas se excluyen ingresos cuya última PatientConfirmation (Authorization) tiene ConfirmationDate posterior a la fecha máxima de solicitud devuelta por [Authorization].ObtenerFechaMaximaSolicitud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCREGEGRE.NUMINGRES IS NULL para el ingreso del paciente activo en cama → Clasifica como ''3- Pacientes en la unidad'' else Clasifica como ''2- Pacientes alta medica''; si ADINGRESO.SERSUSCEP = 1 → Marca alerta como ''Alerta'' else Marca como ''Normal''; si FECHEGRESO IS NULL (o FECALTPAC IS NULL) → Reporta ''Sin egreso'' como FechaEgreso else Formatea fecha de egreso a ''dd/MM/yyyy''; si Última HCHISPACA.INDICAPAC = ''13'' → Clasifica como ''3- Pacientes en la unidad'' (continúa en unidad - BUG-4409) else Clasifica como ''1- Pacientes Urgencias con orden de hospitalizacion''; si HCHISPACA.INDICAPAC ∈ {2,3,4,5,6,18,19,20,21} → Asigna descripción de destino (Observación, Hospitalización, UCI Adulto/Pediátrica/Neonatal, Estancia con la madre, U. Cuidado Intermedio, U. Básica, Hospitalización Pediatría); si ADINGRESO.DESTINOPAC IN (9,10,11,12,15,16,17) → Considera el ingreso como destino de salida para el cruce con HCHISPACA en el bloque de egresados de urgencias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.PuntajeEscalaNorton; dbo.splitstring; Authorization.ObtenerFechaMaximaSolicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioOLD';
-- GO
