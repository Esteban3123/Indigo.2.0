CREATE TABLE [Billing].[InvoiceDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]                 INT             NOT NULL,
    [ServiceOrderDetailId]      INT             NOT NULL,
    [ServiceDate]               DATETIME        CONSTRAINT [DF_InvoiceDetail_ServiceDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [InvoicedQuantity]          INT             CONSTRAINT [DF_InvoiceDetail_InvoicedQuantity] DEFAULT ((0)) NOT NULL,
    [TotalSalesPrice]           NUMERIC (20, 2) CONSTRAINT [DF_InvoiceDetail_TotalSalesPrice] DEFAULT ((0)) NOT NULL,
    [ThirdPartyDiscount]        NUMERIC (20, 2) CONSTRAINT [DF_InvoiceDetail_ThirdPartyDiscount] DEFAULT ((0)) NOT NULL,
    [Presentation]              TINYINT         NULL,
    [RecordType]                TINYINT         CONSTRAINT [DF_InvoiceDetail_RecordType] DEFAULT ((0)) NOT NULL,
    [GrandTotalSalesPrice]      NUMERIC (20, 2) NOT NULL,
    [GrandTotalDiscount]        NUMERIC (20, 2) CONSTRAINT [DF_InvoiceDetail_GrandTotalDiscount] DEFAULT ((0)) NOT NULL,
    [DistributionType]          TINYINT         NOT NULL,
    [ThirdPartySalesPrice]      NUMERIC (20, 2) NOT NULL,
    [ThirdPartyPercentage]      NUMERIC (5, 2)  NOT NULL,
    [ApplyRecoveryFee]          TINYINT         NOT NULL,
    [RecoveryFeeType]           TINYINT         NOT NULL,
    [SubTotalPatientSalesPrice] NUMERIC (20, 2) NOT NULL,
    [PatientPercentage]         NUMERIC (5, 2)  NOT NULL,
    [Balance]                   NUMERIC (20, 2) CONSTRAINT [DF_InvoiceDetail_Balance] DEFAULT ((0)) NOT NULL,
    [NetWorth]                  NUMERIC (20, 2) CONSTRAINT [DF__InvoiceDe__NetWo__547DB563] DEFAULT ((0)) NOT NULL,
    [GrandTotalTaxes]           NUMERIC (20, 2) CONSTRAINT [DF__InvoiceDe__Grand__5571D99C] DEFAULT ((0)) NOT NULL,
    [TaxId]                     INT             NULL,
    CONSTRAINT [PK_InvoiceDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CC_InvoiceDetail_Balance] CHECK ([Balance]>=(0)),
    CONSTRAINT [FK_InvoiceDetail_GeneralLedgerIVA] FOREIGN KEY ([TaxId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceDetail_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [CC_InvoiceDetail_Balance];


GO
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [FK_InvoiceDetail_GeneralLedgerIVA];


GO
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [FK_InvoiceDetail_Invoice];


GO
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [FK_InvoiceDetail_ServiceOrderDetail];


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail_InvoiceId]
    ON [Billing].[InvoiceDetail]([InvoiceId] ASC)
    INCLUDE([Id], [InvoicedQuantity], [ServiceOrderDetailId], [GrandTotalSalesPrice], [SubTotalPatientSalesPrice], [ThirdPartySalesPrice], [RecoveryFeeType]);


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail_Details]
    ON [Billing].[InvoiceDetail]([GrandTotalSalesPrice] ASC)
    INCLUDE([GrandTotalDiscount], [InvoicedQuantity], [InvoiceId], [ServiceOrderDetailId], [ThirdPartySalesPrice], [TotalSalesPrice]);


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail__ServiceOrderDetailId__INC__Id__InvoiceId]
    ON [Billing].[InvoiceDetail]([ServiceOrderDetailId] ASC)
    INCLUDE([Id], [InvoiceId]);


