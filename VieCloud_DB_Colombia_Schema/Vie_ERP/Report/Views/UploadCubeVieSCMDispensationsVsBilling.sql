

--CREATE OR ALTER PROCEDURE [Billing].[SP_DISPENSACIONES_VS_FACTURACION]
--DECLARE	@FecIni Datetime='2024-05-01';
--DECLARE	@FechFin Datetime ='2024-05-31';
--AS

CREATE view [Report].[UploadCubeVieSCMDispensationsVsBilling] as
	WITH CTE_PRODUCTOS_FACTURADOS
	AS
	(

		select 
			F.AdmissionNumber,F.InvoiceNumber ,F.InvoiceDate  ,PR.Code,SOD.ProductId  ,DF.InvoicedQuantity ,DF.TotalSalesPrice ,DF.GrandTotalSalesPrice ,OS.EntityId  as 'IDORDEN',
			SOD.ServiceOrderId,DF.ServiceOrderDetailId  ,SOD.InvoicedQuantity CANTIDADDETALLE,SOD.DevolutionQuantity ,F.HealthAdministratorId ,F.CareGroupId, HA.Code 'CODIGO ENTIDAD' ,HA.Name 'ENTIDAD',
			CG.Code 'CODIGO GRUPO ATENCION', CG.Name 'GRUPO ATENCION'
		FROM Billing.Invoice AS F WITH (NOLOCK)
		INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
		INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
		LEFT JOIN Inventory.InventoryProduct AS PR WITH (NOLOCK) ON PR.Id = SOD.ProductId
		LEFT JOIN Billing.ServiceOrder AS OS WITH (NOLOCK) ON OS.Id = SOD.ServiceOrderId
		LEFT JOIN Contract .HealthAdministrator as HA  WITH (NOLOCK) ON HA.Id =F.HealthAdministratorId 
		LEFT JOIN Contract .CareGroup AS CG WITH (NOLOCK) ON CG.Id =F.CareGroupId 
		LEFT JOIN Billing .ServiceOrderDetail AS SOD2 WITH (NOLOCK) ON SOD2.Id=SOD.IncludeServiceOrderDetailId
		where  OS.AffectInventory =1 AND F.Status ='1' AND  cast(F.InvoiceDate as date)>='2023-07-01'
		--CAST(F.InvoiceDate AS DATE) BETWEEN @FecIni AND (DATEADD(MONTH,4,@FechFin))  
		-- f.AdmissionNumber ='80216' --AND PR.Code ='0200456'-- ORDER BY 12

	)

	----*******DISPENSACIONES*********
	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		PD.Code AS 'NRO DOCUMENTO',--[NroDocumento],
		CAST(PD.DocumentDate AS date) AS 'FECHA DOCUMENTO',--[FechaDocumento],
		CAST(PD.ConfirmationDate AS DATE) AS 'FECHA CONFIRMACION',--[FechaConfirmacion],
		CASE PD.Status 
			WHEN 1 THEN 'REGISTRADO' 
			WHEN 2 THEN 'CONFIRMADO'
			WHEN 3 THEN 'ANULADO' END AS 'ESTADO',--[Estado], 
		HA.Name AS 'ENTIDAD DISPENSACION',--[EntidadDispensacion],
		CG.Name AS 'GRUPO ATENCION DISPENSACION',--[GrupoAtencionDispensacion],
		PD.AdmissionNumber AS 'NRO INGRESO',--[NroIngreso],
		CAST(ING.IFECHAING AS DATE) AS 'FECHA INGRESO',--[FechaIngreso],
		PAC.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
		PAC.IPNOMCOMP AS 'NOMBRE PACIENTE',--[NombrePaciente],
		IP.Code AS 'CODIGO PRODUCTO',--[CodigoProducto],
		IP.CodeCUM AS 'CUM',--[CUM],
		IP.CodeAlternative AS 'CODIGO ALTERNO',--[CodigoAlterno],
		IP.CodeAlternativeTwo 'CODIGO ALTERNO II',--[CodigoAlternoII],
		IP.Name AS 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
		G.Quantity  AS 'CANTIDAD DISPENSADA',--[CantidadDispensada],
		PDD.ReturnedQuantity AS 'CANTIDAD DEVUELTA',--[CantidadDevuelta], 
		(G.Quantity-PDD.ReturnedQuantity) AS 'CANTIDAD REAL DISPENSADA',--[CantidadRealDispensada],
		PDD.AverageCost AS 'COSTO PRODUCTO',--[CostoProducto],
		cast(PDD.TotalSalesPrice as numeric ) AS 'VALOR UNITARIO DISPENSADO',--[ValorUnitarioDispensado],
		CAST((G.Quantity * PDD.TotalSalesPrice) AS numeric )  AS 'VALOR TOTAL DISPENSADO',--[ValorTotalDispensado],
		CAST((G.Quantity-PDD.ReturnedQuantity)*cast(PDD.TotalSalesPrice as numeric ) AS numeric ) AS 'VALOR TOTAL DESPUES DEVOLUCIONES',--[ValorTotalDespuesDevoluciones],
		PF.InvoiceNumber AS 'NRO FACTURA',--[NroFactura],
		CAST(pf.InvoiceDate AS DATE) AS 'FECHA FACTURA',--[FechaFactura],
		PF.InvoicedQuantity AS 'CANTIDAD FACTURADA',--[CantidadFacturada],
		PF.TotalSalesPrice AS 'VALOR UNITARIO FACTURADO',--[ValorUnitarioFacturado], 
		PF.GrandTotalSalesPrice AS 'VALOR TOTAL FACTURADO',--[ValorTotalFacturado],
		PF.ENTIDAD AS 'ENTIDAD FACTURA',--[EntidadFactura],
		PF.[GRUPO ATENCION] AS 'GRUPO ATENCION FACTURA',--[GrupoAtencionFactura]
		CAST(PD.ConfirmationDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
	FROM Inventory .PharmaceuticalDispensing PD 
	INNER JOIN Inventory .PharmaceuticalDispensingDetail as PDD WITH (NOLOCK) ON PDD.PharmaceuticalDispensingId =PD.Id 
	INNER JOIN Inventory .InventoryProduct AS IP WITH (NOLOCK) ON IP.Id =PDD.ProductId
	INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES =PD.AdmissionNumber 
	INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI =ING.IPCODPACI
	LEFT JOIN Contract .HealthAdministrator as HA  WITH (NOLOCK) ON HA.Id =PDD.HealthAdministratorId 
	LEFT JOIN Contract .CareGroup AS CG WITH (NOLOCK) ON CG.Id =PDD.CareGroupId
	LEFT JOIN (

		SELECT PharmaceuticalDispensingDetailId ,SUM(Quantity) Quantity,SUM(OutstandingQuantity) CANTIDAD_REAL
		FROM Inventory.PharmaceuticalDispensingDetailBatchSerial AS PDDBS WITH (NOLOCK) 
		GROUP BY PharmaceuticalDispensingDetailId

	) AS G ON G.PharmaceuticalDispensingDetailId =PDD.Id 
	LEFT JOIN CTE_PRODUCTOS_FACTURADOS PF ON PF.AdmissionNumber =PD.AdmissionNumber AND PDD.ProductId =PF.ProductId  AND PF.IDORDEN =PD.Id 
	WHERE PD.Status =2  AND cast(PD.ConfirmationDate as date)>='2023-07-01'
	--CAST(PD.ConfirmationDate AS DATE) BETWEEN @FecIni AND @FechFin   
	--pd.AdmissionNumber ='82650' AND 
	--IP.Code ='19966576-03' AND ING.IPCODPACI ='94251959'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de conciliación que cruza dispensaciones farmacéuticas confirmadas contra los productos efectivamente facturados, exponiendo cantidades, valores y entidades para alimentar un cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de dispensaciones farmacéuticas con estado confirmado (Status=2) y fecha de confirmación a partir del 2023-07-01.; Las admisiones referenciadas deben existir en dbo.ADINGRESO y los pacientes en dbo.INPACIENT.; Para considerar un cruce con facturación, la factura debe estar activa (Status=''1''), su orden de servicio debe afectar inventario (AffectInventory=1) y la fecha de factura ser >= 2023-07-01.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan dispensaciones en estado CONFIRMADO (Status=2).; Solo se consideran facturas con Status=''1'' y cuya orden de servicio afecta inventario (AffectInventory=1).; El horizonte temporal mínimo es 2023-07-01 tanto para fecha de confirmación de dispensación como para fecha de factura.; La cantidad real dispensada se calcula como Quantity (de lotes/seriales) menos cantidad devuelta.; El valor total después de devoluciones se obtiene multiplicando (cantidad dispensada - cantidad devuelta) por el precio unitario.; El cruce dispensación-factura exige correspondencia exacta de admisión, producto y orden de servicio (ServiceOrder.Id = PharmaceuticalDispensing.Id).; La fecha de última actualización se entrega convertida a zona horaria ''Pakistan Standard Time''.; El identificador de compañía se obtiene dinámicamente del nombre de la base de datos actual.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Facturación; Devoluciones de medicamentos; Orden de servicio; Admisión/Ingreso del paciente; Paciente; Entidad administradora de salud (EPS); Grupo de atención; Producto de inventario (medicamentos/insumos); CUM (Código Único de Medicamento); Lotes y seriales; Costo y precio de venta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un dataset por cada detalle de dispensación farmacéutica confirmada (PD.Status=2) desde 2023-07-01, enriquecido opcionalmente con datos de facturación cuando coinciden AdmissionNumber, ProductId y ServiceOrderId (IDORDEN=PD.Id).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.Status = 1 → Estado se reporta como ''REGISTRADO''; si PD.Status = 2 → Estado se reporta como ''CONFIRMADO'' (único estado incluido por el WHERE); si PD.Status = 3 → Estado se reporta como ''ANULADO''; si Existe coincidencia en CTE de facturados por AdmissionNumber + ProductId + ServiceOrder → Se completan columnas de factura (número, fecha, cantidad, valores, entidad, grupo de atención) else Las columnas de facturación quedan en NULL (LEFT JOIN sin match)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Inventory.InventoryProduct; Contract.HealthAdministrator; Contract.CareGroup; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMDispensationsVsBilling';
GO
