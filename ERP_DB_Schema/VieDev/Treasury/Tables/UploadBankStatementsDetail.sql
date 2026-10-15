CREATE TABLE [Treasury].[UploadBankStatementsDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UploadBankStatementsId] INT             NOT NULL,
    [TransactionDate]        DATETIME        NOT NULL,
    [ConsecutiveBank]        VARCHAR (60)    NULL,
    [TransactionCode]        VARCHAR (60)    NOT NULL,
    [DescriptionTransaction] VARCHAR (60)    NULL,
    [ValueDebit]             DECIMAL (18, 2) NULL,
    [ValueCredit]            DECIMAL (18, 2) NULL,
    [BankCheck]              BIGINT          NULL,
    [PaymentReferenceOne]    VARCHAR (15)    NULL,
    [PaymentReferenceTwo]    VARCHAR (15)    NULL,
    [DocumentType]           INT             NULL,
    CONSTRAINT [PK_UploadBankStatementsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Treasury_UploadBankStatements] FOREIGN KEY ([UploadBankStatementsId]) REFERENCES [Treasury].[UploadBankStatements] ([Id])
);


GO
ALTER TABLE [Treasury].[UploadBankStatementsDetail] NOCHECK CONSTRAINT [FK_Treasury_UploadBankStatements];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento: 1. Recibo de caja. 2. Comprobante de egreso. 3. Notas. 4. Consignaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia de pago 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia de pago 1', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cheque bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueCredit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Débito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueDebit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'Id';

