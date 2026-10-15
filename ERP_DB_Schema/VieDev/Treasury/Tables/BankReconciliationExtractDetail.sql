CREATE TABLE [Treasury].[BankReconciliationExtractDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationId] INT             NOT NULL,
    [DocumentType]         TINYINT         NOT NULL,
    [DocumentDate]         DATETIME        NOT NULL,
    [Description]          VARCHAR (250)   NOT NULL,
    [DocumentNumber]       VARCHAR (50)    NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_BankReconciliationExtractDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationExtractDetail_BankReconciliation] FOREIGN KEY ([BankReconciliationId]) REFERENCES [Treasury].[BankReconciliation] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliationExtractDetail] NOCHECK CONSTRAINT [FK_BankReconciliationExtractDetail_BankReconciliation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del movimiento  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationExtractDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';

