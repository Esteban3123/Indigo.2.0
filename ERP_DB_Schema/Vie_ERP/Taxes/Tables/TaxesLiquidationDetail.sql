CREATE TABLE [Taxes].[TaxesLiquidationDetail] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesLiquidationId] INT            NOT NULL,
    [TaxesPropertyId]    INT            NOT NULL,
    [Appraisal]          DECIMAL (18)   NOT NULL,
    [ThirdPartyId]       INT            NOT NULL,
    [RateOwner]          DECIMAL (5, 2) NOT NULL,
    [PercentageOwner]    DECIMAL (5, 2) NOT NULL,
    [TotalValueTax]      DECIMAL (18)   NOT NULL,
    [ValueOwner]         DECIMAL (18)   NOT NULL,
    [TaxesInvoiceId]     INT            NULL,
    CONSTRAINT [PK_TaxesLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesInvoice] FOREIGN KEY ([TaxesInvoiceId]) REFERENCES [Taxes].[TaxesInvoice] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesLiquidation] FOREIGN KEY ([TaxesLiquidationId]) REFERENCES [Taxes].[TaxesLiquidation] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesInvoice];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesLiquidation];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesProperty];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesLiquidationId__TaxesPropertyId__INC__Id]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesLiquidationId] ASC, [TaxesPropertyId] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesLiquidationId__INC__Id__TaxesPropertyId__ThirdPartyId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesLiquidationId] ASC)
    INCLUDE([Id], [TaxesPropertyId], [ThirdPartyId]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesInvoiceId__INC__TaxesLiquidationId__TaxesPropertyId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesInvoiceId] ASC)
    INCLUDE([TaxesLiquidationId], [TaxesPropertyId]);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesLiquidationDetail__TaxesPropertyId__ThirdPartyId__INC__Id__TaxesInvoiceId]
    ON [Taxes].[TaxesLiquidationDetail]([TaxesPropertyId] ASC, [ThirdPartyId] ASC)
    INCLUDE([Id], [TaxesInvoiceId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de factura de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor que le corresponde pagar propietario por el impuesto, este valor esta asociado al porcentaje del propietario sobre el terreno', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValueOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor total del impuesto al predio sin importar el propietario o los propietarios que tenga', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'PercentageOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarifa Propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RateOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Avaluo del Predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID impuestos a la propiedad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de liquidación de impuestos', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TaxesLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';

