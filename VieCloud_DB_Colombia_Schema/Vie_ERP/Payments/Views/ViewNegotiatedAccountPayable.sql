CREATE VIEW [Payments].[ViewNegotiatedAccountPayable] 
as  
	SELECT 
	ap.Id,
	tp.Nit,
	ap.Code As EntityCode,
	s.Name AS NameSupplier,
	dl.IdMainAccount As MainAccountIdDistributionLine,	
	dl.Name as DistributionLine,
	ap.BillNumber as InvoiceNumber,
	ap.Balance as InvoiceBalance,	
	ap.Term as Term
	FROM Payments.AccountPayable ap 
	JOIN Common.ThirdParty tp ON tp.Id = ap.IdThirdParty	
	JOIN Common.Supplier s ON s.IdThirdParty = tp.Id
	LEFT JOIN (SELECT top 1 sdl.IdSupplier,dl.Name,dl.IdMainAccount
		FROM Common.SuppliersDistributionLines sdl
		JOIN Common.DistributionLines dl ON sdl.IdDistributionLine = dl.Id
		WHERE sdl.Factoring = 1
	) AS dl	on dl.IdSupplier = s.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las cuentas por pagar negociadas con proveedores, combinando la información del documento o factura recibida (número, saldo y plazo) con los datos del proveedor (NIT y nombre) y la línea de distribución contable asociada a factoring. Integra las tablas de cuentas por pagar, terceros, proveedores y líneas de distribución para exponer en un solo resultado los datos necesarios para gestionar y consultar pagos pendientes bajo esquemas de negociación o factoring. Es útil para reportería de tesorería, conciliación de saldos con proveedores y seguimiento de facturas pendientes de pago por línea contable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewNegotiatedAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewNegotiatedAccountPayable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las cuentas por pagar a proveedores enriquecidas con datos del tercero y su línea de distribución asociada a factoring, para gestión de pagos negociados.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada cuenta por pagar debe tener un tercero asociado (IdThirdParty) que exista como proveedor en Common.Supplier.; Para obtener la línea de distribución, el proveedor debe tener al menos un registro en Common.SuppliersDistributionLines con Factoring = 1.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran líneas de distribución con factoring activo (Factoring = 1).; Una cuenta por pagar nunca se excluye por falta de línea de distribución (uso de LEFT JOIN).; La subconsulta de líneas de distribución retorna como máximo un registro (TOP 1) sin ORDER BY definido.; Solo se incluyen cuentas por pagar cuyo tercero esté registrado como proveedor.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Proveedor; Tercero; Factura; Saldo de factura; Plazo de pago; Línea de distribución; Cuenta contable principal; Factoring; NIT', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.ViewNegotiatedAccountPayable: Devuelve cuentas por pagar unidas a tercero y proveedor; la línea de distribución solo se incluye cuando existe una asociación del proveedor marcada con Factoring = 1, tomando el TOP 1.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sdl.Factoring = 1 en Common.SuppliersDistributionLines → Se asocia la línea de distribución (Name e IdMainAccount) al proveedor else Los campos de línea de distribución (MainAccountIdDistributionLine, DistributionLine) quedan en NULL por el LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.ThirdParty; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedAccountPayable';
GO
