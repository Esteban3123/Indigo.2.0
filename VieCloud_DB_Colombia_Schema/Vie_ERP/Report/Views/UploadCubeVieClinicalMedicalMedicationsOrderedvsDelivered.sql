
--CREATE PROCEDURE [EHR].[SP_MEDICAMENTOS_ORDENADOS_ENTREGADOS]
--DECLARE	@FECINI Datetime='2024-06-01';
--DECLARE	@FECFIN Datetime ='2024-06-30';
--AS

CREATE view [Report].[UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered] AS

	WITH DISPENSACION
	AS
	(
		SELECT 
			AdmissionNumber 'INGRESO',C.ConfirmationDate 'FECHA ENTREGA',ATC.Code 'CODIGO MEDICAMENTO',P.Code 'CUM',D.Quantity 'CANTIDAD ENTREGADA',D.EntityId 'IDFORMULA',
			war.code + ' - ' + war.name AS [NOMBRE ALMACEN], cc.name AS [CENTRO DE COSTO ALMACEN]
		FROM Inventory.PharmaceuticalDispensing AS C WITH (NOLOCK)
		JOIN Inventory.PharmaceuticalDispensingDetail AS D WITH (NOLOCK) ON C.Id =D.PharmaceuticalDispensingId 
		JOIN Inventory.InventoryProduct AS P WITH (NOLOCK) ON  D.ProductId =P.ID 
		JOIN Inventory.ATC AS ATC  WITH (NOLOCK) ON P.ATCId = ATC.Id  
		LEFT JOIN inventory.warehouse AS war  WITH(NOLOCK) ON d.warehouseid = war.id
		LEFT JOIN payroll.costcenter AS cc WITH(NOLOCK) ON war.costcenterid = cc.id
		WHERE C.Status =2
	), CTE_CONSULTAS_ORDENAMIENTO 
	AS
	(
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
				WHEN 15 THEN 'SI' END AS 'TIPO DOCUMENTO',--[TipoDocumento],
			C.IPCODPACI AS 'NRO IDENTIFICACION PACIENTE',--[NroIdentificaionPac],
			RTRIM(PAC.IPNOMCOMP) AS 'NOMBRE PACIENTE',--[NomPac], 
			RTRIM(HA.Name) AS 'ENTIDAD',--[Entidad], 
			RTRIM(cg.name) AS 'GRUPO ATENCION',--[GrpAtencion],
			RTRIM(CEN.NOMCENATE) AS 'CENTRO ATENCION',--[CenAtencion],
			UNI.UFUDESCRI AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
			C.NUMINGRES AS 'NRO INGRESO',--[NroIngreso], 
			MED.NOMMEDICO AS 'NOMBRE MEDICO',--[NomMedico],
			C.CODCONCEC AS 'NRO FORMULA',--[NroFormula],
			C.FECHAORDE AS 'FECHA ORDEN',--[FecOrden],
			D.CODPRODUC AS 'CODIGO MEDICAMENTO',--[CodMedicamento],
			PRO.DESPRODUC AS 'NOMBRE MEDICAMENTO',--[NomMedicamento], 
			PRE.DESFORMED 'PRESENTACION',--[Presentacion],
			PG.Name AS 'GRUPO FARMACOLOGICO',--[GrpFarmacologico],
			CANPEDPRO AS 'CANTIDAD SOLICITADA',--[CanSolicitada],
			DIS.[FECHA ENTREGA] AS 'FECHA ENTREGA',--[FecEntrega],
			ISNULL(DIS.[CANTIDAD ENTREGADA] ,0) AS 'CANTIDAD ENTREGADA',--[CanEntregada],
			dis.[NOMBRE ALMACEN] AS 'NOMBRE ALMACEN',--[NomAlmacen], 
			dis.[CENTRO DE COSTO ALMACEN] AS 'CENTRO ATENCION ALMACEN',--[CenAtenAlmacen]
		    CAST(C.FECHAORDE AS DATE) as 'FECHA BUSQUEDA',
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		FROM HCFARMEPC AS C WITH (NOLOCK)
		JOIN HCFARMEPD AS D WITH (NOLOCK) ON C.CODCONCEC =D.CODCONCEC AND C.NUMINGRES =D.NUMINGRES 
		JOIN IHLISTPRO AS PRO WITH (NOLOCK) ON D.CODPRODUC =PRO.CODPRODUC AND PRO.TIPPRODUC ='1'
		JOIN INPROFSAL AS MED WITH (NOLOCK) ON D.CODPROSAL =MED.CODPROSAL 
		JOIN ADCENATEN AS CEN WITH (NOLOCK) ON C.CODCENATE =CEN.CODCENATE 
		JOIN INUNIFUNC AS UNI WITH (NOLOCK) ON C.UFUCODIGO =UNI.UFUCODIGO 
		JOIN INPACIENT AS PAC WITH (NOLOCK) ON C.IPCODPACI =PAC.IPCODPACI
		JOIN ADINGRESO AS ING WITH (NOLOCK) ON C.NUMINGRES =ING.NUMINGRES 
		JOIN Contract.HealthAdministrator AS HA WITH (NOLOCK) ON ING.GENCONENTITY =HA.Id  
		JOIN contract.caregroup AS cg ON ing.gencaregroup = cg.id
		JOIN Inventory.ATC AS ATC  WITH (NOLOCK) ON D.CODPRODUC =ATC.Code 
		JOIN Inventory.ATCEntity AS ATCE WITH (NOLOCK) ON ATC.ATCEntityId =ATCE.Id 
		JOIN Inventory.PharmacologicalGroup AS PG WITH (NOLOCK) ON PG.Id =ATCE.IdPharmacologicalGroup 
		LEFT JOIN .IHFORMEDI AS PRE WITH (NOLOCK) ON PRO.CODFORMED =PRE.CODFORMED 
		LEFT JOIN DISPENSACION AS DIS WITH (NOLOCK) ON D.ID =DIS.IDFORMULA AND D.NUMINGRES = DIS.INGRESO 
		WHERE ORDESTADO <>'3'  AND CAST(C.FECHAORDE AS DATE) >='2024-01-01'
		--CAST(C.FECHAORDE AS DATE)  BETWEEN @FECINI AND @FECFIN
	)

	SELECT * FROM CTE_CONSULTAS_ORDENAMIENTO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los medicamentos ordenados a pacientes y los efectivamente dispensados desde farmacia, para alimentar un cubo analítico de cumplimiento de entregas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes farmacéuticas deben existir en HCFARMEPC/HCFARMEPD con estado distinto de ''3'' (anuladas/canceladas).; La fecha de la orden (FECHAORDE) debe ser igual o posterior a 2024-01-01.; Para que se considere ''entregado'', la dispensación en Inventory.PharmaceuticalDispensing debe tener Status = 2 (confirmada).; Los productos ordenados deben ser de tipo ''1'' (medicamentos) en IHLISTPRO.; Cada medicamento debe tener clasificación ATC y grupo farmacológico asociado vía ATCEntity y PharmacologicalGroup.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran entregadas las dispensaciones con Status = 2.; Solo se reportan órdenes con fecha de orden a partir del 2024-01-01.; Las órdenes anuladas (ORDESTADO=''3'') nunca aparecen en el resultado.; Solo se incluyen productos clasificados como medicamentos (TIPPRODUC=''1'').; La cantidad entregada nunca es NULL: si no hay dispensación, se sustituye por 0.; La marca de tiempo de actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El identificador de compañía se toma dinámicamente del nombre de la base de datos actual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento de identificación; Ingreso/admisión hospitalaria; Entidad administradora de salud (EPS/aseguradora); Grupo de atención; Centro de atención; Unidad funcional; Médico ordenante; Orden/fórmula médica; Medicamento; Clasificación ATC; Grupo farmacológico; Presentación farmacéutica; Dispensación farmacéutica; Cantidad solicitada vs cantidad entregada; Almacén de farmacia; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por orden-medicamento ordenado desde 2024-01-01 con estado <>''3'', enriquecida con la dispensación correspondiente (LEFT JOIN); si no hay dispensación, CANTIDAD ENTREGADA se reporta como 0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc entre 1 y 15 → Mapea el código numérico a la abreviatura del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI). else NULL (tipo de documento no reconocido); si C.Status = 2 en PharmaceuticalDispensing → Se incluye la dispensación como entrega válida en el CTE DISPENSACION. else Se descarta la dispensación (no se considera entregada).; si ORDESTADO <> ''3'' en HCFARMEPC → La orden se incluye en el reporte de medicamentos ordenados. else La orden se excluye (anulada).; si Existe coincidencia D.ID = DIS.IDFORMULA AND D.NUMINGRES = DIS.INGRESO → Se reporta la cantidad y fecha de entrega real. else CANTIDAD ENTREGADA = 0 y FECHA ENTREGA = NULL (orden no dispensada).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ATCEntity; Inventory.PharmacologicalGroup; inventory.warehouse; payroll.costcenter; Contract.HealthAdministrator; contract.caregroup; HCFARMEPC; HCFARMEPD; IHLISTPRO; INPROFSAL; ADCENATEN; INUNIFUNC; INPACIENT; ADINGRESO; IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalMedicationsOrderedvsDelivered';
GO
