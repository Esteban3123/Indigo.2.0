CREATE TABLE [Billing].[RevenueRecognitionTmp] (
    [Id]                               INT          IDENTITY (1, 1) NOT NULL,
    [CareGroupId]                      INT          NOT NULL,
    [JournalVoucherId]                 INT          NULL,
    [JournalVoucherTypeId]             INT          NOT NULL,
    [JournalVoucherConsecutive]        BIGINT       NULL,
    [OperativeUnitId]                  INT          NOT NULL,
    [TotalCareGroup]                   DECIMAL (18) NOT NULL,
    [VoucherDate]                      DATETIME     NOT NULL,
    [JournalVoucherReverseId]          INT          NULL,
    [JournalVoucherTypeReverseId]      INT          NULL,
    [JournalVoucherReverseConsecutive] BIGINT       NULL,
    [VoucherDateReverse]               DATETIME     NULL,
    [State]                            TINYINT      NOT NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de reconocimiento de ingresos (DATETIME, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro, identificación del operario contable (VARCHAR 20, PII)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del reconocimiento de ingresos: activo, reversado, pendiente (TINYINT, validar contra tabla Estados)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado en el que se encuentra el reconocimieno de entrada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable reversado, anulación o rectificación de asiento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del comprobante reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante contable reversado, secuencia de auditoría (BIGINT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el consecutivo del conprobante contable reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable reversado (FK a JournalVoucherTypes, INT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante contable reversado. Se obtienen de la tabla JournalVoucherTypes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable reversado, anulación de asiento (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del conprobante contable reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable original, asiento contable de ingresos (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del comprobante.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total del grupo de atención reconocido, valor de facturación e ingresos (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el grupo de atención total.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa, centro de atención o consultorio (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante contable, secuencia para auditoría (BIGINT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el consecutivo del comprobante contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable, clasificación del asiento (FK a JournalVoucherTypes, INT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante contable. Se obtienen de la tabla JournalVoucherTypes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable, asiento contable de reconocimiento de ingresos (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención, facturación o ingreso del paciente (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, identificador único del registro temporal de reconocimiento (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla temporal de reconocimiento de ingresos en facturación. Registra los comprobantes contables (vouchers) generados y sus reversiones para grupos de atención, permitiendo el proceso de reconocimiento de ingresos por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionTmp';
