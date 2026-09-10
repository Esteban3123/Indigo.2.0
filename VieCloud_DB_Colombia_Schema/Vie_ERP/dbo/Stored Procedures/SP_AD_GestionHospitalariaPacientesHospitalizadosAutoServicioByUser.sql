

CREATE PROCEDURE [dbo].[SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser]
(                      
@CentroAtencion Char(10),
@UnidadFuncional as varchar(max),
@User as int
)
AS
BEGIN 

if len(@UnidadFuncional) = 0 set @UnidadFuncional=Null
					SET NOCOUNT ON;

	WITH CTEEgresado as (
	SELECT max(aa.ID) AS ID, aa.IPCODPACI 
					FROM dbo.chregesta aa WITH(NOLOCK)
					inner join	CHCAMASHO bb WITH(NOLOCK) ON aa.CODICAMAS = bb.CODICAMAS
					INNER JOIN ADINGRESO II WITH(NOLOCK) ON aa.NUMINGRES= II.NUMINGRES AND II.SERSUSCEP = 1
					where regestado = 2 
					and bb.CODCENATE = @CentroAtencion 
					and 
					(
						@UnidadFuncional is null or 
						bb.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))
					)
					group by aa.IPCODPACI
)

	SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '3- Pacientes en la unidad' ELSE '2- Pacientes alta medica' END AS Egreso, 
		CASE J.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END as Alerta, A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
		J.CODESPTRA AS CodigoEspecialidad,RTRIM(isnull(K.DESESPECI,'')) AS DescripcionEspecialidad,IFECHAING, 
		J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS,  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE,  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as UnidadFuncionalActual, RTRIM(J.UFUACTPAC) as CodigoUfuActual
		, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS FechaEgreso, IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS Edad,RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as EntidadPaciente, iif((select Top 1 IPCODPACI from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
		FROM dbo.CHCAMASHO A WITH(NOLOCK)
		join [Authorization].MyAdmissionsByUsers mabu on mabu.UserId = @User
		INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON A.CODCENATE=D.CODCENATE --
		INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON A.UFUCODIGO=E.UFUCODIGO --
		INNER JOIN dbo.CHREGESTA C WITH(NOLOCK) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 AND ESTADCAMA =2 and c.NUMINGRES = mabu.AdmissionNumber
		INNER JOIN dbo.CHTIPESTA G WITH(NOLOCK) ON G.CODTIPEST=C.CODTIPEST 
		INNER JOIN dbo.INPacient H WITH(NOLOCK) ON C.IPCODPACI=H.IPCODPACI
		INNER JOIN dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		INNER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON C.NUMINGRES=J.NUMINGRES
		INNER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
		LEFT OUTER JOIN dbo.HCREGEGRE I WITH(NOLOCK) ON C.NUMINGRES=I.NUMINGRES
		left JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
		WHERE A.CODCENATE= @CentroAtencion 
		AND C.NUMINGRES = mabu.AdmissionNumber 
		AND 
		(
			@UnidadFuncional is null or 
			A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))  
		)
			AND NOT EXISTS (
				SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK)
				WHERE PC.AdmisionNumber = C.NUMINGRES 
					AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(C.NUMINGRES)
			)

