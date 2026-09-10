-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-04
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output]
	@OperatingUnitId INT,
	@AccountReceivableId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @DependencyId INT,
			@AffectBudget INT,			
			@BudgetaryValidityId INT,
			@AccountReceivableType TINYINT,
			@LiquidationType TINYINT,
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@RecognitionId INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		IF NOT EXISTS (SELECT 1 FROM Billing.SettingsBilling WITH (NOLOCK) WHERE BudgetInterface = 1)
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------

		SELECT	@AffectBudget = ar.AffectBudget,
				@BudgetaryValidityId = bv.Id,
				@AccountReceivableType = ar.AccountReceivableType,
				@LiquidationType = IIF(ar.AccountReceivableType = 1, 1, cg.LiquidationType)
		FROM Portfolio.AccountReceivable ar		
		LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
		LEFT JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
		LEFT JOIN Budget.BudgetaryValidity bv ON bh.BudgetaryValidityId = bv.Id AND bv.Status IN (1, 2) AND YEAR(ar.AccountReceivableDate) = bv.Year
		LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		LEFT JOIN Contract.CareGroup cg ON i.CareGroupId = cg.Id
		WHERE ar.Id = @AccountReceivableId

		SELECT
			@DependencyId = IIF(@AccountReceivableType = 1, BasicBillingDependencyId, DependencyId)
		FROM Billing.SettingsBilling
		WHERE IdOperatingUnit = @OperatingUnitId
		
		/************************************************ VALIDACIONES ***********************************************/

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Debe indicar en los parámetros de facturación la dependecia a usar en el reconocimiento de ingresos.'
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención no tienen asignado un rubro presupuestal o el rubro se encuentra asignado a una vigencia no activa.'
			RETURN
		END

		/**************************************** RECONOCIMIENTO PRESUPUESTAL ****************************************/
		
		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					Recognition.*,
					RecognitionDetail.*
				FROM 
				(
					SELECT 
						0 Id,
						@OperatingUnitId OperatingUnitId,
						'' Code,
						@BudgetaryValidityId BudgetaryValidityId,
						ar.AccountReceivableDate DocumentDate,
						ar.InvoiceNumber Document,
						CASE ar.AccountReceivableType
							WHEN 4 THEN 'Reconocimiento generado del pagaré asociado ' + IIF(@LiquidationType = 1, 'a la Factura', 'al Control de Servicio') + ' No. ' + ar.InvoiceNumber 
							WHEN 6 THEN 'Reconocimiento generado del valor del paciente asociado ' + IIF(@LiquidationType = 1, 'a la Factura', 'al Control de Servicio') + ' No. ' + ar.InvoiceNumber 
							ELSE 'Reconocimiento generado desde ' + IIF(@LiquidationType = 1, 'la Factura', 'el Control de Servicio') + ' No. ' + ar.InvoiceNumber 
						END Observations,
						1 RecognitonType,
						ar.ThirdPartyId,
						@DependencyId DependencyId,
						IIF(@LiquidationType = 1, 0, IIF(ar.AccountReceivableType = 6, 1, 0)) AutomaticCollection,
						2 Status,
						ar.Id EntityId,
						ar.InvoiceNumber EntityCode,
						'AccountReceivable' EntityName
					FROM Portfolio.AccountReceivable ar
					WHERE ar.Id = @AccountReceivableId
				) Recognition
				JOIN
				( 
					SELECT
						0 RecognitionId,
						b.CategoryId, 
						b.RevenueTypeId,
						(ar.Value - ISNULL(i.ValueTax, 0)) InitialValue
					FROM Portfolio.AccountReceivable ar
					JOIN Budget.Budget b ON ar.BudgetId = b.Id
					LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
					WHERE ar.Id = @AccountReceivableId
				) RecognitionDetail ON Recognition.Id = RecognitionDetail.RecognitionId
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Budget].[SP_SaveRecognition_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, @RecognitionId OUT, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
			RETURN
		END

		/**************************************** ACTUALIZACION DE REGISTROS *****************************************/
		
		UPDATE ar 
			SET RecognitionId = @RecognitionId
		FROM Portfolio.AccountReceivable ar
		WHERE ar.Id = @AccountReceivableId

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reconocimiento presupuestal de ingresos a partir de una cuenta por cobrar específica en el módulo de cartera. Verifica que la facturación esté integrada con presupuesto, que la cuenta por cobrar afecte el presupuesto y que exista una vigencia presupuestal activa para el año del documento; si alguna validación falla, retorna un mensaje descriptivo al usuario. Compone un XML con el encabezado del reconocimiento (unidad operativa, vigencia, fecha, número de factura o control de servicio, tercero, dependencia contable) y el detalle (rubro presupuestal, tipo de ingreso, valor sin impuestos) tomados de la cuenta por cobrar, la factura y el presupuesto asociado, y lo envía al procedimiento [Budget].[SP_SaveRecognition_Output] para su persistencia. Una vez generado exitosamente, actualiza la cuenta por cobrar con el identificador del reconocimiento creado, vinculando así cartera con la ejecución presupuestal de ingresos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reconocimiento presupuestal de ingresos a partir de una cuenta por cobrar, construyendo el XML con encabezado y detalle, delegando la persistencia y vinculando el reconocimiento creado a la cuenta por cobrar.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en Billing.SettingsBilling con BudgetInterface = 1; en caso contrario el proceso termina silenciosamente sin error.; La cuenta por cobrar (Portfolio.AccountReceivable) debe existir con el Id recibido.; La cuenta por cobrar debe tener AffectBudget distinto de 0 para proceder con el reconocimiento.; Billing.SettingsBilling de la unidad operativa debe tener configurada la dependencia (BasicBillingDependencyId si AccountReceivableType=1, o DependencyId en otro caso).; El presupuesto asociado debe tener una BudgetaryValidity con Status IN (1,2) y cuyo Year coincida con el año de AccountReceivableDate.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si la interfaz presupuestal está apagada (BudgetInterface<>1) o la cuenta por cobrar no afecta presupuesto, no se genera reconocimiento ni se modifica ningún registro.; El reconocimiento siempre se crea con Status=2 y RecognitonType=1.; El detalle del reconocimiento siempre lleva InitialValue = ar.Value - ISNULL(i.ValueTax, 0), es decir, valor sin impuestos.; Solo se actualiza Portfolio.AccountReceivable.RecognitionId si el SP de creación del reconocimiento devolvió código 0.; Para AccountReceivableType=1 el LiquidationType se fuerza a 1 (Factura), independiente del CareGroup.; La vigencia presupuestal usada debe estar en estados 1 o 2 y coincidir con el año de la fecha de la cuenta por cobrar.; Toda excepción se captura y se retorna como CodeResult=999 con el mensaje y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento presupuestal de ingresos; Cuenta por cobrar; Factura; Control de servicio; Pagaré; Valor del paciente; Vigencia presupuestal; Rubro presupuestal; Tipo de ingreso; Grupo de atención (CareGroup); Dependencia contable; Tercero; Interfaz presupuestal; Liquidación (Factura vs Control de Servicio); Recaudo automático', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget (vía Budget.SP_SaveRecognition_Output): Cuando se cumplen todas las validaciones, se invoca SP_SaveRecognition_Output con el XML del reconocimiento (encabezado + detalle) para crearlo en estado 2 y RecognitonType=1.; [UPDATE] Portfolio.AccountReceivable: Tras crearse el reconocimiento exitosamente (Code_Output = 0), se actualiza RecognitionId = @RecognitionId en la cuenta por cobrar identificada por @AccountReceivableId.; [RETURN_RESULT] @CodeResult/@MessageResult: Retorna CodeResult=0 y mensaje vacío si BudgetInterface<>1 o AffectBudget=0; CodeResult=999 con mensaje específico si falta dependencia o vigencia presupuestal; CodeResult=999 con mensaje del SP de reconocimiento si falla; CodeResult=0 al éxito.; [RETURN_RESULT] @CodeResult/@MessageResult: En CATCH retorna CodeResult=999 con ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS configuración Billing.SettingsBilling con BudgetInterface = 1 → Termina sin error (CodeResult=0, mensaje vacío) sin generar reconocimiento. else Continúa con la obtención de datos y validaciones.; si @AffectBudget = 0 → Termina sin error (CodeResult=0, mensaje vacío) — la cuenta por cobrar no afecta presupuesto. else Procede a validar dependencia y vigencia.; si @AccountReceivableType = 1 → Usa BasicBillingDependencyId como dependencia y fuerza @LiquidationType = 1 (Factura). else Usa DependencyId y toma LiquidationType desde Contract.CareGroup.; si @DependencyId IS NULL → Retorna CodeResult=999 con mensaje sobre dependencia faltante en parámetros de facturación.; si @BudgetaryValidityId IS NULL → Retorna CodeResult=999 indicando que el grupo de atención no tiene rubro presupuestal o la vigencia no está activa.; si ar.AccountReceivableType = 4 → Observación: ''Reconocimiento generado del pagaré asociado a la Factura/Control de Servicio No. <InvoiceNumber>''.; si ar.AccountReceivableType = 6 → Observación menciona ''valor del paciente'' y AutomaticCollection=1 (si LiquidationType<>1).; si @LiquidationType = 1 → Texto usa ''la Factura'' y AutomaticCollection=0; en caso contrario usa ''el Control de Servicio''.; si @Code_Output <> 0 tras EXEC SP_SaveRecognition_Output → Retorna CodeResult=999 con el mensaje del SP o ''No se pudo generar el reconocimiento presupuestal''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveRecognition_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Portfolio.AccountReceivable; Budget.Budget; Budget.BudgetHeader; Budget.BudgetaryValidity; Billing.Invoice; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByAccountReceivableId_Output';
-- GO
