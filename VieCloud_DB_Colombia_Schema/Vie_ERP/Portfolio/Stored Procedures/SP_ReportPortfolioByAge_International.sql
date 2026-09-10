
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-06
-- Description:	Procedimiento para el reporte de edades de cartera
-- SP DE CARTERA - internacional
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioByAge_International]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
	WITH RECOMPILE
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

	CREATE TABLE #Table_OperatingUnit (
	    Id INT PRIMARY KEY 
	);
	CREATE TABLE #Table_PersonType (
	    Id INT PRIMARY KEY 
	);
	
	CREATE TABLE #Table_ThirdParty (
	    Id INT PRIMARY KEY
	);
	
	CREATE TABLE #Table_DocumentType (
	    Id INT PRIMARY KEY
	);
	
	CREATE TABLE #Table_Status (
	    Id INT PRIMARY KEY
	);

	CREATE INDEX idx_OperatingUnitId ON #Table_OperatingUnit(Id);
	CREATE INDEX idx_PersonTypeId ON #Table_PersonType(Id);
	CREATE INDEX idx_ThirdPartyId ON #Table_ThirdParty(Id);
	CREATE INDEX idx_DocumentTypeId ON #Table_DocumentType(Id);
	CREATE INDEX idx_StatusId ON #Table_Status(Id);	

	CREATE TABLE #ReportPortfolioByAge
    (
        [OrderBy] [varchar](50) NULL,
        [OrderByAdvanceAtEnd] [int] NOT NULL,
        [Id] [int] NOT NULL,
        [DocumentCode] [varchar](20) NOT NULL,
        [AccountReceivableDate] [datetime] NOT NULL,
        [AccountReceivableType] [int] NOT NULL,
        [PortfolioStatus] [int] NOT NULL,
        [PortfolioStatusName] [varchar](25) NULL,
        [NumberShares] [int] NOT NULL,
        [Term] [int] NOT NULL,
        [OpeningBalance] [int] NOT NULL,
        [ThirdPartyNit] [varchar](25) NOT NULL,
        [ThirdPartyName] [varchar](300) NOT NULL,
        [IdentificationType] [int] NOT NULL,
        [Category] [varchar](123) NULL,
        [Regimen] [varchar](50) NULL,
        [CareGroupCode] [varchar](20) NULL,
        [CareGroupName] [varchar](100) NULL,
        [ContractCode] [varchar](20) NULL,
        [ContractName] [varchar](100) NULL,
        [AccountWithoutRadicateNumber] [varchar](50) NULL,
        [RadicatedConsecutive] [int] NULL,
        [RadicatedUser] [varchar](20) NULL,
        [RadicatedDate] [datetime] NULL,
        [DocumentDate] [datetime] NULL,
        [RadicatedState] [char](1) NULL,
        [MainAccountNumber] [varchar](50) NULL,
        [MainAccountName] [varchar](300) NULL,
        [DocumentValue] [numeric](18, 2) NOT NULL,
        [RetentionValue] [numeric](38, 2) NULL,
        [InitialValue] [numeric](38, 2) NULL,
        [DebitValue] [numeric](38, 2) NULL,
        [CreditValue] [numeric](38, 2) NULL,
        [TransferValue] [decimal](38, 2) NOT NULL,
        [CashReceiptValue] [decimal](38, 2) NOT NULL,
        [CrossingValue] [decimal](38, 2) NOT NULL,
        [Balance] [numeric](38, 2) NULL,
        [CurrentBalance] [numeric](18, 2) NOT NULL,
        [CenterAttention] [char](100) NULL,
        [ExpiredDate] [datetime] NULL,
        [Age] [int] NULL,
        [RegimenCalculated] [varchar](max) NULL,
        [ValueGlosado] [money] NOT NULL,
        [ValueAcceptedFirstInstance] [money] NOT NULL,
        [ValueAcceptedSecondInstance] [money] NOT NULL,
        [GlosaState] [tinyint] NULL,
        [GlosaStateName] [varchar](35) NULL,
        [CurrencyId] [int] NULL,
        [CurrencyName] [varchar](20) NULL
    )

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
		from GeneralLedger.CompanySettings WITH (NOLOCK)
		

		-------------------------------------------------------------------------------------------------
		

		IF ISNULL(@OperatingUnits, '') <> ''
		BEGIN
			SET @FilterByOperatingUnit = 1

			-- Insertar los valores en @Table_OperatingUnit
			INSERT INTO #Table_OperatingUnit (Id)
				SELECT CAST(Data AS INT)
				FROM dbo.Split(@OperatingUnits, ',')
		END

		IF ISNULL(@PersonTypes, '') <> ''
		BEGIN
			SET @FilterByPersonType = 1

			-- Insertar los valores en @Table_PersonType
			INSERT INTO #Table_PersonType (Id)
				SELECT CAST(Data AS INT)
				FROM dbo.Split(@PersonTypes, ',')
		END

		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParty = 1

			 --Insertar los valores en @Table_ThirdParty
			INSERT INTO #Table_ThirdParty (Id)
				SELECT CAST(Data AS INT)
				FROM dbo.Split(@ThirdParties, ',')
		
		END

		IF ISNULL(@DocumentTypes, '') <> ''
		BEGIN
			SET @FilterByDocumentType = 1

			-- Insertar los valores en @Table_DocumentType
			INSERT INTO #Table_DocumentType (Id)
				SELECT CAST(Data AS INT)
				FROM dbo.Split(@DocumentTypes, ',')
		END

		IF ISNULL(@Status, '') <> '' 
		BEGIN
			-- Insertar los valores en @Table_Status
			INSERT INTO #Table_Status (Id)
				SELECT CAST(Data AS INT)
				FROM dbo.Split(@Status, ',')
		-- Establecer @FilterByStatus dependiendo de si el ID 0 existe
			SET @FilterByStatus = IIF(EXISTS(SELECT 1 FROM #Table_Status WHERE Id = 0), 0, 1)
		END   		                  					

					/********************************** OBTENCION DE DATOS **********************************/
		--Reporte Cartera por edades por CXC
       IF @GroupOrDetailByAccount = 0 BEGIN
			WITH Ctr_FilteredData AS (
									SELECT tp.Nit,tp.Name,tp.PersonType,p.IdentificationType, ar.*
									FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
										JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
										JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id											
   									WHERE ar.Balance <> 0
										AND (@FilterByOperatingUnit = 0 OR EXISTS (SELECT 1 FROM #Table_OperatingUnit tou WHERE ar.OperatingUnitId = tou.Id))
										AND (@FilterByPersonType = 0 OR EXISTS (SELECT 1 FROM #Table_PersonType tpt WHERE tp.PersonType = tpt.Id))
										AND (@FilterByThirdParty = 0 OR EXISTS (SELECT 1 FROM #Table_ThirdParty tc WHERE ar.ThirdPartyId = tc.Id))
										AND (@FilterByDocumentType = 0 OR EXISTS (SELECT 1 FROM #Table_DocumentType tdt WHERE ar.AccountReceivableType = tdt.Id))
										AND (@FilterByStatus = 0 OR EXISTS (SELECT 1 FROM #Table_Status ts WHERE ar.PortfolioStatus = ts.Id))  AND @GroupOrDetailByAccount =0
								)					
					

		  INSERT INTO #ReportPortfolioByAge

		  SELECT	
		  	CASE @OrderBy
		  		WHEN 1 THEN CAST(IIF(d.AccountReceivableType = 0, 'ZZZZZZZZZZZZZZZZZZZZ', d.DocumentCode) AS VARCHAR(20))
		  		WHEN 2 THEN CONVERT(VARCHAR(20), IIF(d.AccountReceivableType = 0, @ClosingDate, d.AccountReceivableDate), 112)
		  		WHEN 3 THEN d.ThirdPartyNit
		  		WHEN 4 THEN d.RegimenCalculated
		  		ELSE RIGHT('00000' + CAST(d.Age AS VARCHAR(5)), 5)
		  	END AS OrderBy,
		  	IIF(d.AccountReceivableType = 0, 99, 1) AS OrderByAdvanceAtEnd,
		  	d.*
		  FROM (
		  	SELECT 
		  		ar.Id,
		  		ar.InvoiceNumber AS DocumentCode, 
		  		ar.AccountReceivableDate, 
		  		ar.AccountReceivableType,
		  		ar.PortfolioStatus,
		  		ar.PortfolioStatusName,
		  		ar.NumberShares,
		  		ar.Term,
		  		ar.OpeningBalance,
		  		ar.Nit AS ThirdPartyNit, 
		  		ar.Name AS ThirdPartyName, 
		  		ar.IdentificationType,
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
		  	  CASE WHEN @isValorization = 0 THEN ar.DocumentValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.DocumentValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS DocumentValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.RetentionValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.RetentionValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS RetentionValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.InitialValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.InitialValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS InitialValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.DebitValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.DebitValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS DebitValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.CreditValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.CreditValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS CreditValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.TransferValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.TransferValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS TransferValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.CashReceiptValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.CashReceiptValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS CashReceiptValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.CrossingValue 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.CrossingValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS CrossingValue,
		  		   CASE WHEN @isValorization = 0 THEN ar.Balance 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.Balance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS Balance,
		  		   CASE WHEN @isValorization = 0 THEN ar.CurrentBalance 
		  		   	 ELSE Common.CurrencyConverterWithDate(ar.CurrentBalance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate) 
		  		   END AS CurrentBalance,
		  		c.NOMCENATE AS CenterAttention,
		  		DATEADD(DAY, ar.Term, IIF(@CalculateAgeBy = 1, ar.AccountReceivableDate, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate))) AS ExpiredDate,
		  		IIF(@CalculateAgeBy = 1,
		  			DATEDIFF(DAY, DATEADD(DAY, ar.Term, ar.AccountReceivableDate), @ClosingDate),
		  			DATEDIFF(DAY, ISNULL(DATEADD(DAY, ar.Term, ISNULL(ar.RadicatedDate, ar.AccountReceivableDate)), @ClosingDate), @ClosingDate)) AS Age,
		  		ar.RegimenCalculated,
		  		ISNULL(gpg.ValueGlosado, 0) AS ValueGlosado,
		  		ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) AS ValueAcceptedFirstInstance,
		  		ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) AS ValueAcceptedSecondInstance,
		  		gpg.State AS GlosaState,
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
		  		END AS GlosaStateName,
		  		 iif(@isValorization=0, ar.CurrencyId, @ToCurrency)  CurrencyId
		  		,iif(@isValorization=0, ar.CurrencyName, cu.Name)  CurrencyName
		  	FROM Ctr_FilteredData AS ar	  	
		  	LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
		  	LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
		  	LEFT JOIN Contract.CompanyType AS ctt ON ctt.Id = cg.EntityType 
		  	LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
		  	LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
		  	LEFT JOIN Portfolio.RadicateInvoiceC ric WITH (NOLOCK) ON ric.RadicatedConsecutive = ar.RadicatedConsecutive
		  	LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
		  	LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
		  	LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
		  	LEFT JOIN Common.Currency cu ON cu.Id = @ToCurrency	
		  
		  
		  ) AS d
		  	ORDER BY OrderBy   		  			
		  
		END
		  
		IF @IncludeAdvance = 1 
			BEGIN
				
			INSERT INTO #ReportPortfolioByAge
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
					SELECT pa.Id,
							pa.Code AS DocumentCode, 
							pa.DocumentDate AS AccountReceivableDate, 
							0 AS AccountReceivableType,
							0 AS PortfolioStatus,
							NULL AS PortfolioStatusName,
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
							NULL AS RadicatedState,
							ma.Number AS MainAccountNumber, 
							ma.Name AS MainAccountName,
							CASE 
								WHEN @isValorization = 0 THEN pa.DocumentValue 
								ELSE Common.CurrencyConverterWithDate(pa.DocumentValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS DocumentValue,
							0 AS RetentionValue,
							0 AS InitialValue,
							CASE 
								WHEN @isValorization = 0 THEN pa.DebitValue 
								ELSE Common.CurrencyConverterWithDate(pa.DebitValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS DebitValue,
							CASE 
								WHEN @isValorization = 0 THEN pa.CreditValue 
								ELSE Common.CurrencyConverterWithDate(pa.CreditValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS CreditValue,
							CASE 
								WHEN @isValorization = 0 THEN pa.TransferValue 
								ELSE Common.CurrencyConverterWithDate(pa.TransferValue, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS TransferValue,
							0 AS CashReceiptValue,
							0 AS CrossingValue,
							CASE 
								WHEN @isValorization = 0 THEN pa.Balance 
								ELSE Common.CurrencyConverterWithDate(pa.Balance, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS Balance,
							CASE 
								WHEN @isValorization = 0 THEN pa.CurrentBalance 
								ELSE Common.CurrencyConverterWithDate(pa.CurrentBalance, ISNULL(pa.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
							END AS CurrentBalance,
							NULL AS CenterAttention,
							@ClosingDate AS ExpiredDate,
							99999 AS Age,
							'' AS RegimenCalculated,
							0 AS ValueGlosado,
							0 AS ValueAcceptedFirstInstance,
							0 AS ValueAcceptedSecondInstance,
							NULL AS GlosaState,
							NULL AS GlosaStateName,
							CASE 
								WHEN @isValorization = 0 THEN pa.CurrencyId 
								ELSE @ToCurrency
							END AS CurrencyId,
							CASE 
								WHEN @isValorization = 0 THEN pa.CurrencyName 
								ELSE cu.Name
							END AS CurrencyName
					FROM [Portfolio].[GetPortfolioAdvanceByAge](@ClosingDate) pa
					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
					OUTER APPLY (
						SELECT mac.Id
						FROM GeneralLedger.MainAccountClasses mac
						WHERE mac.Id = ma.IdAccountClass AND mac.Type = 1
					) AS mac							
					LEFT JOIN Common.Currency cu ON cu.Id = @ToCurrency
					WHERE @IncludeAdvance = 1							
						AND (@FilterByPersonType = 0 OR EXISTS (SELECT 1 FROM #Table_PersonType tpt WHERE pa.PersonType = tpt.Id))
						AND (@FilterByThirdParty = 0 OR EXISTS (SELECT 1 FROM #Table_ThirdParty tc WHERE pa.ThirdPartyId = tc.Id))
					AND pa.Balance <> 0

				) d
			END

				--	--cartera por edades a nivel por Cuenta
            IF @GroupOrDetailByAccount = 1 BEGIN

					WITH Ctr_FilteredData AS (
											   SELECT tp.Nit,
													  tp.Name,
													  tp.PersonType,
													  p.IdentificationType,
													  ar.*
												FROM Portfolio.GetAccountReceivableByAgeDetail(NULL, @ClosingDate) AS ar
													JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
													JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id											
   												WHERE ar.Balance <> 0 
													AND (@FilterByOperatingUnit = 0 OR EXISTS (SELECT 1 FROM #Table_OperatingUnit tou WHERE ar.OperatingUnitId = tou.Id))
													AND (@FilterByPersonType = 0 OR EXISTS (SELECT 1 FROM #Table_PersonType tpt WHERE tp.PersonType = tpt.Id))
													AND (@FilterByThirdParty = 0 OR EXISTS (SELECT 1 FROM #Table_ThirdParty tc WHERE ar.ThirdPartyId = tc.Id))
													AND (@FilterByDocumentType = 0 OR EXISTS (SELECT 1 FROM #Table_DocumentType tdt WHERE ar.AccountReceivableType = tdt.Id))
													AND (@FilterByStatus = 0 OR EXISTS (SELECT 1 FROM #Table_Status ts WHERE ar.PortfolioStatus = ts.Id)) AND @GroupOrDetailByAccount =1
											 ) 

					INSERT INTO #ReportPortfolioByAge
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
									ar.Nit AS ThirdPartyNit, 
									ar.Name AS ThirdPartyName, 
									ar.IdentificationType,
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
									CASE 
									   WHEN @isValorization = 0 THEN ar.DocumentValue 
									   ELSE Common.CurrencyConverterWithDate(ar.DocumentValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS DocumentValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.RetentionValue 
									   ELSE Common.CurrencyConverterWithDate(ar.RetentionValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS RetentionValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.InitialValue 
									   ELSE Common.CurrencyConverterWithDate(ar.InitialValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS InitialValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.DebitValue 
									   ELSE Common.CurrencyConverterWithDate(ar.DebitValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS DebitValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.CreditValue 
									   ELSE Common.CurrencyConverterWithDate(ar.CreditValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS CreditValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.TransferValue 
									   ELSE Common.CurrencyConverterWithDate(ar.TransferValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS TransferValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.CashReceiptValue 
									   ELSE Common.CurrencyConverterWithDate(ar.CashReceiptValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS CashReceiptValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.CrossingValue 
									   ELSE Common.CurrencyConverterWithDate(ar.CrossingValue, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS CrossingValue,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.Balance 
									   ELSE Common.CurrencyConverterWithDate(ar.Balance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS Balance,
								   CASE 
									   WHEN @isValorization = 0 THEN ar.CurrentBalance 
									   ELSE Common.CurrencyConverterWithDate(ar.CurrentBalance, ISNULL(ar.CurrencyId, @OfficialCurrency), @ToCurrency, @ClosingDate)
								   END AS CurrentBalance,
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
							FROM Ctr_FilteredData AS ar
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
							LEFT JOIN Common.Currency cu on cu.Id = @ToCurrency							
							

					) AS d
					ORDER BY OrderBy
					--RETURN
				END
				
				--muestra la informacion
				SELECT *
				FROM #ReportPortfolioByAge d
				ORDER BY d.OrderBy

		
        -- Eliminamos La tabla temporal
	    IF OBJECT_ID('tempdb..#ReportPortfolioByAge') IS NOT NULL DROP TABLE #ReportPortfolioByAge
		
	END TRY
	BEGIN CATCH
	 --Eliminamos La tabla temporal
	    IF OBJECT_ID('tempdb..#ReportPortfolioByAge') IS NOT NULL DROP TABLE #ReportPortfolioByAge
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de edades de cartera para la operación internacional, permitiendo analizar las cuentas por cobrar (CXC) vigentes y vencidas agrupadas por rangos de antigüedad (días de mora). Recibe criterios de cálculo como la moneda de valorización, el criterio de antigüedad y filtros de unidades operativas, tipo de persona, terceros, tipos de documento y estado de la cartera, todos enviados como XML. Consulta el motor de cartera a través de la función Portfolio.GetAccountReceivableByAge y cruza con los datos de terceros (aseguradoras, contratistas, pacientes) de Common.ThirdParty y Common.Person para obtener el NIT, nombre, tipo de identificación y tipo de persona del deudor. El resultado sirve para reportería financiera y de cobranza: muestra saldos, valores de glosa, recaudos, créditos, anticipos y el balance final por documento, contrato y tercero, con la opción de presentar el detalle por movimiento contable o agrupado por cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioByAge_International';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioByAge_International';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el reporte internacional de cartera por edades, opcionalmente valorizando los saldos a una moneda destino, calculando la edad por fecha de CXC o de radicación, incluyendo anticipos y enriqueciendo con datos de glosas, contratos y centros de atención.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML @xmlCriterias y @xmlFilters deben tener nodo /Data con los elementos esperados (CalculateAgeBy, IncludeAdvance, OrderBy, GroupOrDetailByAccount, isValorization, ToCurrency, ClosingDate, OperatingUnits, PersonTypes, ThirdParties, DocumentTypes, Status).; GeneralLedger.CompanySettings debe contener al menos un registro con OfficialCurrencyId definido para servir como moneda por defecto cuando el registro no tiene CurrencyId.; Las listas de IDs de filtros deben ser cadenas separadas por coma convertibles a INT por dbo.Split.; Common.Currency debe contener el Id correspondiente a @ToCurrency cuando se usa valorización.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros con Balance distinto de cero (ar.Balance <> 0 y pa.Balance <> 0).; La moneda oficial usada como fallback proviene de GeneralLedger.CompanySettings.OfficialCurrencyId.; Cuando AccountReceivableType = 0 (anticipo), el OrderBy textual se fuerza a ''ZZZZZ...'' o a la fecha de cierre y OrderByAdvanceAtEnd = 99 para empujarlo al final del reporte.; Las glosas solo se aceptan en primera/segunda instancia si su EvaluationDateGlosa/EvaluationDateReiteration es <= @ClosingDate; en caso contrario el valor aceptado se reporta como 0.; Solo se enlazan glosas cuya RadicatedDate (cast a DATE) sea <= @ClosingDate.; Si gpg.State no coincide con un código conocido, GlosaStateName se reporta como ''No esta Glosada''.; El filtro por unidad operativa y por tipo de documento no aplica a los anticipos (solo se filtra por PersonType y ThirdParty).; La tabla temporal #ReportPortfolioByAge se elimina tanto al finalizar el flujo exitoso como en el bloque CATCH.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera por edades; Cuentas por cobrar (CXC); Anticipos de cartera; Glosas (primera y segunda instancia, reiteración, conciliación); Radicación de facturas; Contratos y grupos de atención; Categorías de factura / régimen; Centros de atención y admisiones de pacientes; Valorización / conversión de moneda; Moneda oficial de la compañía; Tercero / NIT / tipo de identificación', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @GroupOrDetailByAccount = 0 → Genera el reporte agrupado por CXC consultando Portfolio.GetAccountReceivableByAge else Si @GroupOrDetailByAccount = 1, genera el reporte detallado por movimiento contable consultando Portfolio.GetAccountReceivableByAgeDetail; si @IncludeAdvance = 1 → Inserta adicionalmente los anticipos obtenidos de Portfolio.GetPortfolioAdvanceByAge marcándolos con AccountReceivableType=0, Age=99999 y OrderByAdvanceAtEnd=99 para que aparezcan al final; si @isValorization = 1 → Convierte todos los valores monetarios (DocumentValue, Balance, etc.) a la moneda @ToCurrency usando Common.CurrencyConverterWithDate con la @ClosingDate y la moneda oficial como fallback cuando CurrencyId es nulo else Si @isValorization = 0, conserva los valores y la moneda originales del registro; si @CalculateAgeBy = 1 → Calcula la edad y la fecha de vencimiento a partir de AccountReceivableDate + Term else Calcula la edad y vencimiento usando ISNULL(RadicatedDate, AccountReceivableDate) + Term; si @OrderBy IN (1,2,3,4) → Ordena por DocumentCode, AccountReceivableDate (yyyymmdd), ThirdPartyNit o RegimenCalculated respectivamente else Ordena por Age (formato numérico de 5 dígitos con padding); si EXISTS(SELECT 1 FROM #Table_Status WHERE Id = 0) → Desactiva el filtro por estado (@FilterByStatus = 0) else Activa el filtro por estado (@FilterByStatus = 1); si Cada filtro de entrada (OperatingUnits, PersonTypes, ThirdParties, DocumentTypes, Status) viene no vacío → Lo split-ea por coma con dbo.Split y carga la tabla temporal correspondiente, activando el flag de filtrado', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; dbo.Split; Portfolio.GetAccountReceivableByAge; Common.ThirdParty; Common.Person; Billing.InvoiceCategories; Contract.CareGroup; Contract.CompanyType; Contract.Contract; GeneralLedger.MainAccounts; Portfolio.RadicateInvoiceC; Glosas.GlosaPortfolioGlosada; dbo.ADINGRESO; dbo.ADCENATEN; Common.Currency; Portfolio.GetPortfolioAdvanceByAge; GeneralLedger.MainAccountClasses; Portfolio.GetAccountReceivableByAgeDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge_International';
-- GO
