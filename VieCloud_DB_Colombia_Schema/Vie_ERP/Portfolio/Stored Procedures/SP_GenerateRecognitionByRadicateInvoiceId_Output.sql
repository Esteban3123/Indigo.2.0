-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-07
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de un radicado
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionByRadicateInvoiceId_Output]
	@OperatingUnitId INT,
	@RadicateInvoiceId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @DocumentDate DATETIME,
			@DependencyId INT,
			@AffectBudget INT,
			@BudgetaryValidityId INT,
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@RecognitionId INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT @DocumentDate = ri.ConfirmDate
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @RadicateInvoiceId

		SELECT @DependencyId = DependencyId
		FROM Portfolio.SettingPortfolio
		WHERE OperatingUnitId = @OperatingUnitId

		--Se actualiza la información de interfaz presupuestal de la factura 
		UPDATE ar
			SET ar.CareGroupId = cg.Id,
				ar.AffectBudget = cg.AffectBudget,
				ar.BudgetId = cg.BillingBudgetId
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		JOIN Contract.CareGroup cg ON ISNULL(i.CareGroupId, ar.CareGroupId) = cg.Id
		WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0

		SELECT TOP 1 
			@BudgetaryValidityId = bv.Id
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Budget.Budget b ON ar.BudgetId = b.Id
		JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
		JOIN Budget.BudgetaryValidity bv ON bh.BudgetaryValidityId = bv.Id AND bv.Status IN (1, 2)
		WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
		ORDER BY bv.Status DESC
		
		/************************************************ VALIDACIONES ***********************************************/

		IF NOT EXISTS
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
		)
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Debe indicar en los parámetros de cuentas por cobrar la dependecia a usar en el reconocimiento de ingresos.'
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las facturas asociadas al radicado no tienen asignado un rubro presupuestal o el rubro se encuentra asignado a una vigencia no activa.'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 0
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas no tienen habilitada la interfaz presupuestal: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
				AND ar.RecognitionId IS NOT NULL
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
							AND ar.RecognitionId IS NOT NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas ya tienen un reconocimiento: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
				AND b.Id IS NULL
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
							AND b.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas no tienen asignado un rubro presupuestal: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			JOIN Budget.Budget b ON ar.BudgetId = b.Id
			JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
				AND bh.BudgetaryValidityId <> @BudgetaryValidityId
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
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
						ISNULL(ri.ConfirmDate, ri.DocumentDate) DocumentDate,
						ri.RadicatedConsecutive Document,
						'Reconocimiento generado desde la Radicacion de Cuentas No. ' + CAST(ri.RadicatedConsecutive AS VARCHAR(20)) Observations,
						1 RecognitonType,
						c.ThirdPartyId,
						@DependencyId DependencyId,
						0 AutomaticCollection,
						2 Status,
						ri.Id EntityId,
						ri.RadicatedConsecutive EntityCode,
						'RadicateInvoiceC' EntityName
					FROM Portfolio.RadicateInvoiceC ri
					JOIN Common.Customer c ON ri.CustomerId = c.Id
					WHERE ri.Id = @RadicateInvoiceId
				) Recognition
				JOIN
				( 
					SELECT
						0 RecognitionId,
						b.CategoryId, 
						b.RevenueTypeId,
						SUM(ar.Balance) InitialValue
					FROM Portfolio.RadicateInvoiceD rid
					JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
					JOIN Budget.Budget b ON ar.BudgetId = b.Id
					JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
					WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND ar.AffectBudget = 1 AND rid.Devolution = 0
					GROUP BY b.CategoryId, b.RevenueTypeId
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
		
		UPDATE ri
			SET ri.RecognitionId = @RecognitionId
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @RadicateInvoiceId

		UPDATE ar
			SET ar.RecognitionId = @RecognitionId
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Budget.Budget b ON ar.BudgetId = b.Id
		JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
		WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND ar.AffectBudget = 1 AND rid.Devolution = 0

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reconocimiento presupuestal de ingresos a partir de un radicado de facturación de cartera (cuentas por cobrar). Toma las facturas del radicado indicado, actualiza su información de interfaz presupuestal (grupo de atención, afectación de presupuesto y rubro) cruzando las cuentas por cobrar con el contrato y la factura de cobro, y luego valida que todas las facturas tengan habilitada la interfaz presupuestal, que cuenten con un rubro activo asignado y que no hayan sido reconocidas previamente. Si todas las validaciones pasan, ejecuta la creación del comprobante de reconocimiento de ingresos usando la dependencia configurada en los parámetros de cartera y la vigencia presupuestal vigente; devuelve un código y mensaje de resultado indicando éxito o el motivo del rechazo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reconocimiento presupuestal de ingresos a partir de un radicado de facturas de cartera, validando interfaz presupuestal, vigencia y rubros, y enlazando el reconocimiento creado con el radicado y sus cuentas por cobrar.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un RadicateInvoiceC con el Id recibido para tomar su ConfirmDate.; Portfolio.SettingPortfolio para la unidad operativa debe tener DependencyId configurado.; Las facturas del radicado (no devueltas) con AccountReceivableType=2 y AffectBudget=1 deben tener BudgetId vigente y rubro asociado a una BudgetaryValidity con Status IN (1,2).; Las facturas no deben tener ya un RecognitionId asignado.; Todas las facturas con AffectBudget=1 deben pertenecer a una única vigencia presupuestal.; Ninguna factura del radicado puede tener AffectBudget=0 (todas deben tener interfaz presupuestal habilitada).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se procesan detalles del radicado con Devolution=0 y AccountReceivableType=2.; Sólo participan en el reconocimiento las cuentas por cobrar con AffectBudget=1.; Se exige que todas las facturas con afectación presupuestal compartan una única BudgetaryValidity con Status IN (1,2).; El reconocimiento se crea con Status=2, RecognitonType=1, AutomaticCollection=0 y EntityName=''RadicateInvoiceC''.; Sólo se considera BudgetHeader con Type=1 para resolver la vigencia presupuestal.; El detalle del reconocimiento se agrupa por CategoryId y RevenueTypeId sumando el Balance de las AR.; Si SP_SaveRecognition_Output falla, no se actualizan RadicateInvoiceC.RecognitionId ni AccountReceivable.RecognitionId.; La vigencia se selecciona priorizando el mayor Status (ORDER BY bv.Status DESC).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivable: Antes de validar, sincroniza CareGroupId, AffectBudget y BudgetId de cada AR (AccountReceivableType=2) tomando el CareGroup de la factura (Billing.Invoice.CareGroupId, o el de la AR si la factura no existe) para los detalles del radicado con Devolution=0.; [INSERT] Budget.Recognition: Construye un XML con encabezado (RecognitonType=1, Status=2, AutomaticCollection=0, EntityName=''RadicateInvoiceC'', Document=RadicatedConsecutive, Observations con texto ''Reconocimiento generado desde la Radicacion de Cuentas No. <consecutivo>'') y detalles agrupados por CategoryId/RevenueTypeId con InitialValue=SUM(Balance), y lo envía a Budget.SP_SaveRecognition_Output para crear el reconocimiento.; [UPDATE] Portfolio.RadicateInvoiceC: Tras crear el reconocimiento exitosamente, asigna ri.RecognitionId = @RecognitionId al radicado procesado.; [UPDATE] Portfolio.AccountReceivable: Tras crear el reconocimiento, asigna ar.RecognitionId=@RecognitionId a las cuentas por cobrar del radicado con AffectBudget=1, Devolution=0 y rubro presupuestal válido.; [RETURN_RESULT] @MessageResult: Devuelve CodeResult=0 con mensaje vacío si no hay facturas con AffectBudget=1 (nada que reconocer); CodeResult=999 con mensaje específico ante cada validación fallida; CodeResult=0 al finalizar exitosamente; CodeResult=999 con ERROR_MESSAGE+línea ante excepción capturada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existen facturas del radicado (Devolution=0) con AccountReceivableType=2 y AffectBudget=1 → Retorna CodeResult=0 y mensaje vacío sin generar reconocimiento.; si @DependencyId IS NULL (no hay dependencia configurada en SettingPortfolio) → Retorna 999 con mensaje pidiendo indicar la dependencia en parámetros de cuentas por cobrar.; si @BudgetaryValidityId IS NULL (no hay rubro o vigencia activa con Status IN (1,2)) → Retorna 999 informando que las facturas no tienen rubro o la vigencia no está activa.; si Existen facturas con AffectBudget=0 → Retorna 999 con la lista de facturas que no tienen habilitada la interfaz presupuestal.; si Existen facturas con AffectBudget=1 y RecognitionId IS NOT NULL → Retorna 999 listando las facturas que ya tienen un reconocimiento.; si Existen facturas con AffectBudget=1 y BudgetId que no resuelve a Budget.Budget → Retorna 999 listando las facturas sin rubro presupuestal asignado.; si Existen facturas cuyo BudgetHeader.BudgetaryValidityId difiere de @BudgetaryValidityId → Retorna 999 indicando que los detalles corresponden a más de una vigencia presupuestal.; si Budget.SP_SaveRecognition_Output devuelve @Code_Output <> 0 → Retorna 999 con el mensaje del SP o ''No se pudo generar el reconocimiento presupuestal''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveRecognition_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.SettingPortfolio; Portfolio.AccountReceivable; Billing.Invoice; Contract.CareGroup; Budget.Budget; Budget.BudgetHeader; Budget.BudgetaryValidity; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionByRadicateInvoiceId_Output';
-- GO
