CREATE TABLE [Taxes].[TaxesPropertyOwner] (
    [Id]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesPropertyId] INT            NOT NULL,
    [ThirdPartyId]    INT            NOT NULL,
    [OrderOwner]      INT            NOT NULL,
    [Percentage]      NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_TaxesPropertyOwner] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesPropertyOwner_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesPropertyOwner_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_TaxesPropertyOwner__TaxesPropertyId__ThirdPartyId] UNIQUE NONCLUSTERED ([TaxesPropertyId] ASC, [ThirdPartyId] ASC)
);


GO
ALTER TABLE [Taxes].[TaxesPropertyOwner] NOCHECK CONSTRAINT [FK_TaxesPropertyOwner_TaxesProperty];


GO
ALTER TABLE [Taxes].[TaxesPropertyOwner] NOCHECK CONSTRAINT [FK_TaxesPropertyOwner_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_TaxesPropertyOwner__ThirdPartyId]
    ON [Taxes].[TaxesPropertyOwner]([ThirdPartyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesPropertyOwner__TaxesPropertyId]
    ON [Taxes].[TaxesPropertyOwner]([TaxesPropertyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de propiedad del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden del propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'OrderOwner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyOwner', @level2type = N'COLUMN', @level2name = N'Id';

