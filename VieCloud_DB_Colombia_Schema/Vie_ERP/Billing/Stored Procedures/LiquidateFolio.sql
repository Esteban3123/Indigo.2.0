-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Liquidación
-- =============================================
CREATE PROCEDURE [Billing].[LiquidateFolio]
	@PatientCode VARCHAR(20),
	@AdmissionNumber VARCHAR(20),
	@ContainerCrystal VARCHAR(10),
	@BillingAuthorizationId INT,
	@OperativeUnitId INT,
	@ThirdPartyPatientId INT,
	@UserCode VARCHAR(20),
	@CompanyType TINYINT,
	@RevenueControlDetailCrossingListXml XML,
	@SkipAccountControlValidations BIT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @ResultStatus BIT, 
			@ResultMessageInvoice VARCHAR(MAX), 
			@ResultMessage VARCHAR(MAX),
			@ResultXml XML

	DECLARE @InvoiceResult AS TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		EXEC Billing.SP_LiquidateFolio_Output	@PatientCode, 
												@AdmissionNumber, 
												@ContainerCrystal, 
												@BillingAuthorizationId, 
												@OperativeUnitId, 
												@ThirdPartyPatientId, 
												@UserCode, 
												@CompanyType, 
												@RevenueControlDetailCrossingListXml,
												@SkipAccountControlValidations,
												--Salidas
												@ResultStatus OUTPUT, 
												@ResultMessageInvoice OUTPUT, 
												@ResultMessage OUTPUT,	
												@ResultXml OUTPUT

		--Se obtiene las facturas que vienen en el XML
		INSERT INTO @InvoiceResult
			SELECT	t.x.value('InvoiceId[1]','INT') InvoiceId,
					t.x.value('InvoiceNumber[1]','VARCHAR(20)') InvoiceNumber
			FROM @ResultXml.nodes('/Data') t(x)

		IF NOT EXISTS (SELECT 1 FROM @InvoiceResult)
		BEGIN
			SELECT @ResultStatus AS [StatusResult], @ResultMessageInvoice AS [MessageResult], @ResultMessage AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
			RETURN
		END
		
		SELECT @ResultStatus AS [StatusResult], @ResultMessageInvoice AS [MessageResult], @ResultMessage AS [Message], InvoiceId, InvoiceNumber
		FROM @InvoiceResult
	END TRY
	BEGIN CATCH
		SELECT CONVERT(BIT, 0) AS [StatusResult], '' AS [MessageResult], CONCAT('Error llamando la liquidación de Folios: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
	END CATCH	

	--SELECT CONVERT(BIT, 0) AS [StatusResult], '' AS [MessageResult], '' AS [Message], 0 AS InvoiceId, '' AS InvoiceNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ejecuta la liquidación de un folio de facturación para un paciente y admisión específicos, generando una o varias facturas como resultado del proceso. Recibe datos clave como el código del paciente (cédula), número de ingreso, autorización de facturación, unidad operativa, tipo de empresa pagadora y un listado XML de cruce de control de ingresos; internamente delega la lógica principal al procedimiento Billing.SP_LiquidateFolio_Output. Una vez procesada la liquidación, extrae del XML de respuesta los identificadores y números de factura generados y los retorna al llamador, indicando también el estado del resultado y mensajes de éxito o error. Existe para centralizar y exponer como interfaz limpia el proceso de cierre y facturación de servicios prestados a un paciente durante su ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'LiquidateFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'LiquidateFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta el proceso de liquidación de folios delegando la lógica a SP_LiquidateFolio_Output y devuelve al llamador las facturas generadas junto con estado y mensajes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El procedimiento Billing.SP_LiquidateFolio_Output debe existir y aceptar los parámetros de entrada/salida esperados.; El XML de cruce de detalle de control de ingresos (@RevenueControlDetailCrossingListXml) debe tener el formato esperado por SP_LiquidateFolio_Output.; El XML de salida @ResultXml debe seguir la estructura /Data con nodos InvoiceId e InvoiceNumber para poder ser deserializado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda excepción se captura y se transforma en un resultset con StatusResult=0; el procedimiento nunca propaga errores al llamador.; El resultset siempre tiene la misma forma de columnas: StatusResult, MessageResult, Message, InvoiceId, InvoiceNumber.; La lógica de negocio de liquidación no se ejecuta aquí; se delega íntegramente a SP_LiquidateFolio_Output.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de folios; Factura (Invoice); Autorización de facturación; Paciente; Admisión; Unidad operativa; Tercero pagador; Tipo de compañía; Control de ingresos (Revenue Control)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando @ResultXml no contiene nodos /Data (no hay facturas generadas), se retorna una única fila con InvoiceId=0 e InvoiceNumber='''' junto con el estado y mensajes.; [RETURN_RESULT] Resultset: Cuando existen filas en @InvoiceResult, se retorna un resultset con una fila por factura (InvoiceId, InvoiceNumber) acompañada de StatusResult, MessageResult y Message.; [RETURN_RESULT] Resultset: Si ocurre cualquier excepción, el CATCH retorna StatusResult=0, MessageResult vacío y Message con el texto ''Error llamando la liquidación de Folios: '' + ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS (SELECT 1 FROM @InvoiceResult) — el XML de salida no produjo facturas → Retorna un resultset con InvoiceId=0 e InvoiceNumber='''' y termina con RETURN. else Retorna el resultset con todas las facturas extraídas del XML.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_LiquidateFolio_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'LiquidateFolio';
-- GO
