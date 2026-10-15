CREATE VIEW [Billing].[ViewElectronicSupportDocumentHeader]
AS

WITH Cte_PickAddress AS (
		SELECT
			ad.IdPerson,
			MIN(ad.Id) AS AddressId
		FROM Common.[Address] ad WITH (NOLOCK)
		WHERE ad.[State] = 1
		GROUP BY ad.IdPerson
	),
	Cte_AdressSupplier AS (
		SELECT 
			a.IdPerson, 
			a.Addresss AS AddressSupplier,
			LEFT(RTRIM(ISNULL(d.Code, '')), 1) DepartmentCode,
			RIGHT('00' + LEFT(LTRIM(RTRIM(ci.Code)), 2), 2) AS CityCode
		FROM Cte_PickAddress pa
		JOIN Common.Address a WITH(NOLOCK) ON pa.AddressId = a.Id
		JOIN Common.City ci ON a.CityId = ci.Id
		JOIN Common.Department d WITH (NOLOCK) ON a.DepartmentId = d.Id
		GROUP BY a.IdPerson, a.Addresss, d.Code, ci.Code
	),
	Cte_MainDataSupplier AS (
		SELECT 
				p.Id AS PersonId,
				tp.Id AS ThirdPartyId,
				tp.Nit AS ThirdPartyIdentificationNumber,
				tp.[Name] AS ThirdPartyName,
				CASE it.SIGLA
					WHEN 'CF' THEN '01'
					WHEN 'CJ' THEN '02'
					WHEN 'DM' THEN '03'
					WHEN 'NI' THEN '04'
					WHEN 'PA' THEN '03'
					ELSE '00'
				END AS IdentificationType,
				ISNULL(tp.Class, 0) AS ThirdPartyClass,
				CASE
					WHEN tp.Class <> 2 AND it.SIGLA <> 'PA'
						THEN 1
					ELSE 0
				END AS IsForeign,
				ISNULL(ea.Code, '')	AS EconomicActivityCode
		FROM Common.ThirdParty tp WITH (NOLOCK)
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		JOIN ADTIPOIDENTIFICA it WITH (NOLOCK) ON p.IdentificationType = it.Id
		LEFT JOIN Common.EconomicActivity ea WITH (NOLOCK) ON tp.EconomicActivityId = ea.Id 
	),
	Cte_EconomyActivitySupplier AS (
		SELECT 
			vt.Id AS VoucherTransactionId,
			MIN(ea.Code) AS EconomicActivityTransactionCode
		FROM  Treasury.VoucherTransaction vt
		JOIN Treasury.VoucherTransactionDetails vtd	ON vt.Id = vtd.IdVoucherTransaction
		LEFT JOIN Common.EconomicActivity ea ON vtd.EconomicActivityId = ea.Id
		GROUP BY vt.Id
	),
	Cte_CurrencyOfficial AS(
		SELECT co.Abbreviation
		FROM GeneralLedger.CompanySettings cs
		JOIN Common.Currency co WITH (NOLOCK) ON co.Id = cs.OfficialCurrencyId
	)
	
	--CUENTAS POR PAGAR
	SELECT
		  -- Datos generales del documento electrónico
		  esd.Id AS Id,
		  ap.Id AS EntityId,
		  ap.Code AS EntityCode,
		  'AccountPayable' AS EntityName,
		  ----------------------------------
		  ISNULL(ea.Code, '') AS EconomyActivityCode,
		  '' AS TaxRegistrationDrinks,
		  '08' AS DocumentType,	 
		  ap.Coments AS Comment,
		  '' AS AnotherContent,
		  '' AS AnotherComment,
		  IIF(esd.StatusElectronic IN (2,3), esd.DocumentNumber, '') AS DocumentNumber,
		  IIF(esd.StatusElectronic IN (2,3), esd.CUDS, '') AS DocumentKey,
		  0 AS BatchId,
		  -- Datos del receptor
		  s.ThirdPartyName AS ReceiverName,
		  s.IdentificationType AS ReceiverIdentificationType,
		  IIF(s.IsForeign = 0, s.ThirdPartyIdentificationNumber, '') AS ReceiverIdentificationNumber,
		  IIF(s.IsForeign = 1, s.ThirdPartyIdentificationNumber, '') AS ReceiverForeignIdentification,
		  s.ThirdPartyName AS ReceiverTradeName,
		  ISNULL(s.EconomicActivityCode, '') AS ReceiverEconomicActivity,
		  IIF(s.IsForeign = 0, ISNULL(ads.DepartmentCode, ''), '') AS Province,
		  IIF(s.IsForeign = 0, ISNULL(ads.CityCode, ''), '') AS Township,
		  IIF(s.IsForeign = 0, ISNULL(ads.CityCode, ''), '') AS District,
		  IIF(s.IsForeign = 0, 'Barrio', '') AS Neighborhood,
		  IIF(s.IsForeign = 0, ISNULL(ads.AddressSupplier, ''), '') AS ReceiverOtherAddresses,
		  IIF(s.IsForeign = 1, ISNULL(ads.AddressSupplier, ''), '') AS ReceiverForeignOtherAddresses,
		  0 AS ReceiverPhoneCountryCode, 
		  0 AS ReceiverPhoneNumber,	
		  '' AS ReceiverEmailAddress,
		  '' AS EmailCopyAddress,
		  '01' AS SaleCondition,
		  '' AS OtherSaleCondition,
		  CAST(ap.Term AS VARCHAR(20)) AS CreditDeadline,
		  -- Medios de pago
		  '04' AS PaymentMethods, --	Transferencia O 99 oTROS
		  ap.Value AS TotalPaymentMethods,
		  -- Moneda
		  ISNULL(c.Abbreviation, cu.Abbreviation) AS CurrencyAbreviation,
		  IIF(ap.CurrencyId = 2,  trm.ValueOfficialToCurrency,CAST(1 AS DECIMAL(18,2))) AS TRMValue,
		  -- Totales
		  CAST(0 AS DECIMAL(18,2)) AS TotalSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalDiscounts,
		  CAST(0 AS DECIMAL(18,2)) AS TotalNetSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalTax,
		  CAST(0 AS DECIMAL(18,2)) AS TotalReceipt --Total de comprobante
    FROM Billing.ElectronicSupportDocument esd WITH(NOLOCK)
    INNER JOIN Payments.AccountPayable ap WITH(NOLOCK) ON ap.Id = esd.EntityId and esd.EntityName = 'AccountPayable'	
	INNER JOIN Cte_MainDataSupplier s ON esd.SupplierThirdPartyId = s.ThirdPartyId
	INNER JOIN Cte_AdressSupplier ads ON s.PersonId = ads.IdPerson
	JOIN Cte_CurrencyOfficial cu ON 1 = 1
	LEFT JOIN Common.EconomicActivity ea WITH(NOLOCK) ON ap.IdEconomicActivity = ea.Id
	LEFT JOIN Common.Currency c WITH(NOLOCK) ON ap.CurrencyId = c.Id
	LEFT JOIN Common.TRM trm on trm.MeasurementDate = convert(date,esd.DocumentDate)
    WHERE ap.HandlesDocumentSupport = 1	

	UNION ALL

	--COMPROBANTE DE EGRESO
	SELECT
		  -- Datos generales del documento electrónico
		  esd.Id AS Id,
		  vt.Id AS EntityId,
		  vt.Code AS EntityCode,
		  'VoucherTransaction' AS EntityName,
		  ----------------------------------
		  --ISNULL(ea.Code, '') AS EconomyActivityCode,
		  ISNULL(eas.EconomicActivityTransactionCode, '') EconomyActivityCode,
		  '' AS TaxRegistrationDrinks,
		  '08' AS DocumentType,	 
		  vt.Detail AS Comment,
		  '' AS AnotherContent,
		  '' AS AnotherComment,
		  IIF(esd.StatusElectronic IN (2,3), esd.DocumentNumber, '') AS DocumentNumber,
		  IIF(esd.StatusElectronic IN (2,3), esd.CUDS, '') AS DocumentKey,
		  0 AS BatchId,
		  -- Datos del receptor
		  s.ThirdPartyName AS ReceiverName,
		  s.IdentificationType AS ReceiverIdentificationType,
		  IIF(s.IsForeign = 0, s.ThirdPartyIdentificationNumber, '') AS ReceiverIdentificationNumber,
		  IIF(s.IsForeign = 1, s.ThirdPartyIdentificationNumber, '') AS ReceiverForeignIdentification,
		  s.ThirdPartyName AS ReceiverTradeName,
		  ISNULL(s.EconomicActivityCode, '') AS ReceiverEconomicActivity,
		  IIF(s.IsForeign = 0, ads.DepartmentCode, '') AS Province,
		  IIF(s.IsForeign = 0, ads.CityCode, '') AS Township,
		  IIF(s.IsForeign = 0, ads.CityCode, '') AS District,
		  IIF(s.IsForeign = 0, 'Barrio', '') AS Neighborhood,
		  IIF(s.IsForeign = 0, ads.AddressSupplier, '') AS ReceiverOtherAddresses,
		  IIF(s.IsForeign = 1, ads.AddressSupplier, '')  AS ReceiverForeignOtherAddresses,
		  0 AS ReceiverPhoneCountryCode, 
		  0 AS ReceiverPhoneNumber,	
		  '' AS ReceiverEmailAddress,
		  '' AS EmailCopyAddress,
		  '01' AS SaleCondition,
		  '' AS OtherSaleCondition,
		  '0' AS CreditDeadline,
		  -- Medios de pago
		  CASE 
			WHEN vt.IdCashRegister IS NOT NULL AND vt.ExpenseType IN (2, 3) 
				THEN '01'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 1 
				THEN '03' -- Cheque = 1, Nota débito = 2, PSE = 3'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 2 
				THEN '04'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 3
				THEN '03'
			ELSE '99'
		  END AS PaymentMethods,
		  vt.[Value] AS TotalPaymentMethods,
		  -- Moneda
		  ISNULL(c.Abbreviation, cu.Abbreviation) CurrencyAbreviation,
		  CAST(1 AS DECIMAL(18,2)) AS TRMValue,
		  -- Totales
		  CAST(0 AS DECIMAL(18,2)) AS TotalSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalDiscounts,
		  CAST(0 AS DECIMAL(18,2)) AS TotalNetSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalTax,
		  CAST(0 AS DECIMAL(18,2)) AS TotalReceipt --Total de comprobante
    from Billing.ElectronicSupportDocument esd
	INNER JOIN Treasury.VoucherTransaction vt WITH(NOLOCK) on vt.Id = esd.EntityId and esd.EntityName ='VoucherTransaction'
	INNER JOIN Cte_MainDataSupplier s ON esd.SupplierThirdPartyId = s.ThirdPartyId
	INNER JOIN Cte_AdressSupplier ads ON s.PersonId = ads.IdPerson
	JOIN Cte_CurrencyOfficial cu ON 1 = 1
	LEFT JOIN Cte_EconomyActivitySupplier eas ON eas.VoucherTransactionId = vt.Id
	LEFT JOIN Common.Currency c WITH(NOLOCK) ON vt.CurrencyId = c.Id
	where vt.HandlesDocumentSupport = 1 AND vt.VoucherClass = 1 -- 1 - Pago; 2 - Reembolso; 3 - Traslado	
	
GO