-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Inventory].[SP_InventoryContractReport] 
	-- Add the parameters for the stored procedure here
	@InventoryReportCode Varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	select ic.Code, Concat(ou.UnitCode, ' - ' , ou.UnitName) As OperatingUnitCodeName,
		Concat(ict.Code, ' - ', ict.[Name]) As ContractTypeName, ic.InitialDate, ic.EndDate,
		Concat(sp.Code, ' - ', sp.[Name]) As SupplierName,
		Concat(dl.Code, ' - ', dl.[Name]) As DistributionLineName,
		ic.ContractNumber, ic.[Description], ic.PaymentMethod, ic.DeliveryMethod, ic.DeliveryPlace,
		Case ic.SourceOrder When 1 Then 'Orden Simple' When 2 Then 'Llamado Oferta' When 3 Then 'Licitacion Publica' Else '' End As SourceOrder,
		Case ic.PurchaseProcess When 1 Then 'Directa' When 2 Then 'Negociacion Conjunta' Else '' End As PurchaseProcess,
		Case ic.Exclusivity When 1 Then 'SI' Else 'NO' End As Exclusivity,
		Case ic.OnlyGuarantee When 1 Then 'SI' Else 'NO' End As OnlyGuarantee,
		ic.TechnicalSupervicion, ic.SupervisionExecution, ic.Clauses, ic.Attachments, ic.Availability, ic.Resolution, ic.ResolutionDate,
		ic.QuoteNumber, ic.QuoteDate, ic.RecordNumber, ic.RecordDate, ic.NegotiationType, ic.Approved, ic.Deadline, ic.ValidityDate,
		ic.Value, ic.DiscountValue, ic.IvaValue, ic.TotalValue,
		Case ic.Status When 1 Then 'Registrado' When 2 Then 'Confirmado' When 3 then 'Anulado' Else '' End as Status, 
		ic.CreationUser, ic.CreationDate,
		ipr.Code As ProductCode, ipr.Name As ProductName, icd.Quantity As Quantity, icd.Value As ValueProduct,
		icd.OutstandingQuantity As OutstandingQuantity, icd.SubTotalValue, icd.TotalValue As TotalValueProduct
	From Inventory.InventoryContract ic With(Nolock)
	inner join Common.OperatingUnit ou With(Nolock) On ic.OperatingUnitId = ou.Id
	inner join Inventory.InventoryContractType ict With(Nolock) On ic.ContractTypeId = ict.Id
	inner join Common.Supplier sp With(Nolock) On ic.SupplierId = sp.Id
	inner join Common.SuppliersDistributionLines so With(Nolock) On ic.SupplierDistributionLineId = so.Id
	inner join Common.DistributionLines dl With(Nolock) on so.IdDistributionLine = dl.Id
	left join Inventory.InventoryContractDetail icd with(nolock) on icd.InventoryContractId = ic.Id
	left outer join Inventory.InventoryProduct ipr with(nolock) on icd.ProductId = ipr.Id
	where ic.Id = @InventoryReportCode
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de un contrato de inventario/compras identificado por su código, consolidando en una sola consulta toda la información comercial y operativa del contrato: sede o unidad operativa, tipo de contrato, proveedor, línea de distribución contable, condiciones de pago y entrega, proceso de compra, exclusividad, cláusulas, valores (descuentos, IVA, total), estado y trazabilidad. Además incluye el detalle de cada producto o insumo pactado en el contrato con cantidades comprometidas, pendientes, precios unitarios y valores totales por línea. Se utiliza para consultar o imprimir el contrato completo con sus ítems desde el módulo de inventario y compras.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_InventoryContractReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_InventoryContractReport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte consolidado de un contrato de inventario con sus condiciones comerciales, partes involucradas, valores y detalle de productos pactados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador recibido debe corresponder al Id de un contrato existente en Inventory.InventoryContract (se compara ic.Id = @InventoryReportCode aunque el parámetro es VARCHAR(20)).; El contrato debe tener asociados unidad operativa, tipo de contrato, proveedor y línea de distribución de proveedor válidos para que aparezca en el resultado (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo retorna información de un único contrato identificado por su Id.; Las uniones a unidad operativa, tipo de contrato, proveedor y línea de distribución son obligatorias (INNER JOIN); un contrato sin estas referencias no se reportará.; Los productos del contrato son opcionales (LEFT JOIN); un contrato sin detalle igualmente se reporta con columnas de producto en NULL.; La línea de distribución se obtiene indirectamente vía la relación proveedor-línea (SuppliersDistributionLines).; Los códigos numéricos de SourceOrder, PurchaseProcess, Exclusivity, OnlyGuarantee y Status se presentan siempre como texto descriptivo legible.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato de inventario; Proveedor; Línea de distribución; Tipo de contrato; Unidad operativa; Producto de inventario; Detalle de contrato; Modalidad de compra (Directa/Negociación Conjunta); Origen de orden (Orden Simple/Llamado Oferta/Licitación Pública); Exclusividad; Garantía única; Estado del contrato (Registrado/Confirmado/Anulado); IVA y descuentos; Cantidad pendiente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada producto del contrato (o una fila con producto nulo si no tiene detalle) con datos de cabecera del contrato, proveedor, unidad operativa, línea de distribución y valores del ítem, filtrando por ic.Id = @InventoryReportCode.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ic.SourceOrder = 1/2/3 → Se traduce a ''Orden Simple'', ''Llamado Oferta'' o ''Licitacion Publica'' respectivamente else Cadena vacía; si ic.PurchaseProcess = 1/2 → Se traduce a ''Directa'' o ''Negociacion Conjunta'' else Cadena vacía; si ic.Exclusivity = 1 → Se muestra ''SI'' else ''NO''; si ic.OnlyGuarantee = 1 → Se muestra ''SI'' else ''NO''; si ic.Status = 1/2/3 → Se traduce a ''Registrado'', ''Confirmado'' o ''Anulado'' else Cadena vacía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryContract; Common.OperatingUnit; Inventory.InventoryContractType; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines; Inventory.InventoryContractDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_InventoryContractReport';
-- GO
