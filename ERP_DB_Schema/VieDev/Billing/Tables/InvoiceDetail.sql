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

