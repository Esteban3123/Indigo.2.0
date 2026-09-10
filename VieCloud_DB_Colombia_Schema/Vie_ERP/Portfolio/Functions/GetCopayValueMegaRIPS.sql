
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el valor de la cuota moderadora (copago) de una factura para los reportes MegaRIPS. Recibe el número de factura y un indicador que determina si se usa el valor total bruto de la línea de facturación (GrandTotalSalesPrice del detalle de factura) o el precio de venta del detalle quirúrgico (TotalSalesPrice de ServiceOrderDetailSurgical, excluyendo registros que sean solo honorarios médicos). Devuelve el valor máximo encontrado entre las líneas de esa factura, representando el monto que corresponde al paciente como copago dentro del proceso de generación de RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetCopayValueMegaRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetCopayValueMegaRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor de copago a reportar en archivos MegaRIPS para una factura, eligiendo entre el total general de la línea o el valor quirúrgico según si el precio total está vacío.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura identificada por el número debe existir en Billing.Invoice con al menos un detalle en Billing.InvoiceDetail.; Para el cálculo quirúrgico, debe existir una orden de servicio (ServiceOrderDetail) ligada al detalle de factura y un registro quirúrgico asociado con OnlyMedicalFees = 0.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera registros quirúrgicos no marcados como exclusivamente honorarios médicos (OnlyMedicalFees = 0).; El resultado siempre es el valor máximo (MAX) entre las líneas asociadas a la factura, no una suma.; El valor retornado se trunca a numeric(18,0), sin decimales.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'copago; factura; detalle de factura; orden de servicio; servicio quirúrgico; honorarios médicos; MegaRIPS', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si TotalSalesPriceEmpty = 1, devuelve MAX(InvoiceDetail.GrandTotalSalesPrice) de la factura indicada.; [RETURN_RESULT] : Si TotalSalesPriceEmpty = 0, devuelve MAX(ServiceOrderDetailSurgical.TotalSalesPrice) filtrando OnlyMedicalFees = 0 para los detalles de la factura indicada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TotalSalesPriceEmpty = 1 → Usa GrandTotalSalesPrice del detalle de factura (Billing.InvoiceDetail) como valor de copago. else Usa TotalSalesPrice del detalle quirúrgico (Billing.ServiceOrderDetailSurgical) excluyendo registros que solo correspondan a honorarios médicos (OnlyMedicalFees = 0).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailSurgical', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCopayValueMegaRIPS';
GO
