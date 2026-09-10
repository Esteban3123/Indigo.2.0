-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-19
-- Description:	Procedimiento encargado de relacionar un folio a una factura
-- =============================================
CREATE PROCEDURE [Billing].[SP_AssociateInvoice]
	@RevenueControlDetailId INT,
	@InvoiceId INT,
	@UserCode varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		DECLARE @InvoiceRevenueControlDetailId INT,
				@OperativeUnitId INT,
				@Errors VARCHAR(MAX) = ''

		-- Se obtiene el folio de la factura a asociar y la unidad operativa de la misma
		SELECT	@InvoiceRevenueControlDetailId = i.RevenueControlDetailId,
				@OperativeUnitId = i.OperatingUnitId
		FROM Billing.Invoice i
		WHERE i.Id = @InvoiceId

		-- Se realiza el recalculo del Folio
		Exec Billing.SP_UpdateRevenueControlDetailValuesNoSelect @RevenueControlDetailId, @OperativeUnitId

		/***********************************************  VALIDACIONES ***********************************************/

		-- Se valida que la factura se encuentre en estado Facturado
		IF NOT EXISTS 
		(
			SELECT 1
			FROM Billing.Invoice i
			WHERE i.Id = @InvoiceId AND i.Status = 1
		) BEGIN
			SELECT	@Errors = @Errors + IIF(@errors = '', '', CHAR(13) + CHAR(10)) + 'La factura se encuentra en estado: ' + CASE i.Status
																																WHEN 2 THEN 'Anulado'
																																ELSE 'N/A'
																															END
			FROM Billing.Invoice i
			WHERE i.Id = @InvoiceId
		END

		-- Se valida que el folio se encuentre en estado registrado 
		IF NOT EXISTS 
		(
			SELECT 1
			FROM Billing.RevenueControlDetail rcd
			WHERE rcd.Id = @RevenueControlDetailId AND rcd.Status = 1
		) BEGIN
			SELECT	@Errors = @Errors + IIF(@errors = '', '', CHAR(13) + CHAR(10)) + 'El folio se encuentra en estado: ' + CASE rcd.Status
																																WHEN 2 THEN 'Facturado'
																																WHEN 3 THEN 'Bloqueado'
																																WHEN 4 THEN 'Anulado'
																																WHEN 5 THEN 'Reconocimiento Ingresos'
																																WHEN 6 THEN 'Factura Asociada'
																																ELSE 'N/A'
																															END
			FROM Billing.RevenueControlDetail rcd
			WHERE rcd.Id = @RevenueControlDetailId
		END

		-- Se valida que el folio no tenga valor
		IF NOT EXISTS 
		(
			SELECT 1
			FROM Billing.RevenueControlDetail rcd
			WHERE rcd.Id = @RevenueControlDetailId AND rcd.TotalFolio = 0
		) BEGIN
			SELECT	@Errors = @Errors + IIF(@errors = '', '', CHAR(13) + CHAR(10)) + 'El folio no debe tener valor'
			FROM Billing.RevenueControlDetail rcd
			WHERE rcd.Id = @RevenueControlDetailId
		END

		-- Se valida que todos los items deben estar incluidos en la factura
		IF EXISTS 
		(
			SELECT 1
			FROM Billing.RevenueControlDetail rcd
			JOIN Billing.ServiceOrderDetailDistribution sodd ON rcd.Id = sodd.RevenueControlDetailId
			JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
			LEFT JOIN Billing.ServiceOrderDetailDistribution isodd ON @InvoiceRevenueControlDetailId = isodd.RevenueControlDetailId AND sod.IncludeServiceOrderDetailId = isodd.ServiceOrderDetailId
			WHERE rcd.Id = @RevenueControlDetailId
				AND isodd.Id IS NULL
		) BEGIN
			SET	@Errors = @Errors + IIF(@errors = '', '', CHAR(13) + CHAR(10)) + 'Existen detalles que no se encuentran incluidos en la factura'
		END

		-- Si no hay mas folios se cierra el ingreso
		IF NOT EXISTS 
		(
			SELECT 1
			FROM 
			(
				SELECT rcd.RevenueControlId
				FROM Billing.RevenueControlDetail rcd
				WHERE rcd.Id = @RevenueControlDetailId
			) rc
			JOIN Billing.RevenueControlDetail rcd ON rc.RevenueControlId = rcd.RevenueControlId
			WHERE rcd.Id <> @RevenueControlDetailId AND rcd.Status NOT IN (2, 3, 6)
		) BEGIN
			DECLARE @AdmissionNumber VARCHAR(50),
					@StatusResult BIT,
					@MessageResult VARCHAR(MAX)

			SELECT @AdmissionNumber = rc.AdmissionNumber
			FROM Billing.RevenueControl rc
			JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
			WHERE rcd.Id = @RevenueControlDetailId

			EXEC [Billing].[SP_CloseAdmission_Output] 
				@AdmissionNumber, 
				'INDIGO008', 
				--------------------------------------------
				@StatusResult OUTPUT, 
				@MessageResult OUTPUT

			IF ISNULL(@StatusResult, 0) = 0
			BEGIN
				SET	@Errors = @Errors + IIF(@errors = '', '', CHAR(13) + CHAR(10)) + ISNULL(@MessageResult, 'Error al cerrar ingreso')
			END
		END

		IF @errors <> ''
		BEGIN
			SELECT	CAST(0 AS BIT) AS StatusResult, 
					@errors AS MessageResult
			RETURN
		END

		/************************************************** PROCESO **************************************************/

		INSERT INTO Billing.RevenueControlDetailInvoice
			(RevenueControlDetailId, InvoiceId, Status, CreationUser, CreationDate)
			SELECT @RevenueControlDetailId, @InvoiceId, 1, @UserCode, [Common].[GETDATE]()

		UPDATE rcd
			SET rcd.Status = 6
		FROM Billing.RevenueControlDetail rcd
		WHERE rcd.Id = @RevenueControlDetailId

		/************************************************* RESULTADO *************************************************/

		SELECT	CAST(1 AS BIT) AS StatusResult, 
				'Factura asociada correctamente al Folio' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT	CAST(0 AS BIT) AS StatusResult, 
				'SP_AssociateInvoice: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS StatusResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asocia un folio de control de ingresos (RevenueControlDetail) a una factura de cobro existente, vinculando ambos registros en la tabla RevenueControlDetailInvoice y marcando el folio como ''Factura Asociada'' (estado 6). Antes de ejecutar la asociación, recalcula los valores del folio, y valida que la factura esté en estado ''Facturado'', que el folio esté en estado ''Registrado'', que el folio tenga valor cero y que todos los ítems de servicio del folio estén incluidos en la factura. Si tras la asociación no quedan más folios pendientes en el control de ingresos, cierra automáticamente el ingreso del paciente invocando SP_CloseAdmission_Output.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_AssociateInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_AssociateInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Asocia un folio (RevenueControlDetail) sin valor a una factura facturada, validando estados, inclusión de ítems en la factura, y cerrando automáticamente el ingreso si es el último folio pendiente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una factura con el Id recibido en Billing.Invoice; Debe existir el folio (RevenueControlDetail) con el Id recibido; El folio de la factura y el folio a asociar deben pertenecer al mismo RevenueControl para validar la inclusión de ítems; Antes de validar se recalculan los valores del folio mediante SP_UpdateRevenueControlDetailValuesNoSelect', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El folio solo se asocia a una factura cuando está en estado Registrado (Status=1) y sin valor (TotalFolio=0); La factura debe estar en estado Facturado (Status=1) para permitir asociación; Tras asociar exitosamente, el folio queda en Status=6 (Factura Asociada); El registro de asociación se crea siempre con Status=1; Todos los ServiceOrderDetail del folio deben estar incluidos (vía IncludeServiceOrderDetailId) en la distribución del folio de la factura destino; Si el folio asociado es el último pendiente del ingreso (no quedan otros con Status fuera de 2,3,6), el ingreso se cierra automáticamente con motivo ''INDIGO008''; Los errores de validación se acumulan y se reportan en conjunto; ninguna escritura ocurre si hay errores; Cualquier excepción se captura y se devuelve como StatusResult=0 con mensaje y línea de error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Folio de facturación; Control de ingresos; Admisión del paciente; Orden de servicio; Distribución financiera; Cierre de ingreso; Estados de factura (Facturado, Anulado); Estados de folio (Registrado, Facturado, Bloqueado, Anulado, Reconocimiento de Ingresos, Factura Asociada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.RevenueControlDetail: Tras pasar todas las validaciones, actualiza Status=6 (Factura Asociada) en el folio indicado; [INSERT] Billing.RevenueControlDetailInvoice: Tras pasar todas las validaciones, inserta el vínculo folio-factura con Status=1 y la fecha/usuario de creación; [RAISERROR] Billing.RevenueControl: Cuando es el último folio pendiente del ingreso, invoca SP_CloseAdmission_Output(''INDIGO008'') para cerrar el ingreso; si falla, agrega el mensaje al error acumulado; [RETURN_RESULT] resultset: Retorna (StatusResult BIT, MessageResult): 0 + errores acumulados si validaciones fallan; 1 + ''Factura asociada correctamente al Folio'' si tiene éxito; 0 + mensaje de excepción con línea en CATCH', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La factura no existe con Status=1 (Facturado) → Acumula error indicando el estado actual de la factura (2=Anulado, otro=N/A); si El folio (RevenueControlDetail) no está en Status=1 (Registrado) → Acumula error con el estado actual del folio (2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada); si RevenueControlDetail.TotalFolio <> 0 → Acumula error ''El folio no debe tener valor''; si Existen ServiceOrderDetail del folio cuyo IncludeServiceOrderDetailId no aparece en la distribución del folio de la factura (@InvoiceRevenueControlDetailId) → Acumula error ''Existen detalles que no se encuentran incluidos en la factura''; si No quedan otros RevenueControlDetail del mismo RevenueControl con Status NOT IN (2,3,6) → Ejecuta SP_CloseAdmission_Output con motivo ''INDIGO008'' para cerrar el ingreso; si retorna StatusResult=0 acumula el mensaje de error retornado; si @errors <> '''' tras las validaciones → Retorna StatusResult=0 con los errores acumulados y aborta el proceso else Inserta en RevenueControlDetailInvoice, actualiza el folio a Status=6 y retorna StatusResult=1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValuesNoSelect; Billing.SP_CloseAdmission_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.RevenueControl', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AssociateInvoice';
-- GO
