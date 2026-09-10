/****** Object:  View [Payments].[ViewNegotiatedInvoicesDetail]    Script Date: 25/04/2022 4:02:00 p. m. ******/

CREATE VIEW [Payments].[ViewNegotiatedInvoicesDetail] 
as  
	SELECT 
	nid.Id As IdNegotiatedInvoicesDetail,
	ni.Id As IdNegotiatedInvoices,	
	ni.CodeHash,
	ap.BillNumber,
	ni.DateNegotiation,
	ap.Id As IdAccountPayable,
	ap.ExpirationDate,
	nid.PaymentDateWithExtension,
	ap.Balance,
	nid.TotalAmount,
	s.Id As IdSupplier,
	sdl.IdDistributionLines as IdDistributionLines,
	nid.Status
	FROM Payments.NegotiatedInvoices ni
	JOIN Payments.NegotiatedInvoicesDetail nid WITH(NOLOCK) on ni.Id = nid.IdNegotiatedInvoices	
	--antiguo proveedor 
	JOIN Common.Supplier sa  WITH(NOLOCK) ON sa.Code = nid.IssuerTaxNumber	
	--NUEVO PROVEEDOR	
	JOIN Common.Supplier s  WITH(NOLOCK) ON s.Code = nid.AssigneeTaxNumber
	JOIN Payments.AccountPayable ap  WITH(NOLOCK) ON ap.BillNumber = nid.InvoiceNumber  and sa.Id = ap.IdSupplier
	--NUEVA LINEA DEL PROVEEDOR MARCADA COMO Factoring
	JOIN (SELECT
					sdl.IdSupplier,					
					dl.Id AS IdDistributionLines
		FROM Common.SuppliersDistributionLines sdl
		JOIN Common.DistributionLines dl WITH(NOLOCK) ON sdl.IdDistributionLine = dl.Id 
		WHERE sdl.Factoring = 1

	) AS sdl on sdl.IdSupplier = s.Id	
	--WHERE ap.IdSupplier = s.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de facturas negociadas mediante confirming o factoring, combinando la cabecera de negociación con el detalle de cada factura individual, la cuenta por pagar asociada y los datos tanto del proveedor original (emisor) como del nuevo beneficiario del pago (cesionario). Integra además la línea de distribución contable marcada como factoring que corresponde al cesionario, permitiendo conocer para cada factura negociada: el número de factura, montos, saldos, fecha de vencimiento original, fecha de pago con prórroga, estado de la negociación y la imputación contable aplicable. Sirve como base de reportería y consulta operativa del proceso de descuento o cesión de facturas a terceros (factoring), facilitando el seguimiento de pagos y la trazabilidad del cambio de acreedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewNegotiatedInvoicesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewNegotiatedInvoicesDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de facturas negociadas (factoring/cesión) cruzando la cuenta por pagar original del proveedor cedente con el nuevo proveedor cesionario y su línea de distribución habilitada para factoring.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de negociación debe referenciar dos proveedores existentes en Common.Supplier: el cedente (IssuerTaxNumber) y el cesionario (AssigneeTaxNumber).; Debe existir una cuenta por pagar (AccountPayable) cuyo BillNumber coincida con el InvoiceNumber del detalle y cuyo IdSupplier corresponda al proveedor cedente.; El proveedor cesionario debe tener al menos una línea de distribución asociada con la marca Factoring = 1.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La factura mostrada siempre pertenece originalmente al proveedor cedente (IssuerTaxNumber) en AccountPayable.; El IdSupplier expuesto corresponde siempre al proveedor cesionario (nuevo) que recibe la cesión.; Solo se exponen negociaciones cuyo proveedor cesionario está habilitado para factoring mediante una línea de distribución marcada.; Empareja la factura por número (BillNumber = InvoiceNumber) restringiendo al proveedor original para evitar colisiones de número entre proveedores.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura negociada; Factoring / cesión de facturas; Proveedor cedente (antiguo); Proveedor cesionario (nuevo); Cuenta por pagar; Línea de distribución contable; Fecha de negociación; Fecha de pago con extensión; Saldo de factura; Fecha de expiración', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.ViewNegotiatedInvoicesDetail: Solo retorna registros donde el proveedor cesionario (AssigneeTaxNumber) tiene al menos una línea de distribución con Factoring = 1; si no la tiene, la negociación se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sdl.Factoring = 1 en Common.SuppliersDistributionLines → Se incluye la línea de distribución del proveedor cesionario en el resultado else Se excluye la negociación del proveedor cesionario sin línea de factoring', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.NegotiatedInvoices; Payments.NegotiatedInvoicesDetail; Common.Supplier; Payments.AccountPayable; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewNegotiatedInvoicesDetail';
GO