GO
-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 01/02/2016
-- Description:	Trigger para que genere error cuando hayan datos inconsistentes
-- =============================================
CREATE TRIGGER [Billing].[TriggerValidateDataInvoiceDetail]
   ON [Billing].[InvoiceDetail]
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- Si todas las filas afectadas son de facturas anuladas (p. ej. solo se ajusta Balance tras Status=2),
	-- no revalidar contra ServiceOrderDetail: el folio pudo cambiar respecto al snapshot facturado.
	IF NOT EXISTS (
		SELECT 1
		FROM INSERTED id
		INNER JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = id.InvoiceId
		WHERE i.Status <> 2
	)
		RETURN

	DECLARE @LiquidateMasterAccount as bit = (	select top 1 isnull(sb.LiquidateMasterAccount,0)
												from INSERTED id
												JOIN Billing.Invoice i WITH(NOLOCK) on i.id = id.InvoiceId
												JOIN Billing.SettingsBilling sb WITH(NOLOCK) on sb.IdOperatingUnit= i.OperatingUnitId
												)

	if EXISTS(SELECT 1 
				from Billing.Invoice i
				JOIN Billing.InvoiceDetail idr on idr.InvoiceId =i.Id
				where i.id in (SELECT InvoiceId from INSERTED id GROUP by id.InvoiceId)
				group by i.InvoiceDate,i.InvoiceNumber, TotalPatientSalesPrice 
				having TotalPatientSalesPrice <> sum(idr.SubTotalPatientSalesPrice) 
				and round(TotalPatientSalesPrice,-2) <> round(sum(idr.SubTotalPatientSalesPrice),-2) and @LiquidateMasterAccount=0) begin
		THROW 51000, 'Error de datos, no concuerda el detalle de la couta moderadora con la cabecera de la factura ', 1
	end
	if EXISTS(select 1 from inserted where DistributionType = 1) begin		
		
		IF EXISTS( SELECT 1 FROM GeneralLedger.CompanySettings WHERE SalePriceIncludeTax=1) BEGIN
			if EXISTS(
				select 1 from inserted i 
				INNER JOIN Billing.Invoice ic ON i.InvoiceId = ic.Id
				INNER JOIN GeneralLedger.CompanySettings cs ON cs.OfficialCurrencyId = ic.CurrencyId
				inner join Billing.ServiceOrderDetail sod on i.ServiceOrderDetailId = sod.Id 
				where i.DistributionType = 1 
					and (ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,2) <> i.GrandTotalSalesPrice 
					and ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-2) <> i.GrandTotalSalesPrice
					and ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-1) <> ROUND(i.GrandTotalSalesPrice,-1))) 
			begin
				THROW 51000, '(InvoiceDetail) Error generado por control desde trigger, no concuerda la operacion (Cantidad * ValorUnitario) = Total', 1
			end
		END
		ELSE BEGIN

			if EXISTS(
				select 1 from inserted i 
				INNER JOIN Billing.Invoice ic ON i.InvoiceId = ic.Id
				INNER JOIN GeneralLedger.CompanySettings cs ON cs.OfficialCurrencyId = ic.CurrencyId
				inner join Billing.ServiceOrderDetail sod on i.ServiceOrderDetailId = sod.Id 
				INNER JOIN Billing.ServiceOrderDetailDistribution sodd on sod.Id=sodd.ServiceOrderDetailId
				INNER JOIN Billing.RevenueControlDetail rcd on sodd.RevenueControlDetailId=rcd.Id
				left JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on sod.IvaId = iva.Id
				LEFT join Billing.SettingsBilling sb WITH(NOLOCK) on sb.IdOperatingUnit = ic.OperatingUnitId
				where i.DistributionType = 1 and sb.LiquidateMasterAccount = 0
					and (ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
									-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity),2) <> i.GrandTotalSalesPrice 
					and ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
									-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity),-2) <> i.GrandTotalSalesPrice)) 
			begin
				THROW 51000, '(InvoiceDetail) Error generado por control desde trigger, no concuerda la operacion (Cantidad * ValorUnitario) = Total', 1
			end
		END					
	end

	-- Se actualiza el redondeo
	UPDATE sod
		SET sod.RoundService =	CASE
									WHEN ROUND((id.InvoicedQuantity * id.TotalSalesPrice), 0) = id.GrandTotalSalesPrice THEN 1
									WHEN ROUND((id.InvoicedQuantity * id.TotalSalesPrice), -1) = id.GrandTotalSalesPrice THEN 10
									WHEN ROUND((id.InvoicedQuantity * id.TotalSalesPrice), -2) = id.GrandTotalSalesPrice THEN 100
									WHEN ROUND((id.InvoicedQuantity * id.TotalSalesPrice), -3) = id.GrandTotalSalesPrice THEN 1000
									ELSE 0
								END
	FROM INSERTED id
	JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
	JOIN Billing.Invoice ic ON id.InvoiceId = ic.Id
    JOIN GeneralLedger.CompanySettings cs ON cs.OfficialCurrencyId = ic.CurrencyId
	WHERE ROUND((id.InvoicedQuantity * id.TotalSalesPrice), 
			CASE sod.RoundService
				WHEN 10 THEN -1
				WHEN 100 THEN -2
				WHEN 1000 THEN -3
				ELSE 0
			END) <> id.GrandTotalSalesPrice
