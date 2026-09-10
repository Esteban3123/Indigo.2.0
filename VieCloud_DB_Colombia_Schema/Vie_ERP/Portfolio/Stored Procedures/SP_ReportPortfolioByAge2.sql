
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-06
-- Description:	Procedimiento para el reporte de edades de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioByAge2]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE -- CRITERIOS --
			@CalculateAgeBy INT,
			@IncludeAdvance INT,
			@OrderBy INT,
			@GroupOrDetailByAccount BIT, --0 Flujo antiguo(agrupado la CXC), 1-Flujo nuevo (Detallado por movimiento contable),
			@isValorization BIT,
			@ToCurrency INT,
			@OfficialCurrency INT,
			-- FILTROS --
			@ClosingDate DATE,
			@OperatingUnits VARCHAR(MAX),
			@PersonTypes VARCHAR(MAX),
			@ThirdParties VARCHAR(MAX),
			@DocumentTypes VARCHAR(MAX),
			@Status VARCHAR(MAX),
			---------------------------------------------------------------------------------------
			@FilterByOperatingUnit BIT = 0,
			@FilterByPersonType BIT = 0,
			@FilterByThirdParty BIT = 0,
			@FilterByDocumentType BIT = 0,
			@FilterByStatus BIT = 0

	DECLARE @Table_OperatingUnit AS TABLE(Id INT)
	DECLARE @Table_PersonType AS TABLE(Id INT)
	DECLARE @Table_ThirdParty AS TABLE(Id INT)
	DECLARE @Table_DocumentType AS TABLE(Id INT)
	DECLARE @Table_Status AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@CalculateAgeBy = t.x.value('CalculateAgeBy[1]','int'),
				@IncludeAdvance = t.x.value('IncludeAdvance[1]','bit'),
				@OrderBy = t.x.value('OrderBy[1]','int'),
				@GroupOrDetailByAccount = T.x.value('GroupOrDetailByAccount[1]', 'BIT'),
				@isValorization = T.x.value('isValorization[1]', 'BIT'),
				@ToCurrency = t.x.value('ToCurrency[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@ClosingDate = t.x.value('ClosingDate[1]','date'),
				---------------------------------------------------------------------------------------
				@OperatingUnits = t.x.value('OperatingUnits[1]','varchar(max)'),
				@PersonTypes = t.x.value('PersonTypes[1]','varchar(max)'),
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)'),
				@DocumentTypes = t.x.value('DocumentTypes[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','varchar(max)')
		FROM @xmlFilters.nodes('/Data') t(x)

		
		--Se obtiene la moneda oficial
		Select  @OfficialCurrency = OfficialCurrencyId
		from GeneralLedger.CompanySettings

		-------------------------------------------------------------------------------------------------

		IF ISNULL(@OperatingUnits, '') <> ''
		BEGIN
			SET @FilterByOperatingUnit = 1

			INSERT INTO @Table_OperatingUnit
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@OperatingUnits, ',')
		END

		IF ISNULL(@PersonTypes, '') <> ''
		BEGIN
			SET @FilterByPersonType = 1

			INSERT INTO @Table_PersonType
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@PersonTypes, ',')
		END

		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParty = 1

			INSERT INTO @Table_ThirdParty
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		IF ISNULL(@DocumentTypes, '') <> ''
		BEGIN
			SET @FilterByDocumentType = 1

			INSERT INTO @Table_DocumentType
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@DocumentTypes, ',')
		END
		
		IF ISNULL(@Status, '') <> '' 
		BEGIN
			
			INSERT INTO @Table_Status
			SELECT CAST(Data AS INT) Data 		
			FROM dbo.Split(@Status, ',')
			
			SET @FilterByStatus = IIF(EXISTS(SELECT 1 FROM @Table_Status WHERE Id=0 ),0,1)
		END

		--IF @ClosingDate >= Common.GETDATE()
		--	BEGIN

			/********************************** OBTENCION DE DATOS **********************************/
					SELECT	
						CASE @OrderBy
							WHEN 1 THEN CAST(IIF(d.AccountReceivableType = 0, 'ZZZZZZZZZZZZZZZZZZZZ', d.DocumentCode) AS VARCHAR(20))
							WHEN 2 THEN CONVERT(VARCHAR(20), IIF(d.AccountReceivableType = 0, @ClosingDate, d.AccountReceivableDate), 112)
							WHEN 3 THEN d.ThirdPartyNit
							WHEN 4 THEN d.RegimenCalculated
							ELSE RIGHT('00000' + CAST(d.Age AS VARCHAR(5)), 5)
						END AS OrderBy,
						IIF(d.AccountReceivableType = 0, 99, 1) OrderByAdvanceAtEnd,
						d.*
					FROM
					(
							SELECT	ar.Id,
									ar.InvoiceNumber AS DocumentCode, 
									ar.AccountReceivableDate, 
									ar.AccountReceivableType,
									ar.PortfolioStatus,
									ar.PortfolioStatusName,
									ar.NumberShares,
									ar.Term,
									ar.OpeningBalance,
									tp.Nit AS ThirdPartyNit, 
									tp.Name AS ThirdPartyName, 
									p.IdentificationType,
									ic.Code + ' - ' + ic.[Name] AS Category, 
									ctt.Name AS Regimen, 
									cg.Code AS CareGroupCode,
									cg.Name AS CareGroupName, 
									ct.Code AS ContractCode,
									ct.ContractName AS ContractName,
									ar.AccountWithoutRadicateNumber, 
									ar.RadicatedConsecutive,						
									ar.RadicatedUser,
									ar.RadicatedDate,
									ric.DocumentDate,
									ar.RadicatedState,
									ma.Number AS MainAccountNumber, 
									ma.Name AS MainAccountName,
									iif(@isValorization=0, ar.DocumentValue, Common.CurrencyConverterWithDate(ar.DocumentValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) DocumentValue, 
									iif(@isValorization=0, ar.RetentionValue, Common.CurrencyConverterWithDate(ar.RetentionValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) RetentionValue, 
									iif(@isValorization=0, ar.InitialValue, Common.CurrencyConverterWithDate(ar.InitialValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) InitialValue,
									iif(@isValorization=0, ar.DebitValue, Common.CurrencyConverterWithDate(ar.DebitValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  DebitValue,
									iif(@isValorization=0, ar.CreditValue, Common.CurrencyConverterWithDate(ar.CreditValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) CreditValue,
									iif(@isValorization=0, ar.TransferValue, Common.CurrencyConverterWithDate(ar.TransferValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  TransferValue,
									iif(@isValorization=0, ar.CashReceiptValue, Common.CurrencyConverterWithDate(ar.CashReceiptValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CashReceiptValue,
									iif(@isValorization=0, ar.CrossingValue, Common.CurrencyConverterWithDate(ar.CrossingValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CrossingValue,
									iif(@isValorization=0, ar.Balance, Common.CurrencyConverterWithDate(ar.Balance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  Balance,
									iif(@isValorization=0, ar.CurrentBalance, Common.CurrencyConverterWithDate(ar.CurrentBalance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CurrentBalance,
									c.NOMCENATE AS CenterAttention,
									DATEADD(DAY, ar.Term, 
										IIF
										(
											@CalculateAgeBy = 1,
											ar.AccountReceivableDate,
											ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)
										)
									) AS ExpiredDate,
									IIF
									(
										@CalculateAgeBy = 1,
										DATEDIFF(DAY, (DATEADD(DAY, ar.Term,ar.AccountReceivableDate)), @ClosingDate),
										DATEDIFF(DAY, ISNULL(DATEADD(DAY, ar.Term, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)), @ClosingDate), @ClosingDate)							
									) AS Age,
									ar.RegimenCalculated,
									ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
									ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
									ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
									gpg.State GlosaState,
									CASE gpg.state 
										WHEN 1 THEN 'Pendiente Confirmar Glosa'
										WHEN 2 THEN 'Pendiente Evaluacion Glosa'
										WHEN 3 THEN 'Pendiente envio de oficio'
										WHEN 4 THEN 'Pendiente confirmar reiteracion'
										WHEN 5 THEN 'Pendiente evaluacion reitreacion'
										WHEN 6 THEN 'Pendiente conciliacion'
										WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
										WHEN 8 THEN 'Conciliada'
										WHEN 9 THEN 'Conciliada Parcialmente'
										WHEN 11 THEN 'Glosa con Respuesta'
										WHEN 12 THEN 'Reiteracion con respuesta'
										WHEN 13 THEN 'Pendiente confirmar pago parcial'
										WHEN 14 THEN 'Confirmado pago parcial'
										WHEN 15 THEN 'Cobro juridico'
										ELSE 'No esta Glosada'
									END as GlosaStateName
									,iif(@isValorization=0, ar.CurrencyId, @ToCurrency)  CurrencyId
									, iif(@isValorization=0, ar.CurrencyName, cu.Name)  CurrencyName
							FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
							JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
							JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
							LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
							LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
							LEFT JOIN Contract.CompanyType as ctt on ctt.Id = cg.EntityType 
							LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
							LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
							LEFT JOIN Portfolio.RadicateInvoiceC ric WITH (NOLOCK) ON ric.RadicatedConsecutive = ar.RadicatedConsecutive
							/********************************** ******* **********************************/
							LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
							LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
							LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
							/**************************************** FILTROS ****************************************/
							LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
							LEFT JOIN @Table_PersonType tpt ON tp.PersonType = tpt.Id
							LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
							LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
							LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
							LEFT JOIN Common.Currency cu on cu.Id = @ToCurrency
							WHERE ((@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
								AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
								AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
								AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
								AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
								AND ar.Balance <> 0 )AND @GroupOrDetailByAccount =0
			
						UNION ALL

							SELECT	pa.Id,
									pa.Code AS DocumentCode, 
									pa.DocumentDate AS AccountReceivableDate, 
									0 AS AccountReceivableType,
									0 AS PortfolioStatus,
									NULL PortfolioStatusName,
									0 AS NumberShares,
									0 AS Term,
									0 AS OpeningBalance,
									pa.ThirdPartyNit, 
									pa.ThirdPartyName, 
									pa.IdentificationType,
									NULL AS Category, 
									NULL AS Regimen, 
									NULL AS CareGroupCode,
									NULL AS CareGroupName, 
									NULL AS ContractCode,
									NULL AS ContractName,
									NULL AS AccountWithoutRadicateNumber, 
									NULL AS RadicatedConsecutive,
									NULL AS DocumentDate,
									NULL AS RadicatedUser,
									NULL AS RadicatedDate,
									NULL RadicatedState,
									ma.Number AS MainAccountNumber, 
									ma.Name AS MainAccountName,
									iif(@isValorization=0, pa.DocumentValue, Common.CurrencyConverterWithDate(pa.DocumentValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) DocumentValue,  
									0 RetentionValue,
									0 InitialValue,
									iif(@isValorization=0, pa.DebitValue, Common.CurrencyConverterWithDate(pa.DebitValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  DebitValue,
									iif(@isValorization=0, pa.CreditValue, Common.CurrencyConverterWithDate(pa.CreditValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) CreditValue,
									iif(@isValorization=0, pa.TransferValue, Common.CurrencyConverterWithDate(pa.TransferValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  TransferValue,
									0 AS CashReceiptValue,
									0 AS CrossingValue,
									iif(@isValorization=0, pa.Balance, Common.CurrencyConverterWithDate(pa.Balance, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  Balance,
									iif(@isValorization=0, pa.CurrentBalance, Common.CurrencyConverterWithDate(pa.CurrentBalance, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CurrentBalance,
									NULL AS CenterAttention,
									@ClosingDate AS ExpiredDate,
									99999 AS Age,
									'' AS RegimenCalculated,
									0 ValueGlosado,
									0 ValueAcceptedFirstInstance,
									0 ValueAcceptedSecondInstance,
									NULL GlosaState,
									NULL as GlosaStateName
									,iif(@isValorization=0, pa.CurrencyId, @ToCurrency)  CurrencyId
									, iif(@isValorization=0, pa.CurrencyName, cu.Name)  CurrencyName
							FROM [Portfolio].[GetPortfolioAdvanceByAge](@ClosingDate) pa
							JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
							JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id AND mac.Type = 1
							/**************************************** FILTROS ****************************************/
							--LEFT JOIN @Table_OperatingUnit tou ON pa.OperatingUnitId = tou.Id
							LEFT JOIN @Table_PersonType tpt ON pa.PersonType = tpt.Id
							LEFT JOIN @Table_ThirdParty tc ON pa.ThirdPartyId = tc.Id
							LEFT JOIN Common.Currency cu on cu.Id = @ToCurrency
							WHERE @IncludeAdvance = 1
								--AND (@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
								AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
								AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
								AND pa.Balance <> 0

						UNION ALL

							SELECT	ar.Id,
									ar.InvoiceNumber AS DocumentCode, 
									ar.AccountReceivableDate, 
									ar.AccountReceivableType,
									ar.PortfolioStatus,
									ar.PortfolioStatusName,
									ar.NumberShares,
									ar.Term,
									ar.OpeningBalance,
									tp.Nit AS ThirdPartyNit, 
									tp.Name AS ThirdPartyName, 
									p.IdentificationType,
									ic.Code + ' - ' + ic.[Name] AS Category, 
									ctt.Name AS Regimen, 
									cg.Code AS CareGroupCode,
									cg.Name AS CareGroupName, 
									ct.Code AS ContractCode,
									ct.ContractName AS ContractName,
									ar.AccountWithoutRadicateNumber, 
									ar.RadicatedConsecutive,						
									ar.RadicatedUser,
									ar.RadicatedDate,
									ric.DocumentDate,
									ar.RadicatedState,
									ma.Number AS MainAccountNumber, 
									ma.Name AS MainAccountName,
									iif(@isValorization=0, ar.DocumentValue, Common.CurrencyConverterWithDate(ar.DocumentValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) DocumentValue, 
									iif(@isValorization=0, ar.RetentionValue, Common.CurrencyConverterWithDate(ar.RetentionValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) RetentionValue, 
									iif(@isValorization=0, ar.InitialValue, Common.CurrencyConverterWithDate(ar.InitialValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) InitialValue,
									iif(@isValorization=0, ar.DebitValue, Common.CurrencyConverterWithDate(ar.DebitValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  DebitValue,
									iif(@isValorization=0, ar.CreditValue, Common.CurrencyConverterWithDate(ar.CreditValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) ) CreditValue,
									iif(@isValorization=0, ar.TransferValue, Common.CurrencyConverterWithDate(ar.TransferValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  TransferValue,
									iif(@isValorization=0, ar.CashReceiptValue, Common.CurrencyConverterWithDate(ar.CashReceiptValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CashReceiptValue,
									iif(@isValorization=0, ar.CrossingValue, Common.CurrencyConverterWithDate(ar.CrossingValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CrossingValue,
									iif(@isValorization=0, ar.Balance, Common.CurrencyConverterWithDate(ar.Balance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  Balance,
									iif(@isValorization=0, ar.CurrentBalance, Common.CurrencyConverterWithDate(ar.CurrentBalance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) )  CurrentBalance,
									c.NOMCENATE AS CenterAttention,
									DATEADD(DAY, ar.Term, 
										IIF
										(
											@CalculateAgeBy = 1,
											ar.AccountReceivableDate,
											ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)
										)
									) AS ExpiredDate,
									IIF
									(
										@CalculateAgeBy = 1,
										DATEDIFF(DAY, ar.AccountReceivableDate, @ClosingDate),
										DATEDIFF(DAY, ISNULL(DATEADD(DAY, ar.Term, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)), @ClosingDate), @ClosingDate)							
									) AS Age,
									ar.RegimenCalculated,
									ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
									ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
									ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
									gpg.State GlosaState,
									CASE gpg.state 
										WHEN 1 THEN 'Pendiente Confirmar Glosa'
										WHEN 2 THEN 'Pendiente Evaluacion Glosa'
										WHEN 3 THEN 'Pendiente envio de oficio'
										WHEN 4 THEN 'Pendiente confirmar reiteracion'
										WHEN 5 THEN 'Pendiente evaluacion reitreacion'
										WHEN 6 THEN 'Pendiente conciliacion'
										WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
										WHEN 8 THEN 'Conciliada'
										WHEN 9 THEN 'Conciliada Parcialmente'
										WHEN 11 THEN 'Glosa con Respuesta'
										WHEN 12 THEN 'Reiteracion con respuesta'
										WHEN 13 THEN 'Pendiente confirmar pago parcial'
										WHEN 14 THEN 'Confirmado pago parcial'
										WHEN 15 THEN 'Cobro juridico'
										ELSE 'No esta Glosada'
									END as GlosaStateName
									,iif(@isValorization=0, ar.CurrencyId, @ToCurrency)  CurrencyId
									, iif(@isValorization=0, ar.CurrencyName, cu.Name)  CurrencyName
							FROM [Portfolio].[GetAccountReceivableByAgeDetail](NULL, @ClosingDate) AS ar
							JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
							JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
							LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
							LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
							LEFT JOIN Contract.CompanyType as ctt on ctt.Id = cg.EntityType 
							LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
							LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
							LEFT JOIN Portfolio.RadicateInvoiceC ric WITH (NOLOCK) ON ric.RadicatedConsecutive = ar.RadicatedConsecutive
							/********************************** ******* **********************************/
							LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
							LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
							LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
							/**************************************** FILTROS ****************************************/
							LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
							LEFT JOIN @Table_PersonType tpt ON tp.PersonType = tpt.Id
							LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
							LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
							LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
							LEFT JOIN Common.Currency cu on cu.Id = @ToCurrency
							WHERE ((@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
								AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
								AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
								AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
								AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
								AND ar.Balance <> 0) AND @GroupOrDetailByAccount =1

					) AS d
					ORDER BY 1, 2
		--END

		--ELSE IF NOT EXISTS(SELECT 1 
		--			FROM [Portfolio].[ReportPortfolioByAge]
		--			WHERE	ClosingDate=@ClosingDate AND OperatingUnits=@OperatingUnits 
		--					AND PersonTypes=@PersonTypes AND ThirdParties= @ThirdParties
		--					AND DocumentTypes =@DocumentTypes AND [Status]= @Status AND CalculateAgeBy=@CalculateAgeBy
		--					AND IncludeAdvance=@IncludeAdvance AND OrderBy=@OrderBy AND ISNULL(GroupOrDetailByAccount, '') = ISNULL(@GroupOrDetailByAccount, ''))
		--BEGIN
		--		DECLARE @IdHead as INT
		--		INSERT INTO [Portfolio].[ReportPortfolioByAge] VALUES(	@ClosingDate,
		--																@OperatingUnits,
		--																@PersonTypes,
		--																@ThirdParties,
		--																@DocumentTypes,
		--																@Status,
		--																@CalculateAgeBy,
		--																@IncludeAdvance,
		--																@OrderBy,
		--																@GroupOrDetailByAccount)
		--		set @IdHead = SCOPE_IDENTITY()
				
		--		INSERT INTO [Portfolio].[ReportPortfolioByAgeDetail] (	[IdReportPortfolioByAge] ,
		--																[OrderBy],
		--																[OrderByAdvanceAtEnd],
		--																[Id],																																																						
		--																[DocumentCode],
		--																[AccountReceivableDate],
		--																[AccountReceivableType],
		--																[PortfolioStatus],
		--																[PortfolioStatusName],
		--																[NumberShares],
		--																[Term],
		--																[OpeningBalance],
		--																[ThirdPartyNit],
		--																[ThirdPartyName],
		--																[IdentificationType],
		--																[Category],
		--																[Regimen],
		--																[CareGroupCode],
		--																[CareGroupName],
		--																[ContractCode],
		--																[ContractName],
		--																[AccountWithoutRadicateNumber],
		--																[RadicatedConsecutive] ,
		--																[RadicatedUser] ,
		--																[RadicatedDate] ,
		--																[DocumentDate] ,
		--																[RadicatedState] ,
		--																[MainAccountNumber] ,
		--																[MainAccountName] ,
		--																[DocumentValue] ,
		--																[RetentionValue] ,
		--																[InitialValue] ,
		--																[DebitValue] ,
		--																[CreditValue] ,
		--																[TransferValue] ,
		--																[CashReceiptValue] ,
		--																[CrossingValue],
		--																[Balance],
		--																[CurrentBalance] ,
		--																[CenterAttention] ,
		--																[ExpiredDate] ,
		--																[Age] ,
		--																[RegimenCalculated] ,
		--																[ValueGlosado] ,
		--																[ValueAcceptedFirstInstance],
		--																[ValueAcceptedSecondInstance] ,
		--																[GlosaState] ,
		--																[GlosaStateName] )
				
		--		SELECT	@IdHead,
		--				CASE @OrderBy
		--					WHEN 1 THEN CAST(IIF(d.AccountReceivableType = 0, 'ZZZZZZZZZZZZZZZZZZZZ', d.DocumentCode) AS VARCHAR(20))
		--					WHEN 2 THEN CONVERT(VARCHAR(20), IIF(d.AccountReceivableType = 0, @ClosingDate, d.AccountReceivableDate), 112)
		--					WHEN 3 THEN d.ThirdPartyNit
		--					WHEN 4 THEN d.RegimenCalculated
		--					ELSE RIGHT('00000' + CAST(d.Age AS VARCHAR(5)), 5)
		--				END AS OrderBy,
		--				IIF(d.AccountReceivableType = 0, 99, 1) OrderByAdvanceAtEnd,
		--				d.*
		--			FROM
		--			(
		--					SELECT	ar.Id,
		--							ar.InvoiceNumber AS DocumentCode, 
		--							ar.AccountReceivableDate, 
		--							ar.AccountReceivableType,
		--							ar.PortfolioStatus,
		--							ar.PortfolioStatusName,
		--							ar.NumberShares,
		--							ar.Term,
		--							ar.OpeningBalance,
		--							tp.Nit AS ThirdPartyNit, 
		--							tp.Name AS ThirdPartyName, 
		--							p.IdentificationType,
		--							ic.Code + ' - ' + ic.[Name] AS Category, 
		--							contract.fnCareGroupEntityType(cg.EntityType) AS Regimen, 
		--							cg.Code AS CareGroupCode,
		--							cg.Name AS CareGroupName, 
		--							ct.Code AS ContractCode,
		--							ct.ContractName AS ContractName,
		--							ar.AccountWithoutRadicateNumber, 
		--							ar.RadicatedConsecutive,						
		--							ar.RadicatedUser,
		--							ar.RadicatedDate,
		--							ric.DocumentDate,
		--							ar.RadicatedState,
		--							ma.Number AS MainAccountNumber, 
		--							ma.Name AS MainAccountName,
		--							ar.DocumentValue, 
		--							ar.RetentionValue,
		--							ar.InitialValue,
		--							ar.DebitValue,
		--							ar.CreditValue,
		--							ar.TransferValue,
		--							ar.CashReceiptValue,
		--							ar.CrossingValue,
		--							ar.Balance,
		--							ar.CurrentBalance,
		--							c.NOMCENATE AS CenterAttention,
		--							DATEADD(DAY, ar.Term, 
		--								IIF
		--								(
		--									@CalculateAgeBy = 1,
		--									ar.AccountReceivableDate,
		--									ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)
		--								)
		--							) AS ExpiredDate,
		--							IIF
		--							(
		--								@CalculateAgeBy = 1,
		--								DATEDIFF(DAY, ar.AccountReceivableDate, @ClosingDate),
		--								DATEDIFF(DAY, ISNULL(DATEADD(DAY, ar.Term, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)), @ClosingDate), @ClosingDate)							
		--							) AS Age,
		--							ar.RegimenCalculated,
		--							ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
		--							ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
		--							ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
		--							gpg.State GlosaState,
		--							CASE gpg.state 
		--								WHEN 1 THEN 'Pendiente Confirmar Glosa'
		--								WHEN 2 THEN 'Pendiente Evaluacion Glosa'
		--								WHEN 3 THEN 'Pendiente envio de oficio'
		--								WHEN 4 THEN 'Pendiente confirmar reiteracion'
		--								WHEN 5 THEN 'Pendiente evaluacion reitreacion'
		--								WHEN 6 THEN 'Pendiente conciliacion'
		--								WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
		--								WHEN 8 THEN 'Conciliada'
		--								WHEN 9 THEN 'Conciliada Parcialmente'
		--								WHEN 11 THEN 'Glosa con Respuesta'
		--								WHEN 12 THEN 'Reiteracion con respuesta'
		--								WHEN 13 THEN 'Pendiente confirmar pago parcial'
		--								WHEN 14 THEN 'Confirmado pago parcial'
		--								WHEN 15 THEN 'Cobro juridico'
		--								ELSE 'No esta Glosada'
		--							END as GlosaStateName
		--					FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
		--					JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		--					JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
		--					LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
		--					LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
		--					LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
		--					LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
		--					LEFT JOIN Portfolio.RadicateInvoiceC ric WITH (NOLOCK) ON ric.RadicatedConsecutive = ar.RadicatedConsecutive
		--					/********************************** ******* **********************************/
		--					LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
		--					LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
		--					LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
		--					/**************************************** FILTROS ****************************************/
		--					LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
		--					LEFT JOIN @Table_PersonType tpt ON tp.PersonType = tpt.Id
		--					LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
		--					LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
		--					LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
		--					WHERE ((@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
		--						AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
		--						AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
		--						AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
		--						AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
		--						AND ar.Balance <> 0) AND @GroupOrDetailByAccount =0
			
		--				UNION ALL

		--					SELECT	pa.Id,
		--							pa.Code AS DocumentCode, 
		--							pa.DocumentDate AS AccountReceivableDate, 
		--							0 AS AccountReceivableType,
		--							0 AS PortfolioStatus,
		--							NULL PortfolioStatusName,
		--							0 AS NumberShares,
		--							0 AS Term,
		--							0 AS OpeningBalance,
		--							pa.ThirdPartyNit, 
		--							pa.ThirdPartyName, 
		--							pa.IdentificationType,
		--							NULL AS Category, 
		--							NULL AS Regimen, 
		--							NULL AS CareGroupCode,
		--							NULL AS CareGroupName, 
		--							NULL AS ContractCode,
		--							NULL AS ContractName,
		--							NULL AS AccountWithoutRadicateNumber, 
		--							NULL AS RadicatedConsecutive,
		--							NULL AS DocumentDate,
		--							NULL AS RadicatedUser,
		--							NULL AS RadicatedDate,
		--							NULL RadicatedState,
		--							ma.Number AS MainAccountNumber, 
		--							ma.Name AS MainAccountName,
		--							pa.DocumentValue, 
		--							0 RetentionValue,
		--							0 InitialValue,
		--							pa.DebitValue,
		--							pa.CreditValue,
		--							pa.TransferValue,
		--							0 AS CashReceiptValue,
		--							0 AS CrossingValue,
		--							pa.Balance,
		--							pa.CurrentBalance,
		--							NULL AS CenterAttention,
		--							@ClosingDate AS ExpiredDate,
		--							99999 AS Age,
		--							'' AS RegimenCalculated,
		--							0 ValueGlosado,
		--							0 ValueAcceptedFirstInstance,
		--							0 ValueAcceptedSecondInstance,
		--							NULL GlosaState,
		--							NULL as GlosaStateName
		--					FROM [Portfolio].[GetPortfolioAdvanceByAge](@ClosingDate) pa
		--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
		--					JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id AND mac.Type = 1
		--					/**************************************** FILTROS ****************************************/
		--					--LEFT JOIN @Table_OperatingUnit tou ON pa.OperatingUnitId = tou.Id
		--					LEFT JOIN @Table_PersonType tpt ON pa.PersonType = tpt.Id
		--					LEFT JOIN @Table_ThirdParty tc ON pa.ThirdPartyId = tc.Id
		--					WHERE @IncludeAdvance = 1
		--						--AND (@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
		--						AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
		--						AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
		--						AND pa.Balance <> 0

		--			UNION ALL

		--					SELECT	ar.Id,
		--							ar.InvoiceNumber AS DocumentCode, 
		--							ar.AccountReceivableDate, 
		--							ar.AccountReceivableType,
		--							ar.PortfolioStatus,
		--							ar.PortfolioStatusName,
		--							ar.NumberShares,
		--							ar.Term,
		--							ar.OpeningBalance,
		--							tp.Nit AS ThirdPartyNit, 
		--							tp.Name AS ThirdPartyName, 
		--							p.IdentificationType,
		--							ic.Code + ' - ' + ic.[Name] AS Category, 
		--							contract.fnCareGroupEntityType(cg.EntityType) AS Regimen, 
		--							cg.Code AS CareGroupCode,
		--							cg.Name AS CareGroupName, 
		--							ct.Code AS ContractCode,
		--							ct.ContractName AS ContractName,
		--							ar.AccountWithoutRadicateNumber, 
		--							ar.RadicatedConsecutive,						
		--							ar.RadicatedUser,
		--							ar.RadicatedDate,
		--							ric.DocumentDate,
		--							ar.RadicatedState,
		--							ma.Number AS MainAccountNumber, 
		--							ma.Name AS MainAccountName,
		--							ar.DocumentValue, 
		--							ar.RetentionValue,
		--							ar.InitialValue,
		--							ar.DebitValue,
		--							ar.CreditValue,
		--							ar.TransferValue,
		--							ar.CashReceiptValue,
		--							ar.CrossingValue,
		--							ar.Balance,
		--							ar.CurrentBalance,
		--							c.NOMCENATE AS CenterAttention,
		--							DATEADD(DAY, ar.Term, 
		--								IIF
		--								(
		--									@CalculateAgeBy = 1,
		--									ar.AccountReceivableDate,
		--									ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)
		--								)
		--							) AS ExpiredDate,
		--							IIF
		--							(
		--								@CalculateAgeBy = 1,
		--								DATEDIFF(DAY, ar.AccountReceivableDate, @ClosingDate),
		--								DATEDIFF(DAY, ISNULL(DATEADD(DAY, ar.Term, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)), @ClosingDate), @ClosingDate)							
		--							) AS Age,
		--							ar.RegimenCalculated,
		--							IIF(ar.PortfolioStatus = 4, ISNULL(gpg.ValueGlosado, 0), 0) ValueGlosado,
		--							IIF(ar.PortfolioStatus = 4, 
		--								ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0), 
		--								0
		--							    ) ValueAcceptedFirstInstance,
		--							IIF(ar.PortfolioStatus = 4, ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0),
		--								0
		--								) ValueAcceptedSecondInstance,
		--							IIF(ar.PortfolioStatus = 4, gpg.State, NULL) GlosaState,
		--							IIF(ar.PortfolioStatus = 4,
		--								CASE gpg.state 
		--									WHEN 1 THEN 'Pendiente Confirmar Glosa'
		--									WHEN 2 THEN 'Pendiente Evaluacion Glosa'
		--									WHEN 3 THEN 'Pendiente envio de oficio'
		--									WHEN 4 THEN 'Pendiente confirmar reiteracion'
		--									WHEN 5 THEN 'Pendiente evaluacion reitreacion'
		--									WHEN 6 THEN 'Pendiente conciliacion'
		--									WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
		--									WHEN 8 THEN 'Conciliada'
		--									WHEN 9 THEN 'Conciliada Parcialmente'
		--									WHEN 11 THEN 'Glosa con Respuesta'
		--									WHEN 12 THEN 'Reiteracion con respuesta'
		--									WHEN 13 THEN 'Pendiente confirmar pago parcial'
		--									WHEN 14 THEN 'Confirmado pago parcial'
		--									WHEN 15 THEN 'Cobro juridico'
		--									ELSE 'No esta Glosada'
		--								END,
		--								NULL
		--                             ) as GlosaStateName
		--					FROM [Portfolio].[GetAccountReceivableByAgeDetail](NULL, @ClosingDate) AS ar
		--					JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		--					JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
		--					LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
		--					LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
		--					LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
		--					LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
		--					LEFT JOIN Portfolio.RadicateInvoiceC ric WITH (NOLOCK) ON ric.RadicatedConsecutive = ar.RadicatedConsecutive
		--					/********************************** ******* **********************************/
		--					LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
		--					LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
		--					LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
		--					/**************************************** FILTROS ****************************************/
		--					LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
		--					LEFT JOIN @Table_PersonType tpt ON tp.PersonType = tpt.Id
		--					LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
		--					LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
		--					LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
		--					WHERE ((@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
		--						AND (@FilterByPersonType = 0 OR tpt.Id IS NOT NULL)
		--						AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
		--						AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
		--						AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
		--						AND ar.Balance <> 0) AND @GroupOrDetailByAccount =1

		--			) AS d
		--			ORDER BY 1, 2

		--			SELECT *
		--				FROM Portfolio.ReportPortfolioByAgeDetail rpad WITH(NOLOCK)
		--				JOIN Portfolio.ReportPortfolioByAge rpa WITH(NOLOCK) on rpad.IdReportPortfolioByAge=rpa.Id
		--				WHERE rpa.Id=@IdHead
		--END
		--ELSE
		--	BEGIN
		--			/********************************** OBTENCION DE DATOS **********************************/
		--			SELECT *
		--			FROM Portfolio.ReportPortfolioByAgeDetail rpad WITH(NOLOCK)
		--			JOIN Portfolio.ReportPortfolioByAge rpa WITH(NOLOCK) on rpad.IdReportPortfolioByAge=rpa.Id
		--			WHERE	rpa.ClosingDate=@ClosingDate AND rpa.OperatingUnits=@OperatingUnits 
		--					AND rpa.PersonTypes=@PersonTypes AND rpa.ThirdParties= @ThirdParties
		--					AND rpa.DocumentTypes =@DocumentTypes AND rpa.[Status]= @Status AND rpa.CalculateAgeBy=@CalculateAgeBy
		--					AND rpa.IncludeAdvance=@IncludeAdvance AND rpa.OrderBy=@OrderBy AND rpa.GroupOrDetailByAccount =@GroupOrDetailByAccount
		--END
		
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que genera el reporte de cartera por antigüedad (aging) a una fecha de corte, aplicando criterios configurables (método de cálculo de edad, inclusión de anticipos, ordenamiento, agrupación por CXC o detalle contable, valorización en moneda destino) y filtros por unidad operativa, tipo de persona, tercero, tipo de documento y estado. Consolida cuentas por cobrar con sus valores de glosa, movimientos contables y datos de radicación, uniendo anticipos mediante UNION ALL y convirtiendo montos con tasa de cambio a la fecha de corte cuando aplica valorización.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con CalculateAgeBy, IncludeAdvance, OrderBy, GroupOrDetailByAccount, isValorization y ToCurrency.; @xmlFilters debe contener nodo /Data con ClosingDate y los filtros opcionales en formato CSV.; Debe existir registro en GeneralLedger.CompanySettings que provea OfficialCurrencyId.; Las funciones Portfolio.GetAccountReceivableByAge, Portfolio.GetAccountReceivableByAgeDetail y Portfolio.GetPortfolioAdvanceByAge deben estar disponibles y aceptar @ClosingDate.; La función dbo.Split debe existir para separar los filtros CSV en enteros.; @ToCurrency debe ser un Id válido en Common.Currency cuando @isValorization = 1.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros con Balance <> 0 (excluye documentos saldados).; La moneda oficial se obtiene de GeneralLedger.CompanySettings.OfficialCurrencyId y se usa como fallback cuando el documento no tiene CurrencyId.; Los anticipos siempre se marcan con AccountReceivableType=0, Age=99999 y OrderByAdvanceAtEnd=99 para forzar su aparición al final del listado.; Los valores aceptados de glosa de primera/segunda instancia solo se reportan si la fecha de evaluación correspondiente es <= @ClosingDate; en caso contrario se devuelve 0.; Solo se consideran glosas cuya RadicatedDate (cast a DATE) es <= @ClosingDate.; Para anticipos solo se consideran cuentas cuya clase contable tiene Type = 1 en GeneralLedger.MainAccountClasses.; El procedimiento solo lee información: no realiza INSERT/UPDATE/DELETE en tablas físicas (la lógica de persistencia en Portfolio.ReportPortfolioByAge / Detail está comentada).; Los errores se atrapan y se devuelven como un resultado con Code=''999'', Message y Line, sin relanzarlos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera por edades; Cuentas por cobrar; Anticipos de cartera; Glosas (estados: pendiente confirmar, evaluación, reiteración, conciliación, cobro jurídico, etc.); Radicación de facturas; Régimen / Tipo de empresa pagadora; Grupo de atención (CareGroup); Contrato con pagador; Plan de cuentas (MainAccounts); Centro de atención; Conversión/valorización de moneda; Tercero (NIT) y tipo de identificación', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @GroupOrDetailByAccount = 0 → Toma cuentas por cobrar agrupadas vía Portfolio.GetAccountReceivableByAge (flujo antiguo, agrupado por CXC). else Cuando @GroupOrDetailByAccount = 1, toma el detalle por movimiento contable vía Portfolio.GetAccountReceivableByAgeDetail.; si @IncludeAdvance = 1 → Incluye anticipos de cartera consultando Portfolio.GetPortfolioAdvanceByAge y los ubica al final del orden (OrderByAdvanceAtEnd = 99). else Si @IncludeAdvance <> 1 los anticipos no se incluyen en el resultado.; si @CalculateAgeBy = 1 → La edad se calcula a partir de AccountReceivableDate (DATEDIFF entre AccountReceivableDate/AccountReceivableDate+Term y @ClosingDate). else Si @CalculateAgeBy <> 1 la edad se calcula a partir de ISNULL(RadicatedDate, AccountReceivableDate)+Term hasta @ClosingDate.; si @isValorization = 0 → Los importes monetarios (DocumentValue, Balance, etc.) se devuelven en su moneda original (CurrencyId/CurrencyName del documento). else Cuando @isValorization = 1 se convierten con Common.CurrencyConverterWithDate desde ISNULL(CurrencyId,@OfficialCurrency) hacia @ToCurrency a la fecha @ClosingDate.; si @OrderBy IN (1,2,3,4) → Ordena por DocumentCode, AccountReceivableDate (formato 112), ThirdPartyNit o RegimenCalculated respectivamente; los anticipos (AccountReceivableType=0) se ordenan al final con ''ZZZZZZZZZZZZZZZZZZZZ'' o @ClosingDate. else Para cualquier otro valor se ordena por Age con padding RIGHT(''00000''+Age,5).; si EXISTS(SELECT 1 FROM @Table_Status WHERE Id=0) → @FilterByStatus = 0 (no se filtra por estado de cartera, se interpreta como ''todos''). else Cuando hay estados específicos distintos de 0, @FilterByStatus = 1 y se exige coincidencia con @Table_Status.; si Cada filtro string @OperatingUnits/@PersonTypes/@ThirdParties/@DocumentTypes/@Status no es nulo ni vacío → Se activa la bandera correspondiente (@FilterByX=1) y se cargan los Ids vía dbo.Split('',''). else Si está vacío/NULL la bandera queda en 0 y el filtro no aplica (pasa todo).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetAccountReceivableByAge; Portfolio.GetAccountReceivableByAgeDetail; Portfolio.GetPortfolioAdvanceByAge; dbo.Split; Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.ThirdParty; Common.Person; Billing.InvoiceCategories; Contract.CareGroup; Contract.CompanyType; Contract.Contract; GeneralLedger.MainAccounts; Portfolio.RadicateInvoiceC; Glosas.GlosaPortfolioGlosada; dbo.ADINGRESO; dbo.ADCENATEN; Common.Currency; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge2';
-- GO
