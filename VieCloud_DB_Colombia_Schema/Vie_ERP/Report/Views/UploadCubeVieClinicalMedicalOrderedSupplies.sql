
--CREATE PROCEDURE [EHR].[SP_INSUMOS_ORDENADOS]
--DECLARE	@FECINI DATE='2024-06-01';
--DECLARE	@FECFIN DATE='2024-06-30';
--AS
CREATE view [Report].[UploadCubeVieClinicalMedicalOrderedSupplies] AS

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion], 
		CAB.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NombrePaciente], 
		HEA.Code AS 'CODIGO ENTIDAD',--[CodEntidad],
		HEA.Name AS 'ENTIDAD',--[Entidad],
		CGR.Name AS 'GRUPO ATENCION',--[GrpAtencion],
		CAB.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		CEN.NOMCENATE 'CENTRO ATENCION',--[CentroAtencion], 
		UNI.UFUDESCRI 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		SAL.NOMMEDICO 'NOMBRE MEDICO',--[NombreMedico],
		CAST(FECHAORDE AS date) 'FECHA SOLICITUD',--[FechaSolicitud],
		DET.CODPRODUC 'CODIGO PRODUCTO',--[CodProducto], 
		PRO.DESPRODUC 'DESCRIPCION PRODUCTO',--[DescripcionProducto] ,
		CASE PRO.TIPPRODUC
			WHEN '3' THEN 'MEDICAMENTO COMO INSUMO' 
			WHEN '2' THEN 'INSUMO' END 'TIPO PRODUCTO',--[TipoProducto],
			CANPEDPRO 'CANTIDAD',--[Cantidad]
		CAST(CAB.FECHAORDE AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		--INTO INDIGODWH.EHR.STG_INSUMOS_ORDENADOS
	FROM dbo.HCSOLINSC AS CAB WITH (NOLOCK)
	INNER JOIN dbo.HCSOLINSD AS DET WITH (NOLOCK) ON CAB.CODCONCEC =DET.CODCONCEC
	INNER JOIN ADINGRESO AS ING  WITH (NOLOCK) ON ING.IPCODPACI =CAB.IPCODPACI AND ING.NUMINGRES =CAB.NUMINGRES
	INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK)  ON CAB.IPCODPACI =PAC.IPCODPACI 
	INNER JOIN dbo.IHLISTPRO AS PRO WITH (NOLOCK) ON DET.CODPRODUC =PRO.CODPRODUC 
	INNER JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CAB.CODCENATE =CEN.CODCENATE 
	INNER JOIN dbo.INUNIFUNC AS UNI WITH (NOLOCK) ON CAB.UFUCODIGO =UNI.UFUCODIGO 
	INNER JOIN dbo.INPROFSAL AS SAL WITH (NOLOCK) ON DET.CODPROSAL =SAL.CODPROSAL 
	INNER JOIN Contract.HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
	INNER JOIN Contract.CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id 
	WHERE PRO.TIPPRODUC <> '1' AND CAST(CAB.FECHAORDE AS DATE)>='2024-01-01'
	--AND CAST(CAB.FECHAORDE AS DATE) BETWEEN  @FECINI AND @FECFIN

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un dataset consolidado de solicitudes de insumos y medicamentos como insumo ordenados desde 2024-01-01, enriquecido con datos del paciente, entidad pagadora, grupo de atención, centro, unidad funcional y profesional, para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada solicitud (HCSOLINSC) debe tener detalle asociado en HCSOLINSD por CODCONCEC.; El paciente y su ingreso deben existir en ADINGRESO e INPACIENT (match por IPCODPACI y NUMINGRES).; El producto solicitado debe existir en el catálogo IHLISTPRO.; El ingreso debe tener entidad administradora (GENCONENTITY) y grupo de atención (GENCAREGROUP) válidos en Contract.HealthAdministrator y Contract.CareGroup.; Centro de atención, unidad funcional y profesional referenciados deben existir en sus catálogos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen solicitudes cuyo producto sea insumo o medicamento usado como insumo (excluye TIPPRODUC=''1'').; Solo se reportan solicitudes con fecha de orden desde 2024-01-01 en adelante.; Todas las consultas se hacen con NOLOCK (lectura sucia, sin bloqueos).; El ID_COMPANY se deriva dinámicamente del nombre de la base de datos actual (DB_NAME), truncado a 9 caracteres.; La marca de última actualización se calcula con la hora actual convertida a la zona horaria ''Pakistan Standard Time''.; Se aplica DISTINCT para evitar duplicados producto de los joins.; Solo aparecen solicitudes asociadas a un ingreso hospitalario existente con entidad pagadora y grupo de atención registrados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo y número de identificación; Ingreso hospitalario; Entidad administradora de salud (EPS/aseguradora); Grupo de atención; Centro de atención; Unidad funcional; Profesional de la salud / médico ordenador; Solicitud de insumos; Medicamento como insumo; Insumo médico; Catálogo de productos; Fecha de orden / solicitud', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalOrderedSupplies: Devuelve filas únicas (DISTINCT) solo cuando PRO.TIPPRODUC <> ''1'' y CAST(CAB.FECHAORDE AS DATE) >= ''2024-01-01''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.TIPPRODUC = ''3'' → Clasifica el producto como ''MEDICAMENTO COMO INSUMO''. else Si TIPPRODUC = ''2'' se clasifica como ''INSUMO''; si = ''1'' se excluye del resultado.; si PAC.IPTIPODOC entre 1 y 15 → Mapea el código numérico de tipo de documento a su sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI). else Devuelve NULL como tipo de identificación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLINSC; dbo.HCSOLINSD; dbo.ADINGRESO; dbo.INPACIENT; dbo.IHLISTPRO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; Contract.HealthAdministrator; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedSupplies';
GO
