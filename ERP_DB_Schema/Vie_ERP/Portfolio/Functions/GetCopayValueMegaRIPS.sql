
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Portfolio].[GetCopayValueMegaRIPS]
(
	@InvoiceNumber varchar(30),
	@TotalSalesPriceEmpty bit
)
RETURNS numeric(18,0)
AS
BEGIN
	declare @copayValue numeric(18,0)
	
	if @TotalSalesPriceEmpty=1 
	begin		
		SELECT @copayValue = MAX(INVD.GrandTotalSalesPrice) 
		FROM Billing.Invoice AS INV 
		INNER JOIN Billing.InvoiceDetail AS INVD ON INV.Id=INVD.InvoiceId 
		WHERE INV.InvoiceNumber=@InvoiceNumber
	end
	else 
	begin		
		SELECT @copayValue = MAX(dq.TotalSalesPrice) 
		FROM Billing.Invoice AS INV 
		INNER JOIN Billing.InvoiceDetail AS INVD ON INV.Id=INVD.InvoiceId 
		INNER JOIN Billing.ServiceOrderDetail AS dos WITH (nolock) ON dos.Id = INVD.ServiceOrderDetailId 
		INNER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH (nolock) ON dq.ServiceOrderDetailId = dos.Id AND dq.OnlyMedicalFees = 0
		WHERE INV.InvoiceNumber=@InvoiceNumber
	end

	return @copayValue
END