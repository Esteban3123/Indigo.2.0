CREATE TABLE [Glosas].[TransferJuridicalDebtCollectionD] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferJuridicalDebtCollectionCId] INT          NOT NULL,
    [PortfolioGlosaId]                   INT          NULL,
    [AccountReceivableId]                INT          NULL,
    [LegalTransferValue]                 MONEY        CONSTRAINT [DF__TransferJ__Legal__385FCE4B] DEFAULT ((0)) NULL,
    [InvoiceNumber]                      VARCHAR (50) NOT NULL,
    [AccountReceivableDate]              DATETIME     NULL,
    [TimeStamp]                          ROWVERSION   NOT NULL,
    [ReversedUser]                       VARCHAR (20) NULL,
    [ReversedDate]                       DATETIME     NULL,
    CONSTRAINT [PK_TransferJuridicalDebtCollectionD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_GlosaObjectionsReceptionC] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_TransferJuridicalDebtCollectionC] FOREIGN KEY ([TransferJuridicalDebtCollectionCId]) REFERENCES [Glosas].[TransferJuridicalDebtCollectionC] ([Id])
);


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [AccountReceivable];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionD_GlosaObjectionsReceptionC];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionD_TransferJuridicalDebtCollectionC];


GO
CREATE NONCLUSTERED INDEX [IX_TransferJuridicalDebtCollectionD_InvoiceNumber]
    ON [Glosas].[TransferJuridicalDebtCollectionD]([InvoiceNumber] ASC)
    INCLUDE([ReversedUser], [TransferJuridicalDebtCollectionCId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'LegalTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion de cartera de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'PortfolioGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion de cabecera del oficio de traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico del detalle de facturas a traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionD', @level2type = N'COLUMN', @level2name = N'Id';

