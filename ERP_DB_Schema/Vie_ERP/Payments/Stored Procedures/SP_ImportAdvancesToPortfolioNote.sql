-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-02-04
-- Description:	Procedimiento que se encarga de el copyPaste de anticipos del form de notas de cuentas por pagar
-- =============================================
CREATE PROCEDURE [Payments].[SP_ImportAdvancesToPortfolioNote] 
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
		Code VARCHAR(100), 
		Adjusment VARCHAR(100),
		AdjusmentValue NUMERIC(20,4) DEFAULT (0)
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- ADVANCEPAYMENTS --
		AdvancePaymentId INT, 
		Code VARCHAR(20), 
		DocumentDate DATETIME, 
		[Value] NUMERIC(18,2), 
		Balance NUMERIC(18,2), 
		AdjusmentValue NUMERIC(20,4), 
		[Percentage] NUMERIC(18,2),
		HandlesAddModifyDelete INT,
		IdSupplier INT,
		IdAccount INT,
		IdCostCenter INT,
		IdThirdParty INT
	)
	
	--Parametros
	DECLARE @Nature INT,
			@SupplierId INT

	BEGIN TRY

		SELECT 
			@Nature = t.x.value('Nature[1]','int'),
			@SupplierId = t.x.value('SupplierId[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, Code, Adjusment)
			SELECT 
				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField,
				t.x.value('Code[1]','varchar(100)') as BillNumber,
				t.x.value('Adjusment[1]','varchar(100)') as Adjusment
			FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
			GROUP BY t.Code
			HAVING COUNT(*) > 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'Existe mas de un registro asociado al Anticipo ' + t.Code
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.Code
				FROM @TableXmlObject t
				WHERE t.StatusField = 1
				GROUP BY t.Code
				HAVING COUNT(*) > 1
			) t2 ON t.Code = t2.Code
			WHERE t.StatusField = 1
		END

		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.AdjusmentValue) <> 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor de ajuste del registro ' + convert(VARCHAR(3),t.Id) + ' es invalido'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND ISNUMERIC(t.AdjusmentValue) <> 1
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
			LEFT JOIN Payments.AdvancePayments ap WITH (NOLOCK)
				ON t.Code = ap.Code
					AND ap.IdSupplier = @SupplierId
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El anticipo ' + t.Code +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionada con el proveedor'
			FROM @TableXmlObject t
			LEFT JOIN Payments.AdvancePayments ap WITH (NOLOCK)
				ON t.Code = ap.Code
					AND ap.IdSupplier = @SupplierId
			WHERE t.StatusField = 1
				AND ap.Id IS NULL
		END

		IF @Nature = 2
		BEGIN
			IF EXISTS
			(
				SELECT 1
				FROM @TableXmlObject t
				JOIN Payments.AdvancePayments ap WITH (NOLOCK)
					ON t.Code = ap.Code
						AND ap.IdSupplier = @SupplierId
				WHERE t.StatusField = 1
					AND NOT (ap.Balance > 0)
			)
			BEGIN
				UPDATE t
					SET t.StatusField = 0,
						t.MessageField = 'El anticipo ' + t.Code +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no tiene saldo'
				FROM @TableXmlObject t
				JOIN Payments.AdvancePayments ap WITH (NOLOCK)
					ON t.Code = ap.Code
						AND ap.IdSupplier = @SupplierId
				WHERE t.StatusField = 1
					AND NOT (ap.Balance > 0)
			END

			IF EXISTS
			(
				SELECT 1
				FROM @TableXmlObject t
				JOIN Payments.AdvancePayments ap WITH (NOLOCK)
					ON t.Code = ap.Code
						AND ap.IdSupplier = @SupplierId
				WHERE t.StatusField = 1
					AND (t.AdjusmentValue > ap.Balance)
			)
			BEGIN
				UPDATE t
					SET t.StatusField = 0,
						t.MessageField = 'La valor del ajuste (' + CAST(CAST(t.AdjusmentValue AS FLOAT) AS VARCHAR(25)) + ') del anticipo ' + t.Code + ' del registro ' + convert(VARCHAR(3),t.Id) + ' no puede ser mayor al saldo (' + CAST(CAST(ap.Balance AS FLOAT) AS VARCHAR(25)) + ')'
				FROM @TableXmlObject t
				JOIN Payments.AdvancePayments ap WITH (NOLOCK)
					ON t.Code = ap.Code
						AND ap.IdSupplier = @SupplierId
				WHERE t.StatusField = 1
					AND (t.AdjusmentValue > ap.Balance)
			END
		END

		/************************************* INSERTAR ERRORES *************************************/

		INSERT INTO @TableResult 
			(
				StatusField, MessageField, Code, AdjusmentValue
			)
			SELECT StatusField, MessageField, Code, AdjusmentValue
			FROM @TableXmlObject
			WHERE StatusField = 0

		/************************************* RECORREMOS LOS ANTICIPOS VALIDOS *************************************/
		
		INSERT INTO @TableResult
			(
				StatusField, MessageField, 
				-- ACCOUNTPAYABLE --
				AdvancePaymentId, 
				Code, 
				DocumentDate, 
				[Value], 
				Balance, 
				AdjusmentValue, 
				[Percentage],
				HandlesAddModifyDelete,
				IdSupplier,
				IdAccount,
				IdCostCenter,
				IdThirdParty
			)
			SELECT 1, '', 
				-- ACCOUNTPAYABLE --
				ap.Id,
				ap.Code,
				ap.DocumentDate,
				ap.Value,
				ap.Balance,
				t.AdjusmentValue,
				IIF(ap.Balance = 0, 100, ROUND((t.AdjusmentValue / ap.Balance * 100), 2)) Percentage,
				1,
				ap.IdSupplier,
				ap.IdAccount,
				ap.IdCostCenter,
				ap.IdThirdParty
			FROM @TableXmlObject t
			JOIN Payments.AdvancePayments ap WITH (NOLOCK)
				ON t.Code = ap.Code
					AND ap.IdSupplier = @SupplierId
			WHERE t.StatusField = 1
				
	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult
END