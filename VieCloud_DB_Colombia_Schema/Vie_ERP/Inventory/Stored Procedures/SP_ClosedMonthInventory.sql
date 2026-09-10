-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-10-26
-- Description:	sp para procesar el cierre mensual de inventarios
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ClosedMonthInventory]	
	@MonthClosed AS INT,
	@YearClosed AS INT, 
	@CodeUser VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
/* -------------------- DECLARACION DE VARIABLES -------------------- */

		-- Creamos la tabla temporal donde almacenaremos los saldos de inventario
		DECLARE
			@tableInventory TABLE( 
				[Id] INT PRIMARY KEY, 
				[EntityName] VARCHAR(250),
				[EntityId] INT,

				[ProductGroupId] INT,
				
				[DebitValue] DECIMAL(32,4) DEFAULT(0),
				[CreditValue] DECIMAL(32,4) DEFAULT(0)				
			)

		-- Creamos la tabla temporal donde almacenaremos los saldos de contabilidad
		DECLARE
			@tableAccounting TABLE( 
				[Id] INT PRIMARY KEY, 
				[EntityName] VARCHAR(250),
				[EntityId] INT,

				[MainAccount] INT,

				[DebitValue] DECIMAL(32,4) DEFAULT(0),
				[CreditValue] DECIMAL(32,4) DEFAULT(0)				
			)

		-- Creamos la tabla temporal donde almacenaremos los control de movimientos para el ajuste de inventarios
		DECLARE
			@tableClosedMonthControl TABLE ( 
				[Id] INT IDENTITY(1,1) PRIMARY KEY, 
				[Value] [decimal](20, 4) NOT NULL,
				[Nature] [int] NOT NULL,
				[MainAccountId] [int] NOT NULL,
				[ThirdPartyId] [int] NULL,
				[CostCenterId] [int] NULL,
				[EntityId] [int] NOT NULL,
				[EntityName] [varchar](250) NOT NULL
			)

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		DECLARE
			@JournalVourcherTmp TABLE( 
				[Id] INTEGER, 
				[Consecutive] BIGINT, 
				[LegalBookId] INTEGER, 
				[IdJournalVoucher] INTEGER, 
				[VoucherDate] VARCHAR(30), 
				[Imported] VARCHAR(5), 
				[Status] TINYINT, 
				[Detail] VARCHAR(500), 
				[EntityCode] VARCHAR(20), 
				[EntityId] INTEGER, 
				[EntityName] VARCHAR(250), 
				[IsClosedYear] TINYINT
			)

		--Se declara una tabla temporal para los detalles del comprobante
		DECLARE 
			@JournalVourcherDetailTmp TABLE(
				[Id] INTEGER DEFAULT(0), 
				[IdAccounting] INTEGER DEFAULT(0), 
				[IdMainAccount] INTEGER, 
				[IdThirdParty] INTEGER, 
				[IdCostCenter] INTEGER,
				[DebitValue] DECIMAL(20,4), 
				[CreditValue] DECIMAL(20,4), 
				[Detail] VARCHAR(500), 
				[IdRetention] INTEGER, 
				[RetentionRate] DECIMAL(5,3) DEFAULT(0), 
				[BaseValue] DECIMAL(18,0) DEFAULT(0), 
				[BillingValue] DECIMAL(18,0) DEFAULT(0) 
			)

		---Tabla temporal para guardar el resultado del save del comprobante contable
		DECLARE 
			@resultJournalVoucher TABLE (code VARCHAR(20),MessageResult VARCHAR(max), IdJournalVoucher INT)

		--Variables para calculos de saldos totales		
		DECLARE 
			--Inventario legalizado
			@ILegalizedBalance DECIMAL(32,4) = 0,		--Inventario
			@ALegalizedBalance DECIMAL(32,4) = 0,		--Contabilidad		
			--Inventario sin legalizar
			@INoLegalizedBalance DECIMAL(32,4) = 0,		--Inventario
			@ANoLegalizedBalance DECIMAL(32,4) = 0,		--Contabilidad
			--Diferencias
			@Difference DECIMAL(32,4) = 0,
			@DifferenceInCursor DECIMAL(32,4) = 0,
			@DifferenceInCursorDetails DECIMAL(32,4) = 0

		--Variables para cierre de mes
		DECLARE 
			@CloseMonthId INT,
			@CloseMonthCode VARCHAR(20)

		-- Variables para la secuencia numerica del form de cierre mensual de inventario
		DECLARE 
			@idSequenceDetail INT,
			@pattern VARCHAR(300),
			@NextS INT

		--Variables para contabilizaciones (CABECERA)
		DECLARE 
			@LegalBookId INT,
			@JournalVoucherTypeId INT,
			@Consecutive VARCHAR(MAX),
			@JournalVoucherType VARCHAR(MAX),
			@JournalVoucherXML XML

		--Variables para el cursor de las conciliaciones (differences_cursor)
		DECLARE
			@EntityId AS INT,
			@EntityName AS VARCHAR(250),			
			@IDebitValue AS DECIMAL(32,4),
			@ICreditValue AS DECIMAL(32,4),
			@ADebitValue AS DECIMAL(32,4),
			@ACreditValue AS DECIMAL(32,4),
			@cType AS TINYINT

		--Variables para contabilizaciones (DETALLE)
		DECLARE			
			@IdMainAccountInventory AS INT,
			@IdMainAccountCost AS INT,
			@DIDebitValue AS DECIMAL(20,4),
			@DICreditValue AS DECIMAL(20,4),
			@DADebitValue AS DECIMAL(20,4),
			@DACreditValue AS DECIMAL(20,4),			
			@IdThirdParty AS INT,
			@IdCostCenter AS INT,
			@OperatingUnitId AS INT,
			@FunctionalUnits AS INT,
			@Residue AS DECIMAL(20,4)

		--Tabla temporal en donde se almacenan los errores  y poder validar
		DECLARE 
			@TableErrors TABLE (Id INT IDENTITY(1,1) PRIMARY KEY, MessageError VARCHAR(MAX))
		
		--Tabla temporal en donde se almacena el ponderado de los movimientos realizados por unidad funcional		
		DECLARE 
			@TableWeighted TABLE (
				Id INT IDENTITY(1,1) PRIMARY KEY,
				OperatingUnitId INT,
				FunctionalUnitId INT,				
				Weighted DECIMAL(8,6),
				CostAccountId INT,
				CostCenterId INT
			)

