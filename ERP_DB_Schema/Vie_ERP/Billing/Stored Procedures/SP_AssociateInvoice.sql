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