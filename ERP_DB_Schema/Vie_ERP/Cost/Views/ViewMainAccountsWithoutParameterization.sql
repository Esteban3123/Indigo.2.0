
CREATE VIEW [Cost].[ViewMainAccountsWithoutParameterization]
AS
SELECT DISTINCT
	CONCAT(glb.Year, '-', glb.Month, '-', ma.Id, '-', cc.Id, '-', tp.Id) Id,
	glb.Year, glb.Month,
	pma.Number ParentAccountNumber,
	pma.Name ParentAccountName,
    ma.Number MainAccountNumber, 
	ma.Name MainAccountName,
    cc.Code CostCenterCode, 
	cc.Name CostCenterName,
	tp.Nit ThirdPartyNit,
	tp.Name ThirdPartyName,
	(glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1) Balance
FROM GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK)
JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON ma.LegalBookId = lb.Id AND lb.OfficialBook = 1
JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
JOIN Payroll.CostCenter cc WITH (NOLOCK) ON glb.IdCostCenter = cc.Id
LEFT JOIN GeneralLedger.MainAccounts pma WITH (NOLOCK) ON lb.Id = pma.LegalBookId AND SUBSTRING(ma.Number, 1, 4) = pma.Number
LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id
LEFT JOIN
(
    SELECT cpch.AccountOriginId, cpccc.CostCenterId
    FROM Cost.CostProductionCenterHomologation cpch WITH (NOLOCK)
    JOIN Cost.CostProductionCenterCostCenter cpccc WITH (NOLOCK) ON cpch.ProductionCenterId = cpccc.ProductionCenterId
    GROUP BY cpch.AccountOriginId, cpccc.CostCenterId
) pc ON glb.IdMainAccount = pc.AccountOriginId AND ISNULL(glb.IdCostCenter, 0) = pc.CostCenterId
WHERE mac.Type = 2 AND (glb.DebitValue - glb.CreditValue) <> 0
    AND pc.AccountOriginId IS NULL