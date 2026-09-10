
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a la carga de un cubo OLAP (BI) con información maestra de terceros del módulo financiero-contable. Consolida datos de identificación tributaria (NIT, dígito de verificación, tipo de persona, tipo de retención e ICA) junto con la dirección geográfica (país, departamento, municipio y dirección) de cada tercero. Excluye registros con NIT genéricos o de prueba, y expone el nombre de la base de datos como identificador de compañía para soportar entornos multi-empresa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de terceros con su información tributaria y de ubicación principal para alimentar el cubo financiero contable.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tercero debe tener un NIT distinto a los valores considerados ficticios/genéricos (''0000'',''00000'',''0000000'',''00000000'',''000000000000000'',''00000123'',''0001'',''0002'',''0003'',''000CIR'',''999'',''999999'',''99999999'',''999999999''); Para obtener dirección, el tercero debe estar vinculado a una persona con dirección, departamento, país y ciudad registrados', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye sistemáticamente NITs marcadores/ficticios definidos en lista negra; ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''; La dirección se devuelve en mayúsculas y sin espacios finales; Por cada NIT se selecciona una sola dirección (rownumber implícito = 1 por la lógica de ROW_NUMBER, aunque el LEFT JOIN no lo filtra explícitamente); FechaRegistro se expone tanto como datetime original como casteada a date (FECHA BUSQUEDA)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; NIT; Dígito de verificación; Tipo de persona (Natural/Jurídica); Tipo de retención; Autorretenedor; ICA; Porcentaje ICA; Nacionalidad; Departamento; Municipio; Dirección; Cubo financiero contable', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un registro por tercero válido con su dirección principal (la primera según ROW_NUMBER PARTITION BY nit ORDER BY nit, code, code DESC); si no tiene dirección, los campos de ubicación quedan en NULL por el LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si thi.persontype = 1 → TipoPersona = ''Natural'' else Si persontype = 2 → ''Jurídico''; otro valor → NULL; si thi.retentiontype IN (0,1,2,3) → Mapea a ''Ninguna'',''Exento de retención'',''Hace Retención'',''Autoretenedor'' respectivamente else Otro valor → NULL; si thi.ica = 0 → ManejaICA = ''No'' else Si ica = 1 → ''Si''; otro valor → NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'common.thirdparty; common.person; common.address; common.department; common.country; common.city', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingThirdparty';
GO
