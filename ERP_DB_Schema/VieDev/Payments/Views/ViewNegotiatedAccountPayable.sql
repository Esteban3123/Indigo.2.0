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