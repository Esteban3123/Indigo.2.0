-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-07-26
-- Description:	Procedimiento que se encarga de generar el reintegro
-- =====================================================================================
CREATE PROCEDURE [Budget].[SP_GenerateReimbursementResource]	
	@EntityId INT,
	@EntityCode VARCHAR(20),
	@EntityName VARCHAR(250),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables a utilizar
	DECLARE @OperatingUnitId INT,
			------------------------------
			@DocumentDate DATETIME,
			@UpTo TINYINT,
			@Observations VARCHAR(MAX),
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		BudgetaryValidityId INT,
		PaymentOrderId INT,
		PaymentOrderDetailId INT,
		Value NUMERIC(18, 0)
	)

	BEGIN TRY
		SELECT TOP 1
			@OperatingUnitId = tn.OperatingUnitId,
			@DocumentDate = tn.NoteDate,
			@UpTo = 1, -- Hasta la obligación
			@Observations = tn.Description
		FROM Treasury.TreasuryNote tn
		WHERE tn.Id = @EntityId

		INSERT INTO @Details
		(
			BudgetaryValidityId, PaymentOrderId, PaymentOrderDetailId, Value
		)
		SELECT
			po.BudgetaryValidityId, po.Id PaymentOrderId, pod.Id PaymentOrderDetailId, SUM(pod.InitialValue) Value
		FROM Treasury.TreasuryNote tn
		JOIN Budget.PaymentOrder po ON tn.VoucherTransactionId = po.EntityId AND po.EntityName = 'VoucherTransaction'
		JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
		WHERE tn.Id = @EntityId
		GROUP BY po.BudgetaryValidityId, po.Id, pod.Id

		--Si el comprobante de egreso no hizo interfaz o no esta activa la interfaz de presupuesto, retornamos
		IF
		(
			NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.BudgetInterface = 1)
			OR
			NOT EXISTS (SELECT 1 FROM @Details)
		)
		BEGIN
			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Se valida que todos los detalles pertenezcan a la misma vigencia
		IF EXISTS
		(
			SELECT 1
			FROM
			(
				SELECT @EntityId Id, D.BudgetaryValidityId
				FROM @Details d
				GROUP BY d.BudgetaryValidityId
			) d 
			GROUP BY Id
			HAVING COUNT(*) > 1
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------------------------------------------

		--se llama al procedimiento almacenado encargado de la crear la orden de pago
		DECLARE @PaymentOrderRows INT = 1,
				@PaymentOrderId INT = 0,
				@BudgetaryValidityId INT

		WHILE @PaymentOrderRows > 0
		BEGIN
			SELECT TOP 1
				@PaymentOrderId = d.PaymentOrderId,
				@BudgetaryValidityId = d.BudgetaryValidityId
			FROM @Details d
			WHERE d.PaymentOrderId > @PaymentOrderId
			ORDER BY d.PaymentOrderId

			SET @PaymentOrderRows = @@ROWCOUNT
			IF @PaymentOrderRows = 0 
			BEGIN
				BREAK
			END

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						ReimbursementResource.*,
						ReimbursementResourceDetail.*
					FROM
					(
						SELECT 
							0 Id,
							@OperatingUnitId OperatingUnitId,
							'' Code,
							@BudgetaryValidityId BudgetaryValidityId,
							@DocumentDate DocumentDate,
							@PaymentOrderId PaymentOrderId,
							@UpTo UpTo,
							@EntityCode Document,
							@Observations Observations,
							2 Status,
							@EntityId EntityId,
							@EntityCode EntityCode,
							@EntityName EntityName
					) ReimbursementResource
					JOIN
					( 
						SELECT
							0 ReimbursementResourceId,
							d.PaymentOrderDetailId,
							d.Value
						FROM @Details d
						WHERE d.PaymentOrderId = @PaymentOrderId
					) ReimbursementResourceDetail ON ReimbursementResource.Id = ReimbursementResourceDetail.ReimbursementResourceId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveReimbursementResource_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reintegro presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = @Message
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reintegro presupuestal de recursos a partir de una nota de tesorería (débito o crédito), revertiendo los valores ejecutados en las órdenes de pago y sus detalles asociados al comprobante contable original. Verifica primero que la unidad operativa tenga activa la interfaz presupuestal en la configuración de pagos y que todos los detalles a reintegrar pertenezcan a una misma vigencia presupuestal; si alguna condición no se cumple, retorna sin procesar. Para cada orden de pago involucrada construye un XML con los datos del reintegro (vigencia, fecha, valores por renglón) y lo persiste llamando al procedimiento SP_SaveReimbursementResource_Output. Es utilizado cuando se anula o ajusta un comprobante de egreso (voucher de tesorería) y se necesita liberar el presupuesto comprometido en las órdenes de pago correspondientes.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateReimbursementResource';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateReimbursementResource';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera reintegros presupuestales por cada orden de pago asociada a una nota de tesorería (típicamente cuando se anula/ajusta un comprobante de egreso) liberando el presupuesto comprometido.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una Treasury.TreasuryNote con Id igual al EntityId recibido.; La unidad operativa de la nota debe tener Payments.SettingPayments con BudgetInterface = 1 (interfaz presupuestal activa).; Debe existir al menos un Budget.PaymentOrder vinculado por VoucherTransactionId/EntityName=''VoucherTransaction'' con su respectivo Budget.PaymentOrderDetail.; Todos los detalles cargados deben pertenecer a una única vigencia presupuestal (BudgetaryValidityId).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reintegro siempre se genera con UpTo=1 (hasta la obligación) y Status=2.; El valor del reintegro por renglón corresponde a la suma de InitialValue de los PaymentOrderDetail de la orden de pago.; Se procesa una orden de pago por iteración, agrupando sus detalles, garantizando un XML por PaymentOrderId.; Solo se procesan órdenes de pago cuyo EntityName origen sea ''VoucherTransaction''.; Nunca se ejecuta el reintegro si la interfaz presupuestal está desactivada para la unidad operativa.; La fecha del documento de reintegro se toma de TreasuryNote.NoteDate y las observaciones de TreasuryNote.Description.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reintegro presupuestal; Orden de pago; Vigencia presupuestal; Nota de tesorería; Comprobante de egreso (VoucherTransaction); Interfaz presupuestal; Unidad operativa; Obligación presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.ReimbursementResource: Por cada PaymentOrderId distinto en @Details se construye un XML con UpTo=1 y Status=2 y se invoca SP_SaveReimbursementResource_Output, que persiste el reintegro presupuestal.; [RETURN_RESULT] OUTPUT: Si no hay interfaz presupuestal activa o no hay detalles, retorna @CodeResult=0 y @MessageResult='''' sin procesar.; [RETURN_RESULT] OUTPUT: Si los detalles abarcan más de una vigencia presupuestal, retorna @CodeResult=999 con mensaje ''Los detalles corresponden a más de una vigencia presupuestal.''; [RETURN_RESULT] OUTPUT: Si SP_SaveReimbursementResource_Output devuelve @Code_Output<>0, retorna @CodeResult=999 con el mensaje recibido o ''No se pudo generar el reintegro presupuestal''.; [RETURN_RESULT] OUTPUT: Si ocurre excepción, retorna @CodeResult=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS configuración con BudgetInterface=1 para la unidad operativa OR no hay detalles cargados → Retorna inmediatamente con CodeResult=0 y mensaje vacío (no se genera reintegro). else Continúa el flujo de validación de vigencia y generación.; si Los detalles agrupados arrojan más de un BudgetaryValidityId → Retorna error 999 indicando múltiples vigencias presupuestales. else Procede a iterar las órdenes de pago.; si @Code_Output <> 0 tras llamar a SP_SaveReimbursementResource_Output → Aborta el ciclo y retorna error 999 con el mensaje de salida. else Acumula el mensaje y continúa con la siguiente orden de pago.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveReimbursementResource_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasuryNote; Budget.PaymentOrder; Budget.PaymentOrderDetail; Payments.SettingPayments', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateReimbursementResource';
-- GO
