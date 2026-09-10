-- =====================================================================================
-- Author: Carlos Mario Arias Rubiano
-- Create date: 2019-06-18
-- Description:	Procedimiento que se encarga de generar las obligaciones desde la CxP
-- =====================================================================================
CREATE PROCEDURE [Budget].[SP_GenerateObligationsByAccountPayable]
	@AccountPayableCode VARCHAR(20),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @OperatingUnitId INT,
			@BudgetaryValidityId INT,
			------------------------------
			@AccountPayableRows INT = 1,			
			@AccountPayableId INT = 0,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT TOP 1
			@OperatingUnitId = ap.IdOperatingUnit,
			@BudgetaryValidityId = c.BudgetaryValidityId
		FROM Payments.AccountPayable ap
		JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
		JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		WHERE ap.Code = @AccountPayableCode

		--Si no esta activa la interfaz de presupuesto, retornamos
		IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.BudgetInterface = 1)
		BEGIN
			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Se valida que existan detalles con compromiso y si la interfaz es obligatoria o no
		IF EXISTS 
		(
			SELECT 1 
			FROM Payments.AccountPayable ap
			LEFT JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
			WHERE ap.Code = @AccountPayableCode AND apc.Id IS NULL
		)
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.ObligationBudgetInterface = 1)
			BEGIN
				SET @CodeResult = 0 
				SET @MessageResult = ''
				RETURN
			END
			ELSE
			BEGIN
				SET @CodeResult = 999 
				SET @MessageResult = 'Existen facturas sin asociar a un compromiso'
				RETURN
			END
		END

		--Se valida que todos los detalles pertenezcan a la misma vigencia
		IF EXISTS
		(
			SELECT 1
			FROM Payments.AccountPayable ap
			JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
			JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
			JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
			WHERE ap.Code = @AccountPayableCode
				AND c.BudgetaryValidityId <> @BudgetaryValidityId
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------------------------------------------

		WHILE @AccountPayableRows > 0
		BEGIN
			SELECT TOP 1
				@AccountPayableId = ap.Id
			FROM Payments.AccountPayable ap
			WHERE ap.Code = @AccountPayableCode
				AND ap.Id > @AccountPayableId
			ORDER BY ap.Id

			SET @AccountPayableRows = @@ROWCOUNT
			IF @AccountPayableRows = 0 
			BEGIN
				BREAK
			END

			/***********************************************************************************************/
						
			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						Obligation.*,
						ObligationDetail.*
					FROM
					(
						SELECT 
							0 Id,
							@OperatingUnitId OperatingUnitId,
							'' Code,
							@BudgetaryValidityId BudgetaryValidityId,
							ap.DocumentDate DocumentDate,
							s.IdThirdParty ThirdPartyId,
							ap.BillNumber Document,
							1 ObligationType,
							CONCAT(ap.Coments, CHAR(13) + CHAR(10) + 'Obligación generada desde la cuenta por pagar No. ', ap.Code) Observations,
							2 Status
						FROM Payments.AccountPayable ap
						JOIN Common.Supplier s ON ap.IdSupplier = s.Id
						WHERE ap.Id = @AccountPayableId
					) Obligation
					JOIN
					( 
						SELECT
							0 ObligationId,
							cd.Id CommitmentDetailId,
							cd.CategoryId,
							cd.RevenueTypeId,
							ap.ExpirationDate ExpiredDate,
							SUM(apc.Value) InitialValue,
							ap.Id EntityId,
							ap.Code EntityCode,
							'AccountPayable' EntityName
						FROM Payments.AccountPayable ap
						JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
						JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
						WHERE apc.AccountPayableId = @AccountPayableId
						GROUP BY ap.Id, ap.Code, ap.ExpirationDate,
							cd.Id, cd.CategoryId, cd.RevenueTypeId
					) ObligationDetail ON Obligation.Id = ObligationDetail.ObligationId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveObligation_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la obligación presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera obligaciones presupuestales a partir de cuentas por pagar (facturas de proveedores). Para una cuenta por pagar identificada por su código, valida que la interfaz presupuestal esté activa, que todos los ítems tengan compromiso presupuestal asociado y que pertenezcan a una misma vigencia presupuestal; si todo es correcto, construye el XML de la obligación (cabecera con tercero, fecha y documento, más el detalle con compromiso, categoría, tipo de ingreso y valores) y lo registra invocando el procedimiento SP_SaveObligation_Output. Sirve como puente entre el módulo de pagos/cuentas por pagar y el módulo de presupuesto, garantizando que cada factura aprobada quede respaldada por una obligación presupuestal formal antes de su pago.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateObligationsByAccountPayable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera obligaciones presupuestales a partir de una cuenta por pagar, validando su consistencia y delegando el registro en SP_SaveObligation_Output.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una cuenta por pagar con el código recibido en Payments.AccountPayable.; La unidad operativa de la CxP debe tener configuración en Payments.SettingPayments.; Los detalles de la CxP deben estar asociados a compromisos en Payments.AccountPayableCommitments con CommitmentDetail/Commitment válidos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La obligación generada se crea siempre con ObligationType=1 y Status=2.; La vigencia presupuestal de la obligación es única y proviene del primer Commitment encontrado.; Las observaciones siempre incluyen la traza ''Obligación generada desde la cuenta por pagar No. <Code>''.; El valor inicial del detalle es la suma agrupada de AccountPayableCommitments.Value por CommitmentDetail/Categoría/TipoIngreso.; La EntityName del detalle es siempre la cadena literal ''AccountPayable''.; Si la interfaz de presupuesto está desactivada, el procedimiento no genera obligaciones ni reporta error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Obligación presupuestal; Compromiso presupuestal; Vigencia presupuestal; Interfaz de presupuesto; Tercero/Proveedor; Unidad operativa; Categoría presupuestal; Tipo de ingreso', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.SP_SaveObligation_Output: Por cada AccountPayable encontrado se construye un XML con cabecera (OperatingUnit, Vigencia, Tercero, Fecha, Documento, ObligationType=1, Status=2) y detalle agrupado por CommitmentDetail/Categoría/TipoIngreso con SUM(apc.Value), y se invoca SP_SaveObligation_Output.; [RETURN_RESULT] @MessageResult: Si la interfaz de presupuesto no está activa (SettingPayments.BudgetInterface<>1), retorna CodeResult=0 y mensaje vacío sin generar obligación.; [RETURN_RESULT] @MessageResult: Si existen detalles sin compromiso asociado y ObligationBudgetInterface=1, retorna CodeResult=999 con mensaje ''Existen facturas sin asociar a un compromiso''.; [RETURN_RESULT] @MessageResult: Si los detalles pertenecen a más de una vigencia presupuestal (Commitment.BudgetaryValidityId distintos), retorna CodeResult=999 con mensaje ''Los detalles corresponden a más de una vigencia presupuestal.''; [RETURN_RESULT] @MessageResult: Si SP_SaveObligation_Output retorna código distinto de 0, retorna CodeResult=999 con el mensaje devuelto o ''No se pudo generar la obligación presupuestal''.; [RETURN_RESULT] @MessageResult: En CATCH retorna CodeResult=999 con ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SettingPayments.BudgetInterface = 1 para la unidad operativa → Continúa el proceso de validación y generación else Sale silenciosamente con CodeResult=0 y mensaje vacío; si Existen filas en AccountPayable sin AccountPayableCommitments asociado → Si ObligationBudgetInterface<>1 sale con CodeResult=0; si =1 retorna error 999 ''Existen facturas sin asociar a un compromiso'' else Continúa con la validación de vigencia; si Algún CommitmentDetail pertenece a una BudgetaryValidityId distinta a la de la cabecera → Retorna CodeResult=999 ''Los detalles corresponden a más de una vigencia presupuestal.'' else Procede a iterar y generar la obligación; si @Code_Output <> 0 tras invocar SP_SaveObligation_Output → Aborta y retorna CodeResult=999 con el mensaje recibido else Acumula mensajes y continúa con la siguiente CxP', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveObligation_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.AccountPayableCommitments; Budget.CommitmentDetail; Budget.Commitment; Payments.SettingPayments; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationsByAccountPayable';
-- GO
