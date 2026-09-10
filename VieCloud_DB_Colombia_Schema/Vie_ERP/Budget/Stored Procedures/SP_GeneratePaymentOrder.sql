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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera órdenes de pago presupuestarias a partir de un documento de origen (actualmente comprobantes de egreso de tesorería, VoucherTransaction). Toma los datos del comprobante (unidad operativa, fecha, tercero, observaciones) y consolida el detalle de obligaciones presupuestarias vinculadas a través de las facturas de egreso (DischargeBill y DischargeBillBudget), verificando que todos los ítems pertenezcan a una única vigencia presupuestal. Si la interfaz presupuestal está activa para la unidad operativa, arma un XML con la cabecera y el detalle de la orden de pago y lo envía al procedimiento SP_SavePaymentOrder_Output para su registro formal; en caso contrario, limpia los registros de presupuesto asociados y finaliza sin generar la orden.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePaymentOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePaymentOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la orden de pago presupuestal a partir de un comprobante de tesorería, validando vigencia única y la activación de la interfaz de presupuesto antes de invocar el SP que persiste la orden.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @EntityName debe corresponder a un origen soportado (''VoucherTransaction'') para que se carguen cabecera y detalles.; Para origen VoucherTransaction, debe existir la transacción en Treasury.VoucherTransaction y sus líneas deben estar vinculadas a DischargeBill, DischargeBillBudget, ObligationDetail y Obligation.; Debe existir configuración en Payments.SettingPayments para la unidad operativa para evaluar el flag BudgetInterface.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una orden de pago generada agrupa detalles de una única vigencia presupuestal (BudgetaryValidityId).; Solo se genera orden de pago presupuestal si la unidad operativa tiene activa la interfaz de presupuesto (SettingPayments.BudgetInterface = 1).; Cuando la interfaz de presupuesto está inactiva y el origen es VoucherTransaction, se purgan los registros de DischargeBillBudget vinculados al comprobante.; El InitialValue del detalle de la orden de pago es la suma de los valores en DischargeBillBudget agrupada por obligación, detalle de obligación y vigencia.; El estado inicial de la orden de pago generada es 2 y su PaymentOrderType es 1.; Los errores capturados se devuelven con CodeResult=999 e incluyen mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden de pago; vigencia presupuestal; obligación presupuestaria; detalle de obligación; comprobante de tesorería (VoucherTransaction); descargo de factura (DischargeBill); interfaz de presupuesto; unidad operativa; tercero', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Treasury.DischargeBillBudget: Cuando @EntityName=''VoucherTransaction'' y la unidad operativa NO tiene BudgetInterface=1 en Payments.SettingPayments, se eliminan las filas de DischargeBillBudget asociadas a los DischargeBill de los detalles del comprobante (vtd.IdVoucherTransaction = @EntityId).; [CALL] Budget.SP_SavePaymentOrder_Output: Cuando hay detalles válidos y todos pertenecen a una sola vigencia, se construye un XML con cabecera (Status=2, PaymentOrderType=1) y detalles (ObligationDetailId, ExpiredDate, InitialValue) y se invoca SP_SavePaymentOrder_Output para persistir la orden.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve 999 con mensaje específico ante: múltiples vigencias, error del SP de guardado o excepción capturada (incluye ERROR_MESSAGE y ERROR_LINE); devuelve 0 con mensaje vacío cuando no hay interfaz activa, no hay detalles o el guardado fue exitoso.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName = ''VoucherTransaction'' → Carga datos de cabecera desde Treasury.VoucherTransaction y arma el detalle uniendo VoucherTransactionDetails → DischargeBill → DischargeBillBudget → ObligationDetail → Obligation, agrupando InitialValue como SUM(dbb.Value). else No se cargan datos ni detalles (los detalles quedan vacíos y se sale sin generar orden).; si NOT EXISTS en Payments.SettingPayments con IdOperatingUnit=@OperatingUnitId y BudgetInterface=1 → Si el origen es VoucherTransaction, elimina los registros de Treasury.DischargeBillBudget asociados al comprobante y retorna CodeResult=0 sin generar orden. else Continúa con la generación de la orden de pago presupuestal.; si No existen filas en la tabla temporal de detalles → Retorna CodeResult=0 con mensaje vacío sin generar orden.; si Los detalles pertenecen a más de una BudgetaryValidityId (COUNT(*)>1 sobre vigencias distintas) → Retorna CodeResult=999 con mensaje ''Los detalles corresponden a más de una vigencia presupuestal.''; si @Code_Output <> 0 tras invocar Budget.SP_SavePaymentOrder_Output → Retorna CodeResult=999 con el mensaje devuelto o ''No se pudo generar la orden de pago presupuestal''. else Retorna CodeResult=0 con el mensaje del SP invocado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SavePaymentOrder_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.DischargeBill; Treasury.DischargeBillBudget; Budget.ObligationDetail; Budget.Obligation; Payments.SettingPayments', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePaymentOrder';
-- GO
