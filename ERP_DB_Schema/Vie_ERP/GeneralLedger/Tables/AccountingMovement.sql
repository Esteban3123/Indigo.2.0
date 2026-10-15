CREATE TABLE [GeneralLedger].[AccountingMovement] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalBookId]          INT           NOT NULL,
    [JournalVoucherTypeId] INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Detail]               VARCHAR (500) NOT NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [JournalVoucherXml]    XML           NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    CONSTRAINT [PK_AccountingMovement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingMovement_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_AccountingMovement_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_JournalVoucherTypes];


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_LegalBook];


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AccountingMovement_NoDup]
    ON [GeneralLedger].[AccountingMovement]([EntityCode] ASC, [EntityId] ASC, [EntityName] ASC, [JournalVoucherTypeId] ASC, [LegalBookId] ASC, [VoucherDate] ASC) WHERE ([EntityCode] IS NOT NULL AND [EntityCode]<>'' AND [EntityId] IS NOT NULL AND [EntityId]<>(0) AND [ID]>=(3731314));


GO
CREATE NONCLUSTERED INDEX [IX_AccountingMovement_EntityName_EntityId]
    ON [GeneralLedger].[AccountingMovement]([EntityName] ASC, [EntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del sistema en la que se creo el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'XML que genera el comprobante contable, este solo se guarda cuando el movimiento contable no a afectado ningun libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherXml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad que genera el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad que genera el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la entidad que genero el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante que se envio inicialmente en el XMl', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del libro donde se quiere realizar el comprobante inicial, es decir el que envian por primera vez en el XML del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Id';

