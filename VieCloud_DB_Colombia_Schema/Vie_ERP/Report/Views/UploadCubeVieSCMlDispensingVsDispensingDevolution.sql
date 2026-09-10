

CREATE view [Report].[UploadCubeVieSCMlDispensingVsDispensingDevolution]
as
SELECT  
	    CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		PD.Code AS 'NRO DOCUMENTO',--[NroDocumento],
		CAST(PD.DocumentDate AS date) AS 'FECHA DOCUMENTO',--[FechaDocumento],
		PD.ConfirmationDate 'FECHA CONFIRMACION',--[FechaConfirmacion],
		CASE PD.Status WHEN 1 THEN 'REGISTRADO' WHEN 2 THEN 'CONFIRMADO' WHEN 3 THEN 'ANULADO' END AS 'ESTADO',--[Estado],
		PD.Code AS 'NRO DISPENSACION/DEVOLUCION',--[NroDispensacion],
		IIF(pd.AffectInventory = 0, 'NO', 'SI') AS 'AFECTA INVENTARIO',--[AfectaInventarios],
		PD.EntityName AS 'ORIGEN',--[Origen],
		'Resta' AS 'TIPO OPERACION',--[TipoOperacion],
		CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
		FU.Name AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		PD.AdmissionNumber AS 'NRO INGRESO',--[NroIngreso],
		ING.IFECHAING AS 'FECHA INGRESO',--[FechaIngreso],
		PAC.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		PAC.IPNOMCOMP AS 'PACIENTE',--[Paciente], 
		mun.ubinombre AS 'MUNICIPIO RESIDENCIA',--[MunicipioResidencia],
		TP2.Nit AS 'NIT',--[NIT],
		HA.Name AS 'ENTIDAD',--[Entidad],
		CG.Code + ' - ' + CG.Name AS 'GRUPO ATENCION',--[GrupoAtencion],
		CASE PT.Class WHEN 2 THEN 'MEDICAMENTO' WHEN 3 THEN 'INSUMO' ELSE 'OTRO' END AS 'TIPO PRODUCTO',--[TipoProducto],
		ISNULL(ATC.Code,ISS.Code ) AS 'CODIGO PADRE',--[CodigoPadre],
		ISNULL(ATC.Name,ISS.SupplieName ) AS 'DESCRIPCION PADRE',--[DescripcionPadre],
		IP.Code AS 'CODIGO PRODUCTO',--[CodigoProducto],
		IP.CodeCUM AS 'CUM',--[CUM],
		IP.CodeAlternative AS 'CODIGO ALTERNATIVO 1',--[CodigoAlterno1],
		IP.CodeAlternativeTwo AS 'CODIGO ALTERNATIVO 2',--[CodigoAlterno2],
		IP.Name AS 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
		ISNULL(ATC.Weight,ATC.Volume ) AS 'DOSIS',--[Dosis],
		ISNULL(IMU.Name ,IMU2.Name ) AS 'UNIDAD',--[Unidad],
		PF.Name AS 'FORMA FARMACEUTICA',--[FormaFarmaceutica], 
		pack.name AS 'PRESENTACION',--[Presentacion],
		CASE ATC.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END 'PRODUCTO POS',--[ProductoPOS],
		case when ATC.Conditioned=1 then 'SI' ELSE 'NO' END 'CONDICIONADO',--[Condicionado],
		CASE WHEN atc.UNIRS=1 THEN 'SI' ELSE 'NO' END 'UNIRS',--[UNIRS],
		WH.Code + '-' + WH.Name AS 'ALMACEN',--[Almacen],
		PDDBS.Quantity AS 'CANTIDAD',--[Cantidad],
		G.Quantity AS 'CANTIDAD DEVUELTA',--[CantidadDevuelta],
		PDD.AverageCost AS 'COSTO PRODUCTO',--[CostoProducto],
		cast(PDD.TotalSalesPrice as numeric ) AS 'VALOR UNITARIO',--[ValorUnitario],
		(PDDBS.Quantity * PDD.TotalSalesPrice) AS 'VALOR TOTAL',--[ValorTotal],
		far.fechaorde AS 'FECHA ORDEN',--[FechaOrden],
		pdd.ServiceDate AS 'FECHA DISPENSACION DEVOLUCION',--[FechaDispensacionDevolucion],
		BS .BatchCode AS 'LOTE',--[Lote],
		BS.ExpirationDate AS 'FECHA VENCIMIENTO',--[FechaVencimiento], 
		IP.HealthRegistration AS 'REGISTRO SANITARIO',--[RegistroSanitario],
		IP.ExpirationDate AS 'FECHA VENCIMIENTO RS',--[FechaVencimientoRS],
		TP.Nit +'-'+TP.Name AS 'PROFESIONAL',--[Profesional],
		isnull(SOD.AuthorizationNumber,ING.IAUTORIZA)  AS 'AUTORIZACION',--[Autorizacion],
		SU.CODUSUARI +'-'+SU.NOMUSUARI AS 'USUARIO REGISTRO',--[UsuarioRegistro],
		ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO ) AS 'DIAGNOSTICO',--[Diagnostico],
		'DISPENSACIONES' 'TIPO DOCUMENTO',-- [TipoDocumento],
		CAST(PD.DocumentDate AS DATE) AS 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	from Inventory.PharmaceuticalDispensing PD 
	INNER JOIN	dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =PD.AdmissionNumber 
	INNER JOIN	dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =ING.IPCODPACI  
	INNER JOIN	dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE =ING.CODCENATE 
	INNER JOIN	Inventory.PharmaceuticalDispensingDetail as PDD WITH (NOLOCK) ON PDD.PharmaceuticalDispensingId =PD.Id 
	INNER JOIN	Inventory.InventoryProduct AS IP WITH (NOLOCK) ON IP.Id =PDD.ProductId 
	INNER JOIN	Inventory.ProductType AS PT WITH (NOLOCK) ON PT.ID =IP.ProductTypeId  
	INNER JOIN	Inventory.Warehouse AS WH WITH (NOLOCK) ON WH.Id =PDD.WarehouseId 
	INNER JOIN	Payroll.FunctionalUnit AS FU WITH (NOLOCK) ON FU.Id =PDD.FunctionalUnitId 
	INNER JOIN	Billing.ServiceOrder as SO WITH (NOLOCK) ON PD.Id =SO.EntityId and PD.AdmissionNumber =SO.AdmissionNumber AND SO.Status =1 
	INNER JOIN	Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SO.Id =SOD.ServiceOrderId AND SOD.ProductId =PDD.ProductId AND SOD.IsDelete =0 and SOD.SupplyQuantity  =PDD.Quantity  
	INNER JOIN	Common.ThirdParty AS TP WITH (NOLOCK) ON TP.Id =PDD.OrderedHealthProfessionalThirdPartyId
	LEFT JOIN  DBO.SEGusuaru SU ON PD.CreationUser=SU.CODUSUARI 
	LEFT JOIN	Inventory.ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
	LEFT JOIN	Inventory.InventoryMeasurementUnit AS IMU WITH (NOLOCK) ON ATC.WeightMeasureUnit =IMU.Id  
	LEFT JOIN	Inventory.InventoryMeasurementUnit AS IMU2 WITH (NOLOCK) ON ATC.VolumeMeasureUnit  =IMU2.Id 
	LEFT JOIN	Inventory.PharmaceuticalForm PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId =PF.Id  
	LEFT JOIN	Inventory.InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId 
	LEFT JOIN	Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id =PDD.CareGroupId 
	LEFT JOIN	Contract.HealthAdministrator AS HA WITH (NOLOCK) ON HA.Id =PDD.HealthAdministratorId 
	LEFT JOIN 	Common.ThirdParty AS TP2 WITH (NOLOCK) ON TP2.Id =HA.ThirdPartyId  
	LEFT JOIN	Inventory.PharmaceuticalDispensingDetailBatchSerial AS PDDBS WITH (NOLOCK) ON PDD.ID= PDDBS.PharmaceuticalDispensingDetailId 
	LEFT JOIN 	Inventory.PhysicalInventory PI WITH (NOLOCK) ON PDDBS.PhysicalInventoryId = PI.Id 
	LEFT JOIN	Inventory.BatchSerial AS BS WITH (NOLOCK) ON BS.Id =PI.BatchSerialId 
	LEFT JOIN	dbo.INDIAGNOS AS DX WITH (NOLOCK) ON ING.CODDIAING =DX.CODDIAGNO 
	LEFT JOIN	dbo.INDIAGNOS AS DX2 WITH (NOLOCK) ON ING.CODDIAEGR  =DX2.CODDIAGNO 
	LEFT JOIN
	(
		SELECT  PharmaceuticalDispensingDetailBatchSerialId, SUM(Quantity) Quantity 
		FROM Inventory.PharmaceuticalDispensingDevolutionDetail GROUP BY PharmaceuticalDispensingDetailBatchSerialId
	) AS G ON G.PharmaceuticalDispensingDetailBatchSerialId =  PDDBS.Id 
	LEFT JOIN dbo.hcfarmepd AS fard ON pdd.entityid = fard.id
	LEFT JOIN dbo.hcfarmepc AS far ON fard.codconcec = far.codconcec 
	INNER JOIN inventory.packagingunit AS pack ON ip.packagingunitid = pack.id
	LEFT JOIN dbo.inubicaci AS mun ON pac.auubicaci = mun.auubicaci
	WHERE YEAR(PD.DocumentDate)>=2023 --and month(PD.DocumentDate)>=7
	--CAST(PD.DocumentDate AS DATE) BETWEEN @FECINI AND @FECFIN 
	group by PD.Code,PD.DocumentDate,PD.ConfirmationDate, PD.Status ,pd.AffectInventory,PD.EntityName,CEN.NOMCENATE,FU.Name,PD.AdmissionNumber,ING.IFECHAING ,PAC.IPCODPACI,PAC.IPNOMCOMP,
	TP2.Nit,HA.Name,CG.Code,CG.Name,PT.Class,ISNULL(ATC.Code,ISS.Code ),ISNULL(ATC.Name,ISS.SupplieName ),IP.Code,IP.CodeCUM,IP.CodeAlternative,IP.CodeAlternativeTwo,
	IP.Name,ATC.POSProduct,ATC.Conditioned,atc.UNIRS,WH.Code + '-' + WH.Name,PDDBS.Quantity,G.Quantity,PDD.AverageCost,cast(PDD.TotalSalesPrice as numeric ),PDDBS.Quantity * PDD.TotalSalesPrice, pdd.ServiceDate,FU.Name,
	BS .BatchCode,BS.ExpirationDate, IP.HealthRegistration,IP.ExpirationDate,TP.Nit +'-'+TP.Name,isnull(SOD.AuthorizationNumber,ING.IAUTORIZA),ING.IAUTORIZA ,SU.CODUSUARI +'-'+SU.NOMUSUARI,SOD.InvoicedQuantity ,SOD.SupplyQuantity,
	ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO ),ISNULL(ATC.Weight,ATC.Volume ) ,ISNULL(IMU.Name ,IMU2.Name ),PF.Name, far.fechaorde, pack.name, mun.ubinombre

	UNION ALL

	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		pddev.Code 'NRO DOCUMENTO',
		pddev.DocumentDate 'FECHA DOCUMENTO',
		pddev.ConfirmationDate 'FECHA CONFIRMACION',
		CASE pddev.Status WHEN 1 THEN 'REGISTRADO' WHEN 2 THEN 'CONFIRMADO' WHEN 3 THEN 'ANULADO' END 'ESTADO',
		pd.Code 'NRO DISPENSACION/DEVOLUCION',
		'SI' 'AFECTA INVENTARIO',
		pddev.EntityName 'ORIGEN',
		'Suma' 'TIPO OPERACION',
		CEN.NOMCENATE 'CENTRO ATENCION',
		FU.Name AS 'UNIDAD FUNCIONAL',
		pddev.AdmissionNumber 'NRO INGRESO',
		ING.IFECHAING as 'FECHA INGRESO',
		PAC.IPCODPACI AS 'NRO IDENTIFICACION',
		PAC.IPNOMCOMP AS 'PACIENTE', 
		mun.ubinombre AS 'MUNICIPIO RESIDENCIA',
		TP2.Nit 'NIT',
		HA.Name AS 'ENTIDAD',
		CG.Name AS 'GRUPO ATENCION',
		CASE PT.Class WHEN 2 THEN 'MEDICAMENTO' WHEN 3 THEN 'INSUMO' ELSE 'OTRO' END 'TIPO PRODUCTO',
		ISNULL(ATC.Code,ISS.Code ) AS 'CODIGO PADRE',
		ISNULL(ATC.Name,ISS.SupplieName ) AS 'DESCRIPCION PADRE',
		IP.Code AS 'CODIGO PRODUCTO',
		IP.CodeCUM AS 'CUM',
		IP.CodeAlternative 'CODIGO ALTERNATIVO 1' ,
		IP.CodeAlternativeTwo 'CODIGO ALTERNATIVO 2',
		IP.Name AS 'DESCRIPCION PRODUCTO',
		ISNULL(ATC.Weight,ATC.Volume ) 'DOSIS',
		ISNULL(IMU.Name ,IMU2.Name ) 'UNIDAD',
		PF.Name 'FORMA FARMACEUTICA', 
		pack.name AS 'PRESENTACION',
		CASE ATC.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END 'PRODUCTO POS',
		case when ATC.Conditioned=1 then 'SI' ELSE 'NO' END 'CONDICIONADO',
		CASE WHEN atc.UNIRS=1 THEN 'SI' ELSE 'NO' END 'UNIRS',
		CONCAT(w.Code, ' - ', w.Name) 'ALMACEN',
		pdd.Quantity 'CANTIDAD',
		pddevd.Quantity 'CANTIDAD DEVUELTA',
		pdd.AverageCost 'COSTO PRODUCTO' ,
		cast(PDD.TotalSalesPrice as numeric ) 'VALOR UNITARIO',
		pddevd.Quantity * cast(PDD.TotalSalesPrice as numeric ) 'VALOR TOTAL',
		far.fechaorde AS 'FECHA ORDEN',  
		CAST(pdd.ServiceDate AS date) AS 'FECHA DISPENSACION DEVOLUCION',
		bs.BatchCode 'LOTE',
		bs.ExpirationDate 'FECHA VENCIMIENTO',
		IP.HealthRegistration AS 'REGISTRO SANITARIO',
		IP.ExpirationDate AS 'FECHA VENCIMIENTO RS',
		TP.Nit +'-'+TP.Name AS 'PROFESIONAL',
		PDD.AuthorizationNumber AS 'AUTORIZACION',
		SU.CODUSUARI +'-'+SU.NOMUSUARI 'USUARIO REGISTRO', 
		ISNULL(ING.CODDIAEGR + ' - ' + DX2.NOMDIAGNO , ING.CODDIAING + ' - ' + DX.NOMDIAGNO )'DIAGNOSTICO',
		'DEVOLUCIONES' 'TIPO DOCUMENTO',
		CAST(pddev.DocumentDate AS DATE) AS 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Inventory.PharmaceuticalDispensingDevolution pddev WITH (NOLOCK) 
	INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =pddev.AdmissionNumber 
	INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =ING.IPCODPACI  
	INNER JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE =ING.CODCENATE
	INNER JOIN Inventory.PharmaceuticalDispensingDevolutionDetail pddevd WITH (NOLOCK) ON pddev.Id = pddevd.PharmaceuticalDispensingDevolutionId
	INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
	INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
	INNER JOIN Inventory.PharmaceuticalDispensing pd WITH (NOLOCK) ON pdd.PharmaceuticalDispensingId = pd.Id
	INNER JOIN Inventory.Warehouse w WITH (NOLOCK) ON pdd.WarehouseId = w.Id
	INNER JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
	INNER JOIN Inventory.ProductType AS PT WITH (NOLOCK) ON PT.ID =IP.ProductTypeId 
	INNER JOIN DBO.SEGusuaru SU ON pddev.CreationUser=SU.CODUSUARI
	--JOIN INDIGOSEC.Security.[User] AS US ON US.UserCode =pddev.CreationUser 
	INNER JOIN Payroll.FunctionalUnit AS FU WITH (NOLOCK) ON FU.Id =PDD.FunctionalUnitId
	INNER JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON TP.Id =PDD.OrderedHealthProfessionalThirdPartyId 
	LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phy.Id
	LEFT JOIN Inventory.ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
	LEFT JOIN Inventory.InventoryMeasurementUnit AS IMU WITH (NOLOCK) ON ATC.WeightMeasureUnit =IMU.Id  
	LEFT JOIN Inventory.InventoryMeasurementUnit AS IMU2 WITH (NOLOCK) ON ATC.VolumeMeasureUnit  =IMU2.Id
	LEFT JOIN Inventory.PharmaceuticalForm PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId =PF.Id 
	LEFT JOIN Inventory.InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
	LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id =ING.GENCAREGROUP   --=PDD.CareGroupId 
	LEFT JOIN Contract.HealthAdministrator AS HA WITH (NOLOCK) ON HA.Id =ING.GENCONENTITY  --=PDD.HealthAdministratorId 
	LEFT JOIN Common.ThirdParty AS TP2 WITH (NOLOCK) ON TP2.Id =HA.ThirdPartyId 
	LEFT JOIN dbo.INDIAGNOS AS DX WITH (NOLOCK) ON ING.CODDIAING =DX.CODDIAGNO 
	LEFT JOIN dbo.INDIAGNOS AS DX2 WITH (NOLOCK) ON ING.CODDIAEGR  =DX2.CODDIAGNO
	LEFT JOIN dbo.hcfarmepd AS fard ON pdd.entityid = fard.id
	LEFT JOIN dbo.hcfarmepc AS far ON fard.codconcec = far.codconcec 
	INNER JOIN inventory.packagingunit AS pack ON ip.packagingunitid = pack.id
	LEFT JOIN dbo.inubicaci AS mun ON pac.auubicaci = mun.auubicaci
	WHERE YEAR(pddev.DocumentDate)>=2023 --and  month(pddev.DocumentDate)>=7
	--CAST(pddev.DocumentDate AS DATE) BETWEEN @FECINI AND @FECFIN
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista destinada a alimentar un cubo analítico (OLAP) que consolida, mediante UNION ALL, las dispensaciones farmacéuticas (tipo operación "Resta") y sus devoluciones (tipo operación "Suma") ocurridas desde 2023. Integra datos clínicos del paciente, ingreso, diagnóstico, centro de atención, unidad funcional, entidad pagadora y grupo de atención con el detalle del producto dispensado/devuelto: clasificación ATC o insumo, lote, vencimiento, costos y valores. Permite comparar cantidades dispensadas versus cantidades devueltas por producto, almacén y episodio de ingreso para reporting de farmacia y facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista para cubo de reporte que consolida en un único conjunto las dispensaciones farmacéuticas (resta inventario) y sus devoluciones (suma inventario), con datos clínicos, administrativos y económicos del paciente, producto, lote y autorización, desde 2023.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe la base de datos actual (DB_NAME() se usa como ID_COMPANY truncado a 9 caracteres); Las dispensaciones deben tener una orden de servicio asociada con Status=1 y un detalle no eliminado (SOD.IsDelete=0) cuya cantidad suministrada coincida con la dispensada (SOD.SupplyQuantity = PDD.Quantity); Cada producto debe tener tipo de producto, almacén, unidad funcional, profesional ordenante y unidad de empaque (packagingunit) registrados; El ingreso, paciente y centro de atención referenciados deben existir en ADINGRESO/INPACIENT/ADCENATEN; Sólo se consideran documentos cuyo año de DocumentDate sea >= 2023', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen documentos con DocumentDate de año >= 2023; ID_COMPANY siempre es el nombre de la base de datos actual truncado a 9 caracteres; ULT_ACTUAL refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''; Las filas de tipo ''DISPENSACIONES'' representan operación de resta de inventario y las de ''DEVOLUCIONES'' operación de suma; VALOR TOTAL en dispensaciones = Quantity de lote * TotalSalesPrice; en devoluciones = Quantity devuelta * TotalSalesPrice; Las dispensaciones sólo se reportan si tienen orden de servicio activa (SO.Status=1) con detalle no borrado y cantidades coherentes (SOD.SupplyQuantity = PDD.Quantity); Las devoluciones siempre se consideran que afectan inventario (''SI'') independientemente de la dispensación origen; CANTIDAD DEVUELTA en el bloque de dispensaciones se calcula como suma de Quantity por PharmaceuticalDispensingDetailBatchSerialId desde PharmaceuticalDispensingDevolutionDetail', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Para cada par dispensación-detalle-lote válido, retorna una fila con TIPO OPERACION=''Resta'' y TIPO DOCUMENTO=''DISPENSACIONES''; [RETURN_RESULT] resultset: Para cada devolución farmacéutica con detalle y lote, retorna una fila adicional con TIPO OPERACION=''Suma'', AFECTA INVENTARIO=''SI'' fijo y TIPO DOCUMENTO=''DEVOLUCIONES'' (UNION ALL)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.Status / pddev.Status = 1 / 2 / 3 → Mapea estado a ''REGISTRADO'' / ''CONFIRMADO'' / ''ANULADO'' respectivamente; si pd.AffectInventory = 0 → AFECTA INVENTARIO = ''NO'' else AFECTA INVENTARIO = ''SI'' (en devoluciones siempre se fija ''SI''); si PT.Class = 2 / 3 / otro → TIPO PRODUCTO = ''MEDICAMENTO'' / ''INSUMO'' / ''OTRO''; si ATC.Code IS NOT NULL → Usa ATC.Code/Name como CODIGO/DESCRIPCION PADRE else Usa ISS.Code/SupplieName (insumo) como código/descripción padre; si ATC.Weight IS NOT NULL → DOSIS = ATC.Weight y UNIDAD = IMU.Name (unidad de peso) else DOSIS = ATC.Volume y UNIDAD = IMU2.Name (unidad de volumen); si ATC.POSProduct = 1 / Conditioned = 1 / UNIRS = 1 → Marca respectivamente PRODUCTO POS / CONDICIONADO / UNIRS como ''SI'', en otro caso ''NO''; si SOD.AuthorizationNumber IS NOT NULL → AUTORIZACION = SOD.AuthorizationNumber (en dispensaciones); si es NULL toma ING.IAUTORIZA. En devoluciones AUTORIZACION = PDD.AuthorizationNumber; si ING.CODDIAEGR IS NOT NULL → DIAGNOSTICO = código y nombre del diagnóstico de egreso (DX2) else DIAGNOSTICO = código y nombre del diagnóstico de ingreso (DX); si Bloque dispensación → CareGroup y HealthAdministrator se toman de PDD (PDD.CareGroupId, PDD.HealthAdministratorId) else Bloque devolución: CareGroup y HealthAdministrator se toman del ingreso (ING.GENCAREGROUP, ING.GENCONENTITY)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMlDispensingVsDispensingDevolution';
GO
