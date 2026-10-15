

CREATE view [Report].[UploadCubeVieClinicalExtramuralOrdersSanitasEPS] as

SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		'' AS 'FECHA ENVIO',--[FechaEnvio]
		'801000713' AS 'NIT',--[NIT]
		cen.codipssec AS 'CODIGO SUCURSAL',--[CodigoSucursal]
		'ONCOLOGOS DEL OCCIDENTE SAS' AS 'NOMBRE PRESTADOR REMITENTE',--[NombrePrestadorRemitente]
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
		sips.ipcodpaci AS 'NUMERO IDENTIFICACION AFILIADO',--[NumeroIdentificacionAfiliado]
		paci.ipnomcomp AS 'NOMBRE PACIENTE',--[NombrePaciente]
		paci.iptelmovi AS 'TELEFONO CELULAR 1',--[TelefonoCelular1]
		paci.iptelefon AS 'TELEFONO CELULAR 2',--[TelefonoCelular2]
		indx.coddiagno AS 'CIE10',--CIE10
		CONVERT(VARCHAR, sips.fecordmed, 103) AS 'FECHA ATENCION',--[FechaAtencion] 
		med.nommedico AS 'NOMBRE MEDICO',--[NombreMedico]
		hesp.codsanitas AS 'CODIGO ESPECIALIDAD REMITENTE',--[CodigoEspecialidadRemitente]
		esp.desespeci AS 'ESPECIALIDAD REMITENTE',--[EspecialidadRemitente]
		sips.codserips AS 'CODIGO CUPS PRESTACION',--[CodigoCUPPrestacion]
		sips.canserips AS 'CANTIDAD',--Cantidad
		CASE  
			WHEN desr.name IS NULL THEN cups.description 
			WHEN desr.name IS NOT NULL THEN cups.description + ' / ' +  RTRIM(desr.name) END AS 'DESCRIPCION PRESTACION',--[DescripcionPrestacion]
		sips.obsserips AS 'JUSTIFICACION CLINICA',--[JustificacionClinica]
		'' AS 'EDAD GESTACIONAL',--[EdadGestacional]
		'' AS 'ANESTESIA A',--[AnestesiaA]
		'' AS 'SEDACION S',--[SedacionS]
		'' AS 'CONTRASTE T',--[ContrasteT]
		'' AS 'COMPARATIVO C',--[ComparativoC]
		sips.bilateralidad AS 'BILATERAL B',--[BilateralB]
		'' 'NO APLICA',--[NoAplica]
		CAST(sips.fecordmed AS DATE) 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM 
	(	/** Imagenes */
		SELECT
			oimg.ipcodpaci
			,oimg.numingres
			,oimg.numefolio AS folio
			,oimg.codprosal
			,oimg.fecordmed
			,oimg.codserips
			,NULL AS iddescripcionrelacionada
			,oimg.canserips
			,CASE oimg.lateralidad WHEN 3 THEN 'B' ELSE '' END AS bilateralidad
			,CASE oimg.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
			END AS obsserips
		FROM dbo.hcordimag AS oimg
		INNER JOIN dbo.adingreso AS ing ON oimg.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE oimg.estserips NOT IN(6) AND oimg.manextpro = 1 AND YEAR(oimg.fecordmed)>=2023
		--CAST(oimg.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		/** Laboratorios */
		SELECT
			olab.ipcodpaci
			,olab.numingres
			,olab.numefolio AS folio
			,olab.codprosal
			,olab.fecordmed
			,olab.codserips
			,NULL AS iddescripcionrelacionada
			,olab.canserips
			,'' AS bilateralidad
			,CASE olab.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
			END AS obsserips
		FROM dbo.hcordlabo AS olab
		INNER JOIN dbo.adingreso AS ing ON olab.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE olab.estserips NOT IN(6) AND olab.manextpro = 1 AND YEAR(olab.fecordmed)>=2023
		--CAST(olab.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		/** Patologias */
		SELECT
			opat.ipcodpaci
			,opat.numingres
			,opat.numefolio AS folio
			,opat.codprosal
			,opat.fecordmed
			,opat.codserips
			,NULL AS iddescripcionrelacionada
			,opat.canserips
			,'' AS bilateralidad
			,CASE opat.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
			END AS obsserips
		FROM dbo.hcordpato AS opat
		INNER JOIN dbo.adingreso AS ing ON opat.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE opat.estserips NOT IN(6) AND opat.manextpro = 1 AND YEAR(opat.fecordmed)>=2023
		--CAST(opat.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 

		/** Procedimientos no QX */
		SELECT						
			onqx.ipcodpaci
			,onqx.numingres
			,onqx.numefolio AS folio
			,onqx.codprosal
			,onqx.fecordmed
			,onqx.codserips
			,NULL AS iddescripcionrelacionada
			,onqx.canserips
			,CASE onqx.lateralidad WHEN 3 THEN 'B' ELSE '' END AS bilateralidad
			,CASE onqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
			END AS obsserips
		FROM dbo.hcordpron AS onqx
		INNER JOIN dbo.adingreso AS ing ON onqx.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE onqx.estserips NOT IN(5) AND onqx.manextpro = 1 AND YEAR(onqx.fecordmed)>=2023
		--CAST(onqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		/** Procedimientos QX */
		SELECT						
			opqx.ipcodpaci
			,opqx.numingres
			,opqx.numefolio AS folio
			,opqx.codprosal
			,opqx.fecordmed
			,opqx.codserips
			,NULL AS iddescripcionrelacionada
			,opqx.canserips
			,CASE opqx.lateralidad WHEN 3 THEN 'B' ELSE '' END AS bilateralidad
			,CASE opqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
			END AS obsserips
		FROM dbo.hcordproq AS opqx
		INNER JOIN dbo.adingreso AS ing ON opqx.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE opqx.estserips NOT IN(3) AND opqx.manextpro = 1 AND YEAR(opqx.fecordmed)>=2023
		--CAST(opqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		/** Interconsultas */
		SELECT						
			oint.ipcodpaci
			,oint.numingres
			,oint.numefolio AS folio
			,oint.codprosal
			,oint.fecordmed
			,oint.codserips
			,NULL AS iddescripcionrelacionada
			,oint.canserips
			,'' AS bilateralidad
			,CASE oint.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
			END AS obsserips		
		FROM dbo.hcordinte AS oint
		INNER JOIN dbo.adingreso AS ing ON oint.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE oint.estserips NOT IN(5) AND oint.manextpro = 1 AND YEAR(oint.fecordmed)>=2023
		--CAST(oint.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 

		/** Ordenes de control */
		SELECT 
			hchi.ipcodpaci
			,hchi.numingres
			,hchi.numefolio AS folio
			,hchi.codprosal
			,hchi.fechispac fecordmed
			,coex.codserips
			,coex.iddescripcionrelacionada 
			,1 AS canserips
			,''
			,hchi.datobjeti
		FROM dbo.hchispaca AS hchi
		INNER JOIN dbo.hcdescoex AS coex ON hchi.numingres = coex.numingres AND hchi.numefolio = coex.numefolio
		INNER JOIN dbo.adingreso AS ing ON hchi.numingres = ing.numingres AND ing.genconentity IN(36, 177)
		WHERE YEAR(hchi.fechispac)>=2023
		--CAST(hchi.fechispac AS DATE) BETWEEN @ini_date AND @end_date

	) AS sips
	INNER JOIN dbo.inpacient AS paci ON sips.ipcodpaci = paci.ipcodpaci 
	LEFT JOIN dbo.inprofsal AS med ON sips.codprosal = med.codprosal
	LEFT JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci 
	LEFT JOIN Report.TableEspecialidades AS hesp ON med.codespec1 = hesp.cododo
	INNER JOIN contract.cupsentity AS cups ON sips.codserips = cups.code AND cups.billinggroupid NOT IN (10, 12) AND cups.code NOT IN ('906340', 'C00272')
	LEFT JOIN contract.contractdescriptions AS desr ON sips.iddescripcionrelacionada = desr.id
	INNER JOIN dbo.indiagnop AS indx ON sips.numingres = indx.numingres AND indx.coddiapri = 1
	INNER JOIN dbo.adingreso AS ing ON sips.numingres = ing.numingres
	INNER JOIN dbo.adcenaten AS cen ON ing.codcenate = cen.codcenate