/* ------------------------------- VALIDACIONES ------------------------------ */

		--Debe estar parametrizado la misma fecha de ajuste de inventario para todas las unidades operativas
		IF (
			ISNULL((SELECT TOP 1 COUNT(*) FROM Inventory.SettingInventory GROUP BY [Year], [Month]), 0)
			<>
			ISNULL((SELECT COUNT(*) FROM Inventory.SettingInventory), 0)
		) BEGIN
			SELECT 999 AS CodeMessage, 'El mes y el año de cierre debe ser la misma para todas las Unidades Operativas' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			RETURN
		END

		IF (
			ISNULL((SELECT COUNT(*) FROM Inventory.ClosedMonth WHERE [Year] = @YearClosed AND [Month] = @MonthClosed), 0) > 0
		) BEGIN
			SELECT 999 AS CodeMessage, 'El mes y el año seleccionado ya se encuentra cerrado' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			RETURN
		END

		--Debe estar parametrizado el mismo tipo de comprobante de ajuste de inventario para todas las unidades operativas
		IF (
			ISNULL((SELECT TOP 1 COUNT(*) FROM Inventory.SettingInventory GROUP BY [InventoryCloseAdjustmentJournalVoucherTypeId]), 0)
			<>
			ISNULL((SELECT COUNT(*) FROM Inventory.SettingInventory), 0)
		) BEGIN
			SELECT 999 AS CodeMessage, 'Debe seleccionar el mismo Tipo de Comprobante Ajuste de Inventario para todas las Unidades Operativas' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			RETURN
		END

		IF (
			ISNULL((
				SELECT COUNT(*)
				FROM GeneralLedger.ClosedMonth AS cm
				WHERE cm.[Year] = @YearClosed AND cm.[Month] = @MonthClosed AND cm.Status = 1
			), 0) = 0
		) BEGIN
			SELECT 999 AS CodeMessage, 'El mes y el año seleccionado no es el periodo actual abierto en contabilidad' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			RETURN
		END
		
		-- Consultamos la secuencia numerica del form de cierre mensual de inventario
		SELECT @pattern = cs.Pattern, @NextS = bsd.[Next], @idSequenceDetail = bsd.Id 
		FROM Inventory.InventorySequenceDetail bsd 
		INNER JOIN Inventory.InventorySequence bs ON bs.Id = bsd.InventorySequenceId 
		INNER JOIN Common.Sequense cs ON cs.Id = bsd.IdSequense
		WHERE bs.IdForm = '852'

		--Debe estar parametrizada la secuencia numérica para el formulario
		IF @idSequenceDetail IS NULL BEGIN
			SELECT 999 AS CodeMessage, 'Secuencia numérica para generar el cierre mensual no encontrada' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			return
		End

/* ------------------- OBTENGO LOS SALDOS DE INVENTARIO Y CONTABILIDAD -------------------- */

		--Obtener saldos de inventario
		INSERT INTO @tableInventory
			EXEC Inventory.SP_ConciliationInventory @MonthClosed, @YearClosed;

		--Obtener saldos de contabilidad
		INSERT INTO @tableAccounting
			EXEC Inventory.SP_ConciliationAccounting @MonthClosed, @YearClosed;
	
		--Totalizo los saldos del inventario
		/** --------------------------- Inventario legalizado ------------------------- **/
		SELECT 
			@ILegalizedBalance = ISNULL((SUM(t.DebitValue) - SUM(t.CreditValue)), 0)
		FROM @tableInventory t
		WHERE t.EntityName NOT IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' )		

		SELECT 
			@ALegalizedBalance = ISNULL((SUM(t.DebitValue) - SUM(t.CreditValue)), 0)
		FROM @tableAccounting t
		WHERE t.EntityName NOT IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' )

		/** -------------------- Inventario pendiente de legalizar -------------------- **/
		SELECT 
			@INoLegalizedBalance = ISNULL((SUM(t.DebitValue) - SUM(t.CreditValue)), 0)
		FROM @tableInventory t
		WHERE t.EntityName IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' )

		SELECT 
			@ANoLegalizedBalance = ISNULL((SUM(t.DebitValue) - SUM(t.CreditValue)), 0)
		FROM @tableAccounting t
		WHERE t.EntityName IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' )

