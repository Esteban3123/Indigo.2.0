ALTER VIEW [Contract].[ViewCareGroupsWithGroupers]
AS
	SELECT 
		CONCAT('GroupersCareGroup-', gcg.Id) as Row,
		NULL InvoiceEntityCapitatedId,
		gcg.CareGroupId, 
		gcg.GroupersId, 
		g.Code, 
		g.[Description], 
		g.UserNumber, 
		g.UserMin, 
		g.UserMax, 
		gcg.RealCME,
		g.ProjectCME, 
		g.Frequence,
		gcg.EjectEvent,
		gcg.TotalEject, 
		g.TotalContract, 
		g.UserValue,
		gcg.DocumentDate
	FROM [Contract].GroupersCareGroup gcg
	JOIN [Contract].Groupers g ON g.Id = gcg.GroupersId
UNION
	SELECT 
		CONCAT('InvoiceEntityCapitatedGrouper-', iecg.Id) as Row,
		iecg.InvoiceEntityCapitatedId InvoiceEntityCapitatedId,
		gcg.CareGroupId, 
		gcg.GroupersId, 
		iecg.Code, 
		iecg.[Description], 
		g.UserNumber, 
		iecg.UserMin, 
		iecg.UserMax, 
		gcg.RealCME,
		iecg.ProjectCME, 
		g.Frequence,
		gcg.EjectEvent,
		gcg.TotalEject, 
		iecg.TotalContract, 
		g.UserValue,
		gcg.DocumentDate
	FROM [Billing].[InvoiceEntityCapitatedGroupers] iecg
	JOIN Billing.InvoiceEntityCapitated iec ON iecg.InvoiceEntityCapitatedId = iec.Id
	JOIN [Contract].Groupers g ON iecg.GroupersId = g.Id
	JOIN [Contract].GroupersCareGroup gcg ON g.Id = gcg.GroupersId AND iec.CareGroupId = gcg.CareGroupId