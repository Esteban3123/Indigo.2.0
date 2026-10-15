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