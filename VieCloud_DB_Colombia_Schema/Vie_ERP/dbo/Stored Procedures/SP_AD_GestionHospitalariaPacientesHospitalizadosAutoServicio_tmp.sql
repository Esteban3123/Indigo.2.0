
CREATE PROCEDURE [dbo].[SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp]
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
		FROM dbo.CHCAMASHO A 
		INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE 
		INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
		INNER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1
		INNER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
		INNER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI
		INNER JOIN dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
		INNER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
		INNER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
		INNER JOIN dbo.INUNIFUNC U ON U.UFUCODIGO = J.UFUACTPAC
		WHERE A.CODCENATE= @CentroAtencion  AND convert(varchar(max),A.UFUCODIGO) IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))  AND ESTADCAMA =2 
			AND NOT EXISTS (
				SELECT 1 FROM [Authorization].PatientConfirmation PC 
				WHERE PC.AdmisionNumber = C.NUMINGRES 
					AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(C.NUMINGRES)
			)

UNION ALL

		SELECT  '4- Pacientes egresados' AS 'Egreso', CASE J.SERSUSCEP WHEN 1 THEN 'Alerta' ELSE 'Normal' END AS 'Alerta', A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS 'Cama',dbo.ClaseHabitacion(A.CODCLAHAB) AS 'ClaseHabitacion',dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS 'Identificacion',C.NUMINGRES AS 'Ingreso', dbo.TipoAislamiento(A.CODAISLAM) AS 'Aislamiento',RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS 'Paciente',CAST(0 AS BIT) AS 'Resultado', A.CAMTRACIR AS 'TrasladoCirugia', A.CAMTRAMED AS 'TrasladoMedicamentos', A.CODCONCEC AS 'Consecutivo',CAST('' as bit) AS 'MuestraAlerta',
		J.CODESPTRA AS 'CodigoEspecialidad',RTRIM(K.DESESPECI) AS 'DescripcionEspecialidad',IFECHAING, J.ESCADOWNT , J.ESCARASS, J.ESCVASPAC, J.ESCAPAPAC, J.ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJERASS', dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEVAS',  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJEAPACHE',  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as 'PUNTAJENORTON', RTRIM(J.UFUACTPAC) + ' - ' + RTRIM(U.UFUDESCRI) as 'UnidadFuncionalActual', RTRIM(J.UFUACTPAC) as 'CodigoUfuActual'
		, CASE WHEN FECHEGRESO IS NULL THEN 'Sin egreso' ELSE CONVERT(VARCHAR, (FORMAT(FECHEGRESO, 'dd/MM/yyyy'))) END AS 'FechaEgreso', IPFECNACI AS 'Fecha Nacimiento', CAST('' AS CHAR(50)) AS 'Edad',RTRIM(P.CODENTIDA) + '-'+ RTRIM(P.NOMENTIDA) as 'EntidadPaciente'
		FROM dbo.CHCAMASHO A 
		INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE 
		INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
		LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 2
		INNER JOIN (
					SELECT max(ID) AS ID, IPCODPACI FROM dbo.chregesta aa inner join
					CHCAMASHO bb ON aa.CODICAMAS = bb.CODICAMAS
					where regestado = 2 and bb.CODCENATE = @CentroAtencion and bb.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@UnidadFuncional))
					group by IPCODPACI
				) AS tmp ON tmp.ID = C.ID
		LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
		LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI
		LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
		LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
		LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
		LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
		LEFT OUTER JOIN dbo.INUNIFUNC U ON U.UFUCODIGO = J.UFUACTPAC
		WHERE A.CODCENATE= @CentroAtencion AND convert(varchar(max),A.UFUCODIGO) 
		IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional ))  AND CONVERT(nvarchar, FECHEGRESO) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre  AND SERSUSCEP = 1 
			AND NOT EXISTS (
				SELECT 1 FROM [Authorization].PatientConfirmation PC 
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
			FROM HCREGEGRE A --TIENE EGRESO
			INNER JOIN  dbo.ADINGRESO B ON A.NUMINGRES=B.NUMINGRES
			INNER JOIN  dbo.INPACIENT C ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN  dbo.INESPECIA D ON B.CODESPTRA = D.CODESPECI
			INNER JOIN  dbo.INUNIFUNC E ON B.UFUCODIGO = E.UFUCODIGO
			INNER JOIN  dbo.INENTIDAD F WITH(NOLOCK) ON C.CODENTIDA = F.CODENTIDA
			LEFT OUTER JOIN  dbo.HCHISPACA G ON A.NUMINGRES = G.NUMINGRES AND B.DESTINOPAC IN (9,10,11,12,15,16,17) --DESTINOS DE SALIDA
			WHERE A.CODCENATE=  @CentroAtencion  AND CONVERT(varchar(max),A.UFUCODIGO) IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional )) AND CONVERT(nvarchar, A.FECALTPAC) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre AND SERSUSCEP = 1 
			AND E.UFUTIPUNI = '1' --URGENCIAS
			AND NOT EXISTS (SELECT 1 FROM CHREGESTA PC  WHERE PC.NUMINGRES = A.NUMINGRES) --NO TENGA CAMA por ese ingreso
			AND NOT EXISTS (SELECT 1 FROM [Authorization].PatientConfirmation PC WHERE PC.AdmisionNumber = A.NUMINGRES AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(A.NUMINGRES))

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
				FROM HCHISPACA WHERE NUMINGRES = C.NUMINGRES AND IPCODPACI = C.IPCODPACI AND INDICAPAC IN ('2','3','4','5','6','18','19','20','21','13') ORDER BY FECHISPAC
			)As Historia
			WHERE c.CODCENATE= @CentroAtencion   AND C.UFUACTPAC IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional)) AND CONVERT(nvarchar, IFECHAING) BETWEEN  @FechaInicialEgre  AND @FechaFinalEgre 
				AND NOT EXISTS (
					SELECT 1 FROM [Authorization].PatientConfirmation PC 
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
			INNER JOIN dbo.HCORDPROQD AS QD ON ORPRO.AUTO = QD.AUTOPROCED
			INNER JOIN dbo.INPACIENT H ON H.IPCODPACI=ORPRO.IPCODPACI 
			INNER JOIN dbo.ADINGRESO J ON J.NUMINGRES=ORPRO.NUMINGRES 
			INNER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
			INNER JOIN dbo.INUNIFUNC U ON U.UFUCODIGO = J.UFUACTPAC
			INNER JOIN dbo.CHCAMASHO A ON J.CODCAMACT = A.CODICAMAS
			INNER JOIN dbo.ADcenaten D ON ORPRO.CODCENATE =D.CODCENATE 
			INNER JOIN dbo.INUNIFUNC E ON ORPRO.UFUCODIGO=E.UFUCODIGO 
			LEFT OUTER JOIN	dbo.INENTIDAD P with(nolock) ON P.CODENTIDA = H.CODENTIDA
			WHERE ORPRO.SOLICITAMATOST IN ('1','2')  AND ORPRO.CODCENATE=  @CentroAtencion AND ORPRO.UFUCODIGO IN (SELECT Value FROM dbo.splitstring( @UnidadFuncional )) AND CONVERT(nvarchar, ORPRO.FECORDMED) BETWEEN  @FechaInicialEgre  AND  @FechaFinalEgre 
			AND J.FECHEGRESO IS NULL AND NOT EXISTS (SELECT 1 FROM [Authorization].PatientConfirmation PC WHERE PC.AdmisionNumber = ORPRO.NUMINGRES AND PC.ConfirmationDate > [Authorization].ObtenerFechaMaximaSolicitud(ORPRO.NUMINGRES))
end
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consolida la lista de pacientes hospitalizados para un centro de atención y una o varias unidades funcionales, clasificándolos en categorías: pacientes activos en cama, con alta médica, egresados (con y sin cama), en urgencias con orden de hospitalización y con órdenes de material de osteosíntesis. Para cada paciente retorna datos de cama, identificación, ingreso, entidad aseguradora, especialidad, escalas clínicas (DownTon, RASS, VAS, Apache, Norton) y fecha de egreso, excluyendo aquellos con confirmación de autorización vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados (en unidad, con alta médica, egresados, en urgencias con orden de hospitalización y con órdenes de osteosíntesis) para autoservicio de gestión hospitalaria, excluyendo los ya confirmados por autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CentroAtencion debe corresponder a un centro válido en ADcenaten; @UnidadFuncional es una cadena delimitada parseable por dbo.splitstring con códigos de unidades funcionales; @FechaInicialEgre y @FechaFinalEgre deben ser cadenas convertibles a fecha para los rangos de egreso/ingreso; Las funciones escalares dbo.ClaseHabitacion, ClaseCama, TipoAislamiento y las de PuntajeEscala* deben existir; El esquema [Authorization] con PatientConfirmation y ObtenerFechaMaximaSolicitud debe estar disponible', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye siempre pacientes cuyo NUMINGRES tenga en [Authorization].PatientConfirmation una ConfirmationDate posterior a la fecha máxima de solicitud (ObtenerFechaMaximaSolicitud); Solo considera camas con ESTADCAMA=2 y REGESTADO=1 para el bloque de pacientes en unidad/alta médica; Para egresados con cama solo incluye registros con REGESTADO=2 y SERSUSCEP=1, tomando el máximo ID por paciente (último estado); Para egresados de urgencias sin cama exige SERSUSCEP=1 y unidad funcional de tipo urgencias (UFUTIPUNI=''1''); El bloque de urgencias con orden de hospitalización exige IESTADOIN='''', CODICAMHO IS NULL, UFUTIPUNI=1 y existencia de un INDICAPAC válido en HCHISPACA; Filtra en todos los bloques por @CentroAtencion y por las unidades funcionales contenidas en @UnidadFuncional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Cama hospitalaria; Unidad funcional; Centro de atención; Ingreso/Admisión; Egreso/Alta médica; Urgencias; Orden de hospitalización; Traslado (cirugía/medicamentos); Aislamiento; Clase de habitación / clase de cama; Especialidad médica; Escalas clínicas (Downton, RASS, VAS, APACHE, Norton); Entidad pagadora del paciente; Confirmación de autorización; Material de osteosíntesis; Servicio susceptible/Alerta (SERSUSCEP); Destinos clínicos (Observación, UCI Adulto/Pediátrica/Neonatal, Cuidado Intermedio, Pediatría, Estancia con la madre)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un único conjunto de resultados con la unión (UNION ALL) de cinco categorías de pacientes: en la unidad / con alta médica, egresados con cama, egresados de urgencias sin cama, urgencias con orden de hospitalización y pacientes con órdenes de material de osteosíntesis', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES (HCREGEGRE) IS NULL en bloque de camas con ESTADCAMA=2 y REGESTADO=1 → Clasifica como ''3- Pacientes en la unidad'' else Clasifica como ''2- Pacientes alta medica''; si SERSUSCEP = 1 en ADINGRESO → Marca columna Alerta=''Alerta'' else Marca columna Alerta=''Normal''; si FECHEGRESO / FECALTPAC IS NULL → FechaEgreso=''Sin egreso'' else FechaEgreso se formatea como ''dd/MM/yyyy''; si En el bloque de urgencias con orden de hospitalización, INDICAPAC=13 (HCHISPACA) → Egreso se reporta como ''3- Pacientes en la unidad'' (BUG-4409) else Egreso=''1- Pacientes Urgencias con orden de hospitalizacion''; si INDICAPAC en {2,3,4,5,6,18,19,20,21} → Asigna texto descriptivo de destino (Observación, Hospitalización, UCI Adulto/Pediátrica/Neonatal, Estancia con la madre, Cuidado Intermedio, Básica, Pediatría); si Para egresados de urgencias sin cama: E.UFUTIPUNI=''1'' y NO EXISTS CHREGESTA para ese ingreso y B.DESTINOPAC IN (9,10,11,12,15,16,17) → Incluye al paciente como ''4- Pacientes egresados'' procedente de urgencias; si ORPRO.SOLICITAMATOST IN (''1'',''2'') y J.FECHEGRESO IS NULL → Incluye al paciente como ''5- Pacientes con ordenes de material de osteosintesis''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.PuntajeEscalaNorton; Authorization.ObtenerFechaMaximaSolicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.INENTIDAD; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.HCHISPACA; dbo.HCORDPROQ; dbo.HCORDPROQD; Authorization.PatientConfirmation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GestionHospitalariaPacientesHospitalizadosAutoServicio_tmp';
-- GO
