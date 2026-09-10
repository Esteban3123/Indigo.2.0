-- =============================================
-- Author:		Diego Roldan
-- Create date: 2024-01-21
-- Description:	Genera un cruce de anticipos vs CxC
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GeneratePortfolioTransferAsList]
	@ListPortfolioAdvanceCrossingXml XML,
	@OperativeUnitId INT,
	@UserCode VARCHAR(20),
	@AccountReceivableId INT,
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		DECLARE @CodeResult INT,
				@MessageResult VARCHAR(MAX)

		EXEC Portfolio.SP_GeneratePortfolioTransfer	
			@ListPortfolioAdvanceCrossingXml,
			@OperativeUnitId, 
			@UserCode, 
			@AccountReceivableId, 
			@CompanyType, 
			@CodeResult OUTPUT, 
			@MessageResult OUTPUT

		SELECT	@CodeResult AS Code, @MessageResult AS Message
	END TRY
	BEGIN CATCH
		SELECT	99 AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el cruce de anticipos contra cuentas por cobrar (CxC) de cartera, procesando una lista de cruces en formato XML. Actúa como envoltorio simplificado del procedimiento Portfolio.SP_GeneratePortfolioTransfer, al que delega toda la lógica de negocio, y devuelve un código y mensaje de resultado indicando si la operación fue exitosa o si ocurrió un error. Se usa en el módulo de cartera para aplicar anticipos de pagadores o pacientes a facturas o documentos de cobro pendientes, en el contexto de una unidad operativa y tipo de empresa específicos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePortfolioTransferAsList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta el cruce de anticipos contra cuentas por cobrar y devuelve el resultado como conjunto (Code/Message) capturando errores en CATCH.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse un XML con la lista de cruces de anticipos de cartera; Debe existir una cuenta por cobrar y unidad operativa válidas referenciadas por los identificadores recibidos', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna exactamente un resultset con columnas Code y Message, nunca propaga la excepción al llamador; El código 99 está reservado para errores capturados por el bloque CATCH', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipos; Cuentas por Cobrar (CxC); Cruce de cartera; Unidad operativa; Tipo de compañía', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras ejecutar SP_GeneratePortfolioTransfer, retorna un resultset con columnas Code y Message provenientes de los OUTPUT del SP invocado; [RETURN_RESULT] (resultset): Si ocurre cualquier error en el TRY, retorna Code=99 y Message con ERROR_MESSAGE() concatenado con '' - Linea: '' y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BEGIN CATCH (cualquier excepción durante la ejecución del SP interno) → Devuelve resultset con Code=99 y mensaje de error con número de línea else Devuelve el Code/Message producidos por SP_GeneratePortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_GeneratePortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePortfolioTransferAsList';
-- GO
