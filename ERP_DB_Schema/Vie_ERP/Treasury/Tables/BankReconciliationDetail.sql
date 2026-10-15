CREATE TABLE [Treasury].[BankReconciliationDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationId] INT             NOT NULL,
    [DocumentType]         TINYINT         NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [EntityId]             INT             NOT NULL,
    [EntityCode]           VARCHAR (20)    NOT NULL,
    [EntityName]           VARCHAR (250)   NOT NULL,
    [Reconciled]           BIT             NOT NULL,
    CONSTRAINT [PK_BankReconciliationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationDetail_BankReconciliation] FOREIGN KEY ([BankReconciliationId]) REFERENCES [Treasury].[BankReconciliation] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliationDetail] NOCHECK CONSTRAINT [FK_BankReconciliationDetail_BankReconciliation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro se encuentra o no en el extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del movimiento  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la conciliación bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';