UNION ALL

		SELECT  '4- Pacientes egresados' AS 'Egreso', CASE J.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END AS 'Alerta', A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS 'Cama',dbo.ClaseHabitacion(A.CODCLAHAB) AS 'ClaseHabitacion',dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS 'Identificacion',C.NUMINGRES AS 'Ingreso', dbo.TipoAislamiento(A.CODAISLAM) AS 'Aislamiento',RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS 'Paciente',CAST(0 AS BIT) AS 'Resultado', A.CAMTRACIR AS 'TrasladoCirugia', A.CAMTRAMED AS 'TrasladoMedicamentos', A.CODCONCEC AS 'Consecutivo',CAST('' as bit) AS 'MuestraAlerta',
		J.CODESPTRA AS 'CodigoEspecialidad',RTRIM(K.DESESPECI) AS 'DescripcionEspecialidad',IFECHAING, J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJERASS', dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEVAS',  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEAPACHE',  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJENORTON', RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as 'UnidadFuncionalActual', RTRIM(J.UFUACTPAC) as 'CodigoUfuActual'
		, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS 'FechaEgreso', IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS 'Edad',RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as 'EntidadPaciente', iif((select Top 1 IPCODPACI from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
		FROM dbo.CHCAMASHO A WITH(NOLOCK) 
		join [Authorization].MyAdmissionsByUsers mabu on mabu.UserId = @User
		INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON A.CODCENATE=D.CODCENATE --
		INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON A.UFUCODIGO=E.UFUCODIGO --
		LEFT OUTER JOIN dbo.CHREGESTA C WITH(NOLOCK) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 2 and c.NUMINGRES = mabu.AdmissionNumber
		INNER JOIN  CTEEgresado AS tmp ON tmp.ID = C.ID
		LEFT OUTER JOIN dbo.CHTIPESTA G WITH(NOLOCK) ON G.CODTIPEST=C.CODTIPEST 
		LEFT OUTER JOIN dbo.INPacient H WITH(NOLOCK) ON C.IPCODPACI=H.IPCODPACI
		LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		LEFT OUTER JOIN dbo.HCREGEGRE I WITH(NOLOCK) ON C.NUMINGRES=I.NUMINGRES--
		LEFT OUTER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON C.NUMINGRES=J.NUMINGRES
		LEFT OUTER JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
		LEFT OUTER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
		WHERE A.CODCENATE= @CentroAtencion 
		AND C.NUMINGRES = mabu.AdmissionNumber 
		AND 
			(
				@UnidadFuncional is null or 
				A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional ))   AND SERSUSCEP = 1 
			)
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
			RTRIM(F.CODENTIDA) + '-'+ RTRIM(F.NOMENTIDA) as 'EntidadPaciente',
			iif((select Top 1 IPCODPACI from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
			FROM HCREGEGRE A WITH(NOLOCK)--TIENE EGRESO
			join [Authorization].MyAdmissionsByUsers mabu on mabu.UserId = @User and a.NUMINGRES = mabu.AdmissionNumber
			INNER JOIN  dbo.ADINGRESO B WITH(NOLOCK) ON A.NUMINGRES=B.NUMINGRES 
			INNER JOIN  dbo.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN  dbo.INESPECIA D WITH(NOLOCK) ON B.CODESPTRA = D.CODESPECI
			INNER JOIN  dbo.INUNIFUNC E WITH(NOLOCK) ON B.UFUCODIGO = E.UFUCODIGO
			INNER JOIN  dbo.INENTIDAD F WITH(NOLOCK) ON C.CODENTIDA = F.CODENTIDA
			INNER JOIN  dbo.HCHISPACA G WITH(NOLOCK) ON A.NUMINGRES = G.NUMINGRES AND B.DESTINOPAC IN (9,10,11,12,15,16,17) --DESTINOS DE SALIDA--
			WHERE A.CODCENATE=  @CentroAtencion  
			AND (
				@UnidadFuncional is null or 
				A.UFUCODIGO IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional ))  AND SERSUSCEP = 1 
				)
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
			, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS FechaEgreso, IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS Edad,RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as EntidadPaciente, iif((select Top 1 IPCODPACI from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
			FROM  dbo.ADINGRESO C with (NOLOCK)
			join [Authorization].MyAdmissionsByUsers mabu on mabu.UserId = @User
			INNER JOIN dbo.ADcenaten D  with(nolock) ON C.CODCENATE=D.CODCENATE --
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
			WHERE c.CODCENATE= @CentroAtencion   
			AND C.NUMINGRES = mabu.AdmissionNumber 
			AND 
			(
				@UnidadFuncional is null or 
				C.UFUACTPAC IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional)) 
			)
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
			RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as 'UnidadFuncionalActual', RTRIM(J.UFUACTPAC) as 'CodigoUfuActual', 'Sin egreso' AS 'FechaEgreso', H.IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS 'Edad', RTRIM(P.CODENTIDA) + ' - '+ RTRIM(P.NOMENTIDA) as 'EntidadPaciente',
			iif((select Top 1 IPCODPACI from dbo.RecommendPatient where IPCODPACI = ORPRO.IPCODPACI and NUMINGRES = ORPRO.NUMINGRES) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
			FROM dbo.HCORDPROQ AS ORPRO 
			join [Authorization].MyAdmissionsByUsers mabu on mabu.UserId = @User
			INNER JOIN dbo.HCORDPROQD AS QD WITH(NOLOCK) ON ORPRO.AUTO = QD.AUTOPROCED
			INNER JOIN dbo.INPACIENT H WITH(NOLOCK) ON H.IPCODPACI=ORPRO.IPCODPACI 
			INNER JOIN dbo.ADINGRESO J WITH(NOLOCK) ON J.NUMINGRES=ORPRO.NUMINGRES 
			INNER JOIN dbo.INESPECIA K WITH(NOLOCK) ON J.CODESPTRA=K.CODESPECI
			INNER JOIN dbo.INUNIFUNC U WITH(NOLOCK) ON U.UFUCODIGO = J.UFUACTPAC
			INNER JOIN dbo.CHCAMASHO A WITH(NOLOCK) ON J.CODCAMACT = A.CODICAMAS
			INNER JOIN dbo.ADcenaten D WITH(NOLOCK) ON ORPRO.CODCENATE =D.CODCENATE 
			INNER JOIN dbo.INUNIFUNC E WITH(NOLOCK) ON ORPRO.UFUCODIGO=E.UFUCODIGO 
			LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
			WHERE ORPRO.SOLICITAMATOST IN ('1','2')  AND ORPRO.CODCENATE=  @CentroAtencion 
			and ORPRO.NUMINGRES = mabu.AdmissionNumber
			AND(
					@UnidadFuncional is null or 
					ORPRO.UFUCODIGO IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional )) 
				)
			AND J.FECHEGRESO IS NULL AND NOT EXISTS (SELECT 1 FROM [Authorization].PatientConfirmation PC WITH(NOLOCK) WHERE PC.AdmisionNumber = ORPRO.NUMINGRES AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(ORPRO.NUMINGRES))
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna la lista de pacientes hospitalizados asignados a un usuario específico del sistema de autoservicio, filtrando por centro de atención y opcionalmente por unidad funcional (sala o servicio). Combina información de camas (CHCAMASHO), estados de estancia (CHREGESTA), datos del ingreso (ADINGRESO) y el control de acceso por usuario (MyAdmissionsByUsers) para mostrar solo los ingresos que el usuario tiene autorizados. Clasifica a los pacientes en tres grupos: actualmente en la unidad, con alta médica pendiente de egreso formal, y ya egresados, incluyendo para cada uno datos clínicos como escalas de valoración (DownTon, RASS, VAS, Apache, Norton), tipo de aislamiento, clase de cama, entidad aseguradora, especialidad tratante y si el paciente tiene recomendación activa. Sirve como fuente principal del panel de gestión hospitalaria del autoservicio clínico, garantizando que cada profesional solo vea los pacientes bajo su responsabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, para un usuario autorizado, los pacientes hospitalizados/egresados/en urgencias con orden de hospitalización y con órdenes de osteosíntesis en un centro de atención y unidades funcionales dadas, clasificándolos por estado clínico-administrativo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe tener admisiones asociadas en Authorization.MyAdmissionsByUsers para que se devuelva información; Debe existir un centro de atención válido; Si @UnidadFuncional viene vacío se trata como NULL para no filtrar por unidad funcional; Los ingresos no deben tener una confirmación posterior a la fecha máxima de solicitud (Authorization.ObtenerFechaMaximaSolicitud)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila devuelta corresponde a un ingreso accesible por el usuario vía Authorization.MyAdmissionsByUsers; Toda fila pertenece al centro de atención solicitado; No se devuelven ingresos cuya última confirmación de paciente es posterior a la fecha máxima de solicitud; Para egresados por urgencias (BUG-4409), la unidad funcional siempre es de tipo urgencias (UFUTIPUNI=''1'') y el ingreso no tiene cama registrada en CHREGESTA; El procedimiento es de sólo lectura: no realiza INSERT/UPDATE/DELETE; Para el subconjunto de egresados desde CHREGESTA se toma sólo el registro con MAX(ID) por paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas categorizadas en 5 grupos: ''1- Pacientes Urgencias con orden de hospitalizacion'', ''2- Pacientes alta medica'', ''3- Pacientes en la unidad'', ''4- Pacientes egresados'', ''5- Pacientes con ordenes de material de osteosintesis''; [RETURN_RESULT] RESULTSET: Marca Alerta=''Alerta'' cuando ADINGRESO.SERSUSCEP = 1, sino ''Normal''; [RETURN_RESULT] RESULTSET: El grupo ''2- Pacientes alta medica'' aplica cuando existe egreso (HCREGEGRE.NUMINGRES no nulo) y CHREGESTA.REGESTADO=1 con ESTADCAMA=2; ''3- Pacientes en la unidad'' cuando no hay egreso registrado en HCREGEGRE; [RETURN_RESULT] RESULTSET: El grupo ''4- Pacientes egresados'' aplica cuando CHREGESTA.REGESTADO=2 (cama egresada) tomando el último ID por paciente, o cuando hay registro en HCREGEGRE con destinos de salida (B.DESTINOPAC IN (9,10,11,12,15,16,17)) en unidades de urgencias (UFUTIPUNI=''1'') y sin cama asociada; [RETURN_RESULT] RESULTSET: Para el grupo de urgencias con orden de hospitalización se usa HCHISPACA.INDICAPAC IN (''2'',''3'',''4'',''5'',''6'',''18'',''19'',''20'',''21'',''13''); si INDICAPAC=13 se reclasifica como ''3- Pacientes en la unidad''; [RETURN_RESULT] RESULTSET: Pacientes en urgencias con orden de hospitalización se incluyen sólo si IESTADOIN='''' y CODICAMHO IS NULL y la unidad funcional actual es de tipo urgencias (UFUTIPUNI=1); [RETURN_RESULT] RESULTSET: El grupo de osteosíntesis sólo incluye órdenes con HCORDPROQ.SOLICITAMATOST IN (''1'',''2'') y cuyo ingreso aún no tiene fecha de egreso (J.FECHEGRESO IS NULL); [RETURN_RESULT] RESULTSET: Marca Recomendacion=1 si existe registro en RecommendPatient para el paciente e ingreso, sino 0; [RETURN_RESULT] RESULTSET: Excluye ingresos cuya PatientConfirmation.ConfirmationDate sea posterior a ObtenerFechaMaximaSolicitud(NUMINGRES)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si len(@UnidadFuncional) = 0 → Se asigna NULL para desactivar el filtro por unidad funcional; si HCREGEGRE.NUMINGRES IS NULL → Egreso = ''3- Pacientes en la unidad'' else Egreso = ''2- Pacientes alta medica''; si ADINGRESO.SERSUSCEP = 1 → Alerta = ''Alerta'' else Alerta = ''Normal''; si ADINGRESO.FECHEGRESO IS NULL → FechaEgreso = ''Sin egreso'' else FechaEgreso = formato dd/MM/yyyy; si HCHISPACA.INDICAPAC = 13 → Egreso = ''3- Pacientes en la unidad'' else Egreso = ''1- Pacientes Urgencias con orden de hospitalizacion''; si ADINGRESO.SERSUSCEP = 1 (en bloque urgencias->hospitalización egresados) → Sólo se aplica el filtro de unidad funcional cuando el ingreso es de servicio susceptible; si HCHISPACA.INDICAPAC en (''2'',''3'',''4'',''5'',''6'',''18'',''19'',''20'',''21'') → Se asigna la descripción de destino correspondiente (Observación, Hospitalización, UCI Adulto/Pediátrica/Neonatal, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.PuntajeEscalaNorton; Authorization.ObtenerFechaMaximaSolicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicioByUser';
-- GO
