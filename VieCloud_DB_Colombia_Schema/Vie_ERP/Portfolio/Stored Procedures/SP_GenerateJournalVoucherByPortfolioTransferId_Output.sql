-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-09
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output]
	@PortfolioTransferId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
	/******************************** Se ejecuta el OVERLOAD del SP *******************************/

					EXEC [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad] @PortfolioTransferId, @CodeUser,'', @Code_Output OUT, @Message_Output OUT

	/************************************************* RESULTADO *************************************************/
					
					SELECT	@CodeResult = @Code_Output,
							@MessageResult =ISNULL(@Message_Output, '')
					RETURN	
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (reconocimiento de cartera) a partir de una transferencia de cartera identificada por su ID. Actúa como punto de entrada que delega la lógica principal al procedimiento sobrecargado SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad, pasándole el identificador de la transferencia de cartera y el usuario que ejecuta la acción. Retorna un código y mensaje de resultado que indican si la generación del voucher contable fue exitosa o si ocurrió algún error, capturando excepciones de forma controlada. Se usa en el módulo de cartera y cuentas por cobrar para registrar contablemente los movimientos de transferencia de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la generación del comprobante contable (reconocimiento) asociado a una transferencia de cartera al procedimiento sobrecargado, devolviendo código y mensaje de resultado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una transferencia de cartera identificable para la cual generar el reconocimiento contable; El procedimiento sobrecargado SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad debe estar disponible', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre invoca el SP OverLoad pasando cadena vacía '''' como tercer parámetro adicional; @MessageResult nunca es NULL: se aplica ISNULL(..., '''') sobre el mensaje recibido; Ante cualquier error capturado, el código de resultado siempre es 999', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cuenta por cobrar; reconocimiento contable; transferencia de cartera (PortfolioTransfer); comprobante contable (JournalVoucher)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado de salida): Asigna a las variables de salida el código y mensaje devueltos por el SP sobrecargado: @CodeResult = @Code_Output, @MessageResult = ISNULL(@Message_Output, ''''); [RETURN_RESULT] (resultado de salida): En caso de excepción (BEGIN CATCH), retorna @CodeResult = 999 y @MessageResult con ERROR_MESSAGE() concatenado con '' - Linea: '' + ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRY exitoso al invocar el SP sobrecargado → Devuelve los códigos/mensajes producidos por el OverLoad else CATCH: devuelve código 999 con el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByPortfolioTransferId_Output';
-- GO
