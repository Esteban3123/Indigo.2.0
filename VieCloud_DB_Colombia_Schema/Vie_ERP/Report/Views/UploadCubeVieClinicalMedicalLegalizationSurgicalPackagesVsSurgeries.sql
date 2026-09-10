
--CREATE PROCEDURE [EHR].[SP_CIRUGIA_LEGALIZACION_PAQUETES_QX]

--DECLARE @FECHAINI AS DATE='2024-06-01';
--DECLARE @FECHAFIN AS DATE='2024-06-10';

CREATE view [Report].[UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries] AS

	WITH CTE_PAQUETE_CABECERA AS 
	(
		SELECT 
			CASE PAC.IPTIPODOC 	
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
				WHEN 15 THEN 'SI' END AS [TipoIdentificacion], 
			CAB.ID 'ID_CAB_PAQUETE',
			CAB.CONSECUTIVO,
			CAB.IPCODPACI 'IDENTIFICACION',
			PAC.IPNOMCOMP 'PACIENTE',
			CAB.NUMINGRES 'INGRESO',
			CAB.FECHAREGISTRO 'FECHA REGISTRO',
			CAB.ESTADO,
			CAST(QX.FECHORAIN AS DATE) 'FECHA CIRUGIA',
			CAB.IDAGEPROGQX,QX.CODSERIPS 'CUPS',
			CUPS.DESSERIPS 'PROCEDIMIENTOS QX',
			QX.CODCENATE,
			CEN.NOMCENATE 'CENTRO DE ATENCION'
		FROM  dbo.HCHOJAGASTOQX CAB WITH (nolock)
		INNER JOIN DBO.AGEPROGQX QX WITH (nolock) ON CAB.IDAGEPROGQX=QX.CODAUTONU
		INNER JOIN DBO.INPACIENT AS PAC WITH (nolock) ON PAC.IPCODPACI=CAB.IPCODPACI
		INNER JOIN DBO.INCUPSIPS AS CUPS WITH (nolock) ON CUPS.CODSERIPS =QX.CODSERIPS  
		INNER JOIN DBO.ADCENATEN AS CEN WITH (nolock) ON CEN.CODCENATE =QX.CODCENATE 
		WHERE CAB.ESTADO = 4 AND CAST(QX.FECHORAIN AS DATE)>='2024-01-01'
		--AND CAST(QX.FECHORAIN AS DATE)  BETWEEN @FECHAINI AND @FECHAFIN
	)

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		cab.[TipoIdentificacion] 'TIPO IDENTIFICACION',--,
		CAB.IDENTIFICACION AS 'IDENTIFICACION',--[NroIdentificacion],
		RTRIM(CAB.PACIENTE) AS 'NOMBRE PACIENTE',--[NombrePaciente],
		CAB.[CENTRO DE ATENCION] AS 'CENTRO ATENCION',--[CentroAtencion],
		CAB.INGRESO AS 'NRO INGRESO',--[NroIngreso],
		CAB.CUPS AS 'CUPS',--[CodCUPS],
		CAB.[PROCEDIMIENTOS QX] AS 'PROCEDIMIENTO',--[Procedimiento],
		CAB.[FECHA CIRUGIA] AS 'FECHA PROCEDIMIENTO',--[FechaProcedimiento],
		DET.CODPRODUC AS 'CODIGO PRODUCTO',--[CodProducto], 
		RTRIM(PRO.DESPRODUC) AS 'DESCRIPCOIN PRODUCTO',--[DescripcionProducto],
		DET.CANTIDADENTREGADA AS 'CANTIDAD ENTREGADA',--[CantidadEntregada], 
		DET.CANTIDADGASTADA AS 'CANTIDAD USUADA',--[CantidadUsada],
		DET.CANTIDADACEPTADADEV AS 'CANTIDAD DEVUELTA',--[CantidadDevuelta]
		CAST(CAB.[FECHA CIRUGIA] AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM DBO.HCHOJAGASTOQXD DET
	INNER JOIN dbo.IHLISTPRO AS PRO WITH (nolock) ON DET.CODPRODUC = PRO.CODPRODUC
	INNER JOIN CTE_PAQUETE_CABECERA AS CAB WITH (nolock) ON CAB.ID_CAB_PAQUETE = DET.IDHCHOJAGASTOQX

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para alimentar un cubo, el detalle de productos consumidos en hojas de gasto de cirugía legalizadas (estado 4) frente a la programación quirúrgica del paciente, desde 2024-01-01.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La hoja de gasto quirúrgico debe existir y estar en estado = 4 (legalizada).; La cirugía programada asociada debe tener fecha de inicio (FECHORAIN) mayor o igual a 2024-01-01.; Deben existir relaciones íntegras entre hoja de gasto, programación quirúrgica, paciente, CUPS y centro de atención.; Cada detalle de producto debe corresponder a un producto vigente en el catálogo IHLISTPRO.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan hojas de gasto quirúrgico legalizadas (ESTADO = 4).; Solo se incluyen cirugías cuya fecha de inicio sea a partir del 1 de enero de 2024.; Cada fila representa un producto de una hoja de gasto vinculada a una cirugía programada específica.; El identificador de compañía se toma dinámicamente del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.; La marca de última actualización se calcula con la hora actual convertida a la zona horaria ''Pakistan Standard Time''.; El resultado es DISTINCT (no se duplican filas idénticas de producto-cirugía-paciente).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Ingreso hospitalario; Cirugía programada; Hoja de gasto quirúrgico; Legalización de cirugía; Paquete quirúrgico; Procedimiento CUPS; Centro de atención; Producto/insumo médico; Cantidad entregada / usada / devuelta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando la hoja de gasto quirúrgico tiene ESTADO = 4 y la fecha del procedimiento es >= 2024-01-01, devuelve una fila por producto consumido con cantidades entregada, usada y devuelta, junto a datos del paciente, ingreso, CUPS y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1..15) → Traduce el código numérico a sigla del tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else Si el código no está en el rango 1..15, el tipo de identificación queda en NULL; si CAB.ESTADO = 4 AND CAST(QX.FECHORAIN AS DATE) >= ''2024-01-01'' → Incluye la cabecera de la hoja de gasto en el resultado else La excluye', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAGASTOQX; dbo.AGEPROGQX; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.HCHOJAGASTOQXD; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalLegalizationSurgicalPackagesVsSurgeries';
GO
