CREATE TABLE [GeneralLedger].[JournalVouchersRetry] (
    [Id]                   INT           NOT NULL,
    [AccountingMovementId] INT           NOT NULL,
    [Consecutive]          BIGINT        NOT NULL,
    [LegalBookId]          INT           NOT NULL,
    [IdJournalVoucher]     INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Imported]             BIT           NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [Detail]               VARCHAR (MAX) NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [IsClosedYear]         TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [YearMovement]         INT           NOT NULL,
    CONSTRAINT [PK_JournalVouchersRetry__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año del movimiento contable; período fiscal o ejercicio contable en el cual se registra la transacción (INT, filtrable por período)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'YearMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del movimiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'YearMovement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'YearMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP); registra automáticamente el instante exacto de creación, modificación o cambio de estado del comprobante en BD', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de anulación del comprobante contable (DATETIME); momento en que se revoca o cancela la validez del asiento contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de anulación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la anulación (VARCHAR 20); identificación del operario o profesional contable que anuló el comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que anulo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación del comprobante (DATETIME); momento en que se valida y acepta definitivamente el asiento contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el comprobante (VARCHAR 20); identificación del revisor o aprobador que validó el asiento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que confirmo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación del registro (DATETIME); último instante en que se editaron datos del comprobante o reintentos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 20); identificación del operario que editó el comprobante o reintentó la importación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro (DATETIME); momento inicial de registro del comprobante en tabla Retry', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación del registro (VARCHAR 20); identificación del operario o sistema que originó el comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de año cerrado (TINYINT, 0/1); señala si el período fiscal del movimiento está bloqueado para cambios contables', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IsClosedYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año cerrado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IsClosedYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IsClosedYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad, tercero o razón social (VARCHAR 250); denominación comercial del proveedor, paciente, centro de atención o entidad vinculada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad, tercero o cliente (INT); clave foránea que referencia la entidad de negocio (proveedor, paciente, IPS)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad, tercero o centro (VARCHAR 20); código alfanumérico único del tercero, centro de atención o institución vinculada al comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalles del comprobante o motivo de reintento (VARCHAR MAX); información descriptiva, observaciones o causas de fallo en importación contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalles', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del comprobante contable (TINYINT); código que indica: pendiente (0), importado (1), confirmado (2), anulado (3), error en reintento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de importación exitosa (BIT, 0/1); marca si el comprobante fue importado correctamente (1=Sí) o requiere reintento (0=No)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Imported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Importado | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Imported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Imported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable (DATETIME); fecha de emisión o validez contable del asiento, diferente de la fecha de registro en sistema', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable (INT); clave primaria de la transacción o asiento en diario general o libro contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'IdJournalVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro contable o diario (INT); referencia a libro de registros contables (diario general, auxiliar, RIPS, etc.)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante (BIGINT); secuencial único que numeración legal del asiento dentro del período contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del movimiento contable (INT); clave de la transacción u operación contable que genera este comprobante', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AccountingMovementId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del movimiento contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AccountingMovementId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'AccountingMovementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla (INT); clave primaria que identifica cada registro de reintento de comprobante en tabla JournalVouchersRetry', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de comprobantes contables (vouchers) que fallaron al importarse al libro mayor y están pendientes de reintento. Lleva el seguimiento del estado, resultado y auditoría de cada intento de contabilización fallido o pendiente.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchersRetry';
