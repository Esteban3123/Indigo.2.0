-- =============================================
-- Author:		Miguel Fonseca
-- Create date: 2020-02-24
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT004
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT004]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE @Year INT,
			@Month INT,			
			@LegalBookId INT,
			@BusinessLine TINYINT
	---------------------------------------------------------------------------

	DECLARE @Table_Result AS TABLE
	(
		ThirdPartyId INT,
		--MainAccountId INT,
		TipoIdAcreedor VARCHAR(2),
		IdAcreedor VARCHAR(20),
		DvAcreedor VARCHAR(1),
		NombreAcreedor VARCHAR(300),
		ActividadAcreedor VARCHAR(10),
		ConceptoAcreencia TINYINT,
		medicionPosterior TINYINT,
		CxPNoVencidas DECIMAL(20, 4),
		CxPMora30dias DECIMAL(20, 4),
		CxPMora60dias DECIMAL(20, 4),
		CxPMora90dias DECIMAL(20, 4),
		CxPMora180dias DECIMAL(20, 4),
		CxPMora360dias DECIMAL(20, 4),
		CxPMoraMayor360dias DECIMAL(20, 4),
		Ajuste INT,
		Saldo DECIMAL(20, 4),
		CxPRecursos VARCHAR (50),
		MetodoPago VARCHAR(50)
	)
	
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		DECLARE @ClosingDate DATE = '01/' + RIGHT('0' + CAST(@Month AS VARCHAR(20)), 2)+ '/' + CAST(@Year AS VARCHAR(20)),
				@AfterClosingDate DATE

		SET @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate))
		SET @AfterClosingDate = DATEADD(DAY, 1, @ClosingDate)
		SET @BusinessLine = (SELECT cs.BusinessLine FROM GeneralLedger.CompanySettings cs)
	

		INSERT INTO @Table_Result
			SELECT	IdThirdParty, 
					--IdAccount,
					TipoIdAcreedor, 
					IdAcreedor, 
					DvAcreedor, 
					NombreAcreedor, 
					ActividadAcreedor, 
					MAX(ConceptoAcreencia) ConceptoAcreencia, 
					MAX(MedicionPosterior) MedicionPosterior,
					SUM(CxPNoVencidas) AS CxPNoVencidas, 
					SUM(CxPMora30dias) AS CxPMora30dias, 
					SUM(CxPMora60dias) AS CxPMora60dias, 
					SUM(CxPMora90dias) AS CxPMora90dias, 
					SUM(CxPMora180dias) AS CxPMora180dias, 
					SUM(CxPMora360dias) AS CxPMora360dias, 
					SUM(CxPMoraMayor360dias) AS CxPMoraMayor360dias, 
					0 AS Ajuste, 
					SUM(CxPNoVencidas) + SUM(CxPMora30dias) + SUM(CxPMora60dias) + SUM(CxPMora90dias) + SUM(CxPMora180dias) + SUM(CxPMora360dias) + SUM(CxPMoraMayor360dias) AS Saldo,
					CxPRecursos,
					MetodoPago
			FROM 
			(
				SELECT	ap.IdThirdParty,
						ap.IdAccount,
						CASE cp.IdentificationType 
							WHEN 0 THEN 'CC' 
							WHEN 1 THEN 'CE' 
							WHEN 7 THEN 'NI' 
							ELSE 'OT' 
						END AS TipoIdAcreedor,
						ct.Nit AS IdAcreedor,
						IIF(ct.DigitVerification ='',0,ISNULL(ct.DigitVerification,0)) AS DvAcreedor,
						ct.[Name] AS NombreAcreedor,
						IIF(CT.CodeCIIU ='','NA',ISNULL(ct.CodeCIIU,'NA')) AS ActividadAcreedor,
						npdl.CreditConcept AS ConceptoAcreencia,
						npdl.PostMeasurement AS medicionPosterior,
						IIF(ap.Age <= 0, ap.Balance, 0) AS CxPNoVencidas,
						IIF(ap.Age > 0 AND ap.Age <= 30, ap.Balance, 0) as CxPMora30dias,
						IIF(ap.Age > 30 AND ap.Age <= 60, ap.Balance, 0) as CxPMora60dias,
						IIF(ap.Age > 60 AND ap.Age <= 90, ap.Balance, 0) as CxPMora90dias,
						IIF(ap.Age > 90 AND ap.Age <= 180, ap.Balance, 0) as CxPMora180dias,
						IIF(ap.Age > 180 AND ap.Age <= 360, ap.Balance, 0) as CxPMora360dias,
						IIF(ap.Age > 360, ap.Balance, 0) as CxPMoraMayor360dias,
						npdl.ResourcesAccountPayable AS CxPRecursos,
						npdl.PaymentMethod AS MetodoPago
				FROM 
				(
					SELECT 
						ap.Id,
						ap.IdThirdParty,
						ap.IdAccount,
						ap.IdSuppliersDistributionLines,
						DATEDIFF(DAY, ap.DocumentDate, @ClosingDate) AS Age,
						(
							ap.Value --Valor Inicial
							- (ap.Value - ISNULL(ibap.Balance, ap.Value)) --Valor saldo inicial
							- ISNULL(pn.DebitValue, 0) + ISNULL(pn.CreditValue, 0) --Valor Notas
							- ISNULL(pt.TransferValue, 0) --Valor Cruce de Anticipo
							- ISNULL(vt.VoucherTransactionValue, 0) --Valor comprobante de egreso
							- ISNULL(cr.CashReceiptValue, 0) --Valor recibos de caja
							- ISNULL(ca.CrossingValue, 0) --Valor cruzado con una cxp
						) AS Balance
					FROM Payments.AccountPayable ap WITH (NOLOCK)
					LEFT JOIN Payments.InitialBalanceAccountPayable ibap WITH (NOLOCK) ON ap.Id = ibap.AccountPayableId
					LEFT JOIN
					(
						SELECT
							pnapa.AccountPayableId,
							SUM(IIF(pn.Nature = 1, pnapa.AdjusmentValue, 0)) DebitValue,
							SUM(IIF(pn.Nature = 1, 0, pnapa.AdjusmentValue)) CreditValue
						FROM Payments.PaymentNotes pn WITH (NOLOCK)
						JOIN Payments.PaymentNotesAccountPayableAdvance pnapa WITH (NOLOCK) ON pn.Id = pnapa.PaymentNoteId
						WHERE pn.Status = 2 AND CAST(pn.NoteDate AS DATE) <= @ClosingDate
						GROUP BY pnapa.AccountPayableId
					) pn ON ap.Id = pn.AccountPayableId
					LEFT JOIN
					(
						SELECT
							ptd.AccountPayableId,
							SUM(ptd.Value) TransferValue
						FROM Payments.PaymentTransfer pt WITH (NOLOCK)
						JOIN Payments.PaymentTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PaymentTransferId
						WHERE pt.Status = 2 AND CAST(pt.DocumentDate AS DATE) <= @ClosingDate
						GROUP BY ptd.AccountPayableId
					) pt ON ap.Id = pt.AccountPayableId
					LEFT JOIN
					(
						SELECT 
							db.IdAccountPayable,
							SUM(db.AdvancedValue) VoucherTransactionValue
						FROM Treasury.VoucherTransaction vt WITH (NOLOCK)
						JOIN Treasury.VoucherTransactionDetails vtd WITH (NOLOCK) ON vt.Id = vtd.IdVoucherTransaction
						JOIN Treasury.DischargeBill db WITH (NOLOCK) ON vtd.Id = db.IdVoucherTransactionD
						WHERE vt.Status IN (2 , 4)
							AND CAST(vt.DocumentDate AS DATE) <= @ClosingDate
							AND CAST(ISNULL(vt.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
						GROUP BY db.IdAccountPayable
					) vt ON ap.Id = vt.IdAccountPayable
					LEFT JOIN
					(
						SELECT 
							crdap.AccountPayableId,
							SUM(crdap.RefundValue) CashReceiptValue
						FROM Treasury.CashReceipts cr WITH (NOLOCK)
						JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
						JOIN Treasury.CashReceiptDetailAccountPayable crdap WITH (NOLOCK) ON crd.Id = crdap.CashReceiptDetailId
						WHERE cr.Status IN (2 , 4)
							AND CAST(cr.DocumentDate AS DATE) <= @ClosingDate
							AND CAST(ISNULL(cr.ReversedDate, @AfterClosingDate) AS DATE) > @ClosingDate
						GROUP BY crdap.AccountPayableId
					) cr ON ap.Id = cr.AccountPayableId
					LEFT JOIN
					(
						SELECT 
							cad.AccountPayableId,
							SUM(cad.CrossingValue) CrossingValue
						FROM Treasury.CrossingAccount ca WITH (NOLOCK)
						JOIN Treasury.CrossingAccountDetailCxP cad WITH (NOLOCK) ON ca.Id = cad.CrossingAccountId
						WHERE ca.Status = 2 AND CAST(ca.DocumentDate AS DATE) <= @ClosingDate
						GROUP BY cad.AccountPayableId
					) ca ON ap.Id = ca.AccountPayableId
					WHERE ap.Status = 2 AND CAST(ap.ServicePeriodDate AS DATE) <= @ClosingDate
				) AS ap
				INNER JOIN Common.ThirdParty AS ct ON ct.Id = ap.IdThirdParty
				INNER JOIN Common.Person AS cp ON cp.id = ct.PersonId
				LEFT JOIN Common.SuppliersDistributionLines AS sdl ON sdl.id = ap.IdSuppliersDistributionLines
				LEFT JOIN Common.DistributionLines AS dl ON dl.id = sdl.IdDistributionLine
				LEFT JOIN Common.NormativeParametersByDistributionLines AS npdl ON dl.Id = npdl.DistributionLinesId
				WHERE ap.Balance <> 0
			) AS Info
			GROUP BY Info.IdThirdParty, /*Info.IdAccount,*/ Info.TipoIdAcreedor, Info.IdAcreedor, Info.DvAcreedor, Info.NombreAcreedor, Info.ActividadAcreedor, Info.CxPRecursos, Info.MetodoPago --, Info.ConceptoAcreencia, Info.medicionPosterior

		---------------------------------------------------------------------------------------------------------------
		INSERT INTO @Table_Result
			SELECT	jv.ThirdPartyId,
					--jv.IdMainAccount,
					jv.TipoIdAcreedor, 
					jv.IdAcreedor, 
					jv.DvAcreedor, 
					jv.NombreAcreedor, 
					jv.ActividadAcreedor, 
					ISNULL(tr.ConceptoAcreencia, jv.ConceptoAcreencia) ConceptoAcreencia, 
					ISNULL(tr.medicionPosterior, jv.medicionPosterior) medicionPosterior,
					jv.CxPNoVencidas - ISNULL(tr.Saldo, 0) AS CxPNoVencidas, 
					jv.CxPMora30dias, 
					jv.CxPMora60dias, 
					jv.CxPMora90dias, 
					jv.CxPMora180dias, 
					jv.CxPMora360dias, 
					jv.CxPMoraMayor360dias, 
					jv.ajuste, 
					jv.saldo - ISNULL(tr.Saldo, 0) AS saldo, 
					ISNULL(tr.CxPRecursos, jv.CxPRecursos) CxPRecursos,
					ISNULL(tr.MetodoPago, jv.MetodoPago) MetodoPago
			FROM
			(
				SELECT	IIF(ma.RetencionType = 4, 0, tp.Id) ThirdPartyId,
						--jvd.IdMainAccount,
						CASE IIF(hspf.creditorIdBy = 1,IIF(ma.RetencionType = 4, 10, p.IdentificationType),8)
							WHEN 0 THEN 'CC' 
							WHEN 1 THEN 'CE' 
							WHEN 7 THEN 'NI' 
							ELSE 'OT' 
						END AS TipoIdAcreedor, 
						IIF(hspf.creditorIdBy = 1,IIF(ma.RetencionType = 4, ma.Number, ISNULL(tp.Nit, ma.Number)),ma.Number) IdAcreedor,
						IIF(hspf.creditorIdBy = 1,(IIF(ma.RetencionType = 4, '', ISNULL(tp.DigitVerification, 0))),0) DvAcreedor, 
						IIF(ma.RetencionType = 4, ma.Name, ISNULL(tp.Name, ma.Name)) NombreAcreedor, 
						IIF(ma.RetencionType = 4, 'NA',IIF(tp.CodeCIIU='','NA',ISNULL(tp.CodeCIIU,'NA'))) ActividadAcreedor, 
						hspf.CreditConcept ConceptoAcreencia, 
						hspf.SubsequentMeasurement medicionPosterior,
						SUM(jvd.CreditValue - jvd.DebitValue) AS CxPNoVencidas, 
						0 AS CxPMora30dias, 
						0 AS CxPMora60dias, 
						0 AS CxPMora90dias, 
						0 AS CxPMora180dias, 
						0 AS CxPMora360dias, 
						0 AS CxPMoraMayor360dias, 
						0 AS ajuste, 
						SUM(jvd.CreditValue - jvd.DebitValue) AS saldo, 
						null CxPRecursos,
						null MetodoPago
				FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
				JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
				JOIN GeneralLedger.HealthSuperParametersFt004 hspf WITH(NOLOCK) ON ma.Id = hspf.MainAccountId
				JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id
				JOIN GeneralLedger.HealthSuperParametersFt004Detail hspfd WITH(NOLOCK) ON hspf.Id= hspfd.HealthSuperParametersFt004Id
				LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON IIF(ma.RetencionType = 0, jvd.IdThirdParty, ma.IdThirdParty) = tp.Id
				LEFT JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
				WHERE jv.Status = 2 AND jv.Id <> hspfd.JournalVouchersId AND hsp.Status = 1 AND hsp.Format = 4
					AND 
					(
						YEAR(jv.VoucherDate) < @Year
						OR
						(YEAR(jv.VoucherDate) = @Year AND MONTH(jv.VoucherDate) <= @Month)
					)
					AND ma.LegalBookId = @LegalBookId
					--AND ma.Number LIKE '24%'
				GROUP BY	IIF(ma.RetencionType = 4, 0, tp.Id),
							--jvd.IdMainAccount,
							IIF(ma.RetencionType = 4, 10, p.IdentificationType), 
							IIF(ma.RetencionType = 4, ma.Number, ISNULL(tp.Nit, ma.Number)), 
							IIF(ma.RetencionType = 4, '', ISNULL(tp.DigitVerification, 0)),
							IIF(ma.RetencionType = 4, ma.Name, ISNULL(tp.Name, ma.Name)),
							IIF(ma.RetencionType = 4, 'NA', ISNULL(tp.CodeCIIU,'NA')),
							ma.Number,
							hspf.creditorIdBy,
							hspf.CreditConcept,
							hspf.SubsequentMeasurement,
							TP.CodeCIIU,
							MA.RetencionType
				HAVING SUM(jvd.CreditValue - jvd.DebitValue) <> 0
			) jv
			LEFT JOIN @Table_Result tr ON jv.ThirdPartyId = tr.ThirdPartyId --AND jv.IdMainAccount = tr.MainAccountId
			WHERE jv.saldo - ISNULL(tr.Saldo, 0) <> 0

		---------------------------------------------------------------------------------------------------------------

		SELECT	@BusinessLine BusinessLine,
				tr.TipoIdAcreedor, 
				tr.IdAcreedor, 
				tr.DvAcreedor, 
				tr.NombreAcreedor, 
				tr.ActividadAcreedor, 
				MAX(tr.ConceptoAcreencia) ConceptoAcreencia, 
				MAX(tr.MedicionPosterior) MedicionPosterior,
				SUM(CxPNoVencidas) CxPNoVencidas, 
				SUM(CxPMora30dias) CxPMora30dias, 
				SUM(CxPMora60dias) CxPMora60dias, 
				SUM(CxPMora90dias) CxPMora90dias, 
				SUM(CxPMora180dias) CxPMora180dias, 
				SUM(CxPMora360dias) CxPMora360dias, 
				SUM(CxPMoraMayor360dias) CxPMoraMayor360dias, 
				SUM(ajuste) Ajuste, 
				SUM(saldo) Saldo, 
				tr.CxPRecursos,
				tr.MetodoPago
		FROM @Table_Result tr
		GROUP BY	tr.TipoIdAcreedor, 
					tr.IdAcreedor, 
					tr.DvAcreedor, 
					tr.NombreAcreedor, 
					tr.ActividadAcreedor,
					tr.CxPRecursos,
					tr.MetodoPago--, 
					--tr.ConceptoAcreencia, 
					--tr.medicionPosterior

	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END