-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 02-03-2016
-- Description:	valida que las facturas del radicado sean capitadas o de ventas
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ValidateRadicateDetailInvoiceCapitated] 
	@RadicateId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	

    if (SELECT COUNT(*) FROM  Portfolio.RadicateInvoiceD AS rd INNER JOIN Billing.Invoice AS i ON rd.InvoiceNumber = i.InvoiceNumber where i.DocumentType = 4 and rd.RadicateInvoiceCId = @RadicateId) > 0 begin
		select CAST( 1 as int) as StatusResult--capitadas
	end
	else begin
		select CAST( 2 as int) as StatusResult--capitadas
	end
END