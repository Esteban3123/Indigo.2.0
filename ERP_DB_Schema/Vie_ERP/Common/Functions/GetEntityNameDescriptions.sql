
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-28
-- Description:	Obtiene las entidades origen con su descripción
-- =============================================
CREATE FUNCTION [Common].[GetEntityNameDescriptions]
(
)
RETURNS @EntityNames TABLE 
(
	EntityName VARCHAR(220),
	Description VARCHAR(MAX)
)
AS
BEGIN

	INSERT INTO @EntityNames VALUES
		('AccountPayable', 'Cuentas por Pagar'),
		('AccountReceivable', 'Cuenta por Cobrar'),
		('AccountReceivableDocument', 'Documento de Cuenta por Cobrar'),
		('CashReceipts', 'Recibo de Caja'),
		('BasicBilling', 'Factura Básica'),
		('Commitment', 'Compromiso'),
		('Consignment', 'Consignación'),
		('CrossingAccount', 'Cruce de Cuentas'),
		('ConsignmentInventoryRemission', 'Remisión de Inventario en Consignación'),
		('DeferredCausation', 'Causación Diferido'),
		('DocumentInvoiceProductSales', 'Venta de Productos'),
		('DocumentInvoiceProductSalesDevolution', 'Reversión Venta de Productos'),
		('EntranceVoucher', 'Comprobante de Entrada'),
		('EntranceVoucherDevolution', 'Devolución de Compra'),
		('FactoringDocument', 'Documento Factoring'),
		('FixedAssetActiveOutput', 'Salida Activo'),
		('FixedAssetDepreciation', 'Depreciación'),
		('FixedAssetEntry', 'Entrada de Activo Fijo'),
		('FixedAssetPurchaseOrder', 'Orden de Compra de Activo Fijo'),
		('GlosaObjectionsReceptionD', 'Recepción de Objeciones'),
		('InitialBalance', 'Saldo Inicial'),
		('InventoryAdjustment', 'Ajuste de Inventario'),
		('InventoryContract', 'Contrato'),
		('InventoryContractAssignment', 'Cesión de Contrato'),
		('InventoryContractModification', 'Otro si de Contrato'),
		('InventoryControl', 'Control de Inventario'),		
		('Invoice', 'Factura'),
		('InvoiceCancellation', 'Anulación Factura'),
		('InvoiceEntityCapitated', 'Factura Capitada'),
		('InvoiceEntityCapitatedDistribution', 'Distribución Monto Fijo'),
		('JournalVouchers','Contabilidad'),
		('LoanMerchandise', 'Préstamo de Mercancía'),
		('LoanMerchandiseDevolution', 'Devolución de Préstamo de Mercancía'),
		('LowTaxLiquidation', 'Liquidación de Impuestos Menores'),
		('Obligation', 'Obligación'),
		('ObligationModification', 'Modificación de Obligación'),
		('PaymentNotes', 'Notas de Pago'),
		('PaymentTransfer', 'Cruce de Anticipo Vs CXP'),
		('PayrollLiquidation', 'Nómina'),
		('PharmaceuticalDispensing', 'Dispensación Farmacéutica'),
		('PharmaceuticalDispensingDevolution', 'Devolución Dispensación Farmacéutica'),
		('PortfolioNote', 'Nota Cuenta por Cobrar'),
		('PortfolioReclassification', 'Reclasificación de Documentos de Cartera'),
		('PortfolioTransfer', 'Cruce de Anticipo vs CXC'),
		('PurchaseOrder', 'Orden de Compra'),
		('RadicateInvoiceC', 'Factura Radicada'),
		('ReimbursementResource', 'Reintegro'),
		('RemissionDevolution', 'Devolución de Remisión'),
		('RemissionEntrance', 'Remisión de Entrada'),
		('Recognition', 'Reconocimiento'),
		('TransferOrder', 'Orden de Traslado'),
		('TransferOrderDevolution', 'Devolución Orden de Traslado'),
		('TreasuryNote', 'Nota de Tesorería'),
		('VoucherTransaction', 'Comprobante Contable'),		
		('TaxesLiquidation', 'Liquidación de Impuestos'),
		('DistributionCostElements', 'Provisión'),
		('CostDistributionDirectCost', 'Distribución de Elementos del Costo')
	RETURN
END