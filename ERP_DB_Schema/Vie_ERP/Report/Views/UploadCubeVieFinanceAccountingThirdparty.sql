
CREATE view [Report].[UploadCubeVieFinanceAccountingThirdparty] as

	WITH thirdparty AS
	(

		SELECT
			thi.nit AS [NroDocumento],
			thi.name AS [Nombre],
			thi.digitverification AS [DigVerificacion],
			CASE thi.persontype WHEN 1 THEN 'Natural' WHEN 2 THEN 'Jurídico' END AS [TipoPersona],
			CASE thi.retentiontype WHEN 0 THEN 'Ninguna' WHEN 1 THEN 'Exento de retención' WHEN 2 THEN 'Hace Retención' WHEN 3 THEN 'Autoretenedor' END AS [TipoRetencion],
			CASE thi.ica WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS [ManejaICA],
			thi.icapercentage AS [PorcentajeICA],
			thi.creationdate AS [FechaRegistro]
		FROM common.thirdparty AS thi
		WHERE thi.nit NOT IN ('0000', '00000', '0000000', '00000000', '000000000000000', '00000123', '0001', '0002', '0003', '000CIR', '999', '999999', '99999999', '999999999')

	), address AS
	(
		
		SELECT 
			thi.nit,
			ROW_NUMBER() OVER(PARTITION BY thi.nit ORDER BY thi.nit, con.code, con.code DESC) AS rownumber,
			con.nationality AS [Nacionalidad],
			dep.code AS depcode,
			dep.name AS depname,
			cit.code AS muncode,
			cit.name AS munname,
			RTRIM(dir.addresss) AS address
		FROM common.thirdparty AS thi
		INNER JOIN common.person AS per ON thi.personid = per.id
		INNER JOIN common.address AS dir ON per.id = dir.idperson 
		INNER JOIN common.department AS dep ON dir.departmentid = dep.id
		INNER JOIN common.country AS con ON dep.countryid = con.id
		INNER JOIN common.city AS cit ON dir.cityid = cit.id

	)

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		thi.[NroDocumento] 'NRO DOCUMENTO',--,
		thi.[Nombre] 'NOMBRE',--,
		dir.Nacionalidad 'NACIONALIDAD',--,
		dir.depcode AS  'CODIGO DEPARTAMENTO',--[CodDepartamento],
		dir.depname As 'DEPARTAMENTO',--[Departamento] ,
		dir.muncode AS 'CODIGO MUNICIPIO',--[CodMunicipio] ,
		dir.munname AS 'MUNICIPIO',--[Municipio] ,
		UPPER(dir.address) AS 'DIRECCION',--[Direccion] ,
		thi.[DigVerificacion] 'DIG VERIFICACION',--,
		thi.[TipoPersona] 'TIPO PERSONA',--,
		thi.[TipoRetencion] 'TIPO RETENCION',--,
		thi.[ManejaICA] 'MANEJA ICA',--,
		thi.[PorcentajeICA] 'PORCENTAJE ICA',--,
		thi.[FechaRegistro] 'FECHA REGISTRO',-- 
		cast(thi.[FechaRegistro] as date) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM thirdparty AS thi
	LEFT JOIN address AS dir ON thi.[NroDocumento] = dir.nit