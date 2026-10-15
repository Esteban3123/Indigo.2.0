
-- ===============================================================================================================
-- Author:		Juan Pablo Daza Medina
-- Create date: 2023-08-25
-- Description:	Trae las rentas exentas del empleado
-- ==============================================================================================================
CREATE VIEW [Common].[ViewEmployeeExemptIncome]
AS

		SELECT DISTINCT
			CONCAT(l.id, '-', jvt.Code) AS Id,
			tp.id AS ThirdPartyId,
			l.RegisterStatus,
			jvt.[name] AS VoucherType,
			jvt.Code AS VoucherCode,
			l.PayrollDateLiquidated AS DateLiquidation,
			l.TotalPaid AS MonthlyIncome,
			l.ExemptValueRetention AS ExemptIncomeValue,
			YEAR(l.PayrollDateLiquidated) AS YearLiquidated,
			0 AS isNew,
			'' AS Comments
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherDetails jvd WITH(NOLOCK)
			ON jvd.IdAccounting = jv.Id AND jvd.IdThirdParty IS NOT NULL
		JOIN Common.ThirdParty tp ON jvd.IdThirdParty = tp.id
		JOIN Payroll.Employee e ON tp.id = e.ThirdPartyId
		JOIN Payroll.Liquidation l ON e.id = l.EmployeeId
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.id
		WHERE jv.LegalBookId = 1 

		UNION ALL

		SELECT 
			CAST(ei.Id as varchar),
			ei.ThirdPartyId,
			'C' RegisterStatus,
			ei.VoucherType VoucherType,
			ei.VoucherCode VoucherCode,
			ei.DateLiquidation DateLiquidation,
			ei.MonthlyIncome MonthlyIncome,
			ei.ExemptIncomeValue ExemptIncomeValue,
			YEAR(ei.DateLiquidation) YearLiquidated,
			0 isNew,
			ei.Comments Comments
		FROM [Common].[ExemptIncome] ei