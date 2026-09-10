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
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_LegalBook];


GO
ALTER TABLE [GeneralLedger].[AccountingMovement] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);




GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_LegalBook];





GO



GO
ALTER TABLE [GeneralLedger].[AccountingMovement] NOCHECK CONSTRAINT [FK_AccountingMovement_LegalBook];


GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_AccountingMovement_NoDup]
    ON [GeneralLedger].[AccountingMovement]([EntityCode] ASC, [EntityId] ASC, [EntityName] ASC, [JournalVoucherTypeId] ASC, [LegalBookId] ASC, [VoucherDate] ASC) WHERE ([EntityCode] IS NOT NULL AND [EntityCode]<>'' AND [EntityId] IS NOT NULL AND [EntityId]<>(0) AND [ID]>=(3731314));


GO
CREATE NONCLUSTERED INDEX [IX_AccountingMovement_EntityName_EntityId]
    ON [GeneralLedger].[AccountingMovement]([EntityName] ASC, [EntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del sistema en que se creó el registro de movimiento contable (DATETIME, auditoría de creación)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del sistema en la que se creo el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login que generó el registro de movimiento contable (VARCHAR 20, trazabilidad, PII)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'XML del comprobante contable generado; se almacena solo cuando el movimiento no ha afectado ningún libro contable legal (XML, documento fuente, esquema de comprobante)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherXml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'XML que genera el comprobante contable, este solo se guarda cuando el movimiento contable no a afectado ningun libro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherXml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherXml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad/empresa que genera el comprobante contable (VARCHAR 250, razón social, identificación del originante)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad que genera el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entidad que genera el comprobante contable (INT, clave de entidad, FK a tabla de entidades)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad que genera el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico de la entidad generadora del comprobante contable (VARCHAR 20, NIT/RUT, identificación comercial)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad que genero el comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción/detalle del comprobante contable, concepto del movimiento (VARCHAR 500, glosa contable, motivo del asiento)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable (DATETIME, fecha contable, fecha de operación, fecha del documento)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable enviado inicialmente en el XML (INT, FK a JournalVoucherTypes, clase de documento contable)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante que se envio inicialmente en el XMl', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro contable legal donde se registra el comprobante inicial (INT, FK a LegalBook, libro mayor/diario, destino contable)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro donde se quiere realizar el comprobante inicial, es decir el que envian por primera vez en el XML del comprobante contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del movimiento contable (INT IDENTITY, clave primaria, consecutivo del registro)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos contables del libro mayor: registra cada asiento o comprobante contable con su fecha, detalle narrativo, tipo de voucher, entidad relacionada (paciente, proveedor, contrato, etc.) y el XML completo del comprobante. Es el corazón del módulo de contabilidad donde quedan asentadas todas las transacciones financieras.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'AccountingMovement';
