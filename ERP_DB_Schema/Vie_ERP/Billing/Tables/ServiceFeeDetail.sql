CREATE TABLE [Billing].[ServiceFeeDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [ProductAndServiceFeeId] INT             NOT NULL,
    [ServiceId]              INT             NOT NULL,
    [Observations]           VARCHAR (MAX)   NOT NULL,
    [InitialDate]            DATETIME        NOT NULL,
    [FinalDate]              DATETIME        NOT NULL,
    [SalePrice]              NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ServiceFeeDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceFeeDetail_BillingConcept] FOREIGN KEY ([ServiceId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_ServiceFeeDetail_ProductAndServiceFee] FOREIGN KEY ([ProductAndServiceFeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceFeeDetail] NOCHECK CONSTRAINT [FK_ServiceFeeDetail_BillingConcept];


GO
ALTER TABLE [Billing].[ServiceFeeDetail] NOCHECK CONSTRAINT [FK_ServiceFeeDetail_ProductAndServiceFee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha limite final ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha limite inicial ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del detalle', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'ServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cabecera - Tarifa de productos y servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceFeeDetail', @level2type = N'COLUMN', @level2name = N'Id';

