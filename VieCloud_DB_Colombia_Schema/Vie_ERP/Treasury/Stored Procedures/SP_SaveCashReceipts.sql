-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 09-06-2016
-- Description:	Procedimiento para generar un recibo de caja
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveCashReceipts] 
	@CashReceiptsXml AS XML,
	@User VARCHAR(20),
	@CompanyType int
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@StatusResult TINYINT,
			@CashReceiptId INT

	EXEC [Treasury].[SP_SaveCashReceipts_Output] 
			@CashReceiptsXml, 
			@User, 
			@CompanyType, 
			-----------------------------------------------
			@CodeResult OUTPUT, 
			@MessageResult OUTPUT,
			@StatusResult OUTPUT,
			@CashReceiptId OUTPUT

	SELECT	CAST(@CodeResult AS VARCHAR(20)) AS CodeMessage, 
			@MessageResult AS Message, 
			@CashReceiptId AS CashReceiptId,
			@StatusResult Status
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y registra un recibo de caja en el módulo de Tesorería. Recibe como entrada un XML con los datos del recibo, el usuario que lo genera y el tipo de empresa, y delega el procesamiento real al procedimiento interno SP_SaveCashReceipts_Output. Retorna el identificador del recibo creado, un código de resultado, un mensaje descriptivo y un estado de la operación, permitiendo al sistema confirmador saber si el recibo se grabó correctamente. Se usa en flujos de cobro y pagos para registrar ingresos de caja asociados a pacientes, facturas o contratos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCashReceipts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCashReceipts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la generación de un recibo de caja en SP_SaveCashReceipts_Output y expone como result set el código, mensaje, identificador del recibo y estado del proceso.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con la estructura del recibo de caja, el usuario y el tipo de compañía esperados por SP_SaveCashReceipts_Output.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se devuelve como conjunto con cuatro columnas: CodeMessage, Message, CashReceiptId y Status.; El código de mensaje se entrega siempre convertido a VARCHAR(20).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'recibo de caja; tesorería', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveCashReceipts_Output, retorna un SELECT con CodeMessage (cast a VARCHAR(20)), Message, CashReceiptId y Status provenientes de los OUTPUT del procedimiento interno.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Treasury.SP_SaveCashReceipts_Output', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCashReceipts';
-- GO
