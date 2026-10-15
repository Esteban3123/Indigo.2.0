



CREATE VIEW [Billing].[ViewListRecognitionEntrance]
AS
	SELECT	cg.Id CareGroupId,
			CONCAT(cg.Code, ' - ', cg.Name) as CareGroupCodeName,
			cg.OperativeUnitId,
			SUM(sodd.ThirdPartySalesPrice + sodd.SubTotalPatientSalesPrice + sodd.GrandTotalDiscount - sodd.GrandTotalTaxes) TotalCareGroup,
			COUNT(DISTINCT rcd.Id) FolioQuantity
	FROM Contract.CareGroup cg WITH (NOLOCK)
	JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON cg.Id = rcd.CareGroupId
	JOIN Billing.RevenueControl rc WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
	JOIN ..ADINGRESO ing WITH (NOLOCK) ON ing.NUMINGRES = rc.AdmissionNumber
	WHERE rcd.[Status] IN( 1,3) 
		and rcd.IsMasterAccount <> 3
		AND ing.IESTADOIN IN (' ', 'P','B')
		AND cg.LiquidationType In (1, 3)
		AND sod.IsDelete = 0 
		AND sod.SettlementType != 3 
		AND sod.GrandTotalSalesPrice > 0
	GROUP BY cg.Id, cg.Code, cg.Name, cg.OperativeUnitId