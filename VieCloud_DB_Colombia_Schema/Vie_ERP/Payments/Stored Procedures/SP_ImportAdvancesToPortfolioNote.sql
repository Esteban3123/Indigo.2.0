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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa anticipos de proveedores desde un XML (copiado desde el formulario de notas de cuentas por pagar) y los valida antes de incorporarlos a una nota de cartera. Recibe como parámetros el listado de anticipos seleccionados y datos de contexto como el proveedor y la naturaleza del documento. Ejecuta validaciones masivas sobre cada anticipo: verifica que no haya duplicados por código, que el valor de ajuste sea numérico y mayor a cero, que el anticipo exista y esté relacionado con el proveedor indicado en la tabla Payments.AdvancePayments, y si la naturaleza es de egreso, que el anticipo tenga saldo suficiente disponible. Devuelve un conjunto de resultados con los anticipos válidos (con su valor, saldo, cuenta contable, centro de costos y tercero) y los rechazados con su mensaje de error, para que el formulario de notas de cuentas por pagar muestre el resultado del proceso de importación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara, a partir de un XML de anticipos copiados/pegados, un conjunto de filas listas para ser usadas como detalle de una nota de cuentas por pagar, marcando errores por registro.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de parámetros debe traer Nature y SupplierId.; El XML de objetos debe traer filas con CountFields, StatusField, MessageField, Code y Adjusment.; Para validar saldos, los anticipos deben existir en Payments.AdvancePayments asociados al proveedor indicado.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan como válidos los registros que mantienen StatusField=1 tras todas las validaciones.; Los anticipos siempre se vinculan filtrando por IdSupplier = @SupplierId.; Las consultas a Payments.AdvancePayments se hacen con NOLOCK (lectura sucia).; Las filas válidas devueltas siempre llevan HandlesAddModifyDelete=1 y StatusField=1.; Las validaciones de saldo solo aplican cuando la naturaleza es 2 (débito/aplicación de anticipo).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipos a proveedores; Notas de cuentas por pagar; Saldo de anticipo; Ajuste/aplicación de anticipo; Proveedor; Cuenta contable; Centro de costo; Tercero', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @TableXmlObject: Cuando un mismo Code aparece más de una vez con StatusField=1, se marca StatusField=0 con mensaje ''Existe mas de un registro asociado al Anticipo <Code>''.; [UPDATE] @TableXmlObject: Cuando ISNUMERIC(Adjusment)<>1, se marca StatusField=0 con mensaje ''El valor de ajuste del registro <Id> es invalido''.; [UPDATE] @TableXmlObject: Cuando AdjusmentValue no es mayor a 0, se marca StatusField=0 con mensaje ''El valor de ajuste del registro <Id> no puede ser igual o menor a 0''.; [UPDATE] @TableXmlObject: Cuando el Code no existe en Payments.AdvancePayments para el SupplierId dado, se marca StatusField=0 con mensaje ''El anticipo <Code> del registro <Id> no existe o no se encuentra relacionada con el proveedor''.; [UPDATE] @TableXmlObject: Cuando @Nature=2 y el anticipo no tiene Balance>0, se marca StatusField=0 con mensaje ''El anticipo <Code> del registro <Id> no tiene saldo''.; [UPDATE] @TableXmlObject: Cuando @Nature=2 y AdjusmentValue > Balance del anticipo, se marca StatusField=0 con mensaje indicando que el ajuste no puede ser mayor al saldo.; [INSERT] @TableResult: Las filas con StatusField=0 (errores) se insertan en el resultado con su mensaje y datos básicos (Code, AdjusmentValue).; [INSERT] @TableResult: Las filas con StatusField=1 (válidas) se insertan con datos del anticipo (Id, Code, DocumentDate, Value, Balance, IdSupplier, IdAccount, IdCostCenter, IdThirdParty), AdjusmentValue y Percentage.; [INSERT] @TableResult: Si Balance=0 el Percentage se fija en 100; en caso contrario se calcula como ROUND(AdjusmentValue/Balance*100, 2).; [INSERT] @TableResult: Ante cualquier excepción, se inserta una fila con StatusField=0 y mensaje compuesto por ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE().; [RETURN_RESULT] @TableResult: Al finalizar se retorna la tabla con los resultados (errores y filas válidas).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen Code duplicados con StatusField=1 → Se invalidan todos los registros duplicados con mensaje de duplicidad.; si AdjusmentValue no es numérico o no es mayor a 0 → Se invalida el registro con mensaje específico.; si El anticipo no existe para el proveedor → Se invalida el registro indicando que no existe o no está relacionado con el proveedor.; si @Nature = 2 → Se aplican validaciones adicionales: el anticipo debe tener saldo > 0 y el AdjusmentValue no puede superar el Balance. else No se validan saldo ni tope contra Balance.; si ap.Balance = 0 al construir resultado válido → Percentage se fuerza a 100. else Percentage = ROUND(AdjusmentValue/Balance*100, 2).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AdvancePayments', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_ImportAdvancesToPortfolioNote';
-- GO
