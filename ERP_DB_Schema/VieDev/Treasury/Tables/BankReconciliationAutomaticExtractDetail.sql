CREATE TABLE [Treasury].[BankReconciliationAutomaticExtractDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationAutomaticId]       INT             NOT NULL,
    [DocumentDate]                        DATETIME        NOT NULL,
    [BankCheck]                           BIGINT          NULL,
    [PaymentReferenceOne]                 VARCHAR (60)    NULL,
    [PaymentReferenceTwo]                 VARCHAR (60)    NULL,
    [Value]                               DECIMAL (18, 2) NOT NULL,
    [Nature]                              TINYINT         NULL,
    [Reconciled]                          BIT             CONSTRAINT [DF__BankRecon__Recon__7BD517A1] DEFAULT ((0)) NOT NULL,
    [UploadBankStatementsDetailId]        INT             NOT NULL,
    [DocumentType]                        INT             NULL,
    [ConsecutiveBank]                     VARCHAR (100)   NULL,
    [CodeNoteReconciled]                  VARCHAR (MAX)   NULL,
    [BankReconciliationAutomaticOriginId] INT             NULL,
    CONSTRAINT [PK_BankReconciliationExtractDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationAutomaticExtractDetail_BankReconciliationAutomatic] FOREIGN KEY ([BankReconciliationAutomaticId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticExtractDetail_BankReconciliationAutomaticOrigin] FOREIGN KEY ([BankReconciliationAutomaticOriginId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticExtractDetail_UploadBankStatementsDetail] FOREIGN KEY ([UploadBankStatementsDetailId]) REFERENCES [Treasury].[UploadBankStatementsDetail] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliationAutomaticExtractDetail] NOCHECK CONSTRAINT [FK_BankReconciliationAutomaticExtractDetail_BankReconciliationAutomatic];


GO
ALTER TABLE [Treasury].[BankReconciliationAutomaticExtractDetail] NOCHECK CONSTRAINT [FK_BankReconciliationAutomaticExtractDetail_UploadBankStatementsDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la conciliación bancaria original donde quedó pendiente por Conciliar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Documento conciliado con el detalle del extracto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'CodeNoteReconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del extracto ubicado en el cargue', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reconciliado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nature 1 - Debito 2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia de pago 1 que referencia el tercero o num documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia de pago 1 que referencia el tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la conciliación bancaria automatica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Id';

