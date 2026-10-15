

CREATE VIEW [Billing].[ViewTaxDevolution]
AS
WITH cte_temp AS(SELECT gli.Id,dbo.Data PaymentMethodTypes
				from GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
				CROSS APPLY dbo.Split(gli.PaymentMethodTypes,',') dbo
				where gli.PaymentMethodTypes is not null
				group by gli.Id,dbo.Data,dbo.Id),

	cte_Sodd AS(SELECT sodd.RevenueControlDetailId, SUM(sodd.GrandTotalSalesPrice) GrandTotalSalesPrice
				FROM Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
				GROUP BY sodd.RevenueControlDetailId)


SELECT	CAST(CONCAT(sodd.RevenueControlDetailId,gli2.PaymentMethodTypes) AS INT) Id,
		sodd.RevenueControlDetailId,
		sum(sodd.taxes) TaxValue,
		cs.GrandTotalSalesPrice - sum(sodd.taxes)  ValueWithTaxDevolutionOfficialCurrency,
		cs.GrandTotalSalesPrice - sum(sodd.taxes)ValueWithTaxDevolution,
		CAST(gli2.PaymentMethodTypes AS TINYINT) PaymentMethodType,
		CASE gli2.PaymentMethodTypes
		WHEN '1' THEN 'Efectivo'
		WHEN '2' THEN 'Cheque'
		WHEN '3' THEN 'Tarjeta'
		WHEN '4' THEN 'Consignación'
		ELSE 'Otro' END PaymentMethodTypeName
FROM GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
JOIN (
		SELECT	rcd.Id RevenueControlDetailId,
				sod.IvaId,
				sum(sodd.GrandTotalTaxes) taxes
		from Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id=sodd.ServiceOrderDetailId
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
		where sod.IvaId is not null 
		GROUP BY rcd.Id,sod.IvaId) sodd ON sodd.IvaId= gli.Id
JOIN cte_temp gli2 ON gli.Id=gli2.Id
JOIN cte_Sodd cs on sodd.RevenueControlDetailId = cs.RevenueControlDetailId
WHERE gli.ApplyTaxDevolution=1
GROUP BY sodd.RevenueControlDetailId, gli2.PaymentMethodTypes,cs.GrandTotalSalesPrice

UNION ALL

SELECT CAST(CONCAT(cs.RevenueControlDetailId,0) AS INT) Id,
		cs.RevenueControlDetailId,
		0 TaxValue,
		cs.GrandTotalSalesPrice ValueWithTaxDevolutionOfficialCurrency,
		cs.GrandTotalSalesPrice ValueWithTaxDevolution,
		CAST(0 AS TINYINT) PaymentMethodType,
		'Otros'  PaymentMethodTypeName
from cte_Sodd cs