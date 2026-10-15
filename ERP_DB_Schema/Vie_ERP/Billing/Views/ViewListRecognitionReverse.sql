CREATE VIEW [Billing].[ViewListRecognitionReverse]
AS
	SELECT	cg.Id CareGroupId,
			CONCAT(cg.Code, ' - ', cg.Name) as CareGroupCodeName,
			cg.OperativeUnitId,
			rr.Id as RecognitionId,
			rr.VoucherDate,
			CONCAT(jvt.Code, ' - ', jvt.[Name]) as JournalVoucherTypeCodeName,
			jvt.Description,
			SUM(rrd.ThirdPartySalesPrice + rrd.SubTotalPatientSalesPrice + rrd.GrandTotalDiscount) as TotalCareGroup,
			COUNT(DISTINCT rrd.RevenueControlDetailId) as FolioQuantity,
			us.UserCode,
			0 as StateOperation,
			'' as MessageInfo
	FROM Contract.CareGroup cg WITH (NOLOCK)
	JOIN Billing.RevenueRecognition rr WITH (NOLOCK) ON cg.Id = rr.CareGroupId
	JOIN Billing.RevenueRecognitionDetail rrd WITH (NOLOCK) ON rr.Id = rrd.RevenueRecognitionId
	JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON rr.JournalVoucherTypeId = jvt.Id
	JOIN [Security].[User] us ON rr.CreationUser = us.Id
	WHERE rr.State = 1 And cg.LiquidationType In (1, 3)
	GROUP BY cg.Id, cg.Code, cg.Name, cg.OperativeUnitId, 
			rr.Id, rr.VoucherDate, jvt.Code, jvt.[Name], jvt.Description, us.UserCode