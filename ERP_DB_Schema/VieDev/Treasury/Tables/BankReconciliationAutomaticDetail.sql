CREATE TABLE [Treasury].[BankReconciliationAutomaticDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankReconciliationAutomaticId]       INT             NOT NULL,
    [DocumentType]                        TINYINT         NOT NULL,
    [Nature]                              TINYINT         NOT NULL,
    [Value]                               DECIMAL (18, 2) NOT NULL,
    [EntityId]                            INT             NOT NULL,
    [EntityCode]                          VARCHAR (20)    NOT NULL,
    [EntityName]                          VARCHAR (250)   NOT NULL,
    [Reconciled]                          BIT             NOT NULL,
    [Comments]                            VARCHAR (100)   NULL,
    [ReconciledStatus]                    TINYINT         NULL,
    [BankReconciliationAutomaticOriginId] INT             NULL,
    [DocumentDate]                        DATE            NOT NULL,
    CONSTRAINT [PK_BankReconciliationDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliationAutomaticDetail_BankReconciliationAutomatic] FOREIGN KEY ([BankReconciliationAutomaticId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id]),
    CONSTRAINT [FK_BankReconciliationAutomaticDetail_BankReconciliationAutomaticOrigin] FOREIGN KEY ([BankReconciliationAutomaticOriginId]) REFERENCES [Treasury].[BankReconciliationAutomatic] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliationAutomaticDetail] NOCHECK CONSTRAINT [FK_BankReconciliationAutomaticDetail_BankReconciliationAutomatic];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la conciliación bancaria original donde quedó pendiente por Conciliar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle en el proceso de Conciliación: 1. Pendiente por Conciliar.  2. Descartado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'ReconciledStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información actualizada para posteriormente realiza el cierre de la conciliación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro se encuentra o no en el extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del movimiento  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la conciliación bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticDetail', @level2type = N'COLUMN', @level2name = N'Id';