/* ------------------- REGISTRO AJUSTES CIERRE INVENTARIO -------------------- */

		/*****************************************************************************************/

		/** Analisis Inventario Legalizado **/
		--IF (@ILegalizedBalance <> @ALegalizedBalance) BEGIN
		--	PRINT 'Diferencia en inventario legalizado'
		--	SELECT @Difference = 0

		--	--Ponderación por unidad operativa y funcional
		--	INSERT INTO @TableWeighted
		--		EXEC Inventory.SP_WeightedMonthlyMovementByUnitFunctional @MonthClosed, @YearClosed;

		--	DECLARE differences_cursor CURSOR FOR
		--		SELECT IIF(ti.EntityId IS NULL, ta.EntityId, ti.EntityId), IIF(ti.EntityName IS NULL, ta.EntityName, ti.EntityName), ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0), IIF(ta.EntityId IS NULL, 1, IIF(ti.EntityId IS NULL, 2, 3))
		--		FROM ( 
		--			SELECT ti.EntityId, ti.EntityName, SUM(ISNULL(ti.DebitValue, 0)) DebitValue, SUM(ISNULL(ti.CreditValue, 0)) CreditValue
		--			FROM @tableInventory ti
		--			GROUP BY ti.EntityId, ti.EntityName
		--		) AS ti	
		--		FULL JOIN (
		--			SELECT ta.EntityId, ta.EntityName, SUM(ISNULL(ta.DebitValue, 0)) DebitValue, SUM(ISNULL(ta.CreditValue, 0)) CreditValue
		--			FROM @tableAccounting ta
		--			GROUP BY ta.EntityId, ta.EntityName	
		--		) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName
		--		WHERE ( ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0) ) AND
		--			( ti.EntityName NOT IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' ) OR ta.EntityName NOT IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' ) )

		--	OPEN differences_cursor
		--		FETCH NEXT FROM differences_cursor INTO @EntityId, @EntityName, @IDebitValue, @ICreditValue, @ADebitValue, @ACreditValue, @cType

		--		WHILE @@FETCH_STATUS = 0
		--		BEGIN
		--			SELECT
		--				@IdMainAccountInventory = 0,
		--				@IdMainAccountCost = 0,
		--				@DIDebitValue = 0,
		--				@DICreditValue = 0,
		--				@DADebitValue = 0,
		--				@DACreditValue = 0,
		--				@DifferenceInCursor = 0
					
		--			Print @EntityName + ': ' + CAST(@EntityId AS VARCHAR(12))
						
		--			/*********************************** Debe existir el registro tanto en contabilidad como en inventario ***********************************/
		--			IF @cType = 1 BEGIN
		--				Print 'Registrado solo en inventario';

		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT DISTINCT 'El registro de tipo "' + k.EntityName + '" y Consecutivo "' + CAST(k.EntityCode AS VARCHAR) + '" fue realizado solo desde inventario'
		--					FROM Inventory.Kardex k
		--					WHERE k.EntityId = @EntityId 
		--						AND ( k.EntityName = @EntityName OR ( @EntityName = 'RemissionDevolutionOutput' AND k.EntityName = 'RemissionDevolution' ) );

		--				GOTO FIN_DIFFERENCES_LEGALIZED
		--			END
		--			ELSE IF @cType = 2 BEGIN
		--				Print 'Registrado solo en contabilidad';

		--				IF @EntityName = 'EntranceVoucher' BEGIN
		--					INSERT INTO @TableErrors (MessageError)
		--						SELECT DISTINCT 'El registro de tipo "' + jv.EntityName + '" y Consecutivo "' + CAST(jv.EntityCode AS VARCHAR) + '" fue realizado solo desde contabilidad'
		--						FROM GeneralLedger.JournalVouchers jv
		--						INNER JOIN Payments.AccountPayable ap ON jv.EntityId = ap.Id AND jv.EntityCode = ap.Code AND ap.EntityName = 'EntranceVoucher'								
		--						WHERE ap.EntityId = @EntityId AND ap.EntityName = @EntityName
		--				END
		--				ELSE BEGIN
		--					INSERT INTO @TableErrors (MessageError)
		--						SELECT DISTINCT 'El registro de tipo "' + jv.EntityName + '" y Consecutivo "' + CAST(jv.EntityCode AS VARCHAR) + '" fue realizado solo desde contabilidad'
		--						FROM GeneralLedger.JournalVouchers jv								
		--						WHERE jv.Status = 2 AND jv.EntityId = @EntityId 
		--							AND ( jv.EntityName = @EntityName OR ( @EntityName = 'RemissionDevolutionOutput' AND jv.EntityName = 'RemissionDevolution' ) );
		--				END

		--				GOTO FIN_DIFFERENCES_LEGALIZED
		--			END

		--				  -- Importante: Las cuentas contables no deben haber cambiado, de lo contrario las diferencias no podrán ser contabilizadas --
		--			/***************************************************  Contabilización de Diferencias **************** ***********************************/
		--			IF @EntityName = 'RemissionOutput' OR @EntityName = 'RemissionDevolutionOutput' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.CreditAccountId, ti.DebitAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, pg.ReferenceOutputDebitAccountId AS DebitAccountId, pg.ReferenceOutputCreditAccountId AS CreditAccountId, SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						WHERE ( ti.EntityId = @EntityId AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, pg.ReferenceOutputDebitAccountId, pg.ReferenceOutputCreditAccountId
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.CreditAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails),
		--							@IdThirdParty = NULL

		--						IF @EntityName = 'RemissionOutput' BEGIN
		--							SELECT @IdThirdParty = (SELECT c.ThirdPartyId FROM Inventory.RemissionOutput ro INNER JOIN Common.Customer c ON ro.CustomerId = c.Id WHERE ro.Id = @EntityId)										
		--						END
		--						ELSE IF @EntityName = 'RemissionDevolutionOutput' BEGIN
		--							SELECT @IdThirdParty = (SELECT c.ThirdPartyId FROM Inventory.RemissionDevolution rd INNER JOIN Inventory.RemissionOutput ro ON rd.RemissionOutputId = ro.Id INNER JOIN Common.Customer c ON ro.CustomerId = c.Id WHERE rd.Id = @EntityId)
		--						END
								
		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT 
		--								ma.Id,										
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(ma.Id = @IdMainAccountInventory, IIF(@DifferenceInCursorDetails > 0, 1, 2), IIF(@DifferenceInCursorDetails > 0, 2, 1)),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								NULL
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id IN (@IdMainAccountInventory, @IdMainAccountCost)
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE IF @EntityName = 'EntranceVoucher' OR @EntityName = 'EntranceVoucherDevolution' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.DebitAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, apc.IdAccount AS DebitAccountId, SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						INNER JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id								
		--						WHERE (ti.EntityId = @EntityId  AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, apc.IdAccount
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.DebitAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails),
		--							@IdThirdParty = NULL,
		--							@FunctionalUnits = ISNULL((
		--										SELECT COUNT(sifu.Id) 
		--										FROM Inventory.SettingInventory si
		--										INNER JOIN Inventory.SettingInventoryFunctionalUnit sifu ON si.Id = sifu.SettingInventoryId
		--										LEFT JOIN Inventory.EntranceVoucherDevolution evd ON evd.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucherDevolution'
		--										LEFT JOIN Inventory.EntranceVoucher ev ON ev.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucher'
		--										WHERE (@EntityName = 'EntranceVoucherDevolution' AND evd.Id = @EntityId) OR (@EntityName = 'EntranceVoucher' AND ev.Id = @EntityId)
		--									), 0)

		--						IF @EntityName = 'EntranceVoucher' BEGIN
		--							SELECT @IdThirdParty = (SELECT s.IdThirdParty FROM Inventory.EntranceVoucher ev INNER JOIN Common.Supplier s ON ev.SupplierId = s.Id WHERE ev.Id = @EntityId)
		--						END
		--						ELSE IF @EntityName = 'EntranceVoucherDevolution' BEGIN
		--							SELECT @IdThirdParty = (SELECT s.IdThirdParty FROM Inventory.EntranceVoucherDevolution evd INNER JOIN Inventory.EntranceVoucher ev ON evd.EntranceVoucherId = ev.Id INNER JOIN Common.Supplier s ON ev.SupplierId = s.Id WHERE evd.Id = @EntityId)
		--						END

		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT 
		--								ma.Id,
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(@DifferenceInCursorDetails > 0, 1, 2),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								NULL
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id = @IdMainAccountInventory

		--						IF @FunctionalUnits > 0 BEGIN
		--							IF ROUND((@DifferenceInCursorDetails / @FunctionalUnits), 4) <> 0 BEGIN
		--								INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--									SELECT 
		--										ma.Id,
		--										ABS( ROUND((@DifferenceInCursorDetails / @FunctionalUnits), 4) ),
		--										IIF(@DifferenceInCursorDetails > 0, 2, 1),
		--										@EntityId,
		--										@EntityName,
		--										CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--										CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END
		--									FROM Inventory.SettingInventory si
		--									INNER JOIN Inventory.SettingInventoryFunctionalUnit sifu ON si.Id = sifu.SettingInventoryId
		--									INNER JOIN GeneralLedger.MainAccounts ma ON sifu.CostAccountId = ma.Id
		--									INNER JOIN Payroll.FunctionalUnit fu ON sifu.FunctionalUnitId = fu.Id
		--									LEFT JOIN Inventory.EntranceVoucherDevolution evd ON evd.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucherDevolution'
		--									LEFT JOIN Inventory.EntranceVoucher ev ON ev.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucher'
		--									WHERE (@EntityName = 'EntranceVoucherDevolution' AND evd.Id = @EntityId) OR (@EntityName = 'EntranceVoucher' AND ev.Id = @EntityId)
		--							END

		--							SELECT @Residue = (ABS(@DifferenceInCursorDetails) - ABS( ROUND((@DifferenceInCursorDetails / @FunctionalUnits), 4) * @FunctionalUnits ) )

		--							IF @Residue > 0 BEGIN
		--								INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--									SELECT TOP 1
		--										ma.Id,
		--										ABS(@Residue),
		--										IIF(@DifferenceInCursorDetails > 0, 2, 1),
		--										@EntityId,
		--										@EntityName,
		--										CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--										CASE ma.HandlesCostCenter WHEN 1 THEN fu.CostCenterId ELSE NULL END
		--									FROM Inventory.SettingInventory si
		--									INNER JOIN Inventory.SettingInventoryFunctionalUnit sifu ON si.Id = sifu.SettingInventoryId
		--									INNER JOIN GeneralLedger.MainAccounts ma ON sifu.CostAccountId = ma.Id
		--									INNER JOIN Payroll.FunctionalUnit fu ON sifu.FunctionalUnitId = fu.Id
		--									LEFT JOIN Inventory.EntranceVoucherDevolution evd ON evd.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucherDevolution'
		--									LEFT JOIN Inventory.EntranceVoucher ev ON ev.OperatingUnitId = si.OperatingUnitId AND @EntityName = 'EntranceVoucher'
		--									WHERE (@EntityName = 'EntranceVoucherDevolution' AND evd.Id = @EntityId) OR (@EntityName = 'EntranceVoucher' AND ev.Id = @EntityId)
		--							END
		--							ELSE IF @Residue < 0 BEGIN
		--								UPDATE @tableClosedMonthControl
		--									SET [Value] = [Value] + @Residue
		--								WHERE Id = (SELECT MAX(Id) FROM @tableClosedMonthControl WHERE [EntityId] = @EntityId AND [EntityName] = @EntityName)
		--							END
		--						END
		--						ELSE BEGIN
		--							INSERT INTO @TableErrors (MessageError)
		--								SELECT DISTINCT 'El registro de tipo "' + k.EntityName + '" y Consecutivo "' + CAST(k.EntityCode AS VARCHAR) + '" no tiene parametrizada cuenta del costo en la unidad operativa (Parámetros de inventarios)'
		--								FROM Inventory.Kardex k
		--								WHERE k.EntityId = @EntityId  AND k.EntityName = @EntityName;

		--							GOTO FIN_DIFFERENCES_LEGALIZED
		--						END
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE IF @EntityName = 'LoanMerchandise' OR @EntityName = 'LoanMerchandiseDevolution' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.ThirdPartyId, ti.DebitAccountId, ti.CreditAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0),  ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, lm.ThirdPartyId, apc.IdAccount AS DebitAccountId, w.LoanThirdPartyCreditAccountId AS CreditAccountId, SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						INNER JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
		--						LEFT JOIN Inventory.LoanMerchandiseDevolution lmd ON @EntityName = 'LoanMerchandiseDevolution' AND ti.EntityId = lmd.Id
		--						INNER JOIN Inventory.LoanMerchandise lm ON (@EntityName = 'LoanMerchandise' AND ti.EntityId = lm.Id) OR (@EntityName = 'LoanMerchandiseDevolution' AND lmd.LoanMerchandiseId = lm.Id)
		--						INNER JOIN Inventory.Warehouse w ON lm.WarehouseId = w.Id
		--						WHERE (ti.EntityId = @EntityId  AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, lm.ThirdPartyId, apc.IdAccount, w.LoanThirdPartyDebitAccountId, w.LoanThirdPartyCreditAccountId
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.DebitAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdThirdParty, @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails)
								
		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT 
		--								ma.Id,
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(ma.Id = @IdMainAccountInventory, IIF(@DifferenceInCursorDetails > 0, 1, 2), IIF(@DifferenceInCursorDetails > 0, 2, 1)),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								NULL
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id IN (@IdMainAccountInventory, @IdMainAccountCost)
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdThirdParty, @IdMainAccountInventory, @IdMainAccountCost, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE IF @EntityName = 'TransferOrder' OR @EntityName = 'TransferOrderDevolution' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.ThirdPartyId, ti.CostCenterId, ti.DebitAccountId, ti.CreditAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0),  ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, 
		--							IIF([to].OrderType = 1, [to].ThirdPartyId, IIF(si.TakeTransferOrderThirdParty = 1, [to].ThirdPartyId, si.TransferOrderThirdPartyId)) AS ThirdPartyId,
		--							IIF([to].OrderType = 1, w.CostCenterId, IIF([to].DispatchTo = 1, w.CostCenterId, fu.CostCenterId)) AS CostCenterId,
		--							apc.IdAccount AS DebitAccountId, IIF([to].OrderType = 2, ac.AdjustmentAccountId, apc.IdAccount) AS CreditAccountId, 
		--							SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						INNER JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
		--						LEFT JOIN Inventory.TransferOrderDevolution tod ON @EntityName = 'TransferOrderDevolution' AND ti.EntityId = tod.Id
		--						INNER JOIN Inventory.TransferOrder [to] ON (@EntityName = 'TransferOrder' AND ti.EntityId = [to].Id) OR (@EntityName = 'TransferOrderDevolution' AND tod.TransferOrderId = [to].Id)
		--						LEFT JOIN Inventory.AdjustmentConcept ac ON [to].AdjustmentConceptId = ac.Id
		--						LEFT JOIN Inventory.SettingInventory si ON [to].OperatingUnitId = si.OperatingUnitId
		--						LEFT JOIN Inventory.Warehouse w ON [to].TargetWarehouseId = w.Id
		--						LEFT JOIN Payroll.FunctionalUnit fu ON [to].TargetFunctionalUnitId = fu.Id
		--						WHERE (ti.EntityId = @EntityId  AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, [to].OrderType, [to].ThirdPartyId, si.TakeTransferOrderThirdParty, si.TransferOrderThirdPartyId, apc.IdAccount, ac.AdjustmentAccountId, [to].DispatchTo, w.CostCenterId, fu.CostCenterId
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.DebitAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdThirdParty, @IdCostCenter, @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails)
								
		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT 
		--								ma.Id,
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(ma.Id = @IdMainAccountInventory, IIF(@DifferenceInCursorDetails > 0, 1, 2), IIF(@DifferenceInCursorDetails > 0, 2, 1)),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								CASE ma.HandlesCostCenter WHEN 1 THEN @IdCostCenter ELSE NULL END										
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id IN (@IdMainAccountInventory, @IdMainAccountCost)
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdThirdParty, @IdCostCenter, @IdMainAccountInventory, @IdMainAccountCost, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE IF @EntityName = 'DocumentInvoiceProductSales' OR @EntityName = 'PharmaceuticalDispensing' OR @EntityName = 'PharmaceuticalDispensingDevolution' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.DebitAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, apc.IdAccount AS DebitAccountId, SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						INNER JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id								
		--						WHERE (ti.EntityId = @EntityId  AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, apc.IdAccount
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.DebitAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails),
		--							@IdThirdParty = NULL,
		--							@OperatingUnitId = NULL

		--						IF @EntityName = 'DocumentInvoiceProductSales' BEGIN
		--							SELECT @IdThirdParty = dips.ThirdPartyId, @OperatingUnitId = dips.OperatingUnitId
		--							FROM Inventory.DocumentInvoiceProductSales dips 
		--							WHERE dips.Id = @EntityId
		--						END
		--						ELSE IF @EntityName = 'PharmaceuticalDispensing' BEGIN									
		--							SELECT TOP 1 
		--								@IdThirdParty = pdd.ThirdPartyId, @OperatingUnitId = pd.OperatingUnitId
		--							FROM Inventory.PharmaceuticalDispensing pd 
		--							INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pd.Id = pdd.PharmaceuticalDispensingId 
		--							WHERE pd.Id = @EntityId
		--						END
		--						ELSE IF @EntityName = 'PharmaceuticalDispensingDevolution' BEGIN
		--							SELECT TOP 1 
		--								@IdThirdParty = pddetail.ThirdPartyId, @OperatingUnitId = pdd.OperatingUnitId
		--							FROM Inventory.PharmaceuticalDispensingDevolution pdd 
		--							INNER JOIN Inventory.PharmaceuticalDispensing pd ON pdd.AdmissionNumber = pd.AdmissionNumber 
		--							INNER JOIN Inventory.PharmaceuticalDispensingDetail pddetail ON pd.Id = pddetail.PharmaceuticalDispensingId 
		--							WHERE pdd.Id = @EntityId
		--						END

		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT 
		--								ma.Id,
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(@DifferenceInCursorDetails > 0, 1, 2),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								NULL
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id = @IdMainAccountInventory

		--						IF (SELECT COUNT(*) FROM @TableWeighted WHERE OperatingUnitId = @OperatingUnitId) > 0 BEGIN
		--							INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--								SELECT 
		--									ma.Id,
		--									ABS( ROUND((@DifferenceInCursorDetails * tw.Weighted), 4) ),
		--									IIF(@DifferenceInCursorDetails > 0, 2, 1),
		--									@EntityId,
		--									@EntityName,
		--									CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--									CASE ma.HandlesCostCenter WHEN 1 THEN tw.CostCenterId ELSE NULL END
		--								FROM @TableWeighted tw
		--								INNER JOIN GeneralLedger.MainAccounts ma ON tw.CostAccountId = ma.Id
		--								WHERE tw.OperatingUnitId = @OperatingUnitId
										
		--							SELECT @Residue = ABS(@DifferenceInCursorDetails) - ISNULL((SELECT SUM( ABS( ROUND((@DifferenceInCursorDetails * tw.Weighted), 4) ) ) FROM @TableWeighted tw WHERE tw.OperatingUnitId = @OperatingUnitId), 0)

		--							IF @Residue > 0 BEGIN
		--								INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--									SELECT TOP 1
		--										ma.Id,
		--										ABS(@Residue),
		--										IIF(@DifferenceInCursorDetails > 0, 2, 1),
		--										@EntityId,
		--										@EntityName,
		--										CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--										CASE ma.HandlesCostCenter WHEN 1 THEN tw.CostCenterId ELSE NULL END
		--									FROM @TableWeighted tw
		--									INNER JOIN GeneralLedger.MainAccounts ma ON tw.CostAccountId = ma.Id
		--									WHERE tw.OperatingUnitId = @OperatingUnitId
		--							END
		--							ELSE IF @Residue < 0 BEGIN
		--								UPDATE @tableClosedMonthControl
		--									SET [Value] = [Value] + @Residue
		--								WHERE Id = (SELECT MAX(Id) FROM @tableClosedMonthControl WHERE [EntityId] = @EntityId AND [EntityName] = @EntityName)
		--							END
		--						END
		--						ELSE BEGIN
		--							INSERT INTO @TableErrors (MessageError)
		--								SELECT DISTINCT 'El registro de tipo "' + k.EntityName + '" y Consecutivo "' + CAST(k.EntityCode AS VARCHAR) + '" no tiene parametrizada cuenta del costo en la unidad operativa (Parámetros de inventarios)'
		--								FROM Inventory.Kardex k
		--								WHERE k.EntityId = @EntityId  AND k.EntityName = @EntityName;

		--							GOTO FIN_DIFFERENCES_LEGALIZED
		--						END
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE BEGIN
		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT 'Conciliación no controlada para el tipo: ' + @EntityName
		--				RETURN
		--			END
										
		--			--Verificar que la diferencia a ajustar haya sido conciliada
		--			IF @DifferenceInCursor <> (@IDebitValue - @ICreditValue) - (@ADebitValue - @ACreditValue) BEGIN
		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT 'La diferencia a ajustar (' + CAST(((@IDebitValue - @ICreditValue) - (@ADebitValue - @ACreditValue)) AS VARCHAR(MAX)) + ') no es igual a la diferencia conciliada (' + CAST(@DifferenceInCursor AS VARCHAR(MAX)) + ') para el registro (' + CAST(@EntityId AS VARCHAR(MAX)) + ') de tipo (' + @EntityName + ')'
		--			END

		--			SELECT @Difference += @DifferenceInCursor

		--			FIN_DIFFERENCES_LEGALIZED:
		--			FETCH NEXT FROM differences_cursor INTO @EntityId, @EntityName, @IDebitValue, @ICreditValue, @ADebitValue, @ACreditValue, @cType
		--		END		 
		--	CLOSE differences_cursor
		--	DEALLOCATE differences_cursor

		--	--Verificar que la diferencia a ajustar sea la misma resultante
		--	IF @Difference <> (@ILegalizedBalance - @ALegalizedBalance) BEGIN
		--		INSERT INTO @TableErrors (MessageError)
		--			SELECT 'La diferencia del inventario legalizado (' + CAST((@ILegalizedBalance - @ALegalizedBalance) AS VARCHAR(MAX)) + ') y el valor a ajustar (' + CAST(@Difference AS VARCHAR(MAX)) + ') no coinciden'
		--	END
		--END

		--/*****************************************************************************************/

		--/** Analisis Inventario no Legalizado **/
		--IF (@INoLegalizedBalance <> @ANoLegalizedBalance) BEGIN
		--	PRINT 'Diferencia en inventario no legalizado'
		--	SELECT @Difference = 0
			
		--	DECLARE differences_cursor CURSOR FOR
		--		SELECT IIF(ti.EntityId IS NULL, ta.EntityId, ti.EntityId), IIF(ti.EntityName IS NULL, ta.EntityName, ti.EntityName), ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0), IIF(ta.EntityId IS NULL, 1, IIF(ti.EntityId IS NULL, 2, 3))
		--		FROM ( 
		--			SELECT ti.EntityId, ti.EntityName, SUM(ISNULL(ti.DebitValue, 0)) DebitValue, SUM(ISNULL(ti.CreditValue, 0)) CreditValue
		--			FROM @tableInventory ti
		--			GROUP BY ti.EntityId, ti.EntityName
		--		) AS ti
		--		FULL JOIN (
		--			SELECT ta.EntityId, ta.EntityName, SUM(ISNULL(ta.DebitValue, 0)) DebitValue, SUM(ISNULL(ta.CreditValue, 0)) CreditValue
		--			FROM @tableAccounting ta
		--			GROUP BY ta.EntityId, ta.EntityName	
		--		) ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName
		--		WHERE ( ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0) ) AND
		--			( ti.EntityName IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' ) OR ta.EntityName IN ( 'RemissionEntrance', 'RemissionReclassification', 'RemissionDevolutionEntrance' ) )

		--	OPEN differences_cursor
		--		FETCH NEXT FROM differences_cursor INTO @EntityId, @EntityName, @IDebitValue, @ICreditValue, @ADebitValue, @ACreditValue, @cType

		--		WHILE @@FETCH_STATUS = 0
		--		BEGIN
		--			SELECT
		--				@IdMainAccountInventory = 0,
		--				@IdMainAccountCost = 0,
		--				@DIDebitValue = 0,
		--				@DICreditValue = 0,
		--				@DADebitValue = 0,
		--				@DACreditValue = 0,
		--				@DifferenceInCursor = 0

		--			Print @EntityName + ': ' + CAST(@EntityId AS VARCHAR(12))
						
		--			/*********************************** Debe existir el registro tanto en contabilidad como en inventario ***********************************/
		--			IF @cType = 1 BEGIN
		--				Print 'Registrado solo en inventario';

		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT DISTINCT 'El registro de tipo "' + k.EntityName + '" y Consecutivo "' + CAST(k.EntityCode AS VARCHAR) + '" fue realizado solo desde inventario'
		--					FROM Inventory.Kardex k
		--					WHERE k.EntityId = @EntityId 
		--						AND ( k.EntityName = @EntityName OR ( @EntityName = 'RemissionDevolutionEntrance' AND k.EntityName = 'RemissionDevolution' ) );

		--				GOTO FIN_DIFFERENCES_LEGALIZED
		--			END
		--			ELSE IF @cType = 2 BEGIN
		--				Print 'Registrado solo en contabilidad';

		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT DISTINCT 'El registro de tipo "' + jv.EntityName + '" y Consecutivo "' + CAST(jv.EntityCode AS VARCHAR) + '" fue realizado solo desde contabilidad'
		--					FROM GeneralLedger.JournalVouchers jv								
		--					WHERE jv.Status = 2 AND jv.EntityId = @EntityId 
		--						AND ( jv.EntityName = @EntityName OR ( @EntityName = 'RemissionDevolutionEntrance' AND jv.EntityName = 'RemissionDevolution' ) );

		--				GOTO FIN_DIFFERENCES_LEGALIZED
		--			END

		--				  -- Importante: Las cuentas contables no deben haber cambiado, de lo contrario las diferencias no podrán ser contabilizadas --
		--			/***************************************************  Contabilización de Diferencias **************** ***********************************/
		--			IF @EntityName = 'RemissionEntrance' OR @EntityName = 'RemissionReclassification' OR @EntityName = 'RemissionDevolutionEntrance' BEGIN
		--				--Recorrer por los Distintos grupos en los detalles 
		--				DECLARE difference_details_cursor CURSOR FOR
		--					SELECT ti.DebitAccountId, ti.CreditAccountId, ISNULL(ti.DebitValue, 0), ISNULL(ti.CreditValue, 0), ISNULL(ta.DebitValue, 0), ISNULL(ta.CreditValue, 0)
		--					FROM (
		--						SELECT ti.EntityId, ti.EntityName, pg.ReferenceInputDebitAccountId AS DebitAccountId, pg.ReferenceInputCreditAccountId AS CreditAccountId, SUM(ti.DebitValue) AS DebitValue, SUM(ti.CreditValue) AS CreditValue
		--						FROM @tableInventory ti
		--						INNER JOIN Inventory.ProductGroup pg ON ti.ProductGroupId = pg.Id
		--						WHERE ( ti.EntityId = @EntityId AND ti.EntityName = @EntityName )
		--						GROUP BY ti.EntityId, ti.EntityName, pg.ReferenceInputDebitAccountId, pg.ReferenceInputCreditAccountId
		--					) ti
		--					INNER JOIN (
		--						SELECT ta.EntityId, ta.EntityName, ta.MainAccount, SUM(ta.DebitValue) AS DebitValue, SUM(ta.CreditValue) AS CreditValue
		--						FROM @tableAccounting ta
		--						WHERE ( ta.EntityId = @EntityId AND ta.EntityName = @EntityName )
		--						GROUP BY ta.EntityId, ta.EntityName, ta.MainAccount
		--					) AS ta ON ti.EntityId = ta.EntityId AND ti.EntityName = ta.EntityName AND ti.DebitAccountId = ta.MainAccount
		--					WHERE ISNULL(ti.DebitValue, 0) <> ISNULL(ta.DebitValue, 0) OR ISNULL(ti.CreditValue, 0) <> ISNULL(ta.CreditValue, 0)

		--				OPEN difference_details_cursor
		--					FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue

		--					WHILE @@FETCH_STATUS = 0
		--					BEGIN
		--						SELECT 
		--							@DifferenceInCursorDetails = (@DIDebitValue - @DICreditValue) - (@DADebitValue - @DACreditValue),
		--							@DifferenceInCursor += @DifferenceInCursorDetails,
		--							@DifferenceInCursorDetails = IIF(@DifferenceInCursorDetails = 0, (@DIDebitValue - @DADebitValue), @DifferenceInCursorDetails),
		--							@IdThirdParty = NULL

		--						IF @EntityName = 'RemissionEntrance' BEGIN
		--							SELECT @IdThirdParty = (SELECT s.IdThirdParty FROM Inventory.RemissionEntrance re INNER JOIN Common.Supplier s ON re.SupplierId = s.Id WHERE re.Id = @EntityId)
		--						END
		--						ELSE IF @EntityName = 'RemissionReclassification' BEGIN
		--							SELECT @IdThirdParty = (SELECT s.IdThirdParty FROM Inventory.EntranceVoucher ev INNER JOIN Common.Supplier s ON ev.SupplierId = s.Id WHERE ev.Id = @EntityId)
		--						END
		--						ELSE IF @EntityName = 'RemissionDevolutionEntrance' BEGIN
		--							SELECT @IdThirdParty = (SELECT s.IdThirdParty FROM Inventory.RemissionDevolution rd INNER JOIN Inventory.RemissionEntrance re ON rd.RemissionEntranceId = re.Id INNER JOIN Common.Supplier s ON re.SupplierId = s.Id WHERE rd.Id = @EntityId)
		--						END
																
		--						INSERT INTO @tableClosedMonthControl ([MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
		--							SELECT
		--								ma.Id,										
		--								ABS(@DifferenceInCursorDetails),
		--								IIF(ma.Id = @IdMainAccountInventory, IIF(@DifferenceInCursorDetails > 0, 1, 2), IIF(@DifferenceInCursorDetails > 0, 2, 1)),
		--								@EntityId,
		--								@EntityName,
		--								CASE ma.HandlesThirdParty WHEN 1 THEN @IdThirdParty ELSE NULL END,
		--								NULL
		--							FROM GeneralLedger.MainAccounts ma
		--							WHERE ma.Id IN (@IdMainAccountInventory, @IdMainAccountCost)
								
		--						FETCH NEXT FROM difference_details_cursor INTO @IdMainAccountInventory, @IdMainAccountCost, @DIDebitValue, @DICreditValue, @DADebitValue, @DACreditValue
		--					END		 
		--				CLOSE difference_details_cursor
		--				DEALLOCATE difference_details_cursor
		--			END
		--			ELSE BEGIN
		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT 'Conciliación no controlada para el tipo: ' + @EntityName
		--			END

		--			--Verificar que la diferencia a ajustar haya sido conciliada
		--			IF @DifferenceInCursor <> (@IDebitValue - @ICreditValue) - (@ADebitValue - @ACreditValue) BEGIN
		--				INSERT INTO @TableErrors (MessageError)
		--					SELECT 'La diferencia a ajustar (' + CAST(((@IDebitValue - @ICreditValue) - (@ADebitValue - @ACreditValue)) AS VARCHAR(MAX)) + ') no es igual a la diferencia conciliada (' + CAST(@DifferenceInCursor AS VARCHAR(MAX)) + ') para el registro (' + CAST(@EntityId AS VARCHAR(MAX)) + ') de tipo (' + @EntityName + ')'
		--			END

		--			SELECT @Difference += @DifferenceInCursor
					
		--			FIN_DIFFERENCES_NO_LEGALIZED:
		--			FETCH NEXT FROM differences_cursor INTO @EntityId, @EntityName, @IDebitValue, @ICreditValue, @ADebitValue, @ACreditValue, @cType
		--		END		 
		--	CLOSE differences_cursor
		--	DEALLOCATE differences_cursor

		--	--Verificar que la diferencia a ajustar sea la misma resultante
		--	IF @Difference <> (@INoLegalizedBalance - @ANoLegalizedBalance) BEGIN
		--		INSERT INTO @TableErrors (MessageError)
		--			SELECT 'La diferencia del inventario pendiente por legalizar (' + CAST((@INoLegalizedBalance - @ANoLegalizedBalance) AS VARCHAR(MAX)) + ') y el valor a ajustar (' + CAST(@Difference AS VARCHAR(MAX)) + ') no coinciden'
		--	END
		--END

		/** --------- VALIDO SI HUBO ERRORES EN EL REGISTRO AJUSTES CIERRE INVENTARIO --------- **/
		IF (SELECT COUNT(*) FROM @TableErrors) > 0 BEGIN
			DECLARE 
				@MessageError VARCHAR(MAX) = STUFF((SELECT N'' + MessageError + CHAR(13) + CHAR(10) FROM @TableErrors FOR xml path(N''), type).value(N'.[1]', N'NVARCHAR(MAX)'), 1,0, N'')
			
			SELECT 999 AS CodeMessage, @MessageError AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive
			RETURN
		END

/* -------------------- REGISTRO TABLAS CIERRE INVENTARIO -------------------- */

		--Obtener la secuancia de cierre mensual de inventario
		SELECT @CloseMonthCode = dbo.GetSequence('', @pattern, @NextS)

		--Cierre Mensual
		INSERT INTO [Inventory].[ClosedMonth] ([Code], [Year], [Month], [InventoryNoLegalizedBalance], [InventoryLegalizedBalance], [AccountingNoLegalizedBalance], [AccountingLegalizedBalance], [CreationUser], [CreationDate])
			SELECT @CloseMonthCode, @YearClosed, @MonthClosed, @INoLegalizedBalance, @ILegalizedBalance, @ANoLegalizedBalance, @ALegalizedBalance, @CodeUser, [Common].[GETDATE]()		

		--Id Cierre Mensual
		SET @CloseMonthId = SCOPE_IDENTITY();

		--Detalle del Cierre Mensual
		INSERT INTO [Inventory].[ClosedMonthInventory] ([Quantity], [ProductCost], [FinalProductCost], [SellingPrice], [ProductId], [ClosedMonthId])
			SELECT SUM([pi].Quantity), ip.ProductCost, ip.FinalProductCost, ip.SellingPrice, ip.Id, @CloseMonthId
			FROM Inventory.PhysicalInventory [pi]
			INNER JOIN Inventory.Warehouse w ON [pi].WarehouseId = w.Id AND w.VirtualStore = 0
			INNER JOIN Inventory.InventoryProduct ip ON [pi].ProductId = ip.Id
			GROUP BY ip.ProductCost, ip.FinalProductCost, ip.SellingPrice, ip.Id
		
		--Detalle del Cierre Mensual por almacen y lote
		INSERT INTO [Inventory].[ClosedMonthInventoryDetail] ([Quantity], [WarehouseId], [BatchSerialId], [ClosedMonthInventoryId])
			SELECT [pi].Quantity, [pi].WarehouseId, [pi].BatchSerialId, cmi.Id
			FROM Inventory.PhysicalInventory [pi]
			INNER JOIN Inventory.Warehouse w ON [pi].WarehouseId = w.Id AND w.VirtualStore = 0
			INNER JOIN Inventory.ClosedMonthInventory cmi ON [pi].ProductId = cmi.ProductId AND cmi.ClosedMonthId = @CloseMonthId

		--Se genera el mensaje a devolver
		DECLARE @Message VARCHAR(MAX) = 'Se procesó correctamente el cierre mensual con código ' + @CloseMonthCode
		
	---------------------------------------------------------------------------------------------------------
		
		--Si se realizo al menos un ajuste contable
		IF (ISNULL((SELECT COUNT(*) FROM @tableClosedMonthControl), 0) > 0) BEGIN
			--Detalle del Ajuste de cierre de inventario
			INSERT INTO [Inventory].[ClosedMonthControl] ([CloseMonthId], [MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], CostCenterId)
				SELECT @CloseMonthId, [MainAccountId], [Value], [Nature], [EntityId], [EntityName], [ThirdPartyId], [CostCenterId]
				FROM @tableClosedMonthControl

			--Se obtiene el id del libro oficial
			SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1

			--Se obtiene el tipo de comprobante de ajuste de inventario para la unidad operativa
			SELECT TOP 1 @JournalVoucherTypeId = InventoryCloseAdjustmentJournalVoucherTypeId FROM Inventory.SettingInventory

			--Creo la cabecera del comprobante contable
			INSERT INTO @JournalVourcherTmp (Id, Consecutive, LegalBookId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
				VALUES (0, 0, @LegalBookId, @JournalVoucherTypeId, [Common].[GETDATE](), 0, 2, 'Comprobante contable generado desde Cierre Mensual', @CloseMonthCode, @CloseMonthId, 'InventoryCloseMonth', 0)

			--Creo el detalle del comprobante contable
			INSERT INTO @JournalVourcherDetailTmp ([IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue],  [Detail])
				SELECT mc.MainAccountId, mc.ThirdPartyId, mc.CostCenterId, SUM(mc.DebitValue), SUM(mc.CreditValue), 'Detalle generado desde el cierre de inventario'
				FROM (
					SELECT mc.MainAccountId, mc.ThirdPartyId, mc.CostCenterId, SUM(IIF(mc.Nature = 1, mc.Value, 0)) DebitValue, SUM(IIF(mc.Nature = 1, 0, mc.Value)) CreditValue
					FROM @tableClosedMonthControl mc
					WHERE mc.Value <> 0
					GROUP BY mc.MainAccountId, mc.ThirdPartyId, mc.CostCenterId, mc.Nature
				) mc
				GROUP BY mc.MainAccountId, mc.ThirdPartyId, mc.CostCenterId

			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			SELECT @JournalVoucherXML = CONVERT(XML, (SELECT * FROM @JournalVourcherTmp JournalVoucher INNER JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting FOR XML AUTO, TYPE, ELEMENTS))

			--Se consume el sp que guarda el comprobante contable
			INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser

			--Se valida que no hayan errores en el guardado del comprobante contable
			IF (SELECT code FROM @resultJournalVoucher) = '999' BEGIN
				SELECT 999 AS CodeMessage, MessageResult AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive FROM @resultJournalVoucher 
				RETURN
			End  

			--Se obtiene el id que genero el comprobante contable
			DECLARE @JournalVoucherId AS INT = (SELECT IdJournalVoucher FROM @resultJournalVoucher)

			--Se obtiene el consecutivo que generó el comprobante contable
			SELECT @Consecutive = ISNULL((SELECT CAST(Consecutive AS VARCHAR(30)) FROM GeneralLedger.JournalVouchers WHERE Id = @JournalVoucherId ), 0)
			
			SELECT
				 @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name,
				 @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name)
			FROM GeneralLedger.JournalVoucherTypes jvt
			WHERE jvt.Id = @JournalVoucherTypeId
		END

		DECLARE @Date DATE = '01/' + RIGHT( '0' + CAST(@MonthClosed AS VARCHAR(2)), 2) + '/'+ CAST(@YearClosed AS VARCHAR(4))
		DECLARE @DateNext DATE = DATEADD(MONTH, 1, @Date)
		DECLARE @MonthNext INT = MONTH(@DateNext)
		DECLARE @YearNext INT = YEAR(@DateNext)

		UPDATE Inventory.SettingInventory SET Year = @YearNext, Month = @MonthNext, ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE]()
				
		SELECT 0 AS CodeMessage, @Message AS Message, @CloseMonthCode AS Code, @CloseMonthId AS Id, @JournalVoucherType AS JournalVoucherType, @Consecutive AS Consecutive		
	END TRY
	BEGIN CATCH

		SELECT 999 AS CodeMessage, ERROR_MESSAGE() AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutive

	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el cierre mensual del módulo de inventario para un mes y año determinados. Valida que todas las unidades operativas tengan configurado el mismo período de cierre en [Inventory].[SettingInventory] y que el período no haya sido cerrado previamente en [Inventory].[ClosedMonth] ni en [GeneralLedger].[ClosedMonth]. Llama a [Inventory].[SP_ConciliationInventory] y [Inventory].[SP_ConciliationAccounting] para obtener los saldos ponderados de inventario y contabilidad (legalizados y no legalizados), calcula las diferencias de conciliación por unidad funcional y genera automáticamente el comprobante contable de ajuste de cierre usando el tipo de comprobante configurado y las secuencias numéricas de [Inventory].[InventorySequenceDetail]. Es el proceso de fin de período que sella los saldos de inventario, asegura la concordancia entre el módulo de inventario y la contabilidad, y deja trazabilidad del cierre en la tabla [Inventory].[ClosedMonth].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ClosedMonthInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ClosedMonthInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El (Year,Month) que se cierra debe estar abierto en GeneralLedger.ClosedMonth con Status=1; No se puede cerrar dos veces el mismo período (Year,Month) en Inventory.ClosedMonth; Todas las Unidades Operativas deben tener parametrizado el mismo período de ajuste y el mismo tipo de comprobante de ajuste de inventario; El consecutivo del cierre mensual se genera con dbo.GetSequence usando el patrón asociado al formulario 852; El comprobante contable se asocia al cierre con EntityName=''InventoryCloseMonth'', EntityCode=<código de cierre>, EntityId=<Id del cierre>, Status=2, Imported=0, IsClosedYear=0; El detalle de ClosedMonthInventory excluye bodegas virtuales (Warehouse.VirtualStore=0); Solo se inserta detalle en @JournalVourcherDetailTmp para movimientos con Value<>0, agrupados por MainAccountId/ThirdPartyId/CostCenterId; Tras un cierre exitoso, SettingInventory avanza al mes siguiente del período cerrado; El libro contable usado es el oficial (GeneralLedger.LegalBook.OfficialBook=1); ThirdPartyId solo se asigna al asiento si la cuenta tiene HandlesThirdParty=1; CostCenterId solo si HandlesCostCenter=1; Cualquier excepción captura en CATCH se devuelve como CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre mensual de inventario; Conciliación inventario vs contabilidad; Inventario legalizado vs pendiente de legalizar; Comprobante contable de ajuste; Secuencia/consecutivo de formulario; Unidad operativa; Unidad funcional; Plan de cuentas (cuenta principal); Tercero y centro de costo en contabilización; Kardex; Remisiones (entrada, salida, devolución, reclasificación); Comprobante de entrada (EntranceVoucher) y devolución; Préstamo de mercancía; Orden de traslado; Dispensación farmacéutica', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cantidad de filas agrupadas por (Year,Month) en Inventory.SettingInventory ≠ total de filas de Inventory.SettingInventory → Retorna CodeMessage=999 con mensaje exigiendo misma fecha de ajuste para todas las Unidades Operativas y termina; si Existe registro en Inventory.ClosedMonth para el (Year,Month) solicitado → Retorna CodeMessage=999 ''El mes y el año seleccionado ya se encuentra cerrado'' y termina; si Cantidad agrupada por InventoryCloseAdjustmentJournalVoucherTypeId en Inventory.SettingInventory ≠ total de filas → Retorna CodeMessage=999 exigiendo el mismo Tipo de Comprobante de Ajuste para todas las Unidades Operativas y termina; si No existe en GeneralLedger.ClosedMonth un período (Year,Month) con Status=1 (abierto en contabilidad) → Retorna CodeMessage=999 ''no es el periodo actual abierto en contabilidad'' y termina; si No existe secuencia para el formulario IdForm=''852'' (Inventory.InventorySequence)  → Retorna CodeMessage=999 ''Secuencia numérica para generar el cierre mensual no encontrada'' y termina; si Existen errores acumulados en @TableErrors tras la conciliación → Retorna CodeMessage=999 con la concatenación de errores y termina, sin persistir el cierre; si Existen movimientos en @tableClosedMonthControl (al menos un ajuste contable) → Inserta detalle en Inventory.ClosedMonthControl y genera comprobante contable mediante GeneralLedger.SP_CreateAndValidateJournalVoucherMovement else No se genera comprobante contable; solo se registra el cierre y su detalle; si Resultado de SP_CreateAndValidateJournalVoucherMovement.code = ''999'' → Retorna CodeMessage=999 con el MessageResult del comprobante y termina; si Saldos de inventario clasificados como ''legalizados'' (EntityName NOT IN RemissionEntrance/RemissionReclassification/RemissionDevolutionEntrance) vs ''no legalizados'' (EntityName IN ese conjunto) → Se totaliza por separado @ILegalizedBalance/@ALegalizedBalance e @INoLegalizedBalance/@ANoLegalizedBalance y se guardan ambos en Inventory.ClosedMonth', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_ConciliationInventory; Inventory.SP_ConciliationAccounting; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.SettingInventory; Inventory.ClosedMonth; GeneralLedger.ClosedMonth; Inventory.InventorySequenceDetail; Inventory.InventorySequence; Common.Sequense; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ClosedMonthInventory; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthInventory';
-- GO
