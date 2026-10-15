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