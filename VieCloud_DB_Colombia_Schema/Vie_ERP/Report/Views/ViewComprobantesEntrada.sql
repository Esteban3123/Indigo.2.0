
CREATE VIEW [Report].[ViewComprobantesEntrada] AS

WITH
CTE_DETAIL_EntranceVoucher AS
(
	SELECT
	EntranceVoucherId,
	SUM(EVD.Quantity) AS CANTIDAD,
	TIP.Name as TIPO,
	SUM(EVD.TotalValue) AS TOTAL
	FROM 
	Inventory.EntranceVoucherDetail EVD INNER JOIN 
	Inventory.InventoryProduct AS PRO ON EVD.ProductId=PRO.Id INNER JOIN
	Inventory.ProductType TIP ON PRO.ProductTypeId=TIP.Id
	GROUP BY EntranceVoucherId,TIP.Name
)

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
YEAR(CAST(EV.DocumentDate AS DATE)) * 100 + MONTH(CAST(EV.DocumentDate AS DATE)) [ID_TIEMPO],
EV.Code AS [CODIGO COMPROBANTE DE ENTRADA], 
EV.DocumentDate AS [FECHA COMPROBANTE DE ENTRADA], 
TER.Nit AS [NIT TERCERO],
PR.Name AS PROVEEDOR, 
EV.InvoiceNumber AS [# FACTURA], 
EV.Description AS DESCRIPCION, 
FORMAT(EVD.TOTAL,'##########')TOTAL, 
CASE EV.status WHEN 1 THEN 'Registrado' 
				WHEN 2 THEN 'Confirmado' 
				WHEN 3 THEN 'Anulado' END AS ESTADO,
UPPER(EVD.TIPO) TIPO,
EVD.CANTIDAD,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM     
Inventory.EntranceVoucher AS EV INNER JOIN
CTE_DETAIL_EntranceVoucher EVD ON EV.Id=EVD.EntranceVoucherId INNER JOIN
Common.Supplier AS PR ON PR.Id = EV.SupplierId INNER JOIN
Common.ThirdParty AS TER ON PR.IdThirdParty=TER.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a análisis y consulta de comprobantes de entrada de mercancía al inventario. Consolida, por comprobante, el proveedor (NIT y nombre), número de factura, fecha, estado (Registrado/Confirmado/Anulado), y los totales y cantidades agrupados por tipo de producto. Incluye una dimensión de tiempo en formato AAAAMM y el nombre de la base de datos como identificador de compañía, lo que sugiere uso en entornos multitenant o reporting centralizado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reportería que consolida los comprobantes de entrada de inventario con totales y cantidades agregadas por tipo de producto, datos del proveedor y tercero, para análisis BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada EntranceVoucher debe tener al menos un EntranceVoucherDetail asociado (INNER JOIN con el CTE).; Cada producto del detalle debe estar catalogado en InventoryProduct y tener un ProductType válido.; El comprobante debe tener un SupplierId válido en Common.Supplier y éste un IdThirdParty existente en Common.ThirdParty (INNER JOINs).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se deriva de DB_NAME() truncado a 9 caracteres, identificando la base de datos/empresa origen.; ID_TIEMPO se calcula como YEAR*100+MONTH de DocumentDate, formato AAAAMM para dimensión temporal.; El TIPO de producto se expone siempre en mayúsculas (UPPER).; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Los totales y cantidades están agregados al nivel comprobante-tipo de producto, no al detalle individual.; Solo se incluyen comprobantes que tengan detalle, proveedor y tercero asociados (INNER JOINs excluyen huérfanos).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Proveedor; Tercero (NIT); Tipo de producto; Factura de compra; Estado del comprobante (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewComprobantesEntrada: Devuelve una fila por combinación EntranceVoucher × tipo de producto, con SUM(Quantity) y SUM(TotalValue) agrupados por EntranceVoucherId y ProductType.Name.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EV.status = 1 → Estado se reporta como ''Registrado''; si EV.status = 2 → Estado se reporta como ''Confirmado''; si EV.status = 3 → Estado se reporta como ''Anulado'' else Cualquier otro valor de status produce NULL en ESTADO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Inventory.ProductType; Inventory.EntranceVoucher; Common.Supplier; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewComprobantesEntrada';
GO
