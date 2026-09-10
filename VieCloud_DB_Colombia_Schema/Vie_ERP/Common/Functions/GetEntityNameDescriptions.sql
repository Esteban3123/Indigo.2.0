
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
		('CostDistributionDirectCost', 'Distribución de Elementos del Costo'),
		('ConsignmentCostList', 'Lista de costos en consignación'),
		('CausationRecognition', 'Reconocimiento de causación'),
		('CausationRecognitionReverse', 'Anulación econocimiento de causación'),
		('ConsignmentTransferReclassification', 'Traslado en consignación')
	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de catálogo que retorna el listado completo de tipos de documentos y entidades financiero-contables del sistema, cada uno con su nombre técnico en inglés y su descripción en español. Cubre entidades como facturas, cuentas por cobrar y pagar, órdenes de compra, dispensación farmacéutica, nómina, activos fijos, contabilidad y cartera, entre otras. Se usa como tabla de referencia para que otros procesos puedan obtener la descripción legible en negocio del tipo de origen o entidad que generó un movimiento o documento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetEntityNameDescriptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'GetEntityNameDescriptions';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Provee un catálogo fijo que mapea identificadores técnicos de entidades origen del sistema a su descripción funcional en español, abarcando módulos contables, financieros, de inventario, cartera, activos fijos, nómina y salud.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetEntityNameDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de entidades origen es estático y se materializa por código (no proviene de tabla persistida).; Cada entidad origen tiene asociada una única descripción legible en español.; Los nombres de entidad funcionan como claves técnicas (en inglés/PascalCase) y la descripción como etiqueta de presentación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetEntityNameDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por Pagar; Cuentas por Cobrar; Recibo de Caja; Factura; Factura Capitada; Glosa (Recepción de Objeciones); Dispensación Farmacéutica; Inventario; Contrato; Activo Fijo; Nómina; Tesorería; Contabilidad; Orden de Compra; Remisión; Consignación; Compromiso; Obligación; Causación Diferido; Liquidación de Impuestos; Provisión; Distribución del Costo; Factoring; Cartera; Reintegro; Reconocimiento; Traslado', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetEntityNameDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @EntityNames: Siempre retorna el mismo conjunto fijo de pares (EntityName, Description) mediante un INSERT VALUES literal, sin filtros ni parámetros.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetEntityNameDescriptions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'GetEntityNameDescriptions';
GO
