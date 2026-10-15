CREATE PROCEDURE [GeneralLedger].[SP_ReportCertificateRetICA]
	-- Add the parameters for the stored procedure here
    @DateStart as date,
	@DateEnd as date,
	@AccountStart as varchar(50),
	@AccountEnd as varchar(50),
	@NitStart as varchar(15),
	@NitEnd as varchar(15),
	@RetencionType as integer,
	@LegalBookId as integer
AS
BEGIN
	SELECT

		gr.Rate
	   ,ct.Nit
	   ,ct.Name Tercero
	   ,ma.Number
	   ,ma.Name Concept
	   ,SUM
		(
		CASE mc.[Nature]
			WHEN 1 THEN IIF(jvd.CreditValue > 0, jvd.BillingValue * -1, jvd.BillingValue)
			ELSE IIF(jvd.DebitValue > 0, jvd.BillingValue * -1, jvd.BillingValue)
		END
		) ValorFacturado
	   ,SUM
		(
		CASE mc.[Nature]
			WHEN 1 THEN IIF(jvd.CreditValue > 0, BaseValue * -1, BaseValue)
			ELSE IIF(jvd.DebitValue > 0, BaseValue * -1, BaseValue)
		END
		) BaseRetención
	   ,SUM
		(
		IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))
		) ValorRetenido
	   ,'' LettersValue

	FROM GeneralLedger.JournalVoucherDetails jvd
	JOIN GeneralLedger.JournalVouchers jv ON jv.Id = jvd.IdAccounting
	LEFT JOIN GeneralLedger.RetentionConcepts AS gr ON gr.Id = jvd.IdRetention
	JOIN Common.ThirdParty AS ct ON ct.Id = jvd.IdThirdParty
	JOIN GeneralLedger.MainAccounts AS ma ON ma.Id = jvd.IdMainAccount
	JOIN GeneralLedger.MainAccountClasses AS mc ON mc.Id = ma.IdAccountClass
	WHERE CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND ma.Number >= ISNULL(@AccountStart, '0')
			AND ma.Number <= ISNULL(@AccountEnd, 'z')
			AND ct.Nit >= ISNULL(@NitStart, '0')
			AND ct.Nit <= ISNULL(@NitEnd, 'z')
			AND ma.RetencionType = @RetencionType
			AND jv.LegalBookId = @LegalBookId
			AND (jvd.RetentionRate IS NOT NULL OR gr.Rate IS NOT NULL)
			AND jv.IsClosedYear = 0
			AND jv.Status = 2
	GROUP BY gr.Rate
			,ct.Nit
			,ct.Name
			,ma.Number
			,ma.Name
	HAVING SUM(IIF(mc.[Nature] = 1, (jvd.DebitValue - jvd.CreditValue), (jvd.CreditValue - jvd.DebitValue))) > 0
	ORDER BY ct.Nit, ma.Number
END