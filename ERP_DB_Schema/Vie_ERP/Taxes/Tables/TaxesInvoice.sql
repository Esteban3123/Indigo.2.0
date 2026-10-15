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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TaxesInvoice__InvoiceNumber]
    ON [Taxes].[TaxesInvoice]([InvoiceNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TaxesInvoice__Validity__INC__Id__TaxesPropertyId__ThirdPartyId]
    ON [Taxes].[TaxesInvoice]([Validity] ASC)
    INCLUDE([Id], [TaxesPropertyId], [ThirdPartyId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la factura  1 - Activa  2 - Anulada', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especificael Valor del Impuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Avaluo del Predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero propietario', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el consecutivo de facturacion', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'TaxesConsecutiveBillingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de la factura', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Vigencia de la factura', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesInvoice', @level2type = N'COLUMN', @level2name = N'Id';

