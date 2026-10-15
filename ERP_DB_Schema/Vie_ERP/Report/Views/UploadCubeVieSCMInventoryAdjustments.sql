


CREATE view [Report].[UploadCubeVieSCMInventoryAdjustments]
AS



SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	ia.DocumentCode AS 'NRO DOCUMENTO',--[NroDocumento],
	ia.DocumentDate AS 'FECHA DOCUMENTO',--[FechaDocumento],
	ia.ConfirmationDate AS 'FECHA CONFIRMACION',--[FechaConfirmacion],
	ia.[TipoAjuste] 'TIPO AJUSTE',--,
	ia.[ConceptoAjuste] 'CONCEPTO AJUSTE',--,
	ia.[Tercero] 'TERCERO',--,
	ia.[Estado] 'ESTADO',--, 
	ia.UnitName AS 'SEDE',--[Sede],
	ia.Warehouse AS 'BODEGA',--[Bodega],
	ia.Operation AS 'OPERACION',--[Operacion],
	ia.ProductType AS 'TIPO',--[Tipo],
	ia.Code AS 'CODIGO PRODUCTO',--[CodigoProducto],
	ia.ProductName AS 'DESCRIPCION PRODUCTO',--[Descripcion Producto],
	IA.CODIGO_PADRE AS 'CODIGO PADRE',--[CodigoPadre],
	IA.NOMBRE_PADRE AS 'DESCRIPCION PADRE',--[DescripcionPadre],
	IA.HealthRegistration AS 'REGISTRO SANITARIO',--[RegistroSanitario],
	ia.BatchCode AS 'LOTE',--[Lote],
	ia.ExpirationDate AS 'FECHA VENCIMIENTO',--[FechaVencimiento],
	ia.[IVA],
	ia.Quantity AS 'CANTIDAD',--[Cantidad],
	ia.UnitValue AS 'VALOR  UNITARIO',--[ValorUnitario],
	(ia.Quantity * ia.UnitValue) AS 'VALOR TOTAL',--[ValorTotal],
	ia.Patient AS 'PACIENTE',--[Paciente],
	ia.CostCenter AS 'CENTRO DE COSTO',--[CentroCosto]
	cast(ia.ConfirmationDate as date) 'FECHA BUSQUEDA',
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
--INTO INDIGODWH.PBI.STG_AJUSTES_DE_INVENTARIOS
FROM
(
	SELECT	
	ia.Code DocumentCode,
	ia.DocumentDate,
	ia.ConfirmationDate ,
	case ia.AdjustmentType when 1 then 'Entrada' when 2 then 'Salida' when 3 then 'Inventario Fisico' end as [TipoAjuste],
	ac.Code + ' - ' + ac.Name [ConceptoAjuste],
	tp.Nit + ' - ' + tp.Name [TERCERO],
	case ia.Status when 1 then 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end [ESTADO],
	ou.UnitName,
	CONCAT(w.Code, ' - ', w.Name) Warehouse,
	IIF(ia.AdjustmentType = 1, 'Suma', 'Resta') Operation,
	ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,
	ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
	pt.Name ProductType,ip.HealthRegistration,ip.Code ,
	ip.Name ProductName,
	bs.BatchCode,
	bs.ExpirationDate,
	IVA.Name as [IVA],
	iadbs.Quantity,
	iad.UnitValue,
	CONCAT(RTRIM(pat.IPCODPACI), ' - ', pat.IPNOMCOMP) Patient,
	cc.Name CostCenter
FROM Common.OperatingUnit ou WITH (NOLOCK)
	JOIN Inventory.InventoryAdjustment ia WITH (NOLOCK) ON ou.Id = ia.OperatingUnitId
	JOIN Inventory.InventoryAdjustmentDetail iad WITH (NOLOCK) ON ia.Id = iad.InventoryAdjustmentId
	JOIN Inventory.InventoryAdjustmentDetailBatchSerial iadbs WITH (NOLOCK) ON iad.Id = iadbs.InventoryAdjustmentDetailId
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN Inventory.AdjustmentConcept ac WITH (NOLOCK) ON isnull(ia.AdjustmentConceptId,iad.AdjustmentConceptId)= ac.Id
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON ac.CostCenterId = cc.Id
	---------------------------------------------------------------------------------------------------------------
	JOIN Inventory.Warehouse w WITH (NOLOCK) ON ia.WarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON iad.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
    LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON iadbs.BatchSerialId = bs.Id
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN dbo.ADINGRESO ing WITH (NOLOCK) ON ia.AdmissionNumber = ing.NUMINGRES
	LEFT JOIN dbo.INPACIENT pat WITH (NOLOCK) ON ing.IPCODPACI = pat.IPCODPACI
	LEFT JOIN Common.ThirdParty as tp WITH (NOLOCK) ON tp.Id =ia.ThirdPartyId 
	LEFT JOIN GeneralLedger .GeneralLedgerIVA AS IVA WITH (NOLOCK) ON ip.IVAId =IVA.Id 
	--where ia.Code='ACA0000000370'
    --where CAST(ia.ConfirmationDate AS DATE) BETWEEN '2024-05-01' AND '2024-05-10'
) ia