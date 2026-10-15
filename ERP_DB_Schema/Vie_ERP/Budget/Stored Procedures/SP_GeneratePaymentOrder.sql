-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-06-18
-- Description:	Procedimiento que se encarga de generar las ordenes de pago
-- =====================================================================================
CREATE PROCEDURE [Budget].[SP_GeneratePaymentOrder]	
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
			@ThirdPartyId INT,
			@Observations VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		BudgetaryValidityId INT,
		ObligationId INT,
		ObligationDetailId INT,		
		ExpiredDate DATETIME,
		InitialValue NUMERIC(18, 0)
	)

	BEGIN TRY
		--Se obtienen los datos dependiendo del documento de origen
		IF @EntityName = 'VoucherTransaction'
		BEGIN
			SELECT TOP 1
				@OperatingUnitId = vt.IdUnitOperative,
				@DocumentDate = vt.DocumentDate,
				@ThirdPartyId = vt.IdThirdParty,
				@Observations = vt.Detail
			FROM Treasury.VoucherTransaction vt
			WHERE vt.Id = @EntityId

			INSERT INTO @Details
			(
				BudgetaryValidityId, ObligationId, ObligationDetailId, ExpiredDate, InitialValue
			)
			SELECT
				o.BudgetaryValidityId, o.Id ObligationId, od.Id ObligationDetailId, od.ExpiredDate, SUM(dbb.Value) InitialValue
			FROM Treasury.VoucherTransactionDetails vtd
			JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD
			JOIN Treasury.DischargeBillBudget dbb ON db.Id = dbb.DischargeBillId
			JOIN Budget.ObligationDetail od ON dbb.ObligationDetailId = od.Id
			JOIN Budget.Obligation o ON od.ObligationId = o.Id
			WHERE vtd.IdVoucherTransaction = @EntityId
			GROUP BY o.BudgetaryValidityId, o.Id, od.Id, od.ExpiredDate
		END

		--Si no esta activa la interfaz de presupuesto, retornamos
		IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.BudgetInterface = 1)
		BEGIN
			IF @EntityName = 'VoucherTransaction'
			BEGIN
				DELETE dbb
				FROM Treasury.VoucherTransactionDetails vtd
				JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD
				JOIN Treasury.DischargeBillBudget dbb ON db.Id = dbb.DischargeBillId
				WHERE vtd.IdVoucherTransaction = @EntityId
			END

			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Si no hay detalles
		IF NOT EXISTS (SELECT 1 FROM @Details)
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
		DECLARE @BudgetaryValidityId INT

		SELECT TOP 1
			@BudgetaryValidityId = d.BudgetaryValidityId
		FROM @Details d

		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					PaymentOrder.*,
					PaymentOrderDetail.*
				FROM
				(
					SELECT 
						0 Id,
						@OperatingUnitId OperatingUnitId,
						'' Code,
						@BudgetaryValidityId BudgetaryValidityId,
						@DocumentDate DocumentDate,
						@ThirdPartyId ThirdPartyId,
						@EntityCode Document,
						1 PaymentOrderType,
						@Observations Observations,
						2 Status,
						@EntityId EntityId,
						@EntityCode EntityCode,
						@EntityName EntityName
				) PaymentOrder
				JOIN
				( 
					SELECT
						0 PaymentOrderId,
						d.ObligationDetailId,
						d.ExpiredDate,
						d.InitialValue
					FROM @Details d
				) PaymentOrderDetail ON PaymentOrder.Id = PaymentOrderDetail.PaymentOrderId
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Budget].[SP_SavePaymentOrder_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT @CodeResult = 999, 
					@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la orden de pago presupuestal')
			RETURN
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END