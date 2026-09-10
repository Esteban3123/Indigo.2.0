

--CREATE PROCEDURE [EHR].[SP_MEDICAMENTOS_ORDENADOS]
--DECLARE	@FECINI DATE='2024-06-01';
--DECLARE	@FECFIN DATE ='2024-06-30';

CREATE view [Report].[UploadCubeVieClinicalMedicalOrderedMedications] AS

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
		RTRIM(C.IPCODPACI) AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NombrePaciente], 
		HEA.Code AS 'CODIGO ENTIDAD',--[CodEntidad],
		HEA.Name AS 'ENTIDAD',--[Entidad],
		CGR.Name AS 'GRUPO ATENCION',--[GrpAtencion],
		RTRIM(CEN.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion], 
		RTRIM(UFU.UFUDESCRI) AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],

		C.NUMINGRES AS 'NRO INGRESO',--[NroIngreso],
		CAST(ING.IFECHAING AS DATE) AS 'FECHA INGRESO',--[FechaIngreso],
		C.NUMEFOLIO AS 'NRO FOLIO',--[NroFolio], 
		CAST(C.FECINIDOS AS DATE) AS 'FECHA HISTORIA',--[FechaHistoria], 
		RTRIM(C.CODPROSAL) + ' - ' + RTRIM(SAL.NOMMEDICO) AS 'NOMBRE MEDICO',--[NombreMedico], 
		RTRIM(C.CODPRODUC) AS 'CODIGO MEDICAMENTO',--[CodMedicamento],
		RTRIM(PRO.DESPRODUC) AS 'MEDICAMENTO',--[Medicamento],
		PRO.CONCENMED AS 'CONCENTRACION',--[Concentracion],
		VIA.DESVIAADM AS 'VIA ADMINISTRACION',--[ViaAdministracion], 
		FME.DESFORMED AS 'PRESENTACION',--[Presentacion], 
		ISNULL(C.DOSISPROD,C.DOSISPRFN) AS 'DOSIS SOLICITADA',--[DosisSolicitada], 
		UNI.DESUNIMED AS 'UNIDAD',--[Unidad],
		C.DURACIDOS AS 'DURACION',--[Duracion], 
		C.DESADMINI AS 'DESCRIPCION ADMINISTRACION',--[DescripcionAdministracion], 
		IIF(
			C.MANEXTPRO=1,
			ISNULL(C.FECFINDOS,ING.IFECHAING),
			C.FECFINDOS
		) AS 'FECHA FINAL DOSIS',--[FechaFinalDosis],
		C.CODCONCEC AS 'CONSECUTIVO',--[Consecutivo], 
		C.INDAPLMED AS 'INDICACIONES ADMINISTRACION',--[IndicacionesAdministracion],  
		C.MOTSUSMED AS 'MOTIVO SUSPENSION',--[MotivoSuspension], 
		CASE C.MANEXTPRO WHEN 0 THEN 'INTRAHOSPITALARIO' ELSE 'EXTERNO' END AS 'TIPO SOLICITUD',--[TipoSolicitud], 
		C.CANPEDPRO AS 'CANTIDAD CALCULADA',--[CantidadCalculada],
		C.TOTPROUNI AS 'DOSIS MEDICAMENTO',--[DosisMedicamento],
		UNI.DESUNIMED AS 'UNIDAD MEDICAMENTO',--[UnidadMedicamento],
		ESQ.Code +' - ' + ESQ.Description AS 'ESQUEMA',--[Esquema], 
		CASE C.MEDICACUSTODIA WHEN 1 THEN 'SI' ELSE 'NO' END AS 'CUSTODIA',--[Custodia],
		CASE WHEN ATC.POSProduct='0' THEN 'NO' ELSE 'SI' END AS 'INCLUIDO PBS',--[IncluidoPBS],
		CASE WHEN ATC.Conditioned='0' THEN 'NO' ELSE 'SI' END AS 'MEDICAMENTO CONDICIONADO',--[MedicamentoCondicionado],
		CASE WHEN ATC.UNIRS ='0' THEN 'NO' ELSE 'SI' END 'MEDICAMENTO UNIRS',--[MedicamentoUNIRS],
		RTRIM(C.CODDIAGNO) + ' - ' + RTRIM(DIA.NOMDIAGNO) 'DIAGNOSTICO',--[Diagnostico],
		CASE ESTADIO 
			WHEN 0 THEN 'estadio clínico (ec) 0 (tumor in situ)' 
			WHEN 1 THEN 'ec I o 1' 
			WHEN 2 THEN 'ec IA o 1A' 
			WHEN 3 THEN 'ec IA1' 
			WHEN 4 THEN 'ec IA2'
			WHEN 5 THEN 'ec IB o 1b' 
			WHEN 6 THEN 'ec IB1' 
			WHEN 7 THEN 'ec IB2' 
			WHEN 8 THEN 'ec IC o 1c'
			WHEN 9 THEN 'ec IS o 1s' 
			WHEN 10 THEN 'ec II o 2' 
			WHEN 11 THEN 'ec IIA o 2a'
			WHEN 12 THEN 'ec IIA1' 
			WHEN 13 THEN 'ec IIA2' 
			WHEN 14 THEN 'ec IIB o 2b' 
			WHEN 15 THEN 'ec IIC o 2c' 
			WHEN 16 THEN 'ec III o 3' 
			WHEN 17 THEN 'ec IIIA o 3a'
			WHEN 18 THEN 'ec IIIB o 3b' 
			WHEN 19 THEN 'ec IIIC o 3c' 
			WHEN 20 THEN 'ec IV o 4' 
			WHEN 21 THEN 'ec IVA o 4a' 
			WHEN 22 THEN 'ec IVB o 4b' 
			WHEN 23 THEN 'ec IVC o 4c'
			WHEN 24 THEN 'ec 4S (para neuroblastoma)' 
			WHEN 25 THEN 'ec  V o 5' 
			WHEN 26 THEN 'ec Estadio IAB'
			WHEN 55 THEN 'Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorialdurante el periodo de reporte'
			WHEN 93 THEN 'Sin información de estadificación en historia clínica'
			WHEN 98 THEN 'No Aplica (Es cáncer de piel basocelular, es cáncer hematológico o es cáncer en SNC, excepto neuroblastoma)'
			WHEN 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos' else '' END 'ESTADIO',--[Estadio],
		CAST(C.FECINIDOS AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		--INTO EHR.STG_MEDICAMENTOS_ORDENADOS
	FROM DBO.HCPRESCRA C
	INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =C.IPCODPACI 
	INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON C.NUMINGRES =ING.NUMINGRES 
	INNER JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON C.CODCENATE =CEN.CODCENATE
	INNER JOIN dbo.INUNIFUNC AS UFU WITH (NOLOCK) ON C.UFUCODIGO =UFU.UFUCODIGO 
	INNER JOIN dbo.INPROFSAL AS SAL WITH (NOLOCK) ON C.CODPROSAL =SAL.CODPROSAL
	INNER JOIN dbo.IHLISTPRO AS PRO WITH (NOLOCK) ON C.CODPRODUC =PRO.CODPRODUC 
	INNER JOIN Inventory.ATC AS ATC  WITH (NOLOCK) ON PRO.CODPRODUC =ATC.Code 
	INNER JOIN dbo.HCVIAADMI AS VIA WITH (NOLOCK) ON C.CODVIAADM =VIA.CODVIAADM 
	INNER JOIN dbo.IHFORMEDI AS FME WITH (NOLOCK) ON C.CODFORMED =FME.CODFORMED 
	INNER JOIN dbo.INDIAGNOS AS DIA WITH (NOLOCK) ON C.CODDIAGNO =DIA.CODDIAGNO 
	INNER JOIN dbo.INDIAGNOP AS EST WITH (NOLOCK) ON C.IPCODPACI=EST.IPCODPACI AND C.NUMINGRES =EST.NUMINGRES AND est.coddiapri = 1
	JOIN Contract.HealthAdministrator HEA WITH (NOLOCK) ON ING.GENCONENTITY =HEA.ID
	JOIN Contract.CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id
	LEFT JOIN dbo.INUNIMEDI AS UNI WITH (NOLOCK) ON ISNULL(C.CODUNIMED,C.CODUNIMFN )=UNI.CODUNIMED 
	LEFT JOIN EHR.Schemes AS ESQ WITH (NOLOCK) ON C.IDESQUEMAONC =ESQ.Id 
	WHERE CAST(C.FECINIDOS AS DATE)>='2024-01-01'
	--CAST(C.FECINIDOS AS DATE) BETWEEN @FECINI AND @FECFIN
	--C.IPCODPACI ='1111' --AND C.NUMINGRES ='33699'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado de medicamentos ordenados en historia clínica desde 2024-01-01, enriquecido con datos del paciente, ingreso, prescriptor, entidad, diagnóstico, esquema oncológico y clasificación ATC para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo registro de prescripción debe tener paciente, ingreso, centro de atención, unidad funcional, profesional, producto, vía de administración, forma médica y diagnóstico válidos en sus catálogos (INNER JOIN obligatorios).; El producto prescrito debe existir en el catálogo ATC de Inventario.; El ingreso debe tener entidad administradora de salud y grupo de atención asignados (JOIN obligatorios con Contract.HealthAdministrator y Contract.CareGroup).; Debe existir un diagnóstico principal (coddiapri = 1) asociado al paciente y al ingreso en INDIAGNOP.; Solo se incluyen prescripciones con FECINIDOS >= 2024-01-01.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa una prescripción única (DISTINCT) de un medicamento ordenado en historia clínica.; El identificador de compañía corresponde al nombre de la base de datos actual truncado a 9 caracteres.; La marca temporal de actualización se calcula con la zona horaria ''Pakistan Standard Time''.; Solo aparece el diagnóstico principal del ingreso (coddiapri = 1).; Las prescripciones sin entidad administradora o sin grupo de atención no aparecen en el resultado.; Se reportan únicamente prescripciones desde el 1 de enero de 2024 en adelante.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación; Ingreso/admisión; Folio de historia clínica; Profesional de la salud / médico prescriptor; Medicamento y concentración; Vía de administración; Forma farmacéutica / presentación; Dosis y duración; Esquema oncológico; Estadio clínico (clasificación TNM); Diagnóstico principal; Entidad administradora de salud; Grupo de atención; Centro de atención y unidad funcional; Plan de Beneficios en Salud (PBS); Medicamento condicionado; Medicamento UNIRS; Custodia de medicamentos; Medicación intrahospitalaria vs externa', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalOrderedMedications: Devuelve filas DISTINCT por prescripción de medicamento con datos administrativos y clínicos cuando CAST(FECINIDOS AS DATE) >= ''2024-01-01'' y existen todos los maestros relacionados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente entre 1 y 15 → Mapea el código numérico a una abreviatura de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI) else Devuelve NULL para tipo de identificación; si C.MANEXTPRO = 1 (medicación externa) → La fecha final de dosis usa ISNULL(FECFINDOS, IFECHAING) tomando la fecha de ingreso si no hay fecha final else La fecha final de dosis es FECFINDOS tal cual; si C.MANEXTPRO = 0 → Clasifica la solicitud como ''INTRAHOSPITALARIO'' else Clasifica la solicitud como ''EXTERNO''; si C.MEDICACUSTODIA = 1 → Marca el medicamento como en custodia (''SI'') else Marca como ''NO''; si ATC.POSProduct = ''0'' → Marca ''INCLUIDO PBS'' como ''NO'' else Marca como ''SI''; si ATC.Conditioned = ''0'' → Marca ''MEDICAMENTO CONDICIONADO'' como ''NO'' else Marca como ''SI''; si ATC.UNIRS = ''0'' → Marca ''MEDICAMENTO UNIRS'' como ''NO'' else Marca como ''SI''; si Valor de ESTADIO entre 0-26, 55, 93, 98 o 99 → Traduce el código a la descripción textual del estadio clínico (clasificación TNM/oncológica o categoría especial de reporte) else Retorna cadena vacía; si C.CODUNIMED es NULL → Usa C.CODUNIMFN para resolver la unidad de medida del medicamento else Usa C.CODUNIMED; si C.DOSISPROD es NULL → Usa C.DOSISPRFN como dosis solicitada else Usa C.DOSISPROD', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.IHLISTPRO; Inventory.ATC; dbo.HCVIAADMI; dbo.IHFORMEDI; dbo.INDIAGNOS; dbo.INDIAGNOP; Contract.HealthAdministrator; Contract.CareGroup; dbo.INUNIMEDI; EHR.Schemes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalOrderedMedications';
GO
