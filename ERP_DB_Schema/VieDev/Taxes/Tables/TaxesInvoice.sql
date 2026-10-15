CREATE TABLE [Taxes].[TaxesInvoice] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Validity]                  INT           NOT NULL,
    [InvoiceNumber]             VARCHAR (100) NOT NULL,
    [TaxesConsecutiveBillingId] INT           NOT NULL,
    [TaxesPropertyId]           INT           NOT NULL,
    [ThirdPartyId]              INT           NOT NULL,
    [Appraisal]                 DECIMAL (18)  NOT NULL,
    [TaxValue]                  DECIMAL (18)  NOT NULL,
    [State]                     TINYINT       NOT NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [AnnulmentUser]             VARCHAR (20)  NULL,
    [AnnulmentDate]             DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TaxesInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesInvoice_TaxesConsecutiveBilling] FOREIGN KEY ([TaxesConsecutiveBillingId]) REFERENCES [Taxes].[TaxesConsecutiveBilling] ([Id]),
    CONSTRAINT [FK_TaxesInvoice_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesInvoice_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesInvoice] NOCHECK CONSTRAINT [FK_TaxesInvoice_TaxesConsecutiveBilling];


GO
ALTER TABLE [Taxes].[TaxesInvoice] NOCHECK CONSTRAINT [FK_TaxesInvoice_TaxesProperty];


GO
ALTER TABLE [Taxes].[TaxesInvoice] NOCHECK CONSTRAINT [FK_TaxesInvoice_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la factura  1 - Activa  2 - Anulada', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'State';

