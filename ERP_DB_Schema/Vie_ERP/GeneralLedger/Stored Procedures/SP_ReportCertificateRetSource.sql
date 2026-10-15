-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-02-26
-- Description:	Generación reporte retención en la fuente
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetSource]
	-- Add the parameters for the stored procedure here
	@LegalBookId AS INTEGER,
	@Year AS INTEGER,
    @AccountStart AS VARCHAR(50),
	@AccountEnd AS VARCHAR(50),
	@NitStart AS VARCHAR(15),
	@NitEnd AS VARCHAR(15),
	@InitialDate DateTime,
	@FinalDate DateTime
AS
BEGIN
	SELECT 
		tp.Nit ThirdPartyNit, 
		tp.Name as ThirdPartyName,
		tp.Id ThirdPartyId,
		ma.Number MainAccountNumber,
		ma.Name as MainAccountName, 
		Year(@InitialDate) as Validity,
		ma.Id MainAccountId,
		ma.Name + IIF(ma.FreelancerCategory = 1, 
		'', 
		' ' + CAST(CAST(ISNULL(IIF(ma.FreelancerCategory = 1, 0, jvd.RetentionRate), 0) AS FLOAT) AS VARCHAR(20)) + '%') AS RetentionConceptName,
		ma.FreelancerCategory,
		ISNULL(t.AnnualIncome, SUM(jvd.BillingValue)) AnnualIncome,
		SUM(CASE mc.[Nature] 
				WHEN 1 THEN IIF(jvd.CreditValue > 0, BaseValue * - 1, BaseValue) 
				ELSE IIF(jvd.DebitValue > 0, BaseValue * - 1, BaseValue)
			END) as BaseValue,
		SUM(IIF(mc.[Nature] = 1,(jvd.DebitValue - jvd.CreditValue),(jvd.CreditValue - jvd.DebitValue))) as RetentionValue,
		'' as LettersValue
	FROM GeneralLedger.JournalVoucherDetails AS jvd
	INNER JOIN GeneralLedger.JournalVouchers AS jv ON jv.Id = jvd.IdAccounting
	INNER JOIN Common.ThirdParty AS tp ON tp.Id = jvd.IdThirdParty
	INNER JOIN GeneralLedger.MainAccounts AS ma ON ma.Id = jvd.IdMainAccount
	INNER JOIN GeneralLedger.MainAccountClasses AS mc ON mc.Id = ma.IdAccountClass
	LEFT JOIN GeneralLedger.RetentionConcepts AS rc ON rc.Id = jvd.IdRetention
	LEFT JOIN 
	(
		SELECT jvd.IdThirdParty,
			SUM(jvd.DebitValue - jvd.CreditValue) as AnnualIncome
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherDetails jvd ON jv.Id = jvd.IdAccounting
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = jvd.IdMainAccount
		JOIN GeneralLedger.MainAccountClasses mc ON mc.Id = ma.IdAccountClass
		WHERE mc.Type = 2 AND mc.Nature = 1 
			AND jv.LegalBookId = @LegalBookId 
			AND jv.Status = 2 
			AND jv.IsClosedYear = 0
			AND (jv.VoucherDate BETWEEN @InitialDate AND @FinalDate)--YEAR(jv.VoucherDate) = @Year
		GROUP BY jvd.IdThirdParty
	) AS t ON tp.Id = t.IdThirdParty
	WHERE 
		jv.LegalBookId = @LegalBookId 
		AND jv.Status = 2 
		--AND jv.IsClosedYear = 0 
		AND (jvd.RetentionRate IS NOT NULL OR rc.Rate IS NOT NULL)
		AND (ma.RetencionType = 1 OR ma.RetencionType = 4) 
		AND (jv.VoucherDate BETWEEN @InitialDate AND @FinalDate)
		AND  ma.Number >= ISNULL(@AccountStart,'0') AND ma.Number <= ISNULL(@AccountEnd, 'z') 
		AND tp.Nit >= ISNULL(@NitStart,'0') AND tp.Nit <= ISNULL(@NitEnd, 'z')
	GROUP BY tp.Id, tp.Nit, tp.Name, ma.Id, ma.Number, ma.Name, ma.FreelancerCategory, IIF(ma.FreelancerCategory = 1, 0, jvd.RetentionRate), t.AnnualIncome
	HAVING SUM(IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))) > 0
	ORDER BY tp.Nit, ma.FreelancerCategory DESC
END