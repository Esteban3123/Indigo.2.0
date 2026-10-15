

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