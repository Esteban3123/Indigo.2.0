-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 11/12/2015
-- Description:	Procedimiento que se encarga de confirmar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ConfirmPortfolioTransfer] @PortfolioTransferXml AS    XML, 
                                                          @CodeUser AS                VARCHAR(20), 
                                                          @Reverse AS                 BIT, 
                                                          @IdJournalVoucherBilling AS INT, 
                                                          @CompanyType AS             INT
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @CodeMessage INT, @Message VARCHAR(MAX), @IdTransfer INT, @CodeTransfer VARCHAR(20), @Consecutive VARCHAR(20);
        EXEC [Portfolio].[SP_ConfirmPortfolioTransfer_Out] 
             @PortfolioTransferXml, 
             @CodeUser, 
             @Reverse, 
             @IdJournalVoucherBilling, 
             @CompanyType, 
             @CodeMessage OUTPUT, 
             @Message OUTPUT, 
             @IdTransfer OUTPUT, 
             @CodeTransfer OUTPUT, 
             @Consecutive OUTPUT;
        SELECT @CodeMessage AS CodeMessage, 
               @Message AS Message, 
               @IdTransfer AS Id, 
               @CodeTransfer AS CodeTransfer, 
               @Consecutive AS Consecutive;
        RETURN;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma el cruce (aplicación) de anticipos contra cuentas por cobrar (CxC) en el módulo de cartera. Recibe un XML con los detalles del traslado de portafolio, el usuario que ejecuta la acción, un indicador de reversión (para anular el cruce), el comprobante contable de facturación y el tipo de empresa. Delega el procesamiento real al procedimiento interno SP_ConfirmPortfolioTransfer_Out y retorna el resultado: código de mensaje, descripción del resultado, identificador del traslado, código del traslado y consecutivo generado. Se usa para registrar o revertir la imputación de pagos anticipados contra facturas pendientes de cobro en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmPortfolioTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que confirma (o reversa) el cruce contable entre anticipos y cuentas por cobrar, delegando la lógica al procedimiento _Out y devolviendo el resultado como result set.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con la información del traslado de cartera a confirmar; Debe identificarse el usuario que ejecuta la operación; Debe indicarse si la operación es reverso o confirmación mediante el flag correspondiente; Debe existir un comprobante contable de facturación asociado y un tipo de compañía válido', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No realiza lógica transaccional propia: toda la persistencia se delega a SP_ConfirmPortfolioTransfer_Out; Siempre devuelve un único result set con cinco columnas fijas: CodeMessage, Message, Id, CodeTransfer, Consecutive; El comportamiento (confirmar vs reversar) queda determinado exclusivamente por el flag Reverse pasado al procedimiento interno', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo contra cuentas por cobrar (CxC); Traslado de cartera (Portfolio Transfer); Comprobante contable de facturación (Journal Voucher Billing); Reverso de operación; Tipo de compañía', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Tras invocar SP_ConfirmPortfolioTransfer_Out se retorna un result set con CodeMessage, Message, Id (IdTransfer), CodeTransfer y Consecutive obtenidos como OUTPUT', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_ConfirmPortfolioTransfer_Out', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmPortfolioTransfer';
-- GO
