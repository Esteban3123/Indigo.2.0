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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para generar el reporte de conciliación entre inventario físico y contabilidad general, comparando los movimientos del kardex de inventario (entradas y salidas de productos, medicamentos e insumos por bodega) contra los comprobantes contables registrados en el libro mayor, dentro de un rango de fechas seleccionado. Cruza las cuentas contables asociadas a los grupos de productos del inventario (a través de los conceptos de cuentas por pagar) con los asientos contables del diario, permitiendo identificar diferencias o brechas entre el valor del inventario según el kardex y el saldo reflejado en contabilidad. Se utiliza para auditoría contable, cierre de período y conciliación de cuentas de inventario, admitiendo filtros por entidad, tipo de comprobante y cuenta contable principal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de conciliación entre los movimientos de inventario (Kardex) y los asientos contables del libro mayor para las cuentas asociadas a inventario, mostrando diferencias o coincidencias en débitos y créditos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener DateStart, DateEnd y TypeReport válidos.; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la moneda oficial.; Las listas EntityNames, JournalVoucherTypes y MainAccounts deben venir separadas por coma si se usan como filtro.; TypeReport debe ser 1 (diferencias), 2 (coincidencias) o 3 (todos).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera bodegas que no sean de consignación, virtuales ni de custodia (WarehouseConsignment=0, VirtualStore=0, CustodyStore=0).; Excluye del Kardex movimientos de tipo ''RemissionEntrance'', ''ConsignmentInventoryRemission'', ''RemissionDevolution'', ''TransferOrder'' y ''TransferOrderDevolution''.; Excluye de la contabilidad asientos cuya entidad sea ''TransferOrder'' o ''TransferOrderDevolution''.; Solo considera asientos contables con Status = 2 (aprobados/contabilizados).; Las cuentas a conciliar se restringen a las cuentas contables vinculadas a conceptos de cuentas por pagar usados en grupos de productos con inventario físico.; El MovementType=1 en Kardex se interpreta como débito; cualquier otro valor como crédito.; Los valores contables se convierten a la moneda del Kardex usando Common.CurrencyConverterWithDate con la fecha de creación del asiento.; La comparación de valores para conciliación se hace redondeando a 2 decimales.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación contable de inventarios; Kardex de inventario; Comprobantes contables (Journal Vouchers); Plan de cuentas (Main Accounts); Cuentas por pagar y notas de pago; Bodegas (consignación, virtuales, custodia); Moneda oficial y conversión de moneda; Libro legal contable; Grupos de productos e inventario físico; Movimientos de débito y crédito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve filas con fechas, entidades, cuenta principal, valores débito/crédito de Kardex y contabilidad, y abreviatura de moneda, según el TypeReport seleccionado.; [RETURN_RESULT] ResultSet: En caso de error capturado en CATCH, retorna una fila con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@EntityNames,'''') <> '''' → Activa filtro por nombres de entidad y carga la tabla temporal correspondiente.; si ISNULL(@JournalVoucherTypes,'''') <> '''' → Activa filtro por tipos de comprobante contable.; si ISNULL(@MainAccounts,'''') <> '''' → Activa filtro por cuentas contables principales.; si @TypeReport = 3 → Incluye todas las filas (Kardex y contabilidad), sin filtrar por coincidencia o diferencia.; si @TypeReport = 2 y débito o crédito Kardex = débito o crédito contable (redondeado a 2 decimales) → Incluye solo las filas donde los valores coinciden.; si @TypeReport = 1 y débito o crédito Kardex <> débito o crédito contable (redondeado a 2 decimales) → Incluye solo las filas con diferencias entre Kardex y contabilidad.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.CurrencyConverterWithDate; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayableConcepts; Inventory.ProductGroup; Inventory.InventoryProduct; Inventory.PhysicalInventory; GeneralLedger.CompanySettings; Inventory.Warehouse; Inventory.Kardex; GeneralLedger.MainAccounts; GeneralLedger.JournalVoucherTypes; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.LegalBook; Payments.AccountPayable; Payments.PaymentNotes; Common.Currency', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileInventory';
-- GO
