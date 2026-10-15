
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-06
-- Description:	Procedimiento para el reporte de edades de cartera
-- SP DE CARTERA - colombia
-- =============================================
-- =============================================
-- ÍNDICES RECOMENDADOS PARA ESTE SP
-- =============================================
/*
-- 1. GlosaPortfolioGlosada
CREATE INDEX IX_GlosaPortfolioGlosada_Invoice_RadicatedDate
    ON Glosas.GlosaPortfolioGlosada (InvoiceNumber, RadicatedDate)
    INCLUDE (ValueGlosado, EvaluationDateGlosa, ValueAcceptedFirstInstance,
             EvaluationDateReiteration, ValueAcceptedSecondInstance, State);

-- 2. RadicateInvoiceC
CREATE INDEX IX_RadicateInvoiceC_RadicatedConsecutive
    ON Portfolio.RadicateInvoiceC (RadicatedConsecutive)
    INCLUDE (DocumentDate);

-- 3. CareGroup
CREATE INDEX IX_CareGroup_Id_Covering
    ON Contract.CareGroup (Id)
    INCLUDE (Code, Name, EntityType, ContractId);

-- 4. Contract
CREATE INDEX IX_Contract_Id_Covering
    ON Contract.Contract (Id)
    INCLUDE (Code, ContractName);

-- 5. ThirdParty
CREATE INDEX IX_ThirdParty_PersonType
    ON Common.ThirdParty (PersonType)
    INCLUDE (Id, Nit, Name, PersonId);

-- 6. ADINGRESO
CREATE INDEX IX_ADINGRESO_NUMINGRES
    ON dbo.ADINGRESO (NUMINGRES)
    INCLUDE (CODCENATE);
*/
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioByAge_Native]
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

	-- Los índices no clustered son redundantes: la PRIMARY KEY ya genera un clustered index en Id


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

		
		----Se obtiene la moneda oficial
		--Select  @OfficialCurrency = OfficialCurrencyId
		--from GeneralLedger.CompanySettings WITH (NOLOCK)
		

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
										AND (@FilterByStatus = 0 OR EXISTS (SELECT 1 FROM #Table_Status ts WHERE ar.PortfolioStatus = ts.Id))
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
		  		ar.DocumentValue,
		  		ar.RetentionValue,
		  		ar.InitialValue,
		  		ar.DebitValue,
		  		ar.CreditValue,
		  		ar.TransferValue,
		  		ar.CashReceiptValue,
		  		ar.CrossingValue,
		  		ar.Balance,
		  		ar.CurrentBalance,
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
		  	LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND gpg.RadicatedDate < DATEADD(DAY, 1, CAST(@ClosingDate AS DATETIME))
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
						pa.DocumentValue,
						0 AS RetentionValue,
						0 AS InitialValue,
						pa.DebitValue,
						pa.CreditValue,
						pa.TransferValue,
						0 AS CashReceiptValue,
						0 AS CrossingValue,
						pa.Balance,
						pa.CurrentBalance,
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
					WHERE (@FilterByPersonType = 0 OR EXISTS (SELECT 1 FROM #Table_PersonType tpt WHERE pa.PersonType = tpt.Id))
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
											AND (@FilterByStatus = 0 OR EXISTS (SELECT 1 FROM #Table_Status ts WHERE ar.PortfolioStatus = ts.Id))
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
								ar.DocumentValue,
								ar.RetentionValue,
								ar.InitialValue,
								ar.DebitValue,
								ar.CreditValue,
								ar.TransferValue,
								ar.CashReceiptValue,
								ar.CrossingValue,
								ar.Balance,
								ar.CurrentBalance,
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
						LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND gpg.RadicatedDate < DATEADD(DAY, 1, CAST(@ClosingDate AS DATETIME))
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