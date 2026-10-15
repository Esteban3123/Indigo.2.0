
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-05-24
-- Description:	Procedimiento que se encarga de el copyPaste de facturas del form de cruce de anticipo vs CxP
-- =============================================
CREATE PROCEDURE [Payments].[SP_ImportBillsToPaymentTransfer] 
	@XmlObject AS XML,
	@XmlParameters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CountFields INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		SupplierCode VARCHAR(100), 
		BillNumber VARCHAR(100), 
		CrossShare VARCHAR(100),
		CrossShareValue NUMERIC(20,4) DEFAULT (0)
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- SUPPLIER -- 
		SupplierId INT, 
		SupplierCodeName VARCHAR(500), 		
		-- ACCOUNTPAYABLE -- 
		AccountPayableId INT, 
		BillNumber VARCHAR(100), 
		Balance NUMERIC(20,4), 
		-- ACCOUNTPAYABLESHARE -- 
		AccountPayableShareId INT, 
		Share INT, 
		BalanceShare NUMERIC(20,4), 
		-- ACCOUNTDETAIL -- 
		MainAccountId INT,
		MainAccountNumberName VARCHAR(500),
		ThirdPartyId INT,
		CostCenterId INT, 
		-- PAYMENTTRANSFER -- 
		CrossShareValue NUMERIC(20,4),
		TRMValue NUMERIC(20,5)
	)
	
	--Parametros
	DECLARE @TransferType INT,
			@SupplierId INT,
			------------------------------
			@CurrencyId INT,
			@OffcialCurrencyId INT

	--Datos para recorrer las cuentas por pagar validas
	DECLARE @Rows INT,
			@AccountPayableId INT, 
			----------------------------
			@Row_Details INT,
			@AccountPayableShareId INT,
			@ShareBalance NUMERIC(20,4),
			@CrossShareBalance NUMERIC(20,4),
			@CrossShareValue NUMERIC(20,4)

	BEGIN TRY

		SELECT 
			@TransferType = t.x.value('TransferType[1]','int'),
			@SupplierId = t.x.value('SupplierId[1]','int'),
			@CurrencyId = T.x.value('CurrencyId[1]', 'int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, SupplierCode, BillNumber, CrossShare)
			SELECT 
				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField,
				t.x.value('SupplierCode[1]','varchar(100)') as SupplierCode,
				t.x.value('BillNumber[1]','varchar(100)') as BillNumber,
				t.x.value('CrossShare[1]','varchar(100)') as CrossShare
			FROM @XmlObject.nodes('/Data/Row') t(x)

		SELECT TOP 1 @OffcialCurrencyId= cs.OfficialCurrencyId
		FROM GeneralLedger.CompanySettings cs

		/************************************* VALIDACIONES MASIVAS *************************************/

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
			GROUP BY t.SupplierCode, t.BillNumber
			HAVING COUNT(*) > 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'Existe mas de un registro asociado a la Factura ' + t.BillNumber + IIF(@TransferType = 1, '', '(Proveedor: ' + t.SupplierCode + ')')
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.SupplierCode, t.BillNumber
				FROM @TableXmlObject t
				WHERE t.StatusField = 1
				GROUP BY t.SupplierCode, t.BillNumber
				HAVING COUNT(*) > 1
			) t2 ON t.SupplierCode = t2.SupplierCode AND t.BillNumber = t2.BillNumber
			WHERE t.StatusField = 1
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.CrossShare) <> 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor a cruzar del registro ' + convert(VARCHAR(3),t.Id) + ' es invalido'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.CrossShare) <> 1
		END

		UPDATE t
			SET t.CrossShareValue = CAST(t.CrossShare AS NUMERIC(20,4))
		FROM @TableXmlObject t
		WHERE t.StatusField = 1

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.CrossShareValue > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor a cruzar del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser igual o menor a 0'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.CrossShareValue > 0)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			LEFT JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			LEFT JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionada con el proveedor'
			FROM @TableXmlObject t
			LEFT JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			LEFT JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND ap.Status <> 2
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' se encuentra en estado ' + 
						CASE ap.Status 
							WHEN 1 THEN 'Registrado'
							WHEN 3 THEN 'Anulado'
							WHEN 4 THEN 'Reversado'
							ELSE 'N/A'
						END						
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND ap.Status <> 2
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			LEFT JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
			WHERE t.StatusField = 1
				AND aps.Id IS NULL
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no tiene cuotas'
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			LEFT JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
			WHERE t.StatusField = 1
				AND aps.Id IS NULL
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
			WHERE t.StatusField = 1
			GROUP BY ap.Id, ap.Balance
			HAVING ap.Balance <> SUM(aps.Balance)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El saldo de la factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no coincide con el de sus cuotas'
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.BillNumber
				FROM @TableXmlObject t
				JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = s.Id
				JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
				WHERE t.StatusField = 1
				GROUP BY ap.Id, ap.Balance, t.BillNumber
				HAVING ap.Balance <> SUM(aps.Balance)
			) t2 ON t.BillNumber = t2.BillNumber
			WHERE t.StatusField = 1
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND NOT (ap.Balance > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no tiene saldo'
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND NOT (ap.Balance > 0)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND (t.CrossShareValue > ap.Balance)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor a cruzar (' + CONVERT(VARCHAR, CAST(t.CrossShareValue AS MONEY), 1) + ') de la factura ' + t.BillNumber + ' del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser mayor al saldo (' + CONVERT(VARCHAR, CAST(ap.Balance AS MONEY), 1) + ')'
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE t.StatusField = 1
				AND (t.CrossShareValue > ap.Balance)
		END

		/************************************* INSERTAR ERRORES *************************************/

		INSERT INTO @TableResult 
			(
				StatusField, MessageField, BillNumber, CrossShareValue
			)
			SELECT StatusField, MessageField, BillNumber, CrossShareValue
			FROM @TableXmlObject
			WHERE StatusField = 0

		/************************************* RECORREMOS LAS CUENTAS POR PAGAR VALIDAS *************************************/
		
		SET @Rows = 1
		SET @AccountPayableId = 0

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@AccountPayableId = ap.Id,
				@CrossShareBalance = t.CrossShareValue
			FROM @TableXmlObject t
			JOIN Common.Supplier s ON (@TransferType = 1 AND s.Id = @SupplierId) OR (@TransferType = 2 AND s.Code = t.SupplierCode)
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = s.Id
			WHERE ap.Id > @AccountPayableId
				AND StatusField = 1
			ORDER BY ap.Id

			SET @Rows = @@RowCount
			IF @Rows = 0 
				BREAK

			/************************************* RECORREMOS LAS CUOTAS E INSERTAMOS LOS AJUSTES *************************************/

			SET @Row_Details = 1
			SET @AccountPayableShareId = 0

			WHILE @Row_Details > 0
			BEGIN
				SELECT TOP 1
					@AccountPayableShareId = aps.Id,
					@ShareBalance = aps.Balance
				FROM Payments.AccountPayableShares aps
				WHERE aps.IdAccountPayable = @AccountPayableId
					AND aps.Id > @AccountPayableShareId
				ORDER BY aps.Id

				SET @Row_Details = @@RowCount
				IF @Row_Details = 0 OR NOT (@CrossShareBalance > 0)
					BREAK

				SET @CrossShareValue = IIF(@CrossShareBalance > @ShareBalance, @ShareBalance, @CrossShareBalance)

				INSERT INTO @TableResult
					(
						StatusField, MessageField, 
						-- SUPPLIER -- 
						SupplierId, 
						SupplierCodeName, 
						-- ACCOUNTPAYABLE -- 
						AccountPayableId, 
						BillNumber, 
						Balance, 
						-- ACCOUNTPAYABLESHARE -- 
						AccountPayableShareId, 
						Share, 
						BalanceShare, 
						-- ACCOUNTDETAIL -- 
						MainAccountId, 
						MainAccountNumberName, 
						ThirdPartyId,
						CostCenterId, 
						-- PAYMENTTRANSFER -- 
						CrossShareValue,
						TRMValue
					)
					SELECT 1, '', 
						-- SUPPLIER -- 
						s.Id, 
						s.Code + ' - ' + s.Name, 
						---- ACCOUNTPAYABLE -- 
						ap.Id, 
						ap.BillNumber, 
						Common.CurrencyConverter(ap.Balance, ISNULL(ap.CurrencyId, @OffcialCurrencyId), ISNULL(@CurrencyId, @OffcialCurrencyId)), 
						---- ACCOUNTPAYABLESHARE -- 
						aps.Id, 
						aps.Share, 
						Common.CurrencyConverter(aps.Balance, ISNULL(ap.CurrencyId, @OffcialCurrencyId), ISNULL(@CurrencyId, @OffcialCurrencyId)), 
						---- ACCOUNTDETAIL -- 
						ma.Id, 
						ma.Number + ' - ' + ma.Name, 
						ap.IdThirdParty,
						ap.IdCostCenter, 
						---- PAYMENTTRANSFER -- 
						Common.CurrencyConverter(@CrossShareValue, ISNULL(ap.CurrencyId, @OffcialCurrencyId), ISNULL(@CurrencyId, @OffcialCurrencyId)),
						Common.CurrencyConverter(1, ISNULL(ap.CurrencyId, @OffcialCurrencyId), ISNULL(@CurrencyId, @OffcialCurrencyId))
					FROM Payments.AccountPayable ap 					
					JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable 
					JOIN Common.Supplier s ON ap.IdSupplier = s.Id
					JOIN GeneralLedger.MainAccounts ma ON ap.IdAccount = ma.Id
					WHERE aps.Id = @AccountPayableShareId
			END
		END
				
	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult
END