-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-06
-- Description:	Procedimiento almacenado para conciliar inventarios con contabilidad
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcileInventory]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	set DATEFORMAT DMY

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@TypeReport INT,
			@EntityNames VARCHAR(MAX),
			@JournalVoucherTypes VARCHAR(MAX),
			@MainAccounts VARCHAR(MAX),
			@CompanySettingsId INT,
			-------------
			@FilterByEntityName BIT = 0,
			@FilterByJournalVoucherTypes BIT = 0,
			@FilterByMainAccounts BIT = 0
	
	DECLARE @Table_EntityNames AS TABLE(EntityName VARCHAR(250))
	DECLARE @Table_JournalVoucherTypes AS TABLE(Id INT)
	DECLARE @Table_MainAccounts AS TABLE(Id INT)
	DECLARE @InventoryAccount AS TABLE(MainAccountId INT)

	BEGIN TRY

		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@TypeReport = t.x.value('TypeReport[1]','int'),
				@EntityNames = t.x.value('EntityNames[1]','varchar(max)'),
				@JournalVoucherTypes = t.x.value('JournalVoucherTypes[1]','varchar(max)'),
				@MainAccounts = t.x.value('MainAccounts[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@EntityNames, '') <> ''
		BEGIN
			SET @FilterByEntityName = 1

			INSERT INTO @Table_EntityNames
				SELECT Data Data 
				FROM dbo.Split(@EntityNames, ',')
		END

		IF ISNULL(@JournalVoucherTypes, '') <> ''
		BEGIN
			SET @FilterByJournalVoucherTypes = 1

			INSERT INTO @Table_JournalVoucherTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@JournalVoucherTypes, ',')
		END

		IF ISNULL(@MainAccounts, '') <> ''
		BEGIN
			SET @FilterByMainAccounts = 1

			INSERT INTO @Table_MainAccounts
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@MainAccounts, ',')
		END

		INSERT INTO @InventoryAccount
			SELECT apc.IdAccount
			FROM Payments.AccountPayableConcepts apc WITH (NOLOCK)
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON apc.Id = pg.InventoryAccountPayableConceptId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON pg.Id = ip.ProductGroupId
			JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON ip.Id = phy.ProductId
			GROUP BY apc.IdAccount

			select Top(1) @CompanySettingsId = Id  from GeneralLedger.CompanySettings

		/******************************************  OBTENCION DE DATOS ******************************************/

		SELECT	k.DocumentDate,
				jv.DocumentDate AccountingDocumentDate,
				en.Description,
				ISNULL(k.EntityName, jv.EntityName) EntityName,
				ISNULL(k.EntityCode, jv.EntityCode) EntityCode,
				jv.JournalVoucherType,
				jv.Consecutive,
				ISNULL(k.MainAccount, jv.MainAccount) MainAccount,
				--------------------------------------------------------			
				ISNULL(k.DebitValue, 0) DebitValue,
				Common.CurrencyConverterWithDate(ISNULL(jv.DebitValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate) AccountingDebitValue, 
				--------------------------------------------------------
				ISNULL(k.CreditValue, 0) CreditValue,
				Common.CurrencyConverterWithDate(ISNULL(jv.CreditValue, 0),jv.OfficialCurrencyId,ISNULL(k.CurrencyId,jv.OfficialCurrencyId),jv.CreationDate) AccountingCreditValue, 
				isnull(k.CurrencyId,jv.OfficialCurrencyId) CurrencyId,--12
				cy.Abbreviation
		FROM
		(
				SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
						k.EntityName, k.EntityCode, k.EntityId,
						CAST(k.DocumentDate AS DATE) DocumentDate,
						SUM(IIF(k.MovementType = 1, k.Quantity * k.Value, 0)) DebitValue, 
						SUM(IIF(k.MovementType = 1, 0, k.Quantity * k.Value)) CreditValue,
						cs.OfficialCurrencyId CurrencyId
				FROM Inventory.Warehouse w WITH (NOLOCK)
				JOIN Inventory.Kardex k WITH (NOLOCK) ON w.Id = k.WarehouseId
				JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON k.ProductId = ip.Id
				JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
				JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON pg.InventoryAccountPayableConceptId = apc.Id
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = apc.IdAccount
				join GeneralLedger.CompanySettings cs on cs.Id = @CompanySettingsId
				WHERE w.WarehouseConsignment = 0
					AND w.VirtualStore = 0
					AND w.CustodyStore = 0
					AND ISNULL(k.EntityName, '') NOT IN ('RemissionEntrance', 'ConsignmentInventoryRemission', 'RemissionDevolution', 'TransferOrder', 'TransferOrderDevolution')
					AND CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
							k.EntityName, k.EntityCode, k.EntityId,
							CAST(k.DocumentDate AS DATE),cs.OfficialCurrencyId
		) k
		FULL JOIN
		(
			SELECT	ma.Id MainAccountId, CONCAT(ma.Number, ' - ', ma.Name) MainAccount,
					COALESCE(ap.EntityName, pn.EntityName, jv.EntityName) EntityName, 
					COALESCE(ap.EntityCode, pn.EntityCode, jv.EntityCode) EntityCode, 
					COALESCE(ap.EntityId, pn.EntityId, jv.EntityId) EntityId,
					CAST(jv.VoucherDate AS DATE) DocumentDate,					
					jv.Consecutive, 
					jvt.Id JournalVoucherTypeId,
					CONCAT(jvt.Code, ' - ', jvt.Name) JournalVoucherType,
					SUM(jvd.DebitValue) DebitValue, 
					SUM(jvd.CreditValue) CreditValue,
					lb.OfficialCurrencyId,
					jv.CreationDate
			FROM GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK)
			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvt.Id = jv.IdJournalVoucher
			JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
			join GeneralLedger.LegalBook lb WITH (NOLOCK) on jv.LegalBookId = lb.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
			JOIN @InventoryAccount ia ON ma.Id = ia.MainAccountId
			LEFT JOIN Payments.AccountPayable ap WITH (NOLOCK) ON jv.EntityId = ap.Id AND jv.EntityName = 'AccountPayable'
			LEFT JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON jv.EntityId = pn.Id AND jv.EntityName = 'PaymentNotes'
			WHERE jv.Status = 2
				AND ISNULL(jv.EntityName, '') NOT IN ('TransferOrder', 'TransferOrderDevolution')
				AND CAST(jv.VoucherDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY	ma.Id, CONCAT(ma.Number, ' - ', ma.Name),
						COALESCE(ap.EntityName, pn.EntityName, jv.EntityName), 
						COALESCE(ap.EntityCode, pn.EntityCode, jv.EntityCode), 
						COALESCE(ap.EntityId, pn.EntityId, jv.EntityId), 
						CAST(jv.VoucherDate AS DATE), 
						jv.Consecutive, jvt.Id, CONCAT(jvt.Code, ' - ', jvt.Name),lb.OfficialCurrencyId,jv.CreationDate
		) jv ON ISNULL(k.EntityName, '') = ISNULL(jv.EntityName, '') 
			AND ISNULL(k.EntityId, 0) = ISNULL(jv.EntityId, 0) 
			AND k.MainAccountId = jv.MainAccountId
		LEFT JOIN Common.GetEntityNameDescriptions() en ON ISNULL(k.EntityName, jv.EntityName) = en.EntityName
		LEFT JOIN @Table_EntityNames ten ON ISNULL(k.EntityName, jv.EntityName) = ten.EntityName
		LEFT JOIN @Table_JournalVoucherTypes tjvt ON jv.JournalVoucherTypeId = tjvt.Id
		LEFT JOIN @Table_MainAccounts tma ON ISNULL(k.MainAccountId, jv.MainAccountId) = tma.Id
		JOIN Common.Currency cy WITH(NOLOCK) on cy.Id = ISNULL(k.CurrencyId,jv.OfficialCurrencyId)
		WHERE (@FilterByEntityName = 0 OR ten.EntityName IS NOT NULL)
			AND (@FilterByJournalVoucherTypes = 0 OR tjvt.Id IS NOT NULL)
			AND (@FilterByMainAccounts = 0 OR tma.Id IS NOT NULL)
			AND 
			(
				@TypeReport = 3
				OR
				(
					@TypeReport = 2
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) = ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) = ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
				OR
				(
					@TypeReport = 1
					AND
					(
						ROUND(ISNULL(k.DebitValue, 0), 2) <> ROUND(ISNULL(jv.DebitValue, 0), 2)
						OR 
						ROUND(ISNULL(k.CreditValue, 0), 2) <> ROUND(ISNULL(jv.CreditValue, 0), 2)
					)
				)
			)
		ORDER BY	ISNULL(k.DocumentDate, jv.DocumentDate), 
					ISNULL(k.EntityName, jv.EntityName), 
					ISNULL(k.EntityCode, jv.EntityCode),
					ISNULL(k.MainAccount, jv.MainAccount),
					ISNULL(k.CurrencyId, jv.OfficialCurrencyId)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END