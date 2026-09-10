

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista diseñada para alimentar un cubo de datos (BI/OLAP) con el detalle completo de ajustes de inventario (entradas, salidas e inventario físico). Consolida por documento de ajuste cada producto afectado, incluyendo lote, vencimiento, bodega, sede, tipo y concepto de ajuste, valor unitario y total, IVA y centro de costo. Incorpora datos del tercero involucrado y del paciente asociado vía número de admisión. Agrega marca de última actualización en zona horaria Pakistan Standard Time, lo que sugiere sincronización programada hacia un repositorio de reporting externo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los ajustes de inventario confirmados con sus detalles de producto, lote, bodega, sede, tercero, paciente y centro de costo, lista para cargar a un cubo de Power BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas Inventory.InventoryAdjustment, Inventory.InventoryAdjustmentDetail e Inventory.InventoryAdjustmentDetailBatchSerial deben estar relacionadas por sus claves Id/InventoryAdjustmentId/InventoryAdjustmentDetailId.; Cada ajuste debe estar asociado a una OperatingUnit, Warehouse, InventoryProduct y ProductType existentes (joins INNER).; El servidor debe reconocer la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres (DB_NAME()).; El campo ULT_ACTUAL siempre refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''.; VALOR TOTAL es siempre el producto de Quantity (del lote/serial) por UnitValue (del detalle).; FECHA BUSQUEDA es siempre ConfirmationDate truncada a fecha (sin hora).; El TERCERO se concatena como ''Nit - Name'' y el PACIENTE como ''IPCODPACI - IPNOMCOMP''.; La Bodega se presenta concatenada como ''Code - Name''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ajuste de inventario; Tipo de ajuste (Entrada/Salida/Inventario Físico); Concepto de ajuste; Lote y vencimiento; Registro sanitario; Clasificación ATC; Insumo; IVA; Centro de costo; Bodega/Sede; Paciente; Tercero (Nit); Ingreso/Admisión', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMInventoryAdjustments: Devuelve una fila por cada combinación de ajuste de inventario × detalle × lote/serial, enriquecida con datos maestros y con valor total calculado como Quantity * UnitValue.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ia.AdjustmentType = 1 → Etiqueta TipoAjuste=''Entrada'' y Operation=''Suma''. else Si AdjustmentType=2 → ''Salida''; si =3 → ''Inventario Fisico''; en ambos casos Operation=''Resta''.; si ia.Status (1/2/3) → Mapea Estado a ''Registrado'' (1), ''Confirmado'' (2) o ''Anulado'' (3).; si ia.AdjustmentConceptId IS NULL → Toma el concepto de ajuste desde iad.AdjustmentConceptId (nivel detalle). else Usa el concepto de ajuste a nivel cabecera (ia.AdjustmentConceptId).; si IP.ATCId existe en Inventory.ATC → CODIGO_PADRE/NOMBRE_PADRE se toman de ATC (clasificación ATC del medicamento). else Se toman de Inventory.InventorySupplie vía IP.SupplieId (insumo padre).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.OperatingUnit; Inventory.InventoryAdjustment; Inventory.InventoryAdjustmentDetail; Inventory.InventoryAdjustmentDetailBatchSerial; Inventory.AdjustmentConcept; Payroll.CostCenter; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ATC; Inventory.InventorySupplie; Inventory.BatchSerial; dbo.ADINGRESO; dbo.INPACIENT; Common.ThirdParty; GeneralLedger.GeneralLedgerIVA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMInventoryAdjustments';
GO
