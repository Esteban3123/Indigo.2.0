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
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [CC_InvoiceDetail_Balance];


GO



GO



GO





GO
ALTER TABLE [Billing].[InvoiceDetail] NOCHECK CONSTRAINT [CC_InvoiceDetail_Balance];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail_InvoiceId]
    ON [Billing].[InvoiceDetail]([InvoiceId] ASC)
    INCLUDE([Id], [InvoicedQuantity], [ServiceOrderDetailId], [GrandTotalSalesPrice], [SubTotalPatientSalesPrice], [ThirdPartySalesPrice], [RecoveryFeeType]);


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail_Details]
    ON [Billing].[InvoiceDetail]([GrandTotalSalesPrice] ASC)
    INCLUDE([GrandTotalDiscount], [InvoicedQuantity], [InvoiceId], [ServiceOrderDetailId], [ThirdPartySalesPrice], [TotalSalesPrice]);


GO
ALTER INDEX [IX_InvoiceDetail_Details]
    ON [Billing].[InvoiceDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetail__ServiceOrderDetailId__INC__Id__InvoiceId]
    ON [Billing].[InvoiceDetail]([ServiceOrderDetailId] ASC)
    INCLUDE([Id], [InvoiceId]);


GO
ALTER INDEX [IX_InvoiceDetail__ServiceOrderDetailId__INC__Id__InvoiceId]
    ON [Billing].[InvoiceDetail] DISABLE;




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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto aplicado (IVA). Referencia a GeneralLedgerIVA. Clave para búsquedas de gravamen, tributación y normativa fiscal. INT, nullable, FK a [GeneralLedger].[GeneralLedgerIVA]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del impuesto(IVA)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario total del impuesto (IVA) calculado sobre el producto/servicio facturado. NUMERIC(20,2). Búsquedas: tributación, gravamen, impuesto, retención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del impuesto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto facturado: subtotal menos descuentos aplicados. NUMERIC(20,2), default 0. Base para liquidación a terceros y paciente. Búsquedas: neto, líquido, descuento aplicado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'NetWorth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor NETO : subtotal - descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'NetWorth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'NetWorth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente del detalle de factura, afectado por notas de cartera y ajustes de preauditoría. NUMERIC(20,2), default 0, constraint >=0. Búsquedas: saldo, deuda, pendiente, cartera, glosa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del valor total asignado al paciente por cuota de recuperación (moderadora, copago, bono). NUMERIC(5,2). Búsquedas: cuota moderadora, copago, porcentaje paciente, responsabilidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje del valor total cobrado por el Producto / Servicio a cargo del paciente por concepto de cuota de recuperacion (RecoveryFeeType)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado al paciente por cuota de recuperación, según normativa legal pero variable por descuentos concedidos. NUMERIC(20,2). Búsquedas: cuota de recuperación, copago, aporte paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total calculado por el Producto / Servicio al paciente por concepto de cuotas de recuperacion. (RecoveryFeeType). Este valor es el calculado segun se establece en la ley, pero puede variar si al paciente se le concede un descuento sobre el valor de recuperacion.  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuota de recuperación: 1=Ninguna, 2=Cuota Moderadora, 3=Copago, 4=Bono, 5=Cuota Recuperación. TINYINT. Búsquedas: cuota moderadora, copago, bono, responsabilidad paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Cuota de Recuperacion  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Cuota de Recuperación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del cálculo de cuota de recuperación: 0=No aplica, 1=Disponible para cálculo, 2=Aplicada al ítem. TINYINT. Búsquedas: cuota recuperación, cálculo moderadora, estado copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 3 estados para el calculo de la cuota de recuperacion  0 - No Aplica Cuota de Recuperacion  1 - Disponible para hacer el calculo de la cuota de recuperacion  2 - Se esta aplicando cuota de recuperacion sobre el item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje cobrado al tercero responsable (EAPB, asegurador) del producto/servicio; difiere de 100% cuando aplica cuota de recuperación. NUMERIC(5,2). Búsquedas: tercero, asegurador, EAPB, distribución', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del 100% cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado al tercero responsable; difiere de GrandTotalSalesPrice cuando aplica cuota de recuperación. NUMERIC(20,2). Búsquedas: tercero, asegurador, EAPB, valor facturado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del GrandTotalSalesPrice cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución de valor: 1=Ninguno, 2=Normal, 3=Carencia 1er Responsable, 4=Carencia 2do Responsable. TINYINT. Búsquedas: distribución, carencia, responsable, asignación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Distribucion  1 - Ninguno  2 - Distribucion Normal  3 - Distribucion por Carencia 1er Responsable  4 - Distribucion por Carencia 2do Responsable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento total aplicado al ítem, distribuible cuando el servicio es distribuido a múltiples responsables. NUMERIC(20,2), default 0. Búsquedas: descuento, rebaja, oferta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del descuento que se le realizo al item, Este descuento tambien se distribuye cuando el item es distribuido', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total facturado del producto/servicio establecido en ServiceOrderDetailDistribution. NUMERIC(20,2). Base de cálculo para impuestos, descuentos y distribuciones. Búsquedas: valor facturado, precio, tarifa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio establecido en GrandTotalSalesPrice de ServiceOrderDetailDistribution  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de detalle: 1=Servicios (procedimientos, atenciones), 2=Medicamentos (farmacia, insumos). TINYINT. Búsquedas: tipo servicio, medicamento, procedimiento, producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del producto: 1=No Quirúrgico, 2=Quirúrgico, 3=Paquete (combo). TINYINT, nullable. Búsquedas: presentación, formato, quirúrgico, paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento aplicado al tercero responsable (EAPB, asegurador, folio). NUMERIC(20,2), default 0. Búsquedas: descuento tercero, rebaja asegurador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario neto cobrado a EAPB/asegurador = SubTotalSalesPrice - ThirdPartyDiscount. NUMERIC(20,2), default 0. Búsquedas: valor neto, precio final', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada del producto/servicio; para medicamentos: diferencia entre suministrado y devoluciones. INT, default 0. Búsquedas: cantidad, unidades, devolución', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se prestó/dispensó el servicio o medicamento. DATETIME. Búsquedas: fecha prestación, fecha atención, fecha de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al detalle específico de la orden de servicio/medicamento. INT, FK a [Billing].[ServiceOrderDetail]. Búsquedas: orden de servicio, detalle orden', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al encabezado de la factura. INT, FK a [Billing].[Invoice]. Búsquedas: factura, número factura, cabecera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cabecera de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de factura. INT IDENTITY. Clave primaria. Búsquedas: detalle factura, línea factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de líneas de facturación: cada registro representa un servicio o procedimiento facturado dentro de una factura, con sus valores de venta, descuentos, cuotas moderadoras, distribución entre tercero pagador y paciente, impuestos y saldos pendientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetail';
