
--CREATE PROCEDURE [dbo].[ODO_Mezclas_Liquidos]
	-- Add the parameters for the stored procedure here
--	@inidate DATE,
--	@enddate DATE

CREATE view [Report].[UploadCubeVieClinicalMedicalLiquidMixing] AS

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
		pac.ipcodpaci AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		pac.ipnomcomp AS 'NOMBRE',--[Nombre],
		ce.name AS 'ENTIDAD ADMINSTRADORA',--[EntidadAdministradora],
		gp.name AS 'GRUPO ATENCION',--[GrupoAtencion],
		ing.numingres AS 'NRO INGRESO',--[NroIngreso],
		CONVERT(VARCHAR(10), ing.ifechaing, 103) AS 'FECHA INGRESO',--[FechaIngreso],
		RTRIM(ca.nomcenate) AS 'CENTRO ATENCION',--[CentroAtencion], 
		RTRIM(uf.ufudescri) AS 'UNIAD FUNCIONAL',--[UnidadFuncional], 
		CASE mez.cabestado 
			WHEN 1 THEN 'Aplicado' 
			WHEN 2 THEN 'Completado' 
			WHEN 3 THEN 'Descartado/Suspendido' 
			WHEN 4 THEN 'Sin Aplicar/pendiente' END AS 'ESTADO',--[Estado],
		med.nommedico AS 'PROFESIONAL',--[Profesional],
		CONVERT(VARCHAR(10), mez.fecaplmed, 103) AS 'FECHA APLICACION',--[FechaAplicacion],
		mez.nommezcla AS 'MEZCLA LIQUIDO',-- [MezclaLiquidos],
		mez.indaplmed AS 'ADMINISTRACION',--[Administracion],
		atc.code AS 'CODIGO PRODUCTO',--[CodigoProducto],
		RTRIM(atc.name) AS 'NOMBRE PRODUCTO',--[NombreProducto],
		mezd.cantiutil AS 'CANTIDAD',--[Cantidad],
		CONVERT(VARCHAR(5), mez.fecaplmed, 108) AS 'HORA',--[Hora]
		CAST(mez.fecaplmed AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.hchojmezc AS mez
	INNER JOIN dbo.hchojmezd AS mezd ON mez.consecuti = mezd.consecuti
	INNER JOIN inventory.atc AS atc ON mezd.codproduc = atc.code
	INNER JOIN dbo.inprofsal AS med ON mez.codproapl = med.codprosal 
	INNER JOIN dbo.adcenaten AS ca ON mez.codcenate = ca.codcenate
	INNER JOIN dbo.inunifunc AS uf ON mez.ufucodigo = uf.ufucodigo
	INNER JOIN dbo.inpacient AS pac ON mez.ipcodpaci = pac.ipcodpaci
	INNER JOIN dbo.adingreso AS ing ON mez.numingres = ing.numingres
	INNER JOIN contract.healthadministrator AS ce ON ing.genconentity = ce.id
	INNER JOIN contract.caregroup AS gp ON ing.gencaregroup = gp.id
	WHERE CAST(mez.fecaplmed AS DATE)>='2024-01-01'
	--CAST(mez.fecaplmed AS DATE) BETWEEN @inidate AND @enddate

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para carga de cubo analítico la información de mezclas de líquidos/medicamentos aplicadas a pacientes desde 2024-01-01, integrando datos de paciente, ingreso, entidad administradora, profesional, centro/unidad funcional y producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en hchojmezc con fecha de aplicación (fecaplmed) >= 2024-01-01.; Cada mezcla debe tener detalle en hchojmezd, producto en inventory.atc, profesional en inprofsal, centro de atención, unidad funcional, paciente, ingreso, entidad administradora y grupo de atención asociados (joins INNER).; El servidor debe soportar la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan mezclas con fecha de aplicación a partir del 2024-01-01.; Solo se incluyen mezclas con todos los maestros relacionados (paciente, ingreso, entidad, grupo de atención, profesional, centro, unidad funcional, producto y detalle).; El identificador de compañía corresponde al nombre de la BD truncado a 9 caracteres.; La marca de última actualización siempre se entrega convertida a hora de ''Pakistan Standard Time''.; Las fechas se presentan en formato dd/mm/yyyy (estilo 103) y la hora en hh:mi (estilo 108).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Ingreso hospitalario; Entidad administradora de salud; Grupo de atención; Centro de atención; Unidad funcional; Profesional de la salud; Mezcla de líquidos/medicamentos; Aplicación/administración de medicamentos; Producto (ATC); Estado de aplicación (Aplicado/Completado/Descartado/Pendiente)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalLiquidMixing: Devuelve filas distintas de mezclas con fecha de aplicación >= 2024-01-01, mapeando códigos de tipo de documento y estado a etiquetas legibles, y agregando marca temporal de actualización en huso horario ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Traduce el código numérico a una sigla de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI). else NULL (sin etiqueta); si mez.cabestado entre 1 y 4 → Traduce estado a ''Aplicado'', ''Completado'', ''Descartado/Suspendido'' o ''Sin Aplicar/pendiente''. else NULL; si CAST(mez.fecaplmed AS DATE) >= ''2024-01-01'' → Incluye la mezcla en el resultado. else Excluye la mezcla.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hchojmezc; dbo.hchojmezd; inventory.atc; dbo.inprofsal; dbo.adcenaten; dbo.inunifunc; dbo.inpacient; dbo.adingreso; contract.healthadministrator; contract.caregroup', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLiquidMixing';
GO
