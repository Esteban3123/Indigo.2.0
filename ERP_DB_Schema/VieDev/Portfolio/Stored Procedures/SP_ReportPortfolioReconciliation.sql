-- =============================================
-- Author:		Giovanny plazas
-- Create date: 2021-02-08
-- Description:	Procedimiento para el reporte de alistamiento de conciliacion de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioReconciliation]

		@HisContainer AS VARCHAR(20),
		@xmlCriterias AS XML,
		@xmlFilters AS XML				
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE 	
			-- CRITERIOS --
			@IncludeAdvance INT,
			-- FILTROS --
			@ThirdParties VARCHAR(MAX),
			@OperatingUnits VARCHAR(MAX),
			@DocumentTypes VARCHAR(MAX),
			@Status VARCHAR(MAX),
			@InvoiceCategories VARCHAR(MAX),
			@CareGroup VARCHAR(MAX),
			@ClosingDate DATE,
			
			---------------------------------------------------------------------------------------
			@FilterByOperatingUnit BIT = 0,
			@FilterByInvoiceCategories BIT = 0,
			@FilterByCareGroup BIT = 0,
			@FilterByThirdParty BIT = 0,
			@FilterByDocumentType BIT = 0,
			@FilterByStatus BIT = 0

	DECLARE @Table_OperatingUnit AS TABLE(Id INT)
	DECLARE @Table_InvoiceCategories AS TABLE(Id INT)
	DECLARE @Table_CareGroup AS TABLE(Id INT)
	DECLARE @Table_ThirdParty AS TABLE(Id INT)
	DECLARE @Table_DocumentType AS TABLE(Id INT)
	DECLARE @Table_Status AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@IncludeAdvance = t.x.value('IncludeAdvance[1]','bit')
				FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los datos de los filtros
		SELECT	@ClosingDate = t.x.value('ClosingDate[1]','date'),
				---------------------------------------------------------------------------------------
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)'),
				@OperatingUnits = t.x.value('OperatingUnits[1]','varchar(max)'),
				@DocumentTypes = t.x.value('DocumentTypes[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','varchar(max)'),
				@InvoiceCategories = t.x.value('InvoiceCategories[1]','varchar(max)'),
				@CareGroup = t.x.value('CareGroup[1]','varchar(max)')
				FROM @xmlFilters.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------------
		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParty = 1

			INSERT INTO @Table_ThirdParty
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		IF ISNULL(@OperatingUnits, '') <> ''
		BEGIN
			SET @FilterByOperatingUnit = 1

			INSERT INTO @Table_OperatingUnit
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@OperatingUnits, ',')
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
			SET @FilterByStatus = 1

			INSERT INTO @Table_Status
				SELECT CAST(Data AS INT) Data 		
				FROM dbo.Split(@Status, ',')
		END

		IF ISNULL(@InvoiceCategories, '') <> ''
		BEGIN
			SET @FilterByInvoiceCategories = 1

			INSERT INTO @Table_InvoiceCategories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@InvoiceCategories, ',')
		END

		IF ISNULL(@CareGroup, '') <> ''
		BEGIN
			SET @FilterByCareGroup = 1

			INSERT INTO @Table_CareGroup
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@CareGroup, ',')
		END
		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
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
						cg.EntityType AS Regimen, 
						cg.Code AS CareGroupCode,
						cg.Name AS CareGroupName, 
						ct.Code AS ContractCode,
						ct.ContractName AS ContractName,
						ar.AccountWithoutRadicateNumber, 
						ar.RadicatedConsecutive,
						ar.RadicatedUser,
						ar.RadicatedDate,
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
						ar.RegimenCalculated,
						gpg.PatientName As PatientName,
						gpg.Nit AS	PatientNit,
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
				FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
				JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
				JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
				LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
				LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
				LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
				LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
				/********************************** ******* **********************************/
				LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
				LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
				LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
				/**************************************** FILTROS ****************************************/
				LEFT JOIN @Table_OperatingUnit tou ON ar.OperatingUnitId = tou.Id
				LEFT JOIN @Table_InvoiceCategories tpt ON ar.InvoiceCategoryId = tpt.Id
				LEFT JOIN @Table_ThirdParty tc ON ar.ThirdPartyId = tc.Id				
				LEFT JOIN @Table_DocumentType tdt ON ar.AccountReceivableType = tdt.Id
				LEFT JOIN @Table_Status ts ON ar.PortfolioStatus = ts.Id
				LEFT JOIN @Table_CareGroup cp ON ar.CareGroupId = cp.Id
				WHERE (@FilterByOperatingUnit = 0 OR tou.Id IS NOT NULL)
					AND (@FilterByInvoiceCategories = 0 OR tpt.Id IS NOT NULL)
					AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
					AND (@FilterByDocumentType = 0 OR tdt.Id IS NOT NULL)
					AND (@FilterByStatus = 0 OR ts.Id IS NOT NULL)
					AND (@FilterByCareGroup = 0 OR cp.Id IS NOT NULL)
					AND ar.Balance <> 0
			
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
						NULL AS RadicatedUser,
						NULL AS RadicatedDate,
						NULL RadicatedState,
						ma.Number AS MainAccountNumber, 
						ma.Name AS MainAccountName,
						pa.DocumentValue, 
						0 RetentionValue,
						0 InitialValue,
						pa.DebitValue,
						pa.CreditValue,
						pa.TransferValue,
						0 AS CashReceiptValue,
						0 AS CrossingValue,
						pa.Balance,
						pa.CurrentBalance,
						NULL AS CenterAttention,
						'' AS RegimenCalculated,
						Null AS PatientName,
						NULL AS PatientNit,
						0 ValueGlosado,
						0 ValueAcceptedFirstInstance,
						0 ValueAcceptedSecondInstance,
						NULL GlosaState,
						NULL as GlosaStateName
				FROM [Portfolio].[GetPortfolioAdvanceByAge](@ClosingDate) pa
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON pa.MainAccountId = ma.Id
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id AND mac.Type = 1
				/**************************************** FILTROS ****************************************/
				LEFT JOIN @Table_ThirdParty tc ON pa.ThirdPartyId = tc.Id
				WHERE @IncludeAdvance = 1
					AND (@FilterByThirdParty = 0 OR tc.Id IS NOT NULL)
					AND pa.Balance <> 0

		) AS d
		ORDER BY 1, 2
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END