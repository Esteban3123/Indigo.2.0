CREATE TABLE [Billing].[RevenueRecognition] (
    [Id]                               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]                      INT          NOT NULL,
    [JournalVoucherId]                 INT          NULL,
    [JournalVoucherTypeId]             INT          NOT NULL,
    [JournalVoucherConsecutive]        BIGINT       NULL,
    [OperativeUnitId]                  INT          CONSTRAINT [DF_RevenueRecognition_OperativeUnitId] DEFAULT ((14)) NOT NULL,
    [TotalCareGroup]                   DECIMAL (18) CONSTRAINT [DF_RevenueRecognition_TotalCareGroup] DEFAULT ((0)) NOT NULL,
    [VoucherDate]                      DATETIME     NOT NULL,
    [JournalVoucherReverseId]          INT          NULL,
    [JournalVoucherTypeReverseId]      INT          NULL,
    [JournalVoucherReverseConsecutive] BIGINT       NULL,
    [VoucherDateReverse]               DATETIME     NULL,
    [State]                            TINYINT      CONSTRAINT [DF_RevenueRecognition_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL,
    CONSTRAINT [PK_RevenueRecognition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueRecognition_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVouchers] FOREIGN KEY ([JournalVoucherId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVouchers1] FOREIGN KEY ([JournalVoucherReverseId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeReverseId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de generación del reconocimiento de ingresos (DATETIME). Marca temporal de cuándo se creó el registro de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se generó el reconocimiento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login que creó el reconocimiento (VARCHAR 20). Identificador del profesional o sistema que generó la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del reconocimiento: 1=Reconocido, 2=Reversado (TINYINT). Indica si el comprobante está vigente o ha sido anulado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1 - Reconocido  2 - Reversado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable de reversión/anulación (DATETIME, nullable). Cuándo se realizó la contraparte contable que cancela la factura original.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha del co provande de la reversión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante contable de reversión (BIGINT, nullable). Identificador secuencial del asiento de contrapartida que anula el ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivode  comprobante contable generado de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de comprobante contable usado en la reversión (INT, FK a JournalVoucherTypes, nullable). Clasificación contable de la anulación (débito/crédito inverso).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante contable de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del comprobante contable generado por la reversión (INT, FK a JournalVouchers, nullable). Referencia al asiento contable que invierte la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable generado de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del comprobante contable original de reconocimiento (DATETIME). Cuándo se contabilizó el ingreso por facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de de la reversión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario total del comprobante contable (DECIMAL 18, default 0). Importe neto facturado por el grupo de atención/ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total por el que se realiza el comprobante contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa/centro de atención responsable (INT, FK a OperatingUnit, default 14). Identifica la sede, clínica o unidad funcional que genera el ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa a la que corresponde el grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante contable original (BIGINT, nullable). Identificador secuencial del asiento de facturación en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del comprobante contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de comprobante contable (INT, FK a JournalVoucherTypes). Clasificación del asiento (factura, nota crédito, glosa, etc).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del comprobante contable generado (INT, FK a JournalVouchers, nullable). Referencia al asiento contable de facturación en el módulo de contabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable que se genera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de atención/facturación (INT, FK a CareGroup). Agrupa servicios, atenciones, ingresos de un paciente para ser facturados conjuntamente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincremental (INT IDENTITY). Identificador único del registro de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'llave autonumérica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de reconocimiento de ingresos contables por grupo de atención. Vincula cada grupo de cuentas por cobrar con su comprobante contable (voucher) de causación y, cuando aplica, con el comprobante de reversión, permitiendo trazabilidad del ciclo contable de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition';
