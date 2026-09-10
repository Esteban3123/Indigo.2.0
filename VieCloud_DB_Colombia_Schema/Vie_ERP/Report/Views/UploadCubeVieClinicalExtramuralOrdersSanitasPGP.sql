

--DECLARE	@ini_date DATE='2024-04-01';
--DECLARE	@end_date DATE='2024-04-30';

--AS
--BEGIN

CREATE view [Report].[UploadCubeVieClinicalExtramuralOrdersSanitasPGP] AS

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CONVERT(VARCHAR, sips.fecordmed, 103) AS 'FECHA ORDEN',--[FechaOrden] 
		sips.auto AS 'NRO ORDEN',--[NroOrden]
		CASE paci.iptipodoc 
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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION AFILIADO',--[TipoIdentificacionAfiliado]
		sips.ipcodpaci AS 'NUMERO IDENTIFICACION AFILIADO',--[NumeroIdentificacinAfiliado]
		paci.ipprinomb AS 'PRIMER NOMBRE',--[PrimerNombre]
		paci.ipsegnomb AS 'SEGUNDO NOMBRE',--[SegundoNombre]
		paci.ippriapel AS 'PRIMER APELLIDO',--[PrimerApellido]
		paci.ipsegapel AS 'SEGUNDO APELLIDO',--[SegundoApellido]
		paci.iptelmovi AS 'TELEFONO CELULAR',--[TelefonoCelular]
		CASE 
			WHEN paci.corelepac LIKE '%none%' THEN '' ELSE RTRIM(LOWER(paci.corelepac)) END AS 'DIRECCION CORREO ELECTRONICO',--[DireccionCorreoElectronico]
		(SELECT TOP 1 coddiagno FROM dbo.indiagnop AS indx WHERE indx.coddiapri = 1 AND numingres = sips.numingres) AS 'CODIGO DIAGNOSTICO',--[CodigoDiagnostico]
		'801000713-9' AS 'NIT PRESTADOR',--[NITPrestador]
		CASE ing.codcenate 
			WHEN '11011' THEN '10361'
			WHEN '11012' THEN '10361'
			WHEN '12021' THEN '10363' 
			WHEN '12022' THEN '10363' 
			WHEN '12024' THEN '10363' 
			WHEN '12025' THEN '10363' 
			WHEN '13031' THEN '10364' 
			WHEN '13032' THEN '10364' 
			WHEN '13033' THEN '10364'
			WHEN '13034' THEN '10364'
			WHEN '14041' THEN '10362'END AS 'CODIGO PRESTADOR',--[CodigoPrestador]
		CASE ing.codcenate 
			WHEN '11011' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (PEREIRA)'
			WHEN '11012' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (PEREIRA)'
			WHEN '12021' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (MANIZALES)' 
			WHEN '12022' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (MANIZALES)' 
			WHEN '12024' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (MANIZALES)' 
			WHEN '12025' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (MANIZALES)' 
			WHEN '13031' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (ARMENIA)' 
			WHEN '13032' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (ARMENIA)' 
			WHEN '13033' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (ARMENIA)'
			WHEN '13034' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (ARMENIA)'
			WHEN '14041' THEN 'ONCOLOGOS DEL OCCIDENTE SAS (CARTAGO)'END AS 'DESCRIPCION PRESTADOR',--[DescripcionPrestador]
		CASE per.identificationtype WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' END AS 'TIPO IDENTIFICACION MEDICO',--[TipoIdentificacionMedico] 
		per.identification AS 'NRO IDENTIFICACION MEDICO',--[NroIdentificacionMedico]
		med.nommedico AS 'NOMBRE MEDICO',--[NombreMedico]
		esp.desespeci AS 'ESPECIALIDAD REMITENTE',--[EspecialidadRemitente]
		sips.codserips AS 'CODIGO CUPS',--[CodigoCUPS]
		ISNULL([desc].name, cups.description) AS 'DESCRIPCION CUPS',--[DescripcionCUPS]
		sips.canserips AS 'CANTIDAD',--Cantidad
		sips.[Cita de Control] AS 'CITA CONTROL',--[CitaControl]
		sips.[Dias/Meses Control] AS 'DIAS MESES CITA CONTROL',--[DiasMesesCitaControl]
		sips.cupscode AS 'CODIGO CUMS',--[CodigoCUMS]
		sips.[Descripcion del Medicamento] AS 'DESCRIPCION MEDICAMENTO',--[DescripcionMedicamento]
		sips.[Cantidad Medicamento] AS 'CANTIDAD MEDICAMENTO',--[CantidadMedicamento]
		1 AS 'NUMERO ENTREGAS',--[NumeroEntregas]
		LEFT(sips.obsserips, 1000) AS 'JUSTIFICACION CLINICA',--[JustificacionClinica],
		'' AS 'OBSERVACIONES',--[Observaciones],
		'' AS 'ID TRAMITE SERVICIO',--[IDTramiteServicio],
		'' AS 'FECHA CITA',--[FechaCita],
		'' AS 'HORA CITA',--[HoraCita],
		'' AS 'MEDICO',--[Medico],
		'Ninguna' AS 'PREPARACION',--[Preparacion]
		CAST(sips.fecordmed AS DATE) 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 
	FROM 
	(	
	
		--	==================================================================== Imagenes
		SELECT
			oimg.auto
			,oimg.ipcodpaci
			,oimg.numingres
			,oimg.numefolio AS folio
			,oimg.codprosal
			,oimg.fecordmed
			,oimg.codserips
			,oimg.iddescripcionrelacionada
			,oimg.canserips
			,CASE oimg.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio) END AS obsserips
			,'No' AS [Cita de Control]
			,'' AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]		
		FROM dbo.hcordimag AS oimg
		INNER JOIN dbo.adingreso AS ing ON oimg.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE oimg.estserips NOT IN(6) AND oimg.manextpro = 1 AND oimg.codserips NOT IN ('C00272') and YEAR(oimg.fecordmed)>=2023
		--CAST(oimg.fecordmed AS DATE) BETWEEN @ini_date AND @end_date AND 
	
		UNION ALL
		
		--	==================================================================== Laboratorios 
		SELECT
			olab.auto 
			,olab.ipcodpaci
			,olab.numingres
			,olab.numefolio AS folio
			,olab.codprosal
			,olab.fecordmed
			,olab.codserips
			,olab.iddescripcionrelacionada AS iddescripcionrelacionada
			,olab.canserips
			,CASE olab.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio) END AS obsserips
			,'No' AS [Cita de Control]
			,'' AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hcordlabo AS olab
		INNER JOIN dbo.adingreso AS ing ON olab.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE olab.estserips NOT IN(6) AND olab.manextpro = 1 AND olab.codserips NOT IN('906340') AND YEAR(olab.fecordmed)>=2023
		--CAST(olab.fecordmed AS DATE) BETWEEN @ini_date AND @end_date 
		
		UNION ALL

		--	==================================================================== Patologias
		SELECT
			opat.auto
			,opat.ipcodpaci
			,opat.numingres
			,opat.numefolio AS folio
			,opat.codprosal
			,opat.fecordmed
			,opat.codserips
			,NULL AS iddescripcionrelacionada
			,opat.canserips
			,CASE opat.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
			END AS obsserips
			,'No' AS [Cita de Control]
			,'' AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hcordpato AS opat
		INNER JOIN dbo.adingreso AS ing ON opat.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE opat.estserips NOT IN(6) AND opat.manextpro = 1 AND YEAR(opat.fecordmed)>=2023
		--CAST(opat.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 

		--	==================================================================== Procedimientos no quirurgicos
		SELECT		
			onqx.auto
			,onqx.ipcodpaci
			,onqx.numingres
			,onqx.numefolio AS folio
			,onqx.codprosal
			,onqx.fecordmed
			,onqx.codserips
			,onqx.iddescripcionrelacionada 
			,onqx.canserips
			,CASE onqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio) END AS obsserips
			,'No' AS [Cita de Control]
			,'' AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hcordpron AS onqx
		INNER JOIN dbo.adingreso AS ing ON onqx.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE onqx.estserips NOT IN(5) AND onqx.manextpro = 1 AND YEAR(onqx.fecordmed)>=2023
		--CAST(onqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		--	==================================================================== Procedimientos quirurgicos
		SELECT	
			opqx.auto
			,opqx.ipcodpaci
			,opqx.numingres
			,opqx.numefolio AS folio
			,opqx.codprosal
			,opqx.fecordmed
			,opqx.codserips
			,NULL AS iddescripcionrelacionada
			,opqx.canserips
			,CASE opqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio) END AS obsserips
			,'No' AS [Cita de Control]
			,'' AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hcordproq AS opqx
		INNER JOIN dbo.adingreso AS ing ON opqx.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE opqx.estserips NOT IN(3) AND opqx.manextpro = 1 AND YEAR(opqx.fecordmed)>=2023
		--CAST(opqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		--	==================================================================== Interconsultas 
		SELECT		
			oint.auto
			,oint.ipcodpaci
			,oint.numingres
			,oint.numefolio AS folio
			,oint.codprosal
			,oint.fecordmed
			,oint.codserips
			,NULL AS iddescripcionrelacionada
			,oint.canserips
			,CASE oint.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio) END AS obsserips		
			,'No' AS [Cita de Control]
			,'' AS [Dias Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hcordinte AS oint
		INNER JOIN dbo.adingreso AS ing ON oint.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE oint.estserips NOT IN(5) AND oint.manextpro = 1 AND YEAR(oint.fecordmed)>=2023
		--CAST(oint.fecordmed AS DATE) BETWEEN @ini_date AND @end_date
		
		UNION ALL 

		--	==================================================================== Ordenes de control
		SELECT 
			coex.auto
			,hchi.ipcodpaci
			,hchi.numingres
			,hchi.numefolio AS folio
			,hchi.codprosal
			,hchi.fechispac fecordmed
			,coex.codserips
			,coex.iddescripcionrelacionada 
			,1 AS canserips
			,hchi.datobjeti
			,'Si' AS [Cita de Control]
			,CASE coex.unicontrl WHEN 1 THEN CAST(coex.unicontrl AS VARCHAR) + ' Dia(s)' WHEN 2 THEN CAST(coex.unicontrl AS VARCHAR) + ' Mes(es)' END AS [Dias/Meses Control]
			,'' AS cupscode
			,'' AS [Descripcion del Medicamento]
			,'' AS [Cantidad Medicamento]
		FROM dbo.hchispaca AS hchi
		INNER JOIN dbo.hcdescoex AS coex ON hchi.numingres = coex.numingres AND hchi.numefolio = coex.numefolio
		INNER JOIN dbo.adingreso AS ing ON hchi.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		WHERE YEAR(hchi.fechispac)>=2023
		--CAST(hchi.fechispac AS DATE) BETWEEN @ini_date AND @end_date
		
		UNION ALL 
		
		--	==================================================================== Medicamentos quimioterapia
		SELECT 
			ordc.id AS auto,
			ordc.ipcodpaci,
			ordc.numingres,
			ordc.numefolio,
			ordc.codprosal,
			ordc.fecharegistro AS fecordmed,
			'' AS codserips,
			'' AS iddescripcionrelacionada,
			'' AS canserips,
			'' AS obsserips,
			'No' AS [Cita de Control],
			'' AS [Dias/Meses Control],
			atc.code AS cupscode,
			med.name AS [Descripcion del Medicamento],
			SUM(mede.cantidad) AS [Cantidad Medicamento]
		FROM ehr.hcordciclos AS ordc
		INNER JOIN ehr.hcordmedicam AS mede WITH (NOLOCK) ON ordc.schemesid = mede.schemesid AND ordc.idhcordquimio = mede.idhcordquimio AND ordc.ciclo = mede.ciclo
		INNER JOIN dbo.adingreso  AS ing ON ordc.numingres = ing.numingres AND ing.gencaregroup IN (428,429,430,431)
		INNER JOIN inventory.atcentity AS atc ON  mede.atcentityid = atc.id
		INNER JOIN inventory.atc AS med ON mede.codproduc = med.code
		WHERE YEAR(ordc.fecharegistro)>=2023
		--CAST(ordc.fecharegistro AS DATE) BETWEEN @ini_date AND @end_date
		GROUP BY ordc.id, ordc.ipcodpaci, ordc.numingres, ordc.numefolio, ordc.codprosal, ordc.fecharegistro, atc.code, med.name
		
	) AS sips
	INNER JOIN dbo.inpacient AS paci ON sips.ipcodpaci = paci.ipcodpaci 
	LEFT JOIN dbo.inprofsal AS med ON sips.codprosal = med.codprosal
	LEFT JOIN security.personInt AS per ON med.codprosal = per.identification 
	LEFT JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci 
	LEFT JOIN contract.cupsentity AS cups ON sips.codserips = cups.code AND cups.billinggroupid NOT IN (10, 12)
	LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON sips.iddescripcionrelacionada = descr.id
	LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
	INNER JOIN dbo.adingreso AS ing ON sips.numingres = ing.numingres;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a la carga de un cubo de datos (UploadCube) para la aseguradora Sanitas bajo el modelo PGP. Consolida mediante UNION ALL órdenes extramurales de múltiples tipos clínicos —imágenes, laboratorios, patologías, procedimientos no quirúrgicos, quirúrgicos, interconsultas, medicamentos y ciclos— pertenecientes a ingresos de grupos de atención oncológica (gencaregroup 428–431) desde 2023. Cada fila incluye datos del afiliado, médico ordenador, código CUPS/CUMS, diagnóstico principal y sede prestadora de Oncólogos del Occidente SAS en Pereira, Manizales, Armenia y Cartago.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida órdenes clínicas extramurales (imágenes, laboratorios, patologías, procedimientos quirúrgicos y no quirúrgicos, interconsultas, controles y medicamentos de quimioterapia) generadas desde 2023 para pacientes del convenio Sanitas PGP, en el formato requerido para cargue al cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso del paciente (adingreso) debe pertenecer a uno de los grupos de cuidado (gencaregroup) 428, 429, 430 o 431, identificando población Sanitas PGP; La fecha de la orden o registro debe ser del año 2023 en adelante (YEAR(fecordmed) >= 2023); Para órdenes de imágenes y laboratorios: estado de la orden (estserips) distinto de 6 y marca extramural (manextpro = 1); Para procedimientos no quirúrgicos e interconsultas: estserips distinto de 5 y manextpro = 1; Para procedimientos quirúrgicos: estserips distinto de 3 y manextpro = 1; Para laboratorios se excluye explícitamente el código de servicio ''906340''; Para imágenes se excluye explícitamente el código de servicio ''C00272''; El paciente (ipcodpaci) debe existir en dbo.inpacient (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El NIT del prestador siempre es ''801000713-9'' (Oncólogos del Occidente); El número de entregas siempre se reporta como 1; La preparación siempre es ''Ninguna''; Los campos Observaciones, ID Trámite Servicio, Fecha/Hora Cita y Médico se devuelven vacíos (no se reportan); La justificación clínica se trunca a los primeros 1000 caracteres de obsserips; ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres; ULT_ACTUAL se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''; Solo se incluye el diagnóstico marcado como principal (coddiapri = 1) y el primero encontrado para el ingreso; Se excluyen CUPS cuyo billinggroupid sea 10 o 12 (mediante el LEFT JOIN filtrado a contract.cupsentity); Las órdenes de quimioterapia no llevan código CUPS ni cantidad de servicio, solo CUMS y cantidad de medicamento', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden clínica extramural; Paciente y tipo de identificación; Diagnóstico principal (CIE); Prestador de salud (sedes Oncólogos del Occidente); Médico tratante y especialidad; Servicios CUPS; Medicamentos CUMS / código ATC; Quimioterapia (ciclos y esquemas); Cita de control; Justificación clínica; Convenio Sanitas PGP; Ingreso/episodio asistencial; Órdenes de imagenología, laboratorio, patología, procedimientos quirúrgicos/no quirúrgicos e interconsultas', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalExtramuralOrdersSanitasPGP: Devuelve un set de órdenes clínicas extramurales unificadas desde 8 fuentes (hcordimag, hcordlabo, hcordpato, hcordpron, hcordproq, hcordinte, hchispaca/hcdescoex y ehr.hcordciclos) filtradas por gencaregroup IN (428,429,430,431) y año >= 2023', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si paci.iptipodoc entre 1 y 15 → Mapea el código numérico al código alfabético del tipo de documento (1→CC, 2→CE, 3→TI, 4→RC, 5→PA, 6→AS, 7→MS, 8→NU, 9→CN, 10→CD, 11→SC, 12→PE, 13→PT, 14→DE, 15→SI); si paci.corelepac LIKE ''%none%'' → Devuelve cadena vacía como correo electrónico else Devuelve el correo en minúsculas y sin espacios a la derecha; si ing.codcenate corresponde a un centro de atención conocido → Asigna código y nombre de prestador: 11011/11012→10361 Pereira, 12021/12022/12024/12025→10363 Manizales, 13031-13034→10364 Armenia, 14041→10362 Cartago; si per.identificationtype IN (1,2,3) → Mapea tipo de identificación del médico (1→CC, 2→CE, 3→TI); si idetiphis del registro origen (''hcurging1'',''hcurgevo1'',''hcnotevo1'') → Obtiene la justificación clínica (analisisp) de la tabla de historia clínica correspondiente; si Origen es orden de control (hchispaca/hcdescoex) → Marca [Cita de Control]=''Si'' y formatea unicontrl como ''N Dia(s)'' si unicontrl=1 o ''N Mes(es)'' si unicontrl=2 else [Cita de Control]=''No'' para todas las demás fuentes; si Origen es medicamento de quimioterapia (ehr.hcordciclos) → Agrupa por orden y suma cantidades (SUM(mede.cantidad)), reporta código ATC y nombre del medicamento como CUMS y descripción', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hcordimag; dbo.hcordlabo; dbo.hcordpato; dbo.hcordpron; dbo.hcordproq; dbo.hcordinte; dbo.hchispaca; dbo.hcdescoex; ehr.hcordciclos; ehr.hcordmedicam; dbo.adingreso; dbo.hcurging1; dbo.hcurgevo1; dbo.hcnotevo1; dbo.indiagnop; dbo.inpacient; dbo.inprofsal; security.personInt; dbo.inespecia; contract.cupsentity; contract.cupsentitycontractdescriptions; contract.contractdescriptions; inventory.atcentity; inventory.atc', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalExtramuralOrdersSanitasPGP';
GO
