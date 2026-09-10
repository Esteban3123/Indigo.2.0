CREATE view [Report].[UploadCubeVieClinicalMedicalOrdersNEPS] as 

WITH ordenamientos AS 
	(

		--	Medicamentos Ordenados
		SELECT 
			pres.id AS autonro,
			'M: Medicamento' AS typeser,
			pres.ipcodpaci,
			pres.numingres,
			pres.numefolio,
			pres.codprosal,
			pres.fecinidos fecordmed, 
			pres.codproduc,
			pres.canpedpro,
			pres.manextpro,
			pres.codformed, -- Datos medicamentos 
			pres.codviaadm,
			pres.dosisprod,
			pres.codunimed,
			pres.frecuenci,
			CASE 
				WHEN pres.duracidos = 'Dosis Unica' THEN 'Dosis Unica'
				ELSE CASE pres.unifrecue WHEN 1 THEN 'Minuto(s)' WHEN 2 THEN 'Hora(s)' WHEN 3 THEN 'Día(s)' WHEN 4 THEN 'Semana(s)' WHEN 5 THEN 'Mes(es)' WHEN 6 THEN 'Año' END END AS [unifrecue],
			pres.valdurfij,
			CASE pres.unidurfij WHEN 1 THEN 'Minuto(s)' WHEN 2 THEN 'Hora(s)' WHEN 3 THEN 'Día(s)' WHEN 4 THEN 'Semana(s)' WHEN 5 THEN 'Mes(es)' WHEN 6 THEN 'Año' END AS [unidurfij],
			pres.codcenate 
		FROM dbo.hcprescra AS pres
		INNER JOIN dbo.adingreso AS ing ON pres.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE pres.idesquemaonc IS NULL AND YEAR(pres.fecinidos)>=2023
		--AND CAST(pres.fecinidos AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL
		--	 Imagenes 
		SELECT
			img.auto,
			'P: Procedimiento' AS typeser,
			img.ipcodpaci,
			img.numingres,
			img.numefolio,
			img.codprosal,
			img.fecordmed fecordmed,
			img.codserips,
			img.canserips,
			img.manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			img.codcenate
		FROM dbo.hcordimag AS img
		INNER JOIN dbo.adingreso AS ing ON img.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE img.estserips NOT IN(6) AND YEAR(img.fecordmed)>=2023
		--AND CAST(img.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL
		--	 Laboratorios
		SELECT
			lab.auto,
			'P: Procedimiento' AS typeser,
			lab.ipcodpaci,
			lab.numingres,
			lab.numefolio,
			lab.codprosal,
			lab.fecordmed fecordmed,
			lab.codserips,
			lab.canserips,
			lab.manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			lab.codcenate
		FROM dbo.hcordlabo AS lab
		INNER JOIN dbo.adingreso AS ing ON lab.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE lab.estserips NOT IN(6) AND YEAR(lab.fecordmed)>=2023
		--AND CAST(lab.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL
		--	Patologias 
		SELECT
			pat.auto,
			'P: Procedimiento' AS typeser,
			pat.ipcodpaci,
			pat.numingres,
			pat.numefolio,
			pat.codprosal,
			pat.fecordmed fecordmed,
			pat.codserips,
			pat.canserips,
			pat.manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			pat.codcenate
		FROM dbo.hcordpato AS pat
		INNER JOIN dbo.adingreso AS ing ON pat.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE pat.estserips NOT IN(6) AND YEAR(pat.fecordmed)>=2023
		--AND CAST(pat.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL
		/** Procedimientos no Quirurgicos */
		SELECT			
			nqx.auto,
			'P: Procedimiento' AS typeser,
			nqx.ipcodpaci,
			nqx.numingres,
			nqx.numefolio,
			nqx.codprosal,
			nqx.fecordmed fecordmed,
			nqx.codserips,
			nqx.canserips,
			nqx.manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			nqx.codcenate
		FROM dbo.hcordpron AS nqx
		INNER JOIN dbo.adingreso AS ing ON nqx.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE nqx.estserips NOT IN(5) AND YEAR(nqx.fecordmed)>=2023
		--AND CAST(nqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 
		--	 Procedimientos Quirurigicos
		SELECT	
			pqx.auto,
			'P: Procedimiento' AS typeser,
			pqx.ipcodpaci,
			pqx.numingres,
			pqx.numefolio,
			pqx.codprosal,
			pqx.fecordmed fecordmed,
			pqx.codserips,
			pqx.canserips,
			pqx.manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			pqx.codcenate
		FROM dbo.hcordproq AS pqx
		INNER JOIN dbo.adingreso AS ing ON pqx.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE pqx.estserips NOT IN(3) AND YEAR(pqx.fecordmed)>=2023
		--AND CAST(pqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL
		--	Interconsultas 
		SELECT		
			[int].auto,
			'P: Procedimiento' AS typeser,
			[int].ipcodpaci,
			[int].numingres,
			[int].numefolio,
			[int].codprosal,
			[int].fecordmed fecordmed,
			[int].codserips,
			[int].canserips,
			[int].manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			[int].codcenate
		FROM dbo.hcordinte AS [int]
		INNER JOIN dbo.adingreso AS ing ON [int].numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE [int].estserips NOT IN(5) AND YEAR([int].fecordmed)>=2023
		--AND CAST([int].fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 
		--	Ordenes de Control
		SELECT
			coex.auto,
			'P: Procedimiento' AS typeser,
			hchi.ipcodpaci,
			hchi.numingres,
			hchi.numefolio,
			hchi.codprosal,
			hchi.fechispac fecordmed,
			coex.codserips,
			1 AS canserips,
			0 AS manextpro,
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
			hchi.codcenate
		FROM dbo.hchispaca AS hchi
		INNER JOIN dbo.hcdescoex AS coex ON hchi.numingres = coex.numingres AND hchi.numefolio = coex.numefolio
		INNER JOIN dbo.adingreso AS ing ON hchi.numingres = ing.numingres AND ing.genconentity IN(61, 62, 63)
		WHERE YEAR(hchi.fechispac)>=2023
		--CAST(hchi.fechispac AS DATE) BETWEEN @ini_date AND @end_date

	), ingresos AS 
	(

		SELECT DISTINCT numingres FROM ordenamientos

	), diagnosticos AS 
	(

		SELECT 
			ing.numingres,
			ROW_NUMBER() OVER(PARTITION BY ing.numingres ORDER BY dx.coddiapri DESC) AS numrow,
			dx.coddiagno,
			dx.coddiapri
		FROM dbo.indiagnop dx
		INNER JOIN ingresos AS ing ON dx.numingres = ing.numingres
	)

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ord.autonro AS [IdentificadorPrescripcion],
		CASE pac.iptipodoc
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
			WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO AFILIADO',--[TipoDocumentoAfiliado],
		pac.ipcodpaci AS 'NRO IDENTIFICACION AFILIADO',--[NroIdentificacionAfiliado],
		CASE WHEN pac.iptelmovi IS NULL OR pac.iptelmovi = '' THEN pac.iptelefon ELSE pac.iptelmovi END AS 'NRO TELEFONICO AFILIADO',--[NroTelefonicoAfiliado],
		RTRIM(LOWER(pac.corelepac)) AS 'CORREO ELECTRONICO AFILIADO',--[CorreoElectronicoAfiliado],
		CASE ing.tipoingre WHEN 1 THEN '1: Consulta Externa' WHEN 2 THEN '2: Internación' END AS 'GRUPO SERVICIO',--[GrupoServicio],
		CASE ord.manextpro WHEN 1 THEN '2: Extramural' WHEN 0 THEN '1: Intramural' END AS 'MODALIDAD',--[Modalidad],
		ord.fecordmed AS 'FECHA PRESCRIPCION',--[FechaPrescripcion],
		'801000713' AS 'NIT IPS PRESCRIPTORA',--[NITIPSPrescriptora],
		0 AS 'CODIGO SUCURSAL IPS NUEVA EPS',--[CodSucursalIPSNuevaEPS],
		CASE per.identificationtype  WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' END AS 'TIPO DOCUMENTO PROFESIONAL SALUD',--[TipoDocumentoProfesionalSalud],
		RTRIM(med.codigonit) AS 'NRO IDENTIFICACION PROFESIONAL SALUD',--[NroIdentificacionProfesionalSalud],
		RTRIM(med.medpriapel) AS 'PRIMER APELLIDO PROFESIONAL SALUD',--[PrimerApellidoProfesionalSalud],
		RTRIM(med.medsegapel) AS 'SEGUNDO APELLIDO PROFESIONAL SALUD',--[SegundoApellidoProfesionalSalud],
		RTRIM(RTRIM(med.medprinom) + ' ' + med.medsegnom) AS 'NOMBRE PROFESIONAL SALUD',--[NombresProfesionalSalud],
		dxp.coddiagno AS 'CODIGO DIAGNOSTICO PRINCIPAL',--[CodDiagnosticoPrincipal],
		dxs.coddiagno AS 'CODIGO DIAGNOSTICO RELACIONADO 1',--[CodDiagnosticoRelacionado1],
		dxt.coddiagno AS 'CODIGO DIAGNOSTICO RELACIONADO 2',--[CodDiagnosticoRelacionado2],
		ord.typeser AS 'TIPO SERVICIO PRESCRITO',--[TipoServicioPrescrito],
		RTRIM(ISNULL(atc.name, cups.description)) AS 'DESCRIPCION SERVICIO MEDICO',--[DescripcionServiciosMedicamento],
		RTRIM(phaform.name) AS 'CODIGO FORMA FARMACEUTICA',--[CodFormaFarmaceutica],
		RTRIM(way.name) AS 'CODIGO VIA ADMINISTRACION',--[CodViaAdministracion],
		ord.codproduc AS 'CODIGO CUPS/CUMS',--[CodCUPSCUMS],
		ord.dosisprod AS 'DOSIS NUMERO',--[DosisNumero],
		RTRIM(mea.name) AS 'DOSIS UNIDAD MEDIDAD',--[DosisUnidadMedida],
		ord.frecuenci AS 'NUMERO FRECUENCIA ADMINISTRACION',--[NumFrecuenciaAdministracion],
		ord.unifrecue AS 'FRECUENCIA ADMINISTRACION',--[FrecuenciaAdministracion],
		ord.valdurfij AS 'NUMERO DURACION TRATAMIENTO',--[NumDuracionTratamiento],
		ord.unidurfij AS 'DURACION TRATAMIENTO',--[DuracionTratamiento],
		ord.canpedpro AS 'CANTIDAD TOTAL FORMULADA',--[CantidadTotalFormulada],
		'' AS 'UNIDAD FARMACEUTICA CANTIDAD TOTAL',--[Unidad FarmaceuticaCantidadTotal],
		'' AS 'INDICACIONES RECOMENDACIONES PRESCRIPTOR',--[IndicacionesRecomendacionesPrescriptor],
		RTRIM(can.nomcenate) AS 'CENTRO ATENCION',--[CentroAtencion]
		CAST(ord.fecordmed AS DATE) 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM ordenamientos AS ord
	INNER JOIN dbo.inpacient AS pac ON ord.ipcodpaci = pac.ipcodpaci 
	INNER JOIN dbo.adingreso AS ing ON ord.numingres = ing.numingres
	INNER JOIN Security.PersonInt AS per ON ord.codprosal = per.identification 
	INNER JOIN dbo.inprofsal AS med ON ord.codprosal = med.codprosal 
	LEFT JOIN inventory.atc AS atc ON ord.codproduc = atc.code 
	LEFT JOIN contract.cupsentity AS cups ON ord.codproduc = cups.code  

	LEFT JOIN inventory.pharmaceuticalform AS phaform ON ord.codformed = phaform.id 
	LEFT JOIN inventory.administrationroute AS way ON ord.codviaadm = way.id
	LEFT JOIN inventory.inventorymeasurementunit AS mea ON ord.codunimed = mea.id

	LEFT JOIN diagnosticos AS dxp ON ord.numingres = dxp.numingres AND dxp.numrow = 1
	LEFT JOIN diagnosticos AS dxs ON ord.numingres = dxs.numingres AND dxs.numrow = 2
	LEFT JOIN diagnosticos AS dxt ON ord.numingres = dxt.numingres AND dxt.numrow = 3
	LEFT JOIN dbo.adcenaten AS can ON ord.codcenate = can.codcenate
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista diseñada para alimentar un cubo de datos (BI/reporting) con las órdenes médicas clínicas de ingresos pertenecientes a la entidad NEPS (entidades 61, 62, 63), consolidando desde 2023 ocho tipos de órdenes: medicamentos, imágenes, laboratorios, patologías, procedimientos no quirúrgicos, quirúrgicos, interconsultas y controles. Aplana en un único conjunto los datos del paciente, profesional prescriptor, diagnósticos (principal y dos relacionados), características farmacológicas del medicamento y centro de atención, etiquetando cada registro con tipo de servicio, modalidad y grupo de servicio para consumo analítico por parte de Nueva EPS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto las prescripciones clínicas (medicamentos, imágenes, laboratorios, patología, procedimientos quirúrgicos/no quirúrgicos, interconsultas y órdenes de control) desde 2023 para reportar a Nueva EPS con datos de afiliado, prescriptor y diagnósticos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (adingreso) debe tener genconentity en (61, 62, 63) para ser incluido; La fecha de orden/prescripción debe ser de año 2023 o posterior (YEAR(fec...) >= 2023); Las prescripciones de medicamentos (hcprescra) deben tener idesquemaonc IS NULL (excluye esquemas oncológicos); El profesional prescriptor debe existir en Security.PersonInt (vía identification) y en dbo.inprofsal; El paciente debe existir en dbo.inpacient', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos cuyo genconentity pertenece al conjunto {61, 62, 63}; Solo se reportan registros con fecha de orden/prescripción a partir del año 2023; Las prescripciones oncológicas (idesquemaonc no nulo) nunca se incluyen; El NIT de la IPS prescriptora siempre es ''801000713'' y el código de sucursal IPS Nueva EPS siempre es 0; Las órdenes de control se reportan con cantidad fija = 1 y manextpro = 0 (Intramural); Por cada ingreso se exponen hasta 3 diagnósticos: principal (numrow=1), relacionado1 (numrow=2) y relacionado2 (numrow=3), priorizados por coddiapri DESC; ID_COMPANY siempre es el nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción de medicamentos; Órdenes de imágenes diagnósticas; Órdenes de laboratorio; Órdenes de patología; Procedimientos quirúrgicos y no quirúrgicos; Interconsultas; Órdenes de control (descoex); Diagnóstico principal y relacionados; Modalidad intramural/extramural; Consulta externa / Internación; Profesional de la salud prescriptor; Forma farmacéutica y vía de administración; Dosis, frecuencia y duración del tratamiento; Centro de atención; Reporte regulatorio Nueva EPS (NIT 801000713); Esquema oncológico (exclusión); Códigos CUPS/CUMS/ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalOrdersNEPS: Devuelve filas DISTINCT por prescripción con NIT IPS prescriptora fijo ''801000713'' y código de sucursal IPS Nueva EPS = 0; ULT_ACTUAL se calcula con GETDATE() convertido a ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la orden (medicamento vs procedimiento) → Si proviene de hcprescra → typeser = ''M: Medicamento'' con datos de dosis/forma/vía/frecuencia; en los demás orígenes → typeser = ''P: Procedimiento'' con campos farmacológicos en NULL; si pres.duracidos = ''Dosis Unica'' → unifrecue se reporta como ''Dosis Unica'' else Se traduce unifrecue numérico (1..6) a ''Minuto(s)'',''Hora(s)'',''Día(s)'',''Semana(s)'',''Mes(es)'',''Año''; si Estado de la orden por tipo (estserips) → Se excluyen: imágenes/laboratorios/patología con estserips=6; procedimientos no quirúrgicos e interconsultas con estserips=5; procedimientos quirúrgicos con estserips=3; si ing.tipoingre → 1 → ''1: Consulta Externa''; 2 → ''2: Internación''; si ord.manextpro → 1 → ''2: Extramural''; 0 → ''1: Intramural''; si pac.iptelmovi nulo o vacío → Se reporta pac.iptelefon como teléfono del afiliado else Se reporta pac.iptelmovi; si Existencia del código de producto en catálogo ATC → Descripción del servicio = atc.name else Se usa cups.description (contract.cupsentity); si Tipo de documento del afiliado (iptipodoc 1..15) → Se mapea a códigos ''CC'',''CE'',''TI'',''RC'',''PA'',''AS'',''MS'',''NU'',''CN'',''CD'',''SC'',''PE'',''PT'',''DE'',''SI''; si Tipo de documento del profesional (per.identificationtype) → 1 → ''CC''; 2 → ''CE''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcprescra; dbo.adingreso; dbo.hcordimag; dbo.hcordlabo; dbo.hcordpato; dbo.hcordpron; dbo.hcordproq; dbo.hcordinte; dbo.hchispaca; dbo.hcdescoex; dbo.indiagnop; dbo.inpacient; Security.PersonInt; dbo.inprofsal; inventory.atc; contract.cupsentity; inventory.pharmaceuticalform; inventory.administrationroute; inventory.inventorymeasurementunit; dbo.adcenaten', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrdersNEPS';
GO
