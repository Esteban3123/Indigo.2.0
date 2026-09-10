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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si las facturas incluidas en un radicado ante entidad pagadora corresponden a facturas capitadas (tipo de documento 4) o a facturas de venta regulares. Recibe el identificador del radicado y cruza el detalle de facturas radicadas con el encabezado de facturación para verificar el tipo de documento. Retorna 1 si el radicado contiene facturas capitadas, o 2 si no las contiene, permitiendo al proceso de cartera distinguir el tipo de cobro antes de continuar con la gestión de cobro o glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si las facturas asociadas a un radicado corresponden a facturación capitada o a ventas, devolviendo un código de estado según el tipo documental hallado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El radicado debe existir en Portfolio.RadicateInvoiceD con facturas asociadas que puedan cruzarse con Billing.Invoice por InvoiceNumber.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DocumentType = 4 en Billing.Invoice identifica facturas capitadas.; El procedimiento siempre retorna exactamente un resultado escalar con valores 1 o 2.; La clasificación es excluyente: basta una sola factura capitada en el radicado para clasificarlo como capitado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicado; Factura capitada; Factura de venta; Tipo de documento de factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Si existe al menos una factura del radicado con Billing.Invoice.DocumentType = 4 entonces retorna StatusResult = 1 (capitadas); en caso contrario retorna StatusResult = 2 (ventas).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si COUNT(*) de RadicateInvoiceD ⨝ Billing.Invoice donde i.DocumentType = 4 y rd.RadicateInvoiceCId = @RadicateId > 0 → Retorna StatusResult = 1 (facturas capitadas) else Retorna StatusResult = 2 (facturas de ventas)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceD; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateRadicateDetailInvoiceCapitated';
-- GO
