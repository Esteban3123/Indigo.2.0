CREATE VIEW [Budget].[ViewReportPaymentPlan]
AS
SELECT
 ROW_NUMBER() OVER (ORDER BY bc.Code) AS AutoIncrementing,
 bc.Code,
 bc.Status,
 bc.Name AS 'spendingCategoryName',
 ba.Code AS 'availabilityNumber',
 bcm.Code AS 'numberCommitment',
 bo.Code AS 'obligationNumber',
 bpo.Document AS 'paymentNumber',
 thi.Nit,
 thi.Name,
 bo.Observations,
 bpo.DocumentDate AS 'PaymentDate',
 bad.InitialValue,
 bcmd.InitialValue AS 'commitmentValue',
 bod.InitialValue AS 'obligationValue', 
 bpod.TotalPaymentOrder,
 bod.Balance,
 bbv.Id AS 'Validity',
 bc.Code AS 'Category',
 brt.[Type]
FROM
Budget.AvailabilityDetail AS bad
INNER JOIN Budget.Availability AS ba ON bad.AvailabilityId = ba.Id
INNER JOIN Budget.BudgetaryValidity AS bbv ON ba.BudgetaryValidityId = bbv.Id
INNER JOIN Budget.CommitmentDetail AS bcmd ON bcmd.AvailabilityDetailId = bad.Id
INNER JOIN Budget.RevenueType AS brt ON bcmd.RevenueTypeId = brt.Id
INNER JOIN Budget.Commitment AS bcm ON bcmd.CommitmentId = bcm.Id
INNER JOIN Budget.ObligationDetail AS bod ON bod.CategoryId = bcmd.CategoryId--
--INNER JOIN Budget.ObligationDetail AS bod ON bod.CommitmentDetailId = bcmd.Id
INNER JOIN Budget.Budget AS bb ON bad.BudgetId = bb.Id
INNER JOIN Budget.PaymentOrderDetail AS bpod ON bpod.ObligationDetailId = bod.Id
INNER JOIN Budget.PaymentOrder AS bpo ON  bpod.PaymentOrderId = bpo.Id
INNER JOIN Budget.Obligation AS bo ON bod.ObligationId = bo.Id
INNER JOIN Common.ThirdParty AS thi ON bo.ThirdPartyId = thi.Id--THI
INNER JOIN Budget.Category AS bc ON bb.CategoryId = bc.Id
--INNER JOIN Budget.BudgetaryEntity AS bbe ON bbe.ThirdPartyId = thi.Id 