END
GO


-- =============================================
-- Author:		PABLO ALEXANDER SALAZAR SANCHEZ
-- Create date: 03/01/2024
-- Description:	Trigger para que genere error cuando hayan items con valor incluidos en otros servicios
-- =============================================
CREATE TRIGGER [Billing].[TriggerValidateItemsIncludeOtherServices]
   ON [Billing].[InvoiceDetail]
   AFTER INSERT, UPDATE
AS 
BEGIN
	if EXISTS(
		SELECT sod.SubTotalSalesPrice,
			   sod.TotalSalesPrice,
			   sod.IncludeServiceOrderDetailId
		FROM INSERTED id
			join Billing.ServiceOrderDetail sod  on id.ServiceOrderDetailId = sod.id
			JOIN Billing.ServiceOrderDetailDistribution sodd on sod.Id = sodd.ServiceOrderDetailId
		WHERE 
			(id.ThirdPartySalesPrice > 0 OR
			sodd.GrandTotalSalesPrice > 0) AND
		sod.GrandTotalSalesPrice =0 and
		sod.IncludeServiceOrderDetailId is not null)
	BEGIN
		THROW 51000, 'Existen ítems con valor incluidos en otros servicios', 1
	END
END
GO
DISABLE TRIGGER [Billing].[TriggerValidateItemsIncludeOtherServices]
    ON [Billing].[InvoiceDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del impuesto(IVA)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total del impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor NETO : subtotal - descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'NetWorth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al porcentaje del valor total cobrado por el Producto / Servicio a cargo del paciente por concepto de cuota de recuperacion (RecoveryFeeType)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total calculado por el Producto / Servicio al paciente por concepto de cuotas de recuperacion. (RecoveryFeeType). Este valor es el calculado segun se establece en la ley, pero puede variar si al paciente se le concede un descuento sobre el valor de recuperacion.  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Cuota de Recuperacion  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Cuota de Recuperación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica 3 estados para el calculo de la cuota de recuperacion  0 - No Aplica Cuota de Recuperacion  1 - Disponible para hacer el calculo de la cuota de recuperacion  2 - Se esta aplicando cuota de recuperacion sobre el item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al porcentaje cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del 100% cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del GrandTotalSalesPrice cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Distribucion  1 - Ninguno  2 - Distribucion Normal  3 - Distribucion por Carencia 1er Responsable  4 - Distribucion por Carencia 2do Responsable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total del descuento que se le realizo al item, Este descuento tambien se distribuye cuando el item es distribuido', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio establecido en GrandTotalSalesPrice de ServiceOrderDetailDistribution  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cabecera de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';

