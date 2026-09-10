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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del documento origen del movimiento (INT): 1=Recibo de caja, 2=Comprobante de egreso, 3=Notas crédito/débito, 4=Consignaciones. Vincula transacción con comprobante contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documento: 1. Recibo de caja. 2. Comprobante de egreso. 3. Notas. 4. Consignaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda referencia de pago o complemento de radicado (VARCHAR 15). Referencia adicional del movimiento para búsqueda cruzada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia de pago 2', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera referencia de pago o número de radicado (VARCHAR 15). Identificador secundario de la transacción para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia de pago 1', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'PaymentReferenceOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cheque bancario (BIGINT). Identificador único del cheque si la transacción corresponde a instrumento de pago por cheque.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cheque bancario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'BankCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del crédito en moneda (DECIMAL 18,2). Monto sumado a la cuenta; ingreso, depósito o consignación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueCredit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueCredit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueCredit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del débito en moneda (DECIMAL 18,2). Monto restado de la cuenta; egreso, pago o retiro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueDebit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Débito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueDebit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ValueDebit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del movimiento bancario (VARCHAR 60). Concepto o glosa de la transacción registrada por el banco.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de transacción bancaria (VARCHAR 60, requerido). Clasificación de movimiento: transferencia, depósito, retiro, comisión, etc.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo o referencia única del banco (VARCHAR 60). Identificador asignado por la entidad financiera al movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del banco', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'ConsecutiveBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la transacción bancaria (DATETIME). Momento en que el banco registró el movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Transacción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de carga bancaria. Referencia FK a [Treasury].[UploadBankStatements]. Vincula línea con lote de importación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'UploadBankStatementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de transacción bancaria (IDENTITY INT). Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los movimientos (líneas) de extractos bancarios cargados al sistema de tesorería. Cada registro representa una transacción individual de débito o crédito extraída de un archivo bancario, asociada a un encabezado de carga.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'UploadBankStatementsDetail';
