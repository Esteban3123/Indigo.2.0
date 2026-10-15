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