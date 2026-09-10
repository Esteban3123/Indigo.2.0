-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-01-18
-- Description:	Procedimiento que se encarga de confirmar las Cuentas por Pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ConfirmAccountsPayable] 
    @AccountsPayableXml AS XML,
	@CodeUser AS VARCHAR(20),
	@IsMasiveConfirm AS BIT
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	--Tabla con las Cuentas por Pagar a confirmar
	DECLARE @TableAccountPayable TABLE
	(
		TempId INT NOT NULL,
		Code VARCHAR(20) NOT NULL,
		JournalVoucherTypeId INT,
		OriginEntityName VARCHAR(250),
		Status TINYINT DEFAULT(0) --0 No procesado, 1 Erronea, 2 Confirmada
	)

	--Tabla con las Cuentas por Pagar a confirmar
	DECLARE @TableAccountPayableDetailConceptNotHomologated TABLE
	(
		Id INT IDENTITY(1,1),
		ParentId INT NOT NULL,
		LegalBookId INT NOT NULL,
		IdMainAccount INT NOT NULL,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(20,4) NOT NULL,
		CreditValue DECIMAL(20,4) NOT NULL,
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(5,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Tabla con las Cuentas por Pagar a confirmar
	DECLARE @TableResult TABLE
	(
		CodeMessage INT,
		Message VARCHAR(MAX),
		Consecutive VARCHAR(MAX),
		Code VARCHAR(20)
	)

	/******************************** VARIABLES CONTABLES *******************************/

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
	    Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate VARCHAR(30),
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		OriginEntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante (masivos)
	DECLARE @JournalVourcherDetailMassive TABLE 
	(
	    Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2),
		AccountPayableTempId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
	    IdAuto int identity(1,1), --HRR
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(18,2),
		CreditValue DECIMAL(18,2),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML,
			@OfficialCurrencyId as int,
			@TaxRegistration tinyint

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/*************************************** PROCESO ************************************/

	BEGIN TRY
	BEGIN TRANSACTION

	/*se consulta al moneda oficial y el tipo de registro de iva*/
		select @OfficialCurrencyId = cs.OfficialCurrencyId ,
				@TaxRegistration = cs.TaxRegistration
		from GeneralLedger.CompanySettings cs

		--Se obtienen los datos de las Cuentas por Pagar a confirmar
		INSERT INTO @TableAccountPayable 
			(TempId, Code, JournalVoucherTypeId, OriginEntityName)
			SELECT 
				t.x.value('TempId[1]','int'),
				t.x.value('Code[1]','varchar(20)'),
				t.x.value('JournalVoucherTypeId[1]','int'),
				t.x.value('OriginEntityName[1]','varchar(250)')
			FROM @AccountsPayableXml.nodes('/ListAccountPayable/AccountPayable') t(x)
				
		--Se obtienen los datos de las contabilizaciones a libros no homologables para las Cuentas por Pagar a confirmar
		INSERT INTO @TableAccountPayableDetailConceptNotHomologated 
			(ParentId, LegalBookId, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			SELECT 
				t.x.value('ParentId[1]','int'),
				t.x.value('LegalBookId[1]','int'),
				t.x.value('IdMainAccount[1]','int'),
				t.x.value('IdThirdParty[1]','int'),
				t.x.value('IdCostCenter[1]','int'),
				t.x.value('DebitValue[1]','decimal(20,4)'),
				t.x.value('CreditValue[1]','decimal(20,4)'),
				t.x.value('Detail[1]','varchar(max)'),
				t.x.value('IdRetention[1]','int'),
				t.x.value('RetentionRate[1]','decimal(5,3)'),
				t.x.value('BaseValue[1]','decimal(18,2)'),
				t.x.value('BillingValue[1]','decimal(18,2)')
			FROM @AccountsPayableXml.nodes('/ListAccountPayable/AccountPayable/ListAccountPayableDetailConceptNotHomologated/AccountPayableDetailConceptNotHomologated') t(x)

		--Actualizamos el id del padre de los conceptos no homologables con cuentas por pagar duplicadas
		UPDATE tapdcnh 
			SET tapdcnh.ParentId = tap.TempId
		FROM @TableAccountPayable duplicate
		JOIN
		(
			SELECT tap.Code, MIN(tap.TempId) TempId
			FROM @TableAccountPayable tap
			GROUP BY tap.Code
		) tap ON duplicate.Code = tap.Code AND duplicate.TempId <> tap.TempId
		JOIN @TableAccountPayableDetailConceptNotHomologated tapdcnh ON duplicate.TempId = tapdcnh.ParentId

		--Eliminamos las cuentas por pagar duplicadas
		DELETE duplicate
		FROM @TableAccountPayable duplicate
		JOIN
		(
			SELECT tap.Code, MIN(tap.TempId) TempId
			FROM @TableAccountPayable tap
			GROUP BY tap.Code
		) tap ON duplicate.Code = tap.Code AND duplicate.TempId <> tap.TempId

		/************************************* VALIDACIONES ************************************/

		IF NOT EXISTS ( SELECT 1 FROM @TableAccountPayable )
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se envió ninguna factura a confirmar' AS Message, '' AS Consecutive

			ROLLBACK TRANSACTION
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF @IsMasiveConfirm = 1 AND EXISTS ( SELECT 1 FROM @TableAccountPayableDetailConceptNotHomologated )
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, 'No se puede confirmar cuentas por pagar con libros no homologables desde una confirmación masiva' AS Message, '' AS Consecutive

			ROLLBACK TRANSACTION
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			LEFT JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			WHERE ap.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'No existe la cuenta por pagar ' + t.Code AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				LEFT JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				WHERE ap.Id IS NULL
		END

		-- UPDLOCK+HOLDLOCK: toma y retiene el lock de fila (por Code) hasta el COMMIT/ROLLBACK
		-- de esta transacci n. Evita que dos ejecuciones concurrentes de este SP para la MISMA
		-- cuenta por pagar lean Status=1 al mismo tiempo y generen doble comprobante contable.
		-- No afecta cuentas por pagar distintas (lock es por fila, no por tabla).
		IF EXISTS (
			SELECT 1
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(UPDLOCK, HOLDLOCK) ON t.Code = ap.Code
			WHERE ap.Status <> 1
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage,
					'La cuenta por pagar ' + t.Code + ' se encuentra en estado ' +
						CASE ap.Status
							WHEN 2 THEN 'Confirmado'
							WHEN 3 THEN 'Anulado'
							ELSE 'N/A'
						END
					AS Message,
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(UPDLOCK, HOLDLOCK) ON t.Code = ap.Code
				WHERE ap.Status <> 1
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			WHERE ap.Value = 0
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'El valor de la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code + ' es igual a cero' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				WHERE ap.Value = 0
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			WHERE ap.Balance = 0
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'El saldo de la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code + ' es igual a cero' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				WHERE ap.Balance = 0
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			LEFT JOIN Payments.AccountPayableShares aps WITH(NOLOCK) ON ap.Id = aps.IdAccountPayable
			WHERE aps.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'La factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code + ' no tiene ningun detalle de cuota' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				LEFT JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
				WHERE aps.Id IS NULL
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
			WHERE cddc.Status = 1
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'La distribución del elemento del costo ' + cddc.Code + ' asociado a la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + ap.Code + ' no se encuentra confirmada' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
				WHERE cddc.Status = 1
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable AND apdc.IsDirectCost = 1
			JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
			WHERE cddc.Status = 2 AND ap.DeductibleIva = 1
			GROUP BY ap.Id, cddc.Value
			HAVING SUM(apdc.Value) <> cddc.Value
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'El valor distribuido en la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + ap.Code + ' (' + CAST(CAST(SUM(apdc.Value) AS FLOAT) AS VARCHAR(20)) + ')  es diferente al registrado en la distribución de elementos del costo ' + cddc.Code + ' (' + CAST(CAST(cddc.Value AS FLOAT) AS VARCHAR(20)) + ')' AS Message, 
					'' AS Consecutive, ap.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable AND apdc.IsDirectCost = 1
				JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
				WHERE cddc.Status = 2
				GROUP BY ap.Id, ap.Code, ap.BillNumber, cddc.Code, cddc.Value
				HAVING SUM(apdc.Value) <> cddc.Value
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable AND apdc.IsDirectCost = 1
			JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
			WHERE cddc.Status = 2 AND ap.DeductibleIva = 0 AND apdc.IdCostCenter IS NOT NULL
			GROUP BY ap.Id, cddc.Value
			HAVING SUM(apdc.Value) <> cddc.Value
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'El valor distribuido en la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + ap.Code + ' (' + CAST(CAST(SUM(apdc.Value) AS FLOAT) AS VARCHAR(20)) + ')  es diferente al registrado en la distribución de elementos del costo ' + cddc.Code + ' (' + CAST(CAST(cddc.Value AS FLOAT) AS VARCHAR(20)) + ')' AS Message, 
					'' AS Consecutive, ap.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable AND apdc.IsDirectCost = 1
				JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
				WHERE cddc.Status = 2
				GROUP BY ap.Id, ap.Code, ap.BillNumber, cddc.Code, cddc.Value
				HAVING SUM(apdc.Value) <> cddc.Value
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
			WHERE cddc.Status = 2 AND (YEAR(ap.DocumentDate) <> cddc.Year OR MONTH(ap.DocumentDate) <> cddc.Month)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT 999 AS CodeMessage, 
					'El periodo (' + CONCAT(cddc.Year, '-', cddc.Month) + ') de la distribución del elemento del costo ' + cddc.Code + ' no corresponde con la fecha (' + FORMAT(ap.DocumentDate, 'yyy/MM/dd') + ') de factura ' + ap.BillNumber + ' de la cuenta por pagar ' + ap.Code AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				JOIN Cost.CostDistributionDirectCost cddc WITH(NOLOCK) ON ap.Id = cddc.AccountPayableId
				WHERE cddc.Status = 2 AND (YEAR(ap.DocumentDate) <> cddc.Year OR MONTH(ap.DocumentDate) <> cddc.Month)
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(ap.DocumentDate) = cm.Year AND MONTH(ap.DocumentDate) = cm.Month
			WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La fecha del documento (' + FORMAT(ap.DocumentDate, 'yyy/MM/dd') + ') asociado a la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code + ' no se encuentra en un periodo abierto de contabilidad' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(ap.DocumentDate) = cm.Year AND MONTH(ap.DocumentDate) = cm.Month
				WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(ap.ServicePeriodDate) = cm.Year AND MONTH(ap.ServicePeriodDate) = cm.Month
			WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage, 
					'La fecha de radicación (' + FORMAT(ap.ServicePeriodDate, 'yyy/MM/dd') + ') asociado a la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code + ' no se encuentra en un periodo abierto de contabilidad' AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				LEFT JOIN GeneralLedger.ClosedMonth cm WITH(NOLOCK) ON YEAR(ap.ServicePeriodDate) = cm.Year AND MONTH(ap.ServicePeriodDate) = cm.Month
				WHERE cm.Id IS NULL OR ISNULL(cm.Status, 0) = 0

			ROLLBACK TRANSACTION
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			LEFT JOIN Payments.SettingPayments sp WITH(NOLOCK) ON ap.IdOperatingUnit = sp.IdOperatingUnit
			WHERE sp.Id IS NULL
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage, 
					'No existen parámetros de pago en la unidad operativa asociada a la factura ' + ap.BillNumber + ' de la cuenta por pagar ' + t.Code AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			LEFT JOIN Payments.SettingPayments sp WITH(NOLOCK) ON ap.IdOperatingUnit = sp.IdOperatingUnit
			WHERE sp.Id IS NULL

			ROLLBACK TRANSACTION
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		IF EXISTS ( 
			SELECT 1 
			FROM @TableAccountPayable t
			JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
			JOIN Payments.DocumentSupport ds WITH(NOLOCK) ON ap.DocumentSupportId = ds.Id
			WHERE ds.Status = 0 OR (ap.DocumentDate < ds.InitialDate OR ap.DocumentDate >= ds.FinalDate)
		)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
				SELECT DISTINCT 999 AS CodeMessage, 
					IIF
					(
						ds.Status = 0, 'La autorización de documento soporte ' + ds.Code + ' se encuentra inactiva',
						'La autorización de documento soporte ' + ds.Code + ' no se encuentra vigente para la fecha de la cuenta por pagar'
					) AS Message, 
					'' AS Consecutive, t.Code
				FROM @TableAccountPayable t
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON t.Code = ap.Code
				JOIN Payments.DocumentSupport ds WITH(NOLOCK) ON ap.DocumentSupportId = ds.Id
				WHERE ds.Status = 0 OR (ap.DocumentDate < ds.InitialDate OR ap.DocumentDate >= ds.FinalDate)

			ROLLBACK TRANSACTION
			SELECT CodeMessage, Message, Consecutive FROM @TableResult
			RETURN
		END

		-- Eliminamos aquellas facturas que no pasaron el proceso de validación
		DELETE tap
		FROM @TableAccountPayable tap
		JOIN @TableResult tr ON tap.Code = tr.Code

		/***************************** CREACIÓN DOCUMENTO CONTABLE *****************************/

		IF EXISTS ( SELECT 1 FROM @TableAccountPayable )
		BEGIN
			UPDATE tap
				SET tap.JournalVoucherTypeId = sp.IdJournalVoucherAccountPayable
			FROM @TableAccountPayable tap
			JOIN Payments.AccountPayable ap ON tap.Code = ap.Code
			JOIN Payments.SettingPayments sp ON ap.IdOperatingUnit = sp.IdOperatingUnit
			WHERE tap.JournalVoucherTypeId IS NULL

			/***************************** TODOS LOS DETALLES CONTABLES *****************************/
			declare @OriginEntity as VARCHAR(250),
					@CostDistributionDirectCostId as INT,
					@AccountPayableSameSupplier as BIT = 0,
					@CostDistributionDirectCostThirdPartyId as INT

			select @OriginEntity = OriginEntityName ,
				   @CostDistributionDirectCostId = ap.EntityId
			from @TableAccountPayable tmpp
			join Payments.AccountPayable ap on tmpp.Code = ap.Code
			where ap.Code = tmpp.Code

			if(@OriginEntity = 'CostDistributionDirectCost')
			BEGIN
				select @AccountPayableSameSupplier = AccountPayableSameSupplier,
					   @CostDistributionDirectCostThirdPartyId = ThirdPartyId
				from Cost.CostDistributionDirectCost where id = @CostDistributionDirectCostId
			END

			INSERT INTO @JournalVourcherDetailMassive
				( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue, AccountPayableTempId )
				SELECT
					apdc.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					IIF(apdc.Nature = 1, apdc.Value, 0) AS DebitValue,
					IIF(apdc.Nature = 1, 0, apdc.Value) AS CreditValue,
					apdc.IdRetentionConcept AS IdRetention,
					apdc.Percentage AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					IIF(apdc.BillingValue = 0, ap.InvoiceValue, apdc.BillingValue) AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				where (apdc.RateIva IS NULL OR (ap.EntityName IS NOT NULL AND ap.EntityName = 'EntranceVoucher'))
					
				UNION ALL

				SELECT
					ap.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,ap.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					ap.IdCostCenter AS IdCostCenter,
					'' AS Detail,
					0 AS DebitValue,
					ap.Value AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					NULL AS BaseValue,
					NULL AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code

				/********************** IVA DESCONTABLE ******************************************/

				UNION ALL
				SELECT
					apdc.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					IIF(apdc.Nature = 1, apdc.BaseValue, 0) AS DebitValue,
					IIF(apdc.Nature = 1, 0, apdc.BaseValue) AS CreditValue,
					apdc.IdRetentionConcept AS IdRetention,
					apdc.Percentage AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					IIF(apdc.BillingValue = 0, ap.InvoiceValue, apdc.BillingValue) AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				WHERE apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 2 and (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				UNION ALL

				SELECT
					gli.IdAccountPurchaseService AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					NULL AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					apdc.IvaValue AS DebitValue,
					0 AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					apdc.BillingValue AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				JOIN GeneralLedger.GeneralLedgerIVA gli on gli.Id = apdc.RateIva
				WHERE apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 2 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				/********************** IVA DESCONTABLE ******************************************/

				/********************** IVA AL COSTO (CONTROL FISCAL) ****************************/
				
				UNION ALL
				SELECT
					apdc.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					IIF(apdc.Nature = 1, apdc.BaseValue + apdc.IvaValue, 0) AS DebitValue,
					IIF(apdc.Nature = 1, 0, apdc.BaseValue + apdc.IvaValue) AS CreditValue,
					apdc.IdRetentionConcept AS IdRetention,
					apdc.Percentage AS RetentionRate,
					apdc.TotalConcept AS BaseValue,
					IIF(
						  apdc.BillingValue < apdc.TotalConcept,
						  apdc.TotalConcept,
						  IIF(apdc.BillingValue = 0, apdc.TotalConcept, apdc.BillingValue)
						) AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				where apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 1 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				UNION ALL
				SELECT
					gli.IdAccountDebitControlFiscal AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					NULL AS IdCostCenter,
					CONCAT('IVA ',gli.Percentage,' %',' - ',gli.Name) AS Detail,
					SUM(apdc.IvaValue) AS DebitValue,
					0 AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					apdc.BaseValue AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				JOIN GeneralLedger.GeneralLedgerIVA gli on gli.Id = apdc.RateIva
				where apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 1 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')
				GROUP BY gli.IdAccountDebitControlFiscal, apdc.IdThirdParty, apdc.IsDirectCost, ap.Coments, apdc.Detail, apdc.BaseValue, tap.TempId, gli.Percentage, gli.Name

				UNION ALL
				SELECT
					gli.IdAccountCreditControlFiscal AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					NULL AS IdCostCenter,
					CONCAT('IVA ',gli.Percentage,' %',' - ',gli.Name) AS Detail,
					0 AS DebitValue,
					SUM(apdc.IvaValue) AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					apdc.BaseValue AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				JOIN GeneralLedger.GeneralLedgerIVA gli on gli.Id = apdc.RateIva
				where apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 1 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')
				GROUP BY gli.IdAccountCreditControlFiscal, apdc.IdThirdParty, apdc.IsDirectCost, ap.Coments, apdc.Detail, apdc.BaseValue, tap.TempId, gli.Percentage, gli.Name

				/********************** IVA AL COSTO (CONTROL FISCAL) ****************************/

				/********************** IVA AL COSTO *********************************************/

				UNION ALL
				SELECT
					apdc.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					IIF(apdc.IsDirectCost = 1, ISNULL(ap.Coments + CHAR(13) + CHAR(10), ''), '') + apdc.Detail AS Detail,
					IIF(apdc.Nature = 1, apdc.BaseValue, 0) AS DebitValue,
					IIF(apdc.Nature = 1, 0, apdc.BaseValue) AS CreditValue,
					apdc.IdRetentionConcept AS IdRetention,
					apdc.Percentage AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					IIF(apdc.BillingValue = 0, ap.InvoiceValue, apdc.BillingValue) AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				where apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 4 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				UNION ALL

				SELECT
					apdc.IdAccount AS IdMainAccount,
					IIF(@AccountPayableSameSupplier = 0 ,apdc.IdThirdParty,@CostDistributionDirectCostThirdPartyId) AS IdThirdParty,
					apdc.IdCostCenter AS IdCostCenter,
					CONCAT('IVA ',gli.Percentage,' %',' - ',gli.Name) AS Detail,
					IIF(apdc.Nature = 1, apdc.IvaValue, 0) AS DebitValue,
					IIF(apdc.Nature = 1, 0, apdc.IvaValue) AS CreditValue,
					NULL AS IdRetention,
					NULL AS RetentionRate,
					apdc.BaseValue AS BaseValue,
					IIF(apdc.BillingValue = 0, ap.InvoiceValue, apdc.BillingValue) AS BillingValue,
					tap.TempId AccountPayableTempId
				FROM @TableAccountPayable tap
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
				JOIN Payments.AccountPayableDetailConcept apdc WITH(NOLOCK) ON ap.Id = apdc.IdAccountPayable
				JOIN GeneralLedger.GeneralLedgerIVA gli on gli.Id = apdc.RateIva
				where apdc.RateIva IS NOT NULL AND ap.TaxRegistration = 4 AND (ap.EntityName IS NULL OR ap.EntityName <> 'EntranceVoucher')

				/********************** IVA AL COSTO *********************************************/

			/************************************  RECORRER LAS CUENTAS POR PAGAR ************************************/

			DECLARE @Id INT = 0,
					@Code VARCHAR(20),
					@rows INT = 1,
					@Detail VARCHAR(MAX),
					---------------------------
					@CodeMessageResultObligation INT, 
					@MessageResultObligation VARCHAR(MAX),
					---------------------------
					@AccountPayableRows INT,
					@AccountPayableId INT,
					@DocumentSupportId INT,
					@InvoicePrefix VARCHAR(6),
					@AuthorizationConsecutive BIGINT,
					@AuthorizationInitialInvoice BIGINT,
					@AuthorizationFinalInvoice BIGINT

			WHILE @rows > 0
			BEGIN
				SELECT TOP 1 
					@Id = tap.TempId,
					@Code = tap.Code,
					---------------------------
					@AccountPayableRows = 1,
					@AccountPayableId = 0
				FROM @TableAccountPayable tap
				WHERE tap.TempId > @Id
				ORDER BY tap.TempId

				-- Verifico que haya encontrado un resultado
				SET @rows = @@RowCount
				IF @rows = 0
				BEGIN
					BREAK
				END

				SET @Detail = ''
				DELETE FROM @JournalVourcherTmp
				DELETE FROM @JournalVourcherDetailTmp

				-- Documentos de cargue masivo que no se confirmaron
				UPDATE tap
					SET tap.OriginEntityName = NULL
				FROM @TableAccountPayable tap
				WHERE tap.OriginEntityName IN ('InitialBalance', 'LoadMassive', 'CostDistributionDirectCost', 'MedicalFeesLiquidation')

				SELECT @Detail = STUFF((
					SELECT
						CHAR(13) + CHAR(10) +  'Factura No. ' + ap.BillNumber + ' - Proveedor: (' + s.Code + ' - ' + s.Name + ') - Descripción: ' + ap.Coments + IIF(ap.DocumentSupportId = NULL,'',IIF(ap.DocumentSupportId is null,'',' DOCUMENTO SOPORTE N° "'+ISNULL(ds.InvoicePrefix,'')+' '+ISNULL(ds.ResolutionNumber,''))+'"' )
					FROM @TableAccountPayable tap
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
					JOIN Common.Supplier s WITH(NOLOCK) ON ap.IdSupplier = s.Id
					LEFT JOIN Payments.DocumentSupport ds WITH(NOLOCK) ON ap.DocumentSupportId = ds.Id
					WHERE tap.TempId = @Id
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				INSERT INTO @JournalVourcherTmp
					( LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, OriginEntityName, CurrencyId)
					SELECT TOP 1
						NULL LegalBookId, 
						tap.JournalVoucherTypeId IdJournalVoucher, 
						ap.DocumentDate VoucherDate, 
						2 Status, 
						CASE ap.EntityName
							WHEN 'FixedAssetEntry' THEN 'Ingreso de Activo No. ' + ap.EntityCode + ' - '
							ELSE ''
						END + @Detail Detail,
						ap.Code EntityCode,
						ap.Id EntityId,
						'AccountPayable' EntityName,
						tap.OriginEntityName OriginEntityName,
						isnull(ap.CurrencyId,@OfficialCurrencyId)
					FROM @TableAccountPayable tap
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
					WHERE tap.TempId = @Id

				INSERT INTO @JournalVourcherDetailTmp
					( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
					SELECT IdMainAccount, jvd.IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue
					FROM @JournalVourcherDetailMassive jvd
					WHERE jvd.AccountPayableTempId = @Id

					--Eliminamos cuentas en 0
                 DELETE jv FROM @JournalVourcherDetailTmp jv WHERE jv.DebitValue = 0 AND jv.CreditValue = 0

				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				--Se consume el sp que guarda el comprobante contable
				Declare @CodeMessage Int,
					@Message Varchar(Max),
					@IdJournalVoucherResult Int

				--Se consume el sp que guarda el movimiento contable
				insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

				select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
				from @resultJournalVoucher rjv

				--Se valida que no hayan errores en el guardado del comprobante contable
				IF @CodeMessage = '999' 
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 999 AS CodeMessage, 'No se confirmó la cuenta por pagar ' + tap.Code + ' por ' + @Message AS Message, '' AS Consecutive, tap.Code
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id
				END
				ELSE
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 0 AS CodeMessage, 'Se confirmó correctamente la cuenta por pagar ' + @Code AS Message, '' AS Consecutive, @Code AS Code

					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT DISTINCT
							0 AS CodeMessage,
							'Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
							'' AS Consecutive,
							tap.Code
						FROM @TableAccountPayable tap
						JOIN GeneralLedger.JournalVoucherTypes jvt ON tap.JournalVoucherTypeId = jvt.Id
						WHERE tap.TempId = @Id

						UPDATE tap 
							SET tap.Status = 2
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id
				END

				DECLARE @LegalBookId INT = 0,
						@LegalBookRows INT = 1

				WHILE @LegalBookRows > 0
				BEGIN
					SELECT TOP 1 @LegalBookId = tapdcnh.LegalBookId
					FROM @TableAccountPayableDetailConceptNotHomologated tapdcnh
					INNER JOIN @TableAccountPayable tap on tap.TempId = tapdcnh.ParentId
					WHERE tapdcnh.ParentId = @Id 
						AND tapdcnh.LegalBookId > @LegalBookId and isnull(tap.OriginEntityName,'') <> 'FixedAssetEntry'
					ORDER BY tapdcnh.LegalBookId

					-- Verifico que haya encontrado un resultado
					SET @LegalBookRows = @@RowCount

					IF EXISTS (SELECT 1 FROM @TableAccountPayable tap WHERE tap.TempId = @Id AND tap.Status = 1)
					BEGIN
						SET @LegalBookRows = 0
					END

					IF @LegalBookRows = 0
					BEGIN
						BREAK
					END

					UPDATE jv
						SET jv.LegalBookId = @LegalBookId
					FROM @JournalVourcherTmp jv

					DELETE FROM @JournalVourcherDetailTmp

					INSERT INTO @JournalVourcherDetailTmp
						( IdMainAccount, IdThirdParty, IdCostCenter, Detail, DebitValue, CreditValue, IdRetention, RetentionRate, BaseValue, BillingValue )
						SELECT
							tapdcnh.IdMainAccount,
							tapdcnh.IdThirdParty,
							tapdcnh.IdCostCenter,
							tapdcnh.Detail,
							tapdcnh.DebitValue,
							tapdcnh.CreditValue,
							tapdcnh.IdRetention,
							tapdcnh.RetentionRate,
							tapdcnh.BaseValue,
							tapdcnh.BillingValue
						FROM @TableAccountPayableDetailConceptNotHomologated tapdcnh
						WHERE tapdcnh.ParentId = @Id AND tapdcnh.LegalBookId = @LegalBookId
					
						UNION ALL

						SELECT
							ap.IdAccount AS IdMainAccount,
							ap.IdThirdParty AS IdThirdParty,
							ap.IdCostCenter AS IdCostCenter,
							NULL AS Detail,
							0 AS DebitValue,
							ap.Value AS CreditValue,
							NULL AS IdRetention,
							NULL AS RetentionRate,
							NULL AS BaseValue,
							NULL AS BillingValue
						FROM @TableAccountPayable tap
						JOIN Payments.AccountPayable ap WITH(NOLOCK) ON tap.Code = ap.Code
						WHERE tap.TempId = @Id AND ap.Value > 0 

					--Eliminamos cuentas en 0
					DELETE jv FROM @JournalVourcherDetailTmp jv WHERE jv.DebitValue = 0 AND jv.CreditValue = 0

					--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
					SELECT @JournalVoucherXML = CONVERT(xml, 
						(
							SELECT * FROM @JournalVourcherTmp JournalVoucher 
							JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					SELECT @CodeMessage = NULL, @Message = NULL, @IdJournalVoucherResult = NULL

					--Se consume el sp que guarda el movimiento contable
					insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 

					select 
					@CodeMessage = rjv.code, 
					@Message = rjv.MessageResult, 
					@IdJournalVoucherResult = rjv.IdJournalVoucher
					from @resultJournalVoucher rjv

					--Se valida que no hayan errores en el guardado del comprobante contable
					IF @CodeMessage = '999' 
					BEGIN
						INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
							SELECT 999 AS CodeMessage, lb.Code + ' - ' + lb.Name +  ': No se confirmó la cuenta por pagar ' + tap.Code + ' por ' + @Message AS Message, '' AS Consecutive, tap.Code
							FROM @TableAccountPayable tap
							JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON lb.Id = @LegalBookId
							WHERE tap.TempId = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id
					END
					ELSE
					BEGIN
						INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
							SELECT DISTINCT
								0 AS CodeMessage,
								lb.Code + ' - ' + lb.Name +  ': Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name AS Message,
								'' AS Consecutive,
								tap.Code
							FROM @TableAccountPayable tap
							JOIN GeneralLedger.LegalBook lb ON lb.Id = @LegalBookId
							JOIN GeneralLedger.JournalVoucherTypes jvt ON tap.JournalVoucherTypeId = jvt.Id
							WHERE tap.TempId = @Id

						UPDATE tap 
							SET tap.Status = 2
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id
					END
				END

				--Se generan las obligaciones
				EXEC Budget.SP_GenerateObligationsByAccountPayable @Code, @CodeUser, @CodeMessageResultObligation OUTPUT, @MessageResultObligation OUTPUT
				IF @CodeMessageResultObligation = 999
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 999 AS CodeMessage, 'No se confirmó la cuenta por pagar ' + tap.Code + ' por ' + @MessageResultObligation AS Message, '' AS Consecutive, tap.Code
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id

						UPDATE tap 
							SET tap.Status = 1
						FROM @TableAccountPayable tap
						WHERE tap.TempId = @Id
				END
				ELSE IF @MessageResultObligation <> ''
				BEGIN
					INSERT @TableResult (CodeMessage, Message, Consecutive, Code)
						SELECT 0 AS CodeMessage, @MessageResultObligation AS Message, '' AS Consecutive, @Code AS Code
				END
			END

			DELETE pc 
			FROM @TableAccountPayable tap
			JOIN Payments.PaymentsControl pc ON pc.DocumentType = 1 AND pc.DocumentNumber = tap.Code
			WHERE tap.Status = 2

			UPDATE ap
				SET ap.ModificationUser = @CodeUser,
					ap.ModificationDate = [Common].[GETDATE](),
					ap.ConfirmationUser = @CodeUser,
					ap.ConfirmationDate = [Common].[GETDATE](),
					ap.Status = 2
			FROM @TableAccountPayable tap
			JOIN Payments.AccountPayable ap ON tap.Code = ap.Code
			WHERE tap.Status = 2

			UPDATE dc
				SET dc.ModificationUser = @CodeUser,
					dc.ModificationDate = [Common].[GETDATE](),
					dc.ConfirmationUser = @CodeUser,
					dc.ConfirmationDate = [Common].[GETDATE](),
					dc.Status = 2
			FROM @TableAccountPayable tap
			JOIN Payments.AccountPayable ap ON tap.Code = ap.Code
			JOIN Payments.DeferredCausation dc ON ap.Id = dc.IdAccountPayable
			WHERE tap.Status = 2
		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @error_message VARCHAR(MAX) = (SELECT Message FROM @TableResult WHERE CodeMessage = 999)
		IF NOT EXISTS (SELECT 1 FROM @TableResult WHERE CodeMessage = 999)
		BEGIN
			INSERT @TableResult (CodeMessage, Message, Consecutive)
				SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Consecutive
		END
		ELSE
		BEGIN
			THROW 51000, @error_message, 1
		END
	END CATCH

	SELECT CodeMessage, Message, Consecutive 
	FROM @TableResult
	ORDER BY Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma cuentas por pagar recibidas en formato XML, generando los comprobantes contables (asientos de diario) correspondientes en el libro mayor. Valida que las facturas enviadas existan y no estén duplicadas, homologa los conceptos contables y registra débitos y créditos por cada cuenta por pagar, incluyendo retenciones, centros de costo y terceros. Consulta la configuración de la empresa (moneda oficial y tipo de registro de IVA) desde GeneralLedger.CompanySettings para asegurar que la contabilización se realice en la moneda y régimen tributario correctos. Devuelve mensajes de resultado por cada cuenta procesada, indicando si fue confirmada exitosamente o si presentó errores, y soporta tanto confirmaciones individuales como masivas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmAccountsPayable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmAccountsPayable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma cuentas por pagar (individuales o masivas), validando reglas contables y fiscales, generando los comprobantes contables (incluyendo libros no homologables y manejo de IVA) y las obligaciones presupuestales asociadas.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener al menos una cuenta por pagar en /ListAccountPayable/AccountPayable; Cada Code del XML debe existir en Payments.AccountPayable; La cuenta por pagar debe estar en Status = 1 (no confirmada/anulada); La cuenta por pagar debe tener Value distinto de cero y Balance distinto de cero; Debe existir al menos un registro en Payments.AccountPayableShares para la cuenta por pagar; Las distribuciones en Cost.CostDistributionDirectCost asociadas deben estar confirmadas (Status<>1) y cuadrar en valor con la suma de AccountPayableDetailConcept.IsDirectCost=1; El periodo (año/mes) de la distribución de costo debe coincidir con la fecha del documento de la factura; La fecha del documento y la fecha de radicación (ServicePeriodDate) deben estar en un periodo abierto en GeneralLedger.ClosedMonth; Debe existir Payments.SettingPayments para la unidad operativa; Si tiene DocumentSupport, debe estar activo (Status<>0) y la fecha del documento entre InitialDate y FinalDate; En confirmación masiva (@IsMasiveConfirm=1) no se permiten cuentas por pagar con libros no homologables', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando no hay cuentas por pagar en el XML se inserta mensaje 999 ''No se envió ninguna factura a confirmar'' y se retorna; [INSERT] @TableResult: Cuando @IsMasiveConfirm=1 y existen detalles no homologables se inserta error 999 y se retorna; [INSERT] @TableResult: Por cada validación fallida (cuenta inexistente, estado<>1, valor/saldo cero, sin cuotas, distribución de costo no confirmada o descuadrada, periodo de costo inconsistente, periodos contables cerrados, sin SettingPayments, DocumentSupport inactivo o fuera de vigencia) se inserta mensaje 999; [DELETE] @TableAccountPayable: Se eliminan las cuentas por pagar duplicadas por Code conservando la de menor TempId, y se eliminan las que fallaron alguna validación (existen en @TableResult); [INSERT] GeneralLedger.JournalVouchers: Por cada cuenta por pagar válida se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement para crear el comprobante contable principal y, si aplica, uno adicional por cada LegalBookId no homologable; [UPDATE] @TableAccountPayable: Si JournalVoucherTypeId es NULL se asigna SettingPayments.IdJournalVoucherAccountPayable de la unidad operativa; [UPDATE] @TableAccountPayable: OriginEntityName se anula cuando es ''InitialBalance'',''LoadMassive'',''CostDistributionDirectCost'' o ''MedicalFeesLiquidation''; [UPDATE] @TableAccountPayable: Status=1 cuando el SP de comprobante o el SP de obligaciones devuelven CodeMessage=999; Status=2 cuando el comprobante se generó correctamente; [DELETE] Payments.PaymentsControl: Se eliminan los registros con DocumentType=1 y DocumentNumber=Code para las cuentas por pagar con Status=2 (confirmadas exitosamente); [UPDATE] Payments.AccountPayable: Para tap.Status=2 se actualizan ModificationUser/Date, ConfirmationUser/Date con @CodeUser y Common.GETDATE() y Status=2; [UPDATE] Payments.DeferredCausation: Para las cuentas por pagar confirmadas (tap.Status=2) se actualizan ModificationUser/Date, ConfirmationUser/Date y Status=2 en sus causaciones diferidas asociadas; [RETURN_RESULT] @TableResult: Al final se retorna el listado de mensajes (CodeMessage, Message, Consecutive) ordenado por Code; [RAISERROR] @TableResult: En el CATCH, si ya existe un mensaje 999 previo se hace THROW 51000 con ese mensaje; si no, se inserta el ERROR_MESSAGE() con la línea', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmAccountsPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmAccountsPayable';
-- GO
