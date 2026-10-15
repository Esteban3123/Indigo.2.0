CREATE VIEW [Payments].[ViewAccountPayableFactoring] 
as  
	SELECT 
	ap.Id,
	ap.Code,
	ap.BillNumber,
	ap.BillDate,
	ap.ExpirationDate,
	ap.Balance,
	ap.Term,
	Concat(s.Code,' - ',s.Name) as SupplierCodeName,
	m.Number as NumberAccount,
	Concat(dl.Code,' - ',dl.Name) as DistributionLineName,
	ISNULL(ap.Coments,' ') Coments
	FROM Payments.AccountPayable ap
	JOIN Common.Supplier s WITH (NOLOCK) ON s.Id = ap.IdSupplier
	JOIN GeneralLedger.MainAccounts m  WITH (NOLOCK) ON M.Id = AP.IdAccount
	JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON sdl.Id = ap.IdSuppliersDistributionLines
	JOIN Common.DistributionLines dl WITH (NOLOCK) ON dl.Id = sdl.IdDistributionLine	
	WHERE ap.Value = ap.Balance AND ap.Term > 0 and ap.Balance > 0 

	--ap.Id NOT IN (
	--SELECT fdd.IdAccountPayable 
	--	FROM Payments.FactoringDocument  FD
	--JOIN Payments.FactoringDocumentDetail fdd on FDD.IdFactoringDocument = FD.Id
	--WHERE fd.Status = 2 )  AND ap.Term > 0