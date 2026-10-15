CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT003]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY

	DECLARE @Year int,
			@Month int,			
			@LegalBookId int,
			@BusinessLine tinyint,
			@LegalBookIdCopy int,
			@LegalBookType Tinyint


	--Tabla donde se acumula los saldos
	DECLARE @TableBalance table(
								Id int identity(1,1),
								month INT,
								year INT,
								MainAccountId int,
								MainAccountIdH int,
								IdThirdParty int,
								NameAccount varchar(100),
								NumberAccount varchar(50),
								Balance decimal(20,4)
								)
			--------------------------------------------------------------------------------------
	BEGIN TRY

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** CRITERIOS Y FILTROS **********************************/
		DECLARE @ClosingDate DATE = '01/' + RIGHT('0' + CAST(@Month AS VARCHAR(20)), 2)+ '/' + CAST(@Year AS VARCHAR(20))
		SET @ClosingDate = DATEADD(DAY, -1, DATEADD(MONTH, 1, @ClosingDate))
		SET @BusinessLine = (SELECT cs.BusinessLine FROM GeneralLedger.CompanySettings cs)
		/**********************************************************/
		SET @LegalBookIdCopy = @LegalBookId
		SET @LegalBookType = 1

		if  EXISTS(
			SELECT lb.Id
			FROM GeneralLedger.LegalBook lb
			WHERE lb.Id = @LegalBookId and lb.TypeBook = 3)
		BEGIN
			SELECT	@LegalBookId= lb.Id,
					@LegalBookType = 3
			FROM GeneralLedger.LegalBook lb
			WHERE  lb.OfficialBook = 1 AND Status = 1
		END
		/*************************************************************/

		INSERT INTO @TableBalance
			SELECT	gb.Month,
					gb.Year,
					ma.id MainAccountId,
					ma.Id MainAccountIdH,
					gb.IdThirdParty,
					ma.Name,
					ma.Number,
					gb.balance
			FROM(
				SELECT	month,
						Year,
						IdMainAccount,
						IdThirdParty,
						sum(DebitValue -CreditValue) balance 
				FROM GeneralLedger.GeneralLedgerBalance 
				GROUP BY Month,Year,IdMainAccount,IdThirdParty
				HAVING (SUM(CreditValue)<>SUM(DebitValue))) gb
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON gb.IdMainAccount = ma.Id
			JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id AND mac.Type = 1
			WHERE	ma.LegalBookId = @LegalBookId
					AND	((gb.Year < @Year AND gb.Month NOT IN(13,14))
					OR(gb.Year = @Year AND (gb.Month <= @Month AND gb.Month NOT IN(13,14))))
			GROUP BY gb.Month,gb.Year,ma.Id,gb.IdThirdParty,ma.Name,ma.Number,gb.balance

		/*************************************************************/

		IF @LegalBookType = 3
				BEGIN
					UPDATE tb
						SET tb.MainAccountIdH = ma.Id, 
							tb.NumberAccount = ISNULL(ma.Number, 'NH-'+TB.NumberAccount), 
							tb.NameAccount = ISNULL(ma.Name, 'SIN HOMOLOGAR') 
					FROM @TableBalance AS tb
					LEFT JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON tb.MainAccountId = ha.OfficialMainAccountId
					LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id AND ma.LegalBookId = @LegalBookIdCopy
					LEFT JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
					LEFT JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
		END
		/***********************************************************/

		SELECT  @BusinessLine BusinessLine,
				sp.TipoIdDeudor,
				sp.IdDeudor,
				sp.DvDeudor,
				sp.NombreDeudor,
				sp.CodigoMunicipio,
				sp.ConceptoDeudores,
				sp.TipoDeuda,
				sp.MedicionPosterior,
				SUM(sp.CxCPendientesRadicar) CxCPendientesRadicar,
				SUM(sp.CxCNoVencidas) CxCNoVencidas,
				SUM(sp.CxCMora30dias) CxCMora30dias,
				SUM(sp.CxCMora60dias) CxCMora60dias,
				SUM(sp.CxCMora90dias) CxCMora90dias,
				SUM(sp.CxCMora180dias) CxCMora180dias,
				SUM(sp.CxCMora360dias) CxCMora360dias,
				SUM(sp.CxCMoraMayor360dias) CxCMoraMayor360dias,
				SUM(sp.Deterioro30Dias) Deterioro30Dias,
				SUM(sp.Deterioro60Dias) Deterioro60Dias,
				SUM(sp.Deterioro90Dias) Deterioro90Dias,
				SUM(sp.Deterioro180Dias) Deterioro180Dias,
				SUM(sp.Deterioro360Dias) Deterioro360Dias,
				SUM(sp.DeterioroMayor360Dias) DeterioroMayor360Dias,
				sp.Ajuste,
				SUM(sp.CxCPendientesRadicar) + SUM(sp.CxCNoVencidas)+ SUM(sp.CxCMora30dias) + SUM(sp.CxCMora60dias) + SUM(sp.CxCMora90dias) + SUM(sp.CxCMora180dias)
				+ SUM(sp.CxCMora360dias) + SUM(sp.CxCMoraMayor360dias) + SUM(sp.Deterioro30Dias)+ SUM(sp.Deterioro60Dias)+ SUM(sp.Deterioro90Dias)
				+ SUM(sp.Deterioro180Dias) + SUM(sp.Deterioro360Dias) +	SUM(sp.DeterioroMayor360Dias)+ sp.Ajuste AS Saldo

		FROM(
		SELECT 		
			d.Id AS AccountReceivableId,
			d.TipoIdDeudor,
			d.DebtordId AS IdDeudor,
			d.DigitVerification AS DvDeudor,
			d.ThirdPartyName AS NombreDeudor,
			d.CityCode AS CodigoMunicipio,
			d.DebtorsConcept AS ConceptoDeudores,
			d.Typedebt AS TipoDeuda,
			d.SubsequentMeasurement AS MedicionPosterior,
			IIF(d.Typedebt = 1,0,d.CxCPendientesRadicar) CxCPendientesRadicar,
			IIF((d.Age <= 0) AND d.PortfolioStatus <>1, d.Balance, 0) AS CxCNoVencidas,
			IIF((d.Age > 0 AND d.Age <= 30) AND d.PortfolioStatus <>1, d.Balance, 0) as CxCMora30dias,
			IIF((d.Age > 30 AND d.Age <= 60) AND d.PortfolioStatus <>1 , d.Balance, 0) as CxCMora60dias,
			IIF((d.Age > 60 AND d.Age <= 90) AND d.PortfolioStatus <>1, d.Balance, 0) as CxCMora90dias,
			IIF((d.Age > 90 AND d.Age <= 180) AND d.PortfolioStatus <>1, d.Balance, 0) as CxCMora180dias,
			IIF((d.Age > 180 AND d.Age <= 360) AND d.PortfolioStatus <>1, d.Balance, 0) as CxCMora360dias,
			IIF((d.Age > 360) AND d.PortfolioStatus <>1, d.Balance, 0) as CxCMoraMayor360dias,

			IIF(d.Age > 0 AND d.Age <= 30, d.DeteriorationBalance, 0) as Deterioro30Dias,
			IIF(d.Age > 30 AND d.Age <= 60, d.DeteriorationBalance, 0) as Deterioro60Dias,
			IIF(d.Age > 60 AND d.Age <= 90, d.DeteriorationBalance, 0) as Deterioro90Dias,
			IIF(d.Age > 90 AND d.Age <= 180, d.DeteriorationBalance, 0) as Deterioro180Dias,
			IIF(d.Age > 180 AND d.Age <= 360, d.DeteriorationBalance, 0) as Deterioro360Dias,
			IIF(d.Age > 360, d.DeteriorationBalance, 0) as DeterioroMayor360Dias,
			d.Ajuste,
			d.Age,
			d.MainAccountId		
		FROM
		(
				SELECT	ar.Id,
						ar.AccountReceivableType,
						ar.PortfolioStatus,
						ar.PortfolioStatusName,
						CASE p.IdentificationType
							WHEN 0 THEN 'CC' 
							WHEN 1 THEN 'CE' 
							WHEN 7 THEN 'NI' 
							ELSE 'OT' 
						END AS TipoIdDeudor,
						ISNULL(tp.Nit,ma.Number) AS  DebtordId,
						ISNULL(tp.DigitVerification,0) DigitVerification,
						ISNULL(tp.Name,ma.Name) AS ThirdPartyName, 
						ISNULL(c.Code,'NA') AS CityCode,
						CASE ISNULL(cg.EntityType,7) --Homologación Concepto Deudores
							WHEN 1 THEN 10 --UPC Régimen contributivo
							WHEN 2 THEN 11 --UPC Régimen subsidiado
							WHEN 3 THEN 7  --Otros
							WHEN 4 THEN 7  --Otros
							WHEN 5 THEN 9  --ARL
							WHEN 6 THEN 2  --Planes adicionales de salud
							WHEN 7 THEN 7  --Otros
							WHEN 8 THEN 7  --Otros
							WHEN 9 THEN 10 --UPC Régimen Contributivo
							WHEN 10 THEN 8 --SOAT
							WHEN 11 THEN 6 --Reclamaciones (ECAT)
							WHEN 12 THEN 7 --Otros
							WHEN 13 THEN 2 --Planes adicionales de salud
							WHEN 99 THEN 7 --Otros
							ELSE 7 --Otros
						END AS DebtorsConcept,
						2 Typedebt,
						3 AS SubsequentMeasurement,
						IIF(ar.PortfolioStatus = 1,ar.Balance,0) CxCPendientesRadicar,
						ar.AccountWithoutRadicateNumber, 
						ar.RadicatedConsecutive,
						ar.RadicatedDate,
						ar.RadicatedState,
						DATEADD(DAY, ar.Term, ar.AccountReceivableDate) AS ExpiredDate,						
						DATEDIFF(DAY, ar.AccountReceivableDate, @ClosingDate) AS Age,
						ar.Balance,
						ar.CurrentBalance,
						-(are.DeteriorationBalance) as DeteriorationBalance,
						0 Ajuste,
						ma.Id MainAccountId						
				FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
				JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
				JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
				LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
				LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
				LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
				/********************************** ******* **********************************/
				LEFT JOIN Common.City c WITH(NOLOCK) ON p.IdentificacionCityId = c.Id
				LEFT JOIN Portfolio.AccountReceivable are WITH(NOLOCK) ON ar.Id = are.Id				
				WHERE  ar.Balance <> 0 
			
			UNION ALL

				SELECT	hspf.Id,
						0 AS AccountReceivableType,
						0 AS PortfolioStatus,
						NULL PortfolioStatusName,
						CASE p.IdentificationType
							WHEN 0 THEN 'CC' 
							WHEN 1 THEN 'CE' 
							WHEN 7 THEN 'NI' 
							ELSE 'OT' 
						END AS TipoIdDeudor,
						IIF(hspf.DebtorFieldId = 1,ISNULL(tp.Nit,gb.NumberAccount),gb.NumberAccount) AS  DebtordId,
						IIF(hspf.DebtorFieldId=1,ISNULL(tp.DigitVerification,0),0) AS  DigitVerification,
						IIF(hspf.DebtorFieldId=1,ISNULL(tp.Name,gb.NameAccount),gb.NameAccount) ThirdPartyName, 
						IIF(@LegalBookType =3,'NA',ISNULL(c.Code,'NA')) AS CityCode,
						hspf.DebtorsConcept,
						hspf.Typedebt,
						hspf.SubsequentMeasurement,
						0 CxCPendientesRadicar,
						NULL AccountWithoutRadicateNumber, 
						NULL RadicatedConsecutive,
						NULL RadicatedDate,
						NULL RadicatedState,
						@ClosingDate AS ExpiredDate,
						0 Age,
						gb.Balance,
						0 CurrentBalance,
						0 as DeteriorationBalance,
						0 Ajuste,
						gb.MainAccountIdH MainAccountId
				FROM @TableBalance gb				
				LEFT JOIN Common.ThirdParty tp WITH(NOLOCK) ON gb.IdThirdParty = tp.Id
				LEFT JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId =p.Id
				LEFT JOIN GeneralLedger.HealthSuperParametersFt003 hspf WITH(NOLOCK) ON gb.MainAccountId = hspf.MainAccountId
				LEFT JOIN GeneralLedger.HealthSuperParameters hsp WITH(NOLOCK) ON hspf.HealthSuperParametersId = hsp.Id 
				LEFT JOIN Common.City c WITH(NOLOCK) ON p.IdentificacionCityId = c.Id
				/**************************************** FILTROS ****************************************/
				WHERE  gb.Balance <> 0 AND hsp.Format = 3 AND hsp.Status = 1				
		) AS d 
		) sp 
		GROUP BY sp.TipoIdDeudor,sp.IdDeudor,sp.DvDeudor,sp.NombreDeudor,sp.CodigoMunicipio,sp.ConceptoDeudores,sp.TipoDeuda,sp.MedicionPosterior,sp.Ajuste
		HAVING (SUM(sp.CxCPendientesRadicar) + SUM(sp.CxCNoVencidas)+ SUM(sp.CxCMora30dias) + SUM(sp.CxCMora60dias) + SUM(sp.CxCMora90dias) + SUM(sp.CxCMora180dias)
				+ SUM(sp.CxCMora360dias) + SUM(sp.CxCMoraMayor360dias) + SUM(sp.Deterioro30Dias)+ SUM(sp.Deterioro60Dias)+ SUM(sp.Deterioro90Dias)
				+ SUM(sp.Deterioro180Dias) + SUM(sp.Deterioro360Dias) +	SUM(sp.DeterioroMayor360Dias)+ sp.Ajuste) <> 0
		ORDER BY 1, 2
END TRY
BEGIN CATCH
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
END CATCH
END