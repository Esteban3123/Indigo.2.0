-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-05-25
-- Description:	Generación reporte informe de impuestos
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportRetentions]
	-- Add the parameters for the stored procedure here
	@Criterios xml,
	@filtros xml
AS
BEGIN

	set DATEFORMAT YMD

	Declare @DateStart DATE,
			@DateEnd DATE,
			@AccountStart VARCHAR(50),
			@AccountEnd VARCHAR(50),
			@NitStart VARCHAR(15),
			@NitEnd VARCHAR(15),
			@LegalBookId INT

			BEGIN TRY

		--Se obtienen los criterios
		SELECT	@DateStart = t.x.value('DateStart[1]','DATE'),
				@DateEnd = t.x.value('DateEnd[1]','DATE'),
				@LegalBookId = t.x.value('Book[1]','int')
		FROM @Criterios.nodes('/Data') t(x)

		--Se obtienen los filtros
		SELECT	@AccountStart = t.x.value('AccountsStart[1]','VARCHAR(50)'),
				@AccountEnd = t.x.value('AccountsEnd[1]','VARCHAR(50)'),
				@NitStart = t.x.value('ThirdPartyStart[1]','VARCHAR(15)'),
				@NitEnd = t.x.value('ThirdPartyEnd[1]','VARCHAR(15)')
		FROM @filtros.nodes('/Data') t(x)

	SELECT
		jv.Id
	   ,CAST(jv.VoucherDate AS DATE) AS VoucherDate
	   ,JVT.Code + ' ' + JVT.Name AS CodeNameVoucherType
	   ,jv.Consecutive
	   ,CASE JV.EntityName
			WHEN 'AccountPayable' THEN 'Cuenta por Pagar'
			WHEN 'BasicBilling' THEN 'Factura básica'
			WHEN 'CashReceipts' THEN 'Recibo de caja'
			WHEN 'PaymentNotes' THEN 'Nota'
			WHEN 'PayrollLiquidation' THEN 'Liquidación de nomina'
			WHEN 'PortfolioNote' THEN 'Nota'
			WHEN 'TreasuryNote' THEN 'Nota'
			WHEN 'VoucherTransaction' THEN 'Egreso'
			WHEN 'PortfolioTransfer' THEN 'Cruce de anticipo vs CxC'
			WHEN 'PaymentTransfer' THEN 'Cruce de anticipo vs CxP'
			WHEN 'PortfolioReclassification' THEN 'Reclasificación documento de cartera'
			ELSE JVT.NAME
		END + ' ' + ISNULL(jv.EntityCode, '') AS OriginDocument
	   ,ma.Number AS MainAccountNumber
	   ,ma.Name AS MainAccountName
	   ,tp.Nit AS ThirdPartyNit
	   ,tp.Name AS ThirdPartyName
	   ,jvd.DebitValue
	   ,jvd.CreditValue
	   ,jvd.Detail
	   ,ISNULL(rc.Name, '') AS RetentionConceptName
	   ,ISNULL(jvd.RetentionRate, ISNULL(rc.Rate, 0)) AS RetentionRate
	   ,IIF
		(
			ma.FreelancerCategory = 1 AND ISNULL(jvd.BillingValue, 0) > 0,
			jvd.BillingValue,
			ISNULL(CASE ISNULL(jvd.RetentionRate, ISNULL(rc.Rate, 0))
				WHEN 0 THEN CAST(jvd.BaseValue AS BIGINT)
				WHEN 1 THEN CAST(jvd.BaseValue AS BIGINT)
				ELSE CAST(ROUND((ABS(jvd.DebitValue - jvd.CreditValue) * 100 / ISNULL(jvd.RetentionRate, rc.Rate)), 1) AS BIGINT)
			END, 0)
		) * IIF(ma.Nature = 1, IIF(jvd.DebitValue >= jvd.CreditValue, 1, -1), IIF(jvd.CreditValue >= jvd.DebitValue, 1, -1)
		) AS BaseValue
		--,IIF
		--(
		--	ma.FreelancerCategory = 1 AND ISNULL(jvd.BillingValue, 0) > 0,
		--	jvd.BillingValue,
		--	jvd.BaseValue
		--) AS BaseValue
	   ,CASE jv.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Confirmado'
			WHEN 3 THEN 'Anulado'
		END AS Estado
	   , ISNULL(jvd.IdRetention, 0) AS IdRetention
	   ,jv.Status
	   ,tp.Id ThirdPartyId
	   ,ma.Id MainAccountId
	FROM GeneralLedger.JournalVouchers AS jv WITH (NOLOCK)
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.ID = JV.IdJournalVoucher
	JOIN GeneralLedger.JournalVoucherDetails AS jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
	JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = jvd.IdMainAccount
	JOIN GeneralLedger.MainAccountClasses AS mc WITH (NOLOCK) ON mc.Id = ma.IdAccountClass
	LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = jvd.IdThirdParty
	LEFT JOIN GeneralLedger.RetentionConcepts AS rc WITH (NOLOCK) ON rc.Id = jvd.IdRetention
	WHERE 
		jv.LegalBookId = @LegalBookId 
		AND jv.Status = 2 
		AND ma.RetencionType <> 0
		AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd 
		AND ma.Number >= ISNULL(@AccountStart,'0') AND ma.Number <= ISNULL(@AccountEnd, 'z') 
		AND tp.Nit >= ISNULL(@NitStart,'0') AND tp.Nit <= ISNULL(@NitEnd, 'z')
	ORDER BY ma.Number

		END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END