



CREATE VIEW [Cost].[ViewCostDistributionManpower]
AS
	SELECT 
		CONCAT(v.ManpowerType, '-', v.EntityId) UUID, 
		*
	FROM
	(
		SELECT 
			YEAR(l.PayrollDateLiquidated) Year, MONTH(l.PayrollDateLiquidated) Month,
			1 ManpowerType, l.Id EntityId,
			CONCAT(g.Code, ' - ', g.Name) GroupCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			CONCAT(p.Code, ' - ', p.Name) PositionCodeName
		FROM Payroll.Liquidation l
		JOIN Payroll.[Group] g ON l.GroupId = g.Id
		JOIN Payroll.Employee e ON l.EmployeeId = e.Id
		JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id	
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Position p ON c.PositionId = p.Id
		WHERE l.RegisterStatus = 'C'

		UNION ALL

		SELECT
			YEAR(ap.DocumentDate) Year, MONTH(ap.DocumentDate) Month,
			2 ManpowerType, ap.Id EntityId,
			NULL GroupCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			CONCAT(p.Code, ' - ', p.Name) PositionCodeName
		FROM Payments.AccountPayable ap
		JOIN Common.ThirdParty tp ON ap.IdThirdParty = tp.Id
		JOIN Payroll.Position p ON ap.PositionId = p.Id
		JOIN
		(
			SELECT
				apdc.IdAccountPayable
			FROM Cost.CostProductionCenter cpc
			JOIN Cost.CostProductionCenterCostCenter cpccc ON cpc.Id = cpccc.ProductionCenterId
			JOIN Cost.CostProductionCenterHomologation cpch ON cpc.Id = cpch.ProductionCenterId
			JOIN Payments.AccountPayableDetailConcept apdc ON cpccc.CostCenterId = apdc.IdCostCenter AND cpch.AccountOriginId = apdc.IdAccount
			GROUP BY apdc.IdAccountPayable
		) apdc ON ap.Id = apdc.IdAccountPayable
		WHERE ap.Status = 2 
	) v