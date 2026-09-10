
--CREATE PROCEDURE [EHR].[SP_APLICACION_MEDICAMENTOS]

--DECLARE	@FECINI Datetime='2024-06-01';
--DECLARE	@FECFIN Datetime ='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalMedicationApplication] AS

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
		APL.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NomPaciente], 
		HEA.Code AS 'CODIGO ENTIDAD',--[CodEntidad],
		HEA.Name AS 'NOMBRE ENTIDAD',--[NomEntidad],
		CGR.Name AS 'GRUPO ATENCION',--[GrpAtencion],
		CEN.NOMCENATE AS 'CENTRO ATENCION',--[CenAtencion], 
		UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		APL.NUMINGRES 'NRO INGRESO',--[NroIngreso],
		CASE ING.TIPOINGRE 
			WHEN 1 THEN 'AMBULATORIO' 
			WHEN 2 THEN 'HOSPITALARIO' END AS 'TIPO INGRESO',--[TipoIngreso],
				SAL.NOMMEDICO AS 'NOMBRE MEDICO',--[NomMedico],
		APL.CODPRODUC AS 'CODIGO PRODUCTO',--[CodProducto], 
		PRO.DESPRODUC AS 'DESCRIPCION PRODUCTO',--[DesProducto],
		FECINITRA 'FECHA INICIO TRATAMEINTO',--[FecIniTratamiento], 
		FECPROAPL 'FECHA PROGRAMADA',--[FecProgramada], 
		FECAPLMED 'FECHA REAL APLICACION',--[FecRealAplicacion], 
		FECREGSIS 'FECHA REGISTRO SISTEMA',--[FecRegistroSistema], 
		DATEDIFF(MINUTE, FECAPLMED, FECREGSIS) AS 'DIFERENCIA MINUTOS',--[DiferenciaMinutos],
		ISNULL(PRO.PESTOTMED,ISNULL(PRO.VOLTOTMED,'1')) AS 'DOSIS',--[Dosis],
		MED2.DESUNIMED 'UNIDOSIS PRODUCTO',--'UniDosisProducto',
		DOSISPROD AS 'DOSIS SOLICITADA',--[DosisSolicitada], 
		MED.DESUNIMED AS 'UNIDAD DOSIS SOLICITADA',--[UnidadDosisSolicitada], 
		FRECUENCI AS 'FRECUENCIA',--[Frecuencia],
		CASE UNIFRECUE 
			WHEN 1 THEN 'MINUTOS' 
			WHEN 2 THEN 'HORAS' 
			WHEN 3 THEN 'DIAS' END 'TIEMPO',--[Tiempo],
		DURACIDOS AS 'DURACION',--[Duracion],
		CASE CABESTADO 
			WHEN 1 THEN 'Activo' 
			WHEN 2 THEN 'Terminado' 
			WHEN 3 THEN 'Suspendido por modficacion del medicamento' END AS 'ESTADO MEDICAMENTO',--[EstadoMedicamento],
		CANDESCON AS 'CANTIDAD DESCONTADA',--[CanDescontada],
		DESADMINI AS 'DESCRIPCION ADMINISTRACION',--[DesAdministracion],
		ESQ.Description AS 'ESQUEMA',--[Esquema],
		ISNULL(ING.CODDIAING,ISNULL(ING.CODDIAEGR,QUI.CODDIAGNO )) AS 'CODIGO DIAGNOSTICO',--[CodDiagnostico],
		DIA.NOMDIAGNO AS 'DIAGNOSTICO',--[Diagnostico],
		CAST(APL.FECINITRA AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	--INTO INDIGODWH.EHR.STG_APLICACION_MEDICAMENTOS
	FROM  DBO.HCHOJAMED as APL WITH(NOLOCK)
	INNER JOIN .ADINGRESO AS ING ON APL.NUMINGRES =ING.NUMINGRES 
	INNER JOIN dbo.INPACIENT AS PAC WITH(NOLOCK) ON APL.IPCODPACI =PAC.IPCODPACI 
	INNER JOIN dbo.IHLISTPRO AS PRO WITH(NOLOCK) ON APL.CODPRODUC =PRO.CODPRODUC 
	INNER JOIN dbo.ADCENATEN AS CEN WITH(NOLOCK) ON APL.CODCENATE =CEN.CODCENATE 
	INNER JOIN dbo.INUNIFUNC AS UNI WITH(NOLOCK) ON APL.UFUCODIGO =UNI.UFUCODIGO 
	INNER JOIN dbo.INPROFSAL AS SAL WITH(NOLOCK) ON APL.CODPROSAL =SAL.CODPROSAL 
	INNER JOIN Contract .HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
	INNER JOIN Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id 
	LEFT JOIN dbo.INUNIMEDI AS MED WITH(NOLOCK) ON MED.CODUNIMED =APL.CODUNIMED
	LEFT JOIN dbo.INUNIMEDI AS MED2 WITH(NOLOCK) ON MED2.CODUNIMED =ISNULL(PRO.CODUNIPES ,ISNULL(PRO.CODUNIVOL ,PRO.CODUNIADM ))
	LEFT JOIN EHR.HCORDQUIMIO AS QUI WITH(NOLOCK) ON APL.IDHCORDQUIMIO =QUI.ID 
	LEFT JOIN EHR.Schemes AS ESQ WITH(NOLOCK) ON QUI.SchemesId =ESQ.Id 
	LEFT JOIN DBO.INDIAGNOS AS DIA WITH(NOLOCK) ON DIA.CODDIAGNO =ISNULL(ING.CODDIAING ,ISNULL(ING.CODDIAEGR,QUI.CODDIAGNO ))
	WHERE APL.MEDESTADO = 2 AND CAST(APL.FECINITRA AS DATE)>='2024-01-01'
	--and CAST(APL.FECINITRA AS DATE)  BETWEEN @FECINI AND @FECFIN

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para cargue al cubo de BI, las aplicaciones de medicamentos en estado válido desde 2024-01-01, enriquecidas con datos del paciente, ingreso, producto, dosificación, esquema de quimioterapia y diagnóstico asociado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos actual debe tener nombre de hasta 9 caracteres significativos para identificar la compañía; Las tablas maestras de pacientes, productos, centros, unidades funcionales, profesionales, entidad administradora y grupo de atención deben tener registro coincidente con la hoja de medicamentos (uso de INNER JOIN); El servidor debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL; Existe una referencia a la tabla ADINGRESO sin esquema explícito (''.ADINGRESO''), lo que requiere un esquema por defecto resoluble en tiempo de ejecución', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone aplicaciones de medicamento con MEDESTADO = 2 (estado considerado válido para reporte); Solo se reportan registros con fecha de inicio de tratamiento desde 2024-01-01 en adelante; El ID_COMPANY corresponde al nombre de la base de datos actual truncado a 9 caracteres; La fecha de última actualización (ULT_ACTUAL) se calcula con la zona horaria ''Pakistan Standard Time''; El diagnóstico reportado siempre proviene de la primera fuente disponible en orden: ingreso → egreso → orden de quimioterapia; La diferencia en minutos entre aplicación real (FECAPLMED) y registro en sistema (FECREGSIS) se calcula como métrica de oportunidad de registro; Se eliminan duplicados mediante DISTINCT; Solo se incluyen aplicaciones con paciente, ingreso, producto, centro, unidad funcional, profesional, entidad administradora y grupo de atención existentes (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Aplicación de medicamentos; Hoja de medicamentos; Ingreso ambulatorio/hospitalario; Centro de atención; Unidad funcional; Profesional de salud / médico tratante; Entidad administradora de salud (HealthAdministrator); Grupo de atención (CareGroup); Producto farmacéutico; Dosis y unidad de medida; Frecuencia y duración de administración; Estado del medicamento (Activo/Terminado/Suspendido); Orden de quimioterapia; Esquema de tratamiento oncológico; Diagnóstico (ingreso/egreso/quimioterapia)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset de la vista): Cuando MEDESTADO = 2 y CAST(FECINITRA AS DATE) >= ''2024-01-01'', se retorna la fila con los datos consolidados de aplicación de medicamento', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento del paciente (iptipodoc) según valor 1..15 → Se traduce a sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI); si TIPOINGRE del ingreso = 1 o 2 → Se clasifica como ''AMBULATORIO'' (1) u ''HOSPITALARIO'' (2); si UNIFRECUE = 1, 2 o 3 → Se traduce a ''MINUTOS'', ''HORAS'' o ''DIAS''; si CABESTADO del medicamento = 1, 2 o 3 → Se traduce a ''Activo'', ''Terminado'' o ''Suspendido por modificación del medicamento''; si MEDESTADO = 2 y FECINITRA >= 2024-01-01 → Se incluye el registro de aplicación de medicamento en la salida; si Diagnóstico no nulo en orden de ingreso, egreso o quimioterapia → Se prioriza CODDIAING, luego CODDIAEGR, luego CODDIAGNO de quimioterapia (vía ISNULL en cascada); si Dosis del producto → Se prioriza PESTOTMED; si es nulo, VOLTOTMED; si también es nulo, ''1''; si Unidad de dosis del producto → Se prioriza CODUNIPES; si es nulo, CODUNIVOL; si también es nulo, CODUNIADM', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.HCHOJAMED; dbo.ADINGRESO; dbo.INPACIENT; dbo.IHLISTPRO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; Contract.HealthAdministrator; Contract.CareGroup; dbo.INUNIMEDI; EHR.HCORDQUIMIO; EHR.Schemes; DBO.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationApplication';
GO
