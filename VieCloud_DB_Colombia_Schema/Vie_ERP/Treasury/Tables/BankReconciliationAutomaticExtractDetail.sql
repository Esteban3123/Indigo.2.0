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
    [UploadBankStatementsDetailId]        INT             NULL,
    [DocumentType]                        INT             NULL,
    [ConsecutiveBank]                     VARCHAR (100)   NULL,
    [CodeNoteReconciled]                  VARCHAR (MAX)   NULL,
    [BankReconciliationAutomaticOriginId] INT             NULL,
    [IsInitialBalance]                    BIT             CONSTRAINT [DF_BankReconciliationAutomaticExtractDetail_IsInitialBalance] DEFAULT ((0)) NOT NULL,
    [TransactionCode]                     VARCHAR (20)    NULL,
    [DescriptionTransaction]              VARCHAR (200)    NULL,
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la conciliación bancaria original pendiente (FK a BankReconciliationAutomatic); trazabilidad de movimientos en conciliaciones previas no resuelta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la conciliación bancaria original donde quedó pendiente por Conciliar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticOriginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o referencia del documento conciliado (VARCHAR MAX); nota o comprobante interno que respalda la conciliación del detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'CodeNoteReconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Documento conciliado con el detalle del extracto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'CodeNoteReconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'CodeNoteReconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del extracto bancario (VARCHAR 100); número secuencial asignado por la entidad financiera al movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (INT): 1=Recibo de caja, 2=Comprobante de egreso, 3=Notas, 4=Consignaciones; clasificación del movimiento en tesorería', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del documento   1 - Recibo de caja  2 - Comprobante de egreso  3 - Notas  4 - Consignaciones', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del extracto cargado (FK nullable a UploadBankStatementsDetail); vincula al archivo de estado de cuenta original. Es NULL para partidas de saldo inicial importadas por Excel, que rompen intencionalmente esta relación de origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del extracto ubicado en el cargue', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de reconciliación (BIT, default 0); bandera que marca si el detalle fue conciliado exitosamente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reconciliado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Reconciled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento (TINYINT): 1=Débito (salida), 2=Crédito (entrada); tipo de operación bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nature 1 - Debito 2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del movimiento (DECIMAL 18,2); monto en pesos de la transacción bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda referencia de pago (VARCHAR 60); referencia adicional del tercero o número de documento asociado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia de pago 1 que referencia el tercero o num documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera referencia de pago (VARCHAR 60); identifica tercero, acreedor o beneficiario del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia de pago 1 que referencia el tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cheque (BIGINT); identificador de pago por cheque si aplica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cheque', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del documento bancario; fecha de transacción en el extracto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la conciliación bancaria automática relacionada (FK a BankReconciliationAutomatic); referencia principal del proceso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la conciliación bancaria automatica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'BankReconciliationAutomaticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle de extracción en reconciliación bancaria automática', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default 0) que indica si el detalle corresponde a una partida de saldo inicial importada desde Excel, sin un UploadBankStatementsDetail de origen, en lugar de una línea proveniente de un cargue real de extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'IsInitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la partida es un saldo inicial importado manualmente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'IsInitialBalance';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de transacción del extracto (VARCHAR 60). Para partidas de saldo inicial (IsInitialBalance = 1) se diligencia directamente aquí, ya que no existe UploadBankStatementsDetail de origen del cual tomarlo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la transacción del extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la transacción del extracto (VARCHAR 60). Para partidas de saldo inicial (IsInitialBalance = 1) se diligencia directamente aquí, ya que no existe UploadBankStatementsDetail de origen del cual tomarla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la transacción del extracto bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada movimiento bancario procesado en la conciliación automática: registra las líneas individuales del extracto bancario, con su valor, referencia de pago y estado de conciliación frente a los registros contables.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliationAutomaticExtractDetail';
