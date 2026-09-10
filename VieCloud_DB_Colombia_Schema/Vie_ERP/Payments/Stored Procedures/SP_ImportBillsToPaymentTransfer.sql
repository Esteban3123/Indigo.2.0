
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa y valida facturas de proveedores para registrarlas en un cruce de anticipos contra cuentas por pagar (CxP). Recibe dos parámetros XML: uno con los datos de las filas (número de factura, código de proveedor, cuota y valor a cruzar) y otro con los parámetros generales del traslado (tipo de cruce, proveedor y moneda). Realiza validaciones masivas sobre los registros: detecta facturas duplicadas, valores de cruce inválidos o negativos, facturas inexistentes o no asociadas al proveedor en Payments.AccountPayable y Common.Supplier, y facturas en estado distinto a ''Confirmado''. Devuelve un resultado detallado por cada fila indicando si la importación fue exitosa o el motivo del error, utilizado en el formulario de cruce de anticipo contra cuentas por pagar dentro del módulo de pagos y tesorería.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportBillsToPaymentTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara, a partir de un XML de facturas pegadas, el listado de cuentas por pagar y sus cuotas a cruzar contra un anticipo, devolviendo errores y los registros listos para una transferencia de pago.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de parámetros debe contener TransferType, SupplierId y CurrencyId.; El XML de objetos debe traer filas con SupplierCode, BillNumber, CrossShare y StatusField inicial.; Debe existir al menos un registro en GeneralLedger.CompanySettings para obtener la moneda oficial.; Si TransferType=1 se usa un único proveedor (@SupplierId); si TransferType=2 se resuelve el proveedor por SupplierCode de cada fila.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesan e insertan resultados válidos las filas con StatusField=1 tras todas las validaciones.; El cruce contra una factura nunca excede su Balance ni el saldo individual de cada cuota.; La suma de los saldos de las cuotas debe igualar el saldo de la factura para considerarla consistente.; Solo se cruzan facturas en estado 2 (vigentes/aprobadas).; Los montos siempre se devuelven convertidos a la moneda solicitada (o a la oficial si no se especifica) usando Common.CurrencyConverter.; No se permiten facturas duplicadas (mismo proveedor+número) en un mismo lote.; El valor a cruzar debe ser numérico y estrictamente mayor a 0.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Factura de proveedor; Proveedor; Cuotas de pago; Cruce de anticipo vs CxP; Saldo de factura; Conversión de moneda; TRM; Plan de cuentas (cuenta principal); Centro de costos; Tercero', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @TableXmlObject: Cuando existe más de un registro con el mismo SupplierCode+BillNumber y StatusField=1 → marca StatusField=0 con mensaje ''Existe mas de un registro asociado a la Factura...''.; [UPDATE] @TableXmlObject: Cuando ISNUMERIC(CrossShare)<>1 → StatusField=0 con mensaje ''El valor a cruzar... es invalido''.; [UPDATE] @TableXmlObject: Convierte CrossShare a numérico (CrossShareValue) solo en filas con StatusField=1.; [UPDATE] @TableXmlObject: Cuando CrossShareValue<=0 → StatusField=0 con mensaje ''no puede ser igual o menor a 0''.; [UPDATE] @TableXmlObject: Cuando no se encuentra AccountPayable con BillNumber+IdSupplier → StatusField=0 con mensaje ''no existe o no se encuentra relacionada con el proveedor''.; [UPDATE] @TableXmlObject: Cuando AccountPayable.Status<>2 → StatusField=0 con mensaje indicando estado (1=Registrado, 3=Anulado, 4=Reversado, otro=N/A).; [UPDATE] @TableXmlObject: Cuando la factura no tiene cuotas en AccountPayableShares → StatusField=0 con mensaje ''no tiene cuotas''.; [UPDATE] @TableXmlObject: Cuando ap.Balance <> SUM(aps.Balance) → StatusField=0 con mensaje ''El saldo de la factura... no coincide con el de sus cuotas''.; [UPDATE] @TableXmlObject: Cuando ap.Balance<=0 → StatusField=0 con mensaje ''no tiene saldo''.; [UPDATE] @TableXmlObject: Cuando CrossShareValue > ap.Balance → StatusField=0 con mensaje ''no puede ser mayor al saldo''.; [INSERT] @TableResult: Inserta como errores las filas de @TableXmlObject con StatusField=0 (BillNumber, MessageField, CrossShareValue).; [INSERT] @TableResult: Por cada cuota (AccountPayableShares) de cada AccountPayable válida, mientras quede saldo a cruzar, inserta una fila con StatusField=1 distribuyendo CrossShareValue = MIN(saldo restante, saldo de la cuota), convirtiendo montos a la moneda solicitada vía Common.CurrencyConverter.; [INSERT] @TableResult: En CATCH inserta una fila con StatusField=0 y MessageField = ERROR_MESSAGE() + línea.; [RETURN_RESULT] @TableResult: Al final retorna SELECT * FROM @TableResult con errores y registros válidos.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TransferType = 1 → El proveedor se resuelve por @SupplierId (único) y los mensajes omiten el código de proveedor. else TransferType = 2: el proveedor se resuelve por SupplierCode de cada fila e incluye ''(Proveedor: ...)'' en mensajes.; si AccountPayable.Status = 2 → La factura es elegible para cruce. else Se marca como inválida indicando estado Registrado/Anulado/Reversado/N/A según valor.; si CrossShareBalance restante > Balance de la cuota → Se aplica el saldo de la cuota como CrossShareValue de ese registro. else Se aplica todo el CrossShareBalance restante.; si CrossShareBalance <= 0 al iterar cuotas → Sale del loop de cuotas (no se generan más distribuciones para esa factura).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverter', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Supplier; Payments.AccountPayable; Payments.AccountPayableShares; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPaymentTransfer';
-- GO
