
--CREATE PROCEDURE [EHR].[SP_INTERCONSULTAS_MEDICAS]
--DECLARE	@FechaInicio Datetime='2024-06-01';
--DECLARE	@FechaFin Datetime ='2024-06-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalMedicalMedicalInterconsultations] AS

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE i.iptipodoc
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
			WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO PACIENTE',--[TipoDocumentoPaciente],
		a.IPCODPACI AS 'NRO IDENTIFICACION PACIENTE',--[NroIdentificacionPaciente],
		RTRIM(I.IPNOMCOMP) as 'NOMBRE PACIENTE',--[NomPaciente],
		HEA.Code 'CODIGO ENTIDAD',--[CodEntidad],
		HEA.Name 'ENTIDAD',--[Entidad],
		CGR.Name 'GRUPO ATENCION',--[GrpAtencion],
		CEN.NOMCENATE 'CENTRO ATENCION',--[CenAtencion],
		UNI.UFUDESCRI 'UNIDAD FUNCIONAL',--[UniFuncional],
		A.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		CAST(FECORDMED AS DATE) 'FECHA ORDEN',--[FecOrden],
		A.NUMEFOLIO 'NRO FOLIO',--[NroFolio],
		case 
			when MANEXTPRO = 0 then 'HOSPITALARIO' 
			ELSE 'AMBULATORIO' END AS 'TIPO SOLICITUD',--[TipoSolicitud],
		A.CODPROSAL 'NRO IDENTIFICACION MEDICO',--[NroIdentificacionMedico],
		PRO.NOMMEDICO AS 'NOMBRE MEDICO',--[NomMedico],

		A.CODSERIPS AS 'CODIGO SERVICIO',--[CodServicio],
		RTRIM(B.DESSERIPS) AS 'DESCRIPCION SERVICIO',--[DesServicio],
		ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',--[DesRelacionada],
		CASE 
			WHEN NUMFOLINT IS NULL THEN 'Solicitadas' 
			ELSE 'Con Respuesta' END AS 'ESTADO SERVICIO',--[EstadoServicio],
		A.NUMFOLINT 'NRO FOLIO INTERCONSULTA',--[NroFolioInterconsulta],
		CAST(HIS.FECHISPAC AS DATE) 'FECHA INTERCONSULTA',--[FecInterconsulta],
		SUM(A.CANSERIPS) AS 'CANTIDAD',--[Cantidad]
		CAST(A.FECORDMED AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.ADINGRESO ing
	INNER JOIN dbo.HCORDINTE A WITH (NOLOCK) ON ing.NUMINGRES = a.NUMINGRES
	INNER JOIN dbo.INCUPSIPS B WITH (NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPACIENT i WITH (NOLOCK) on a.IPCODPACI = i.IPCODPACI 
	INNER JOIN DBO.ADCENATEN AS CEN WITH (NOLOCK) on CEN.CODCENATE =A.CODCENATE 
	INNER JOIN DBO.INUNIFUNC AS UNI WITH (NOLOCK) on UNI.UFUCODIGO =A.UFUCODIGO 
    INNER JOIN Contract .HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
    INNER JOIN Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN dbo.INPROFSAL AS PRO WITH (NOLOCK) ON A.CODPROSAL =PRO.CODPROSAL
	LEFT JOIN .HCHISPACA AS HIS WITH (NOLOCK) ON A.IPCODPACI =HIS.IPCODPACI AND A.NUMFOLINT =HIS.NUMEFOLIO 
	WHERE  CAST(A.FECORDMED AS DATE)>='2023-01-01'
	--CAST(A.FECORDMED AS DATE) BETWEEN @FechaInicio AND @FechaFin 
	GROUP BY i.iptipodoc, A.CODSERIPS,B.DESSERIPS,B.ARSCODIGO, NUMFOLINT,TIPSERIPS,a.IPCODPACI,A.NUMINGRES,i.IPNOMCOMP, cecd.Id, cd.Id, cd.Code, cd.Name,CAST(FECORDMED AS DATE),
	A.NUMEFOLIO,case when MANEXTPRO= 0 then 'HOSPITALARIO' ELSE 'AMBULATORIO' END,A.CODPROSAL,PRO.NOMMEDICO ,HIS.FECHISPAC,CEN.NOMCENATE,UNI.UFUDESCRI,HEA.Code,HEA.Name,CGR.Name

	UNION ALL

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		'' AS [TipoDocumento],
		a.IPCODPACI AS[IDENTIFICACION],
		'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as [NOMBRE PACIENTE],
		HEA.Code 'CODIGO ENTIDAD' ,
		HEA.Name 'ENTIDAD',
		CGR.Name 'GRUPO DE ATENCION',	
		CEN.NOMCENATE 'CENTRO DE ATENCION',
		UNI.UFUDESCRI 'UNIDAD FUNCIONAL',
		INGMH.NUMINGRES AS [INGRESO],
		CAST(FECORDMED AS DATE) [FECHA SERVICIO],
		A.NUMEFOLIO [FOLIO],
		case 
			when MANEXTPRO= 0 then 'HOSPITALARIO' 
			ELSE 'AMBULATORIO' END AS [TIPO SOLICITUD],
		A.CODPROSAL [IDENTIFICACION PROFESIONAL],
		PRO.NOMMEDICO AS [PROFESIONAL],
		A.CODSERIPS AS [CODIGO SERVICIO],
		RTRIM(B.DESSERIPS) AS [SERVICIO],
		ISNULL(cd.Code + ' - ' + cd.Name, '') [DESCRIPCION RELACIONADA],
		CASE 
			WHEN NUMFOLINT IS NULL THEN 'Solicitadas' 
			ELSE 'Con Respuesta' END AS 'ESTADO SERVICIO',
		A.NUMFOLINT 'FOLIO DONDE SE INTERCONSULTO',
		CAST(HIS.FECHISPAC AS DATE) 'FECHA DE INTERCONSULTA',
		SUM(A.CANSERIPS) AS [CANTIDAD],
		CAST(A.FECORDMED AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.HCORDINTE A 
	INNER JOIN dbo.INCUPSIPS B WITH (NOLOCK) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.HCINGRESORECNAC INGMH WITH (NOLOCK) on a.NUMINGRES = INGMH .NUMINGRESHIJO
	INNER JOIN dbo.HCRECINAC RN WITH (NOLOCK) on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN  dbo.ADINGRESO AS ING WITH (NOLOCK) on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	INNER JOIN DBO.ADCENATEN AS CEN WITH (NOLOCK) on CEN.CODCENATE =A.CODCENATE 
	INNER JOIN DBO.INUNIFUNC AS UNI WITH (NOLOCK) on UNI.UFUCODIGO =A.UFUCODIGO 
	INNER JOIN Contract.HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
    INNER JOIN Contract.CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN dbo.INPROFSAL AS PRO WITH (NOLOCK) ON A.CODPROSAL =PRO.CODPROSAL
	LEFT JOIN .HCHISPACA AS HIS WITH (NOLOCK) ON A.IPCODPACI =HIS.IPCODPACI AND A.NUMFOLINT =HIS.NUMEFOLIO 
	WHERE  CAST(A.FECORDMED AS DATE)>='2023-01-01'
	--CAST(A.FECORDMED AS DATE) BETWEEN @FechaInicio AND @FechaFin
	GROUP BY  A.CODSERIPS,B.DESSERIPS,B.ARSCODIGO, NUMFOLINT,TIPSERIPS,a.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG, cecd.Id, cd.Id, cd.Code, cd.Name,CAST(FECORDMED AS DATE),
	A.NUMEFOLIO,case when MANEXTPRO= 0 then 'HOSPITALARIO' ELSE 'AMBULATORIO' END,A.CODPROSAL,PRO.NOMMEDICO ,HIS.FECHISPAC,CEN.NOMCENATE,UNI.UFUDESCRI,HEA.Code,HEA.Name,CGR.Name

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista para cubo de reportes que consolida las interconsultas médicas (órdenes a otros servicios) tanto de pacientes regulares como de recién nacidos, indicando si están solicitadas o con respuesta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener fecha de orden médica (FECORDMED) mayor o igual a 2023-01-01.; Cada orden debe tener un servicio (CODSERIPS) válido en INCUPSIPS, un paciente en INPACIENT, un centro de atención en ADCENATEN y una unidad funcional en INUNIFUNC.; Para el segundo bloque (recién nacidos), el ingreso debe estar registrado en HCINGRESORECNAC como NUMINGRESHIJO y existir en HCRECINAC.; El ingreso debe estar asociado a una entidad administradora de salud (GENCONENTITY) y a un grupo de atención (GENCAREGROUP) válidos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de la compañía (ID_COMPANY) siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres.; La fecha de última actualización (ULT_ACTUAL) se calcula con la zona horaria ''Pakistan Standard Time''.; Las cantidades (CANSERIPS) se agregan mediante SUM agrupando por servicio, folio, paciente, ingreso, profesional, centro, entidad y demás dimensiones.; Solo se incluyen órdenes desde 2023-01-01 en adelante.; El estado de la interconsulta se infiere exclusivamente de la presencia o ausencia de NUMFOLINT.; En el bloque de recién nacidos no se reporta tipo ni nombre real del paciente, sino la etiqueta ''Hijo N'' según número de hijo en el registro de nacimiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento de identificación; Ingreso/admisión hospitalaria; Orden médica de interconsulta; Servicio CUPS; Entidad administradora de salud (EAPB); Grupo de atención; Centro de atención; Unidad funcional; Profesional de la salud (médico); Folio de interconsulta; Solicitud hospitalaria vs ambulatoria; Estado de interconsulta (solicitada/con respuesta); Recién nacido / hijo de madre hospitalizada; Descripción de contrato CUPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalMedicalInterconsultations: Devuelve el conjunto unificado (UNION ALL) de interconsultas médicas de pacientes adultos/regulares y de recién nacidos cuya fecha de orden es >= 2023-01-01.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente entre 1 y 15 → Mapea a códigos de tipo de documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI.; si MANEXTPRO = 0 → Clasifica la solicitud como ''HOSPITALARIO''. else Clasifica la solicitud como ''AMBULATORIO''.; si NUMFOLINT IS NULL → El estado del servicio es ''Solicitadas''. else El estado del servicio es ''Con Respuesta'' (ya tiene folio de interconsulta respondido).; si Origen del registro: ingreso regular vs ingreso de recién nacido → Primer SELECT toma datos del paciente desde INPACIENT; segundo SELECT identifica al paciente como ''Hijo N'' usando NUMHIJREG de HCRECINAC vinculado vía HCINGRESORECNAC.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.INPROFSAL; dbo.HCHISPACA; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicalInterconsultations';
GO
