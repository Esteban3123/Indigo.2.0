CREATE VIEW [Portfolio].[ViewPortfolioNoteAccountReceivableDetail]
AS
	SELECT	pnard.Id,
			pnard.PortfolioNoteAccountReceivableId,
			----------------------------------
			v.BillingGroupCodeName,
			v.CodeName,
			v.AlternativeCodeName,
			----------------------------------
			v.MainAccountNumberName,
			v.CostCenterCodeName,
			----------------------------------
			v.Quantity,
			v.UnitSalesPrice,
			v.TotalSalesPrice,
			pnard.Value
	FROM Portfolio.PortfolioNoteAccountReceivableDetail pnard
	JOIN Portfolio.ViewInvoiceDetails v ON pnard.EntityName = v.EntityName AND pnard.EntityId = v.EntityId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que expone el detalle de las líneas contables asociadas a cada nota de cartera en cuentas por cobrar, enriquecido con la información de facturación correspondiente. Combina los movimientos de la tabla de detalle de notas de cartera (valores ajustados) con los datos de la vista de detalles de factura, vinculando por entidad, para obtener el grupo de facturación, código del servicio o producto, código alternativo, cuenta contable principal, centro de costo, cantidad, precio unitario y precio total de venta. Se utiliza para conciliación contable, auditoría de ajustes en cartera y reportería financiera de notas débito o crédito en cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioNoteAccountReceivableDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de líneas de notas de cartera de cuentas por cobrar enriquecido con la información contable y comercial proveniente del detalle de facturas asociado a la entidad referenciada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de notas de cartera cuya pareja (EntityName, EntityId) exista en ViewInvoiceDetails (JOIN interno: excluye huérfanos).; El Value mostrado proviene del detalle de la nota de cartera, mientras que cantidades y precios provienen del detalle de factura.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'nota de cartera; cuenta por cobrar; detalle de factura; grupo de facturación; cuenta contable principal; centro de costo; precio unitario de venta; precio total de venta', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewPortfolioNoteAccountReceivableDetail: Devuelve un registro por cada línea de PortfolioNoteAccountReceivableDetail que tenga coincidencia en ViewInvoiceDetails por (EntityName, EntityId).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNoteAccountReceivableDetail; Portfolio.ViewInvoiceDetails', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNoteAccountReceivableDetail';
GO
