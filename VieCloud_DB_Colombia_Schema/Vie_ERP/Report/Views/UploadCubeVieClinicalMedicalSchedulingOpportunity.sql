



--CREATE PROCEDURE [Scheduling].[SP_OPORTUNIDAD_DE_CITAS_SA]
--DECLARE	@FechaInicio DATE='2024-04-01';
--DECLARE @FechaFin DATE ='2024-04-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalMedicalSchedulingOpportunity] AS

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE d.iptipodoc 
			WHEN 1 THEN 'CC'
			WHEN 2 THEN 'CE'
			WHEN 3 THEN 'TI'
			WHEN 4 THEN 'RC'
			WHEN 5 THEN 'PA'
			WHEN 6 THEN 'AS'
			WHEN 7 THEN 'MS'
			WHEN 8 THEN 'NU'
			WHEN 9 THEN 'CN'
			WHEN 10 THEN 'CD'
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE' 
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		D.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion], 
		RTRIM(D.IPPRIAPEL) AS 'PRIMER APELLIDO',--[PrimerApellido],
		RTRIM(D.IPSEGAPEL) AS 'SEGUNDO APELLIDO',--[SegundoApellido], 
		RTRIM(D.IPPRINOMB) AS 'PRIMER NOMBRE',--[PrimerNombre], 
		RTRIM(D.IPSEGNOMB) AS 'SEGUNDO NOMBRE',--[SegundoNombre],
		CAST(D.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS 'EDAD',--[Edad],
		CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'CODIGO SEXO',--[CodSexo],
		CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS 'SEXO',--[Sexo],
		DEP.depcodigo AS 'CODIGO DEPARTAMENTO',--[CodDepartamento],
		DEP.nomdepart AS 'DEPARTAMENTO',--[Departamento], 
		MUN.MUNCODIGO 'CODIGO MUNICIPIO',--[CodMunicipio], 
		MUN.MUNNOMBRE AS 'MUNICIPIO',--[Municipio],
		UPPER(D.IPDIRECCI) AS 'DIRECCION',--[Direccion], 
		D.IPTELMOVI AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal], 
		D.IPTELEFON AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode ) ELSE RTRIM(ENT.CODENTIDA) END AS 'CODIGO ENTIDAD',--[CodEntidad],
		CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) ELSE RTRIM(ENT.NOMENTIDA) END AS 'ENTIDAD',--[Entidad],
		CASE HEA.EntityType
			WHEN 1 THEN 'EPS Contributivo'
			WHEN 2 THEN 'EPS Subsidiado'
			WHEN 3 THEN 'ET Vinculados Municipios'
			WHEN 4 THEN 'ET Vinculados Departamentos'
			WHEN 5 THEN 'ARL Riesgos Laborales'
			WHEN 6 THEN 'MP Medicina Prepagada'
			WHEN 7 THEN 'IPS Privada'
			WHEN 8 THEN 'IPS Publica'
			WHEN 9 THEN 'Regimen Especial'
			WHEN 10 THEN 'Accidentes de transito'
			WHEN 11 THEN 'Fosyga'
			WHEN 12 THEN 'Otros' END AS 'REGIMEN',--[Regimen],
		CG.Name AS 'GRUPO ATENCION',--[GrupoAtencion],

		RTRIM(C.CODIPSSEC) AS 'CODIGO IPS',--[CodIPS], 
		RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
		RTRIM(UNI.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],

		'CITA APOYO DIAGNOSTICO' AS 'TIPO AGENDAMIENTO',--[TipoAgendamiento],
		--REPS.[CodigoReps] AS [CodREPSEspecialidad]
		B.CODESPECI AS 'CODIGO ESPECIALIDAD',--[CodEspecialidad],
		RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',--[Especialidad],
		RTRIM(E.NOMMEDICO) AS 'MEDICO',--[Medico],
		RTRIM(F.DESACTMED) AS 'ACTIVIDAD AGENDAMIENTO',--[ActividadAgendamiento],
		F.DURAACTIV + ' ' + 'Minutos' AS 'DURACION ACTIVIDAD',--[DuracionActividad],
		/*
		CASE D.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL' WHEN '5' THEN 'PASAPORTE'
		WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'
		WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS [DESCRIPCION IDENTIFICACION],
		*/
		CASE WHEN A.CODTIPSOL = '0' THEN 'PRESENCIAL' WHEN A.CODTIPSOL = '1' THEN 'TELEFONICA' END AS 'FORMA SOLICITUD',--[FormaSolicitud],
		IIF (
			GC.NUMINGRES IS NULL
			,CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = '1' THEN 'CONTROL' WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' ELSE 'N/A' END 
			,CASE WHEN ING1.TIPCITMED = '1' THEN 'PRIMERA VEZ' WHEN ING1.TIPCITMED = '2' THEN 'CONTROL' 
			ELSE CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = '1' THEN 'CONTROL' WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' ELSE 'N/A' END END ) AS 'TIPO CITA',--[TipoCita],
		CASE A.MODALIDAD WHEN 0 THEN 'PRESENCIAL' WHEN 1 THEN 'TELECONSULTA' ELSE 'N/A' END AS 'MODALIDAD',--[Modalidad],
		CONVERT(varchar,CAST(A.FECITADES AS date),23) AS 'FECHA DESEADA CITA',--[FechaDeseadaCita], 
		CONVERT(varchar,CAST(A.FECHAOFERTADA AS date),23) AS 'FECHA MEJOR CITA DISPONIBLE',--[FechaMejorCitaDisponible],
		CONVERT(varchar,CAST(FECHORAIN AS date),23) AS 'FECHA ASIGNACION',--[FechaAsignacion],
		CONVERT(varchar,CAST(FECHORAIN AS datetime),20) AS 'FECHA HORA ASIGNACION',--[FechaHoraAsignacion],
		CONVERT(varchar,CAST(FECHORAFI AS date),23) AS 'FECHA FINAL ASIGNACION',--[FechaFinalAsignacion],
		CONVERT(varchar,CAST(FECHORAFI AS datetime),20) AS 'FECHA HORA FINAL ASIGNACION',--[FechaHoraFinalAsignacion],
		CONVERT(varchar,CAST(A.FECREGSIS AS date),23) AS 'FECHA SOLICITUD',--[FechaSolicitud],
		CONVERT(varchar,CAST(A.FECHCANCELA AS datetime),20) AS 'FECHA CANCELACION',--[FechaCancelacion],
		CONVERT(varchar,CAST(A.FECREGSIS AS datetime),23) AS 'FECHA REGISTRO DB',--[FechaRegistroDB],
		DATEDIFF(DAY,A.FECREGSIS,FECHORAIN) AS 'FECHA ASIGNACION VS FECHA SOLICITUD',--[FechaAsignacionVSFechaSolicitud],
		DATEDIFF(DAY,A.FECITADES,FECHORAIN) AS 'FECHA ASIGNACION VS FECHA DESEADA',--[FechaAsignacionVSFechaDeseada],
		DATEDIFF(DAY,A.FECREGSIS,A.FECHAOFERTADA) AS 'DISPONIBILIDAD',--[Disponibilidad],
		CASE WHEN A.CITAEXTRA = 1 THEN 'Si' ELSE 'No' END AS 'CITA EXTRA',--[CitaExtra],
		ISNULL(RTRIM(M.CODSERIPS),ISNULL(RTRIM(CE.Code),ISNULL(RTRIM(O.CODSERIPS),RTRIM(IPS3.CODSERIPS)))) AS 'CODIGO CUPS',--[CodCUP],
		ISNULL(RTRIM(M.DESSERIPS),ISNULL(RTRIM(CD.Name),ISNULL(RTRIM(IPS2.DESSERIPS),RTRIM(IPS3.DESSERIPS)))) AS 'DESCRIPCION CUPS',--[DescripcionCUP],
		RTRIM(G.NOMUSUARI) AS 'USUARIO REGISTRO',--[UsuarioRegistro],
		'ASIGNADA' AS 'ESTADO INICIAL',--[EstadoInicial],
		CASE WHEN A.CODESTCIT = '0' THEN 'ASIGNADA' WHEN A.CODESTCIT = '1' THEN 'CUMPLIDA' WHEN A.CODESTCIT = '2' THEN 'INCUMPLIDA'
		WHEN A.CODESTCIT = '3' THEN 'PREASIGNADA' WHEN A.CODESTCIT = '4' THEN 'CANCELADA'  WHEN A.CODESTCIT = '5' THEN 'CANCELADA' END AS 'ESTADO ACTUAL',--[EstadoActual], 
		ISNULL(A.CODCAUCAN, '00') 'CODIGO CANCELACION',--[CodigoCancelacion],
		ISNULL(RTRIM(CAN.DESCAUCAN),'') AS 'DESCRIPCION CANCELACION',--[DescripcionCancelacion],
		ISNULL(GC.NUMINGRES,'') AS 'NRO INGRESO ATENCION',--[NroIngresoAtencion],
		1 as 'CANTIDAD',--[Cantidad],
		CAST(FECHORAIN AS DATE) [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM AGASICITA A with (nolock)
	INNER JOIN ADCENATEN AS C with (nolock) ON A.CODCENATE = C.CODCENATE
	INNER JOIN AGENSALAC AS AGC with (nolock) ON AGC.CODCONCEC  =A.IDSALA AND AGC.CODCENATE =C.CODCENATE
	INNER JOIN AGACTIMED AS F with (nolock) ON A.CODACTMED = F.CODACTMED
	INNER JOIN INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI
	INNER JOIN INUBICACI AS UBI with (nolock) ON D.AUUBICACI =UBI.AUUBICACI
	INNER JOIN INMUNICIP as MUN with (nolock) ON UBI.DEPMUNCOD =MUN.DEPMUNCOD
	INNER JOIN INDEPARTA AS DEP with (nolock) ON DEP.depcodigo =MUN.DEPCODIGO
	INNER JOIN INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA
	INNER JOIN Contract.HealthAdministrator AS HEA with (nolock) ON A.GENCONENTITY = HEA.Id
	INNER JOIN SEGusuaru AS G with (nolock) ON A.CODUSUASI = G.CODUSUARI
	LEFT JOIN INCUPSIPS AS M with (nolock) ON A.CODSERIPS = M.CODSERIPS
	LEFT JOIN INPROFSAL AS E with (nolock) ON A.CODPROSAL = E.CODPROSAL
	LEFT JOIN INESPECIA AS B with (nolock) ON A.CODESPECI = B.CODESPECI
	LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id =D.GENCAREGROUP
	LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id =CECD.CUPSEntityId
	LEFT JOIN Contract.ContractDescriptions AS CD WITH (NOLOCK) ON CD.Id =CECD.ContractDescriptionId
	LEFT JOIN INUNIFUNC AS UNI with (nolock) ON UNI.UFUCODIGO =AGC.UFUCODIGO
	LEFT JOIN AGCAUCANC AS CAN with (nolock) ON A.CODCAUCAN=CAN.CODCAUCAN
	LEFT JOIN (SELECT CS1.* FROM ADCONCOEX  CS1 INNER JOIN 
	(
	SELECT MAX(CS.CODCONCEC) CODCONCEC,CS.IPCODPACI, CS.IPFECHCIT FROM ADCONCOEX AS CS  with (nolock) 
	WHERE CS.CONESTADO <>'2' /*AND IPCODPACI LIKE '24822605' AND NUMCONCIT='201983'*/
	GROUP BY CS.IPCODPACI, CS.IPFECHCIT
	) CS2 ON CS1.IPCODPACI=CS2.IPCODPACI AND CS1.IPFECHCIT=CS2.IPFECHCIT AND CS1.CODCONCEC=CS2.CODCONCEC) AS GC 
	ON GC.NUMCONCIT =A.CODAUTONU 
	LEFT JOIN RIASCUPS AS O with (nolock) ON A.IDRIASCUPS = O.ID
	LEFT JOIN RIAS AS Q with (nolock) ON O.IDRIAS = Q.ID
	LEFT JOIN INCUPSIPS AS IPS2 with (nolock) ON O.CODSERIPS =IPS2.CODSERIPS
	LEFT JOIN INCUPSIPS AS IPS3 with (nolock) ON F.CODSERIPS =IPS3.CODSERIPS
	LEFT JOIN HCHISPACA AS HIS WITH (NOLOCK) ON GC.NUMINGRES =HIS.NUMINGRES AND HIS.GENCONEXT=1 AND HIS.TIPHISPAC ='I'
	LEFT JOIN HCURGING1 AS ING1 WITH (NOLOCK) ON HIS.NUMINGRES=ING1.NUMINGRES AND HIS.NUMEFOLIO=ING1.NUMEFOLIO
	--LEFT JOIN INDIGOREP.dbo.TablaEspecialidadesReps AS REPS ON B.CODESPECI=REPS.CodigoEspecialidad

	WHERE  A.TIPSOLICITU = 2 AND YEAR(A.FECHORAIN)>=2023

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de citas de apoyo diagnóstico (solicitudes tipo 2) desde 2023 para cargar a un cubo analítico de oportunidad de agendamiento, integrando datos de paciente, entidad, especialidad, fechas y estados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe tener TIPSOLICITU = 2 (apoyo diagnóstico); El año de FECHORAIN debe ser >= 2023; Debe existir relación válida del paciente con ubicación, municipio y departamento (INNER JOIN); Debe existir entidad asociada en INENTIDAD y administrador de salud en Contract.HealthAdministrator (INNER JOIN); Debe existir centro de atención, sala, actividad médica y usuario asignador (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas reportan ''TIPO AGENDAMIENTO'' = ''CITA APOYO DIAGNOSTICO''; Todas las filas reportan ''ESTADO INICIAL'' = ''ASIGNADA'' y CANTIDAD = 1; ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres; Edad calculada como diferencia en años entre IPFECNACI y la fecha actual; ULT_ACTUAL siempre se calcula con la zona horaria ''Pakistan Standard Time''; Códigos de cancelación nulos se reemplazan por ''00'' y descripciones nulas por cadena vacía; Solo se incluyen citas registradas a partir del año 2023; Se excluyen consultas externas con estado ''2'' al buscar el ingreso asociado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Entidad / EPS / Administradora de salud; Régimen de afiliación; Grupo de atención; Centro de atención / IPS; Unidad funcional; Especialidad médica; Médico / Profesional de salud; Cita médica; Apoyo diagnóstico; CUPS (Clasificación Única de Procedimientos en Salud); RIAS (Rutas Integrales de Atención en Salud); Tipo de cita (primera vez, control, posoperatorio); Modalidad (presencial / teleconsulta); Causa de cancelación; Oportunidad de citas (fecha solicitud vs asignación vs deseada); Cita extra; Ingreso de atención; Historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT de citas filtradas por TIPSOLICITU=2 y YEAR(FECHORAIN)>=2023, etiquetadas como ''CITA APOYO DIAGNOSTICO'' y estado inicial ''ASIGNADA''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente (1..15) → Mapea a sigla de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si IPSEXOPAC = ''1'' o ''2'' → Mapea a ''H''/''HOMBRE'' o ''M''/''MUJER''; si HEA.Id IS NOT NULL → Toma código y nombre de entidad desde Contract.HealthAdministrator else Toma código y nombre desde INENTIDAD; si HEA.EntityType (1..12) → Clasifica el régimen (EPS Contributivo, EPS Subsidiado, ARL, MP, IPS Privada/Pública, Régimen Especial, Fosyga, etc.); si CODTIPSOL = ''0'' o ''1'' → Forma de solicitud ''PRESENCIAL'' o ''TELEFONICA''; si GC.NUMINGRES IS NULL → Tipo de cita se deriva de A.CODTIPCIT (0=PRIMERA VEZ, 1=CONTROL, 2=POS OPERATORIO) else Si ING1.TIPCITMED in (1,2) usa ''PRIMERA VEZ''/''CONTROL''; en otro caso vuelve a derivar de A.CODTIPCIT; si A.MODALIDAD = 0 o 1 → Modalidad ''PRESENCIAL'' o ''TELECONSULTA'', else ''N/A''; si A.CITAEXTRA = 1 → Marca ''Si'' en cita extra, sino ''No''; si A.CODESTCIT (0..5) → Mapea estado actual: 0=ASIGNADA, 1=CUMPLIDA, 2=INCUMPLIDA, 3=PREASIGNADA, 4 y 5=CANCELADA; si Prelación de códigos CUPS → Toma el primer no nulo entre M.CODSERIPS, CE.Code, O.CODSERIPS, IPS3.CODSERIPS (e idéntica prelación para descripción); si Subconsulta de ADCONCOEX con CONESTADO <> ''2'' → Selecciona el máximo CODCONCEC por paciente y fecha de cita para vincular el ingreso de atención; si HCHISPACA con GENCONEXT=1 y TIPHISPAC=''I'' → Solo asocia historias clínicas de tipo ''I'' marcadas como conectadas externamente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGASICITA; ADCENATEN; AGENSALAC; AGACTIMED; INPACIENT; INUBICACI; INMUNICIP; INDEPARTA; INENTIDAD; Contract.HealthAdministrator; SEGusuaru; INCUPSIPS; INPROFSAL; INESPECIA; Contract.CareGroup; Contract.CUPSEntityContractDescriptions; Contract.CUPSEntity; Contract.ContractDescriptions; INUNIFUNC; AGCAUCANC; ADCONCOEX; RIASCUPS; RIAS; HCHISPACA; HCURGING1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSchedulingOpportunity';
GO
