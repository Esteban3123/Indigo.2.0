
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-01-31
-- Description:	Procedimiento que se encarga de el copyPaste de facturas del form de notas de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ImportBillsToPortfolioNote] 
	@XmlObject as Xml,
	@XmlParameters as Xml
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
		BillNumber VARCHAR(100), 
		Adjusment VARCHAR(100),
		AdjusmentValue NUMERIC(20,4) DEFAULT (0)
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- ACCOUNTPAYABLE --
		AccountPayableId INT, 
		BillNumber VARCHAR(100), 
		BillDate DATETIME, 
		ExpirationDate DATETIME, 
		[Value] NUMERIC(20,4), 
		Balance NUMERIC(20,4), 
		AdjusmentValue NUMERIC(20,4), 
		[Percentage] NUMERIC(18,2),
		HandlesAddModifyDelete INT,
		IdSupplier INT,
		IdAccount INT,
		IdCostCenter INT,
		IdThirdParty INT,
		-- ACCOUNTPAYABLESHARES --
		AccountPayableShareId INT,
		ValueNoteShare NUMERIC(20,4),
		BalanceNoteShare NUMERIC(20,4)
	)
	
	--Parametros
	DECLARE @Nature INT,
			@SupplierId INT,
			@MainAccountId INT

	--Datos para recorrer las cuentas por pagar validas
	DECLARE @Rows INT,
			@AccountPayableId INT,
			@AdjusmentValue NUMERIC(20,4),
			@Balance NUMERIC(20,4),
			@Count_Shares INT,
			----------------------------
			@Row_Details INT,
			@Current_Share INT,
			@AccountPayableShareId INT,
			@BalanceNoteShare NUMERIC(20,4),
			@AdjustmentBalance NUMERIC(20,4),
			@ValueNoteShare NUMERIC(20,4)

	BEGIN TRY

		SELECT 
			@Nature = t.x.value('Nature[1]','int'),
			@SupplierId = t.x.value('SupplierId[1]','int'),
			@MainAccountId = t.x.value('MainAccountId[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, BillNumber, Adjusment)
			SELECT 
				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField,
				t.x.value('BillNumber[1]','varchar(100)') as BillNumber,
				t.x.value('Adjusment[1]','varchar(100)') as Adjusment
			FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
			GROUP BY t.BillNumber
			HAVING COUNT(*) > 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'Existe mas de un registro asociado a la Factura ' + t.BillNumber
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.BillNumber
				FROM @TableXmlObject t
				WHERE t.StatusField = 1
				GROUP BY t.BillNumber
				HAVING COUNT(*) > 1
			) t2 ON t.BillNumber = t2.BillNumber
			WHERE t.StatusField = 1
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.Adjusment) <> 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor de ajuste del registro ' + convert(VARCHAR(3),t.Id) + ' es invalido'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.Adjusment) <> 1
		END

		UPDATE t
			SET t.AdjusmentValue = CAST(t.Adjusment AS NUMERIC(20,4))
		FROM @TableXmlObject t
		WHERE t.StatusField = 1

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.AdjusmentValue > 0)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor de ajuste del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser igual o menor a 0'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND NOT (t.AdjusmentValue > 0)
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			LEFT JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionada con el proveedor o la cuenta contable de la linea de distribución'
			FROM @TableXmlObject t
			LEFT JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
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
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			WHERE t.StatusField = 1
				AND ap.Status <> 2
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			LEFT JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
			WHERE t.StatusField = 1
				AND aps.Id IS NULL
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no tiene cuotas'
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			LEFT JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
			WHERE t.StatusField = 1
				AND aps.Id IS NULL
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
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
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = @SupplierId
						AND ap.IdAccount = @MainAccountId
				JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
				WHERE t.StatusField = 1
				GROUP BY ap.Id, ap.Balance, t.BillNumber
				HAVING ap.Balance <> SUM(aps.Balance)
			) t2 ON t.BillNumber = t2.BillNumber
			WHERE t.StatusField = 1
		END

		IF @Nature = 1
		BEGIN
			IF EXISTS
			(
				SELECT 1
				FROM @TableXmlObject t
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = @SupplierId
						AND ap.IdAccount = @MainAccountId
				WHERE t.StatusField = 1
					AND NOT (ap.Balance > 0)
			)
			BEGIN
				UPDATE t
					SET t.StatusField = 0,
						t.MessageField = 'La factura ' + t.BillNumber +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no tiene saldo'
				FROM @TableXmlObject t
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = @SupplierId
						AND ap.IdAccount = @MainAccountId
				WHERE t.StatusField = 1
					AND NOT (ap.Balance > 0)
			END

			IF EXISTS
			(
				SELECT 1
				FROM @TableXmlObject t
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = @SupplierId
						AND ap.IdAccount = @MainAccountId
				WHERE t.StatusField = 1
					AND (t.AdjusmentValue > ap.Balance)
			)
			BEGIN
				UPDATE t
					SET t.StatusField = 0,
						t.MessageField = 'La valor del ajuste (' + CAST(CAST(t.AdjusmentValue AS FLOAT) AS VARCHAR(25)) + ') de la factura ' + t.BillNumber + ' del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser mayor al saldo (' + CAST(CAST(ap.Balance AS FLOAT) AS VARCHAR(25)) + ')'
				FROM @TableXmlObject t
				JOIN Payments.AccountPayable ap 
					ON t.BillNumber = ap.BillNumber 
						AND ap.IdSupplier = @SupplierId
						AND ap.IdAccount = @MainAccountId
				WHERE t.StatusField = 1
					AND (t.AdjusmentValue > ap.Balance)
			END
		END

		/************************************* INSERTAR ERRORES *************************************/

		INSERT INTO @TableResult 
			(
				StatusField, MessageField, BillNumber, AdjusmentValue
			)
			SELECT StatusField, MessageField, BillNumber, AdjusmentValue
			FROM @TableXmlObject
			WHERE StatusField = 0

		/************************************* RECORREMOS LAS CUENTAS POR PAGAR VALIDAS *************************************/
		
		SET @Rows = 1
		SET @AccountPayableId = 0

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@AccountPayableId = ap.Id,
				@AdjusmentValue = t.AdjusmentValue,
				@AdjustmentBalance = t.AdjusmentValue,
				@Balance = ap.Balance
			FROM @TableXmlObject t
			JOIN Payments.AccountPayable ap 
				ON t.BillNumber = ap.BillNumber 
					AND ap.IdSupplier = @SupplierId
					AND ap.IdAccount = @MainAccountId
			WHERE ap.Id > @AccountPayableId
				AND StatusField = 1
			ORDER BY ap.Id

			SET @Rows = @@RowCount
			IF @Rows = 0 
				BREAK

			SELECT @Count_Shares = COUNT(*)
			FROM Payments.AccountPayableShares aps
			WHERE aps.IdAccountPayable = @AccountPayableId

			/************************************* RECORREMOS LAS CUOTAS E INSERTAMOS LOS AJUSTES *************************************/

			SET @Row_Details = 1
			SET @AccountPayableShareId = 0
			SET @Current_Share = 0

			WHILE @Row_Details > 0
			BEGIN
				SELECT TOP 1
					@AccountPayableShareId = aps.Id,
					@BalanceNoteShare = aps.Balance,
					@Current_Share = @Current_Share + 1
				FROM Payments.AccountPayableShares aps
				WHERE aps.IdAccountPayable = @AccountPayableId
					AND aps.Id > @AccountPayableShareId
				ORDER BY aps.Id

				SET @Row_Details = @@RowCount
				IF @Row_Details = 0 OR NOT (@AdjustmentBalance > 0)
					BREAK

				IF @Nature = 2 AND @AdjusmentValue > @Balance
				BEGIN
					SET @ValueNoteShare = ROUND((@AdjusmentValue / @Count_Shares), 0)
					SET @ValueNoteShare = IIF(@Current_Share = @Count_Shares, 
												@AdjustmentBalance,
												IIF(@ValueNoteShare > @AdjustmentBalance, 
													@AdjustmentBalance, 
													@ValueNoteShare
												)
										  )
				END
				ELSE
				BEGIN
					SET @ValueNoteShare = IIF(@AdjustmentBalance > @BalanceNoteShare, @BalanceNoteShare, @AdjustmentBalance)
				END

				INSERT INTO @TableResult
					(
						StatusField, MessageField, 
						-- ACCOUNTPAYABLE --
						AccountPayableId, 
						BillNumber, 
						BillDate, 
						ExpirationDate, 
						[Value], 
						Balance, 
						AdjusmentValue, 
						[Percentage],
						HandlesAddModifyDelete,
						IdSupplier,
						IdAccount,
						IdCostCenter,
						IdThirdParty,
						-- ACCOUNTPAYABLESHARES --
						AccountPayableShareId,
						ValueNoteShare,
						BalanceNoteShare
					)
					SELECT 1, '', 
						-- ACCOUNTPAYABLE --
						ap.Id,
						ap.BillNumber,
						ap.BillDate,
						ap.ExpirationDate,
						ap.Value,
						ap.Balance,
						@AdjusmentValue,
						IIF(ap.Balance = 0, 100, ROUND((@AdjusmentValue / ap.Balance * 100), 2)) Percentage,
						1,
						ap.IdSupplier,
						ap.IdAccount,
						ap.IdCostCenter,
						ap.IdThirdParty,
						-- ACCOUNTPAYABLESHARES --
						aps.Id AccountPayableShareId,
						@ValueNoteShare ValueNoteShare,
						@BalanceNoteShare BalanceNoteShare
					FROM Payments.AccountPayable ap 
					JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite importar o copiar facturas de cuentas por pagar hacia una nota de cartera (nota de cuentas por pagar), operación conocida en el sistema como ''copy-paste de facturas''. Recibe por parámetro XML la lista de facturas a importar junto con sus valores de ajuste, y aplica un conjunto de validaciones masivas: que no haya facturas duplicadas en la selección, que el valor de ajuste sea numérico y mayor a cero, que cada factura exista en la tabla Payments.AccountPayable asociada al proveedor y cuenta contable indicados, que esté en estado ''Confirmado'' (estado 2), y que tenga cuotas de pago (AccountPayableShares) disponibles con saldo. Por cada factura válida distribuye el valor de ajuste entre las cuotas pendientes de la cuenta por pagar, registrando los resultados (saldos, valores de nota, estado de éxito o error por fila) en una tabla de retorno que se devuelve al formulario de notas de cuentas por pagar para su confirmación o corrección.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportBillsToPortfolioNote';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara, a partir de un XML de facturas pegadas, las cuentas por pagar y sus cuotas elegibles para ser importadas a una nota de cuentas por pagar, distribuyendo el valor de ajuste entre las cuotas.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de parámetros debe proveer Nature, SupplierId y MainAccountId.; El XML de objetos debe contener filas con BillNumber y Adjusment.; Las filas a procesar deben llegar con StatusField=1 para ser consideradas válidas.; Las facturas referenciadas deben existir en Payments.AccountPayable asociadas al proveedor y cuenta contable indicados.; Las facturas referenciadas deben estar en estado 2 (no Registrado=1, Anulado=3 ni Reversado=4).; Cada factura debe tener al menos una cuota en Payments.AccountPayableShares.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo las filas con StatusField=1 después de todas las validaciones generan distribución de cuotas en el resultado.; La distribución de cuotas se detiene cuando el saldo de ajuste pendiente (@AdjustmentBalance) deja de ser mayor que 0.; El procedimiento nunca modifica datos en Payments.AccountPayable ni en Payments.AccountPayableShares; solo retorna información calculada.; Solo se consideran cuentas por pagar pertenecientes al proveedor (IdSupplier) y cuenta contable principal (IdAccount) recibidos.; Una factura solo es elegible si su estado es 2 (estado válido para importación).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cuenta por pagar; Cuotas de cuenta por pagar; Proveedor; Cuenta contable; Ajuste; Saldo; Nota de cuentas por pagar; Naturaleza (débito/crédito); Estados de factura (Registrado, Anulado, Reversado)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando una fila queda con StatusField=0 tras las validaciones, se inserta en el resultado con su mensaje de error.; [INSERT] @TableResult: Por cada cuota válida de cada factura válida (StatusField=1) se inserta una fila con el detalle de la factura, la cuota y el valor de ajuste calculado (ValueNoteShare).; [INSERT] @TableResult: Si ocurre una excepción no controlada se inserta una fila con StatusField=0 y el mensaje y línea del error capturado.; [RETURN_RESULT] @TableResult: Al final se devuelve el contenido completo de la tabla de resultados (errores + cuotas distribuidas).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen varias filas con el mismo BillNumber y StatusField=1 → Marca esas filas como inválidas con mensaje ''Existe mas de un registro asociado a la Factura ...''; si Adjusment no es numérico (ISNUMERIC<>1) → Marca la fila como inválida indicando que el valor de ajuste es invalido; si AdjusmentValue no es mayor a 0 → Marca la fila como inválida con mensaje ''no puede ser igual o menor a 0''; si La factura no existe para el proveedor y cuenta contable indicados → Marca la fila como inválida indicando que la factura no existe o no está relacionada con proveedor/cuenta; si AccountPayable.Status <> 2 → Marca la fila como inválida mostrando el estado (Registrado=1, Anulado=3, Reversado=4, otros=N/A); si La factura no tiene cuotas asociadas → Marca la fila como inválida con mensaje ''no tiene cuotas''; si ap.Balance no coincide con la suma de Balance de sus cuotas → Marca la fila como inválida indicando que el saldo no coincide con el de sus cuotas; si Nature=1 y ap.Balance no es mayor a 0 → Marca la fila como inválida con ''no tiene saldo''; si Nature=1 y AdjusmentValue > ap.Balance → Marca la fila como inválida porque el ajuste no puede superar el saldo; si Nature=2 y AdjusmentValue > Balance de la factura → Distribuye el ajuste entre las cuotas como ROUND(AdjusmentValue/Count_Shares,0), asignando a la última cuota el saldo remanente del ajuste else Asigna a cada cuota el menor entre el saldo restante del ajuste y el saldo de la cuota; si ap.Balance = 0 al calcular Percentage → Asigna Percentage = 100 else Calcula Percentage = ROUND(AdjusmentValue/Balance*100, 2)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.AccountPayableShares', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportBillsToPortfolioNote';
-- GO
