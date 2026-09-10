CREATE TABLE [MedicalFees].[CausationRecognition] (
    [Id]                               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]                       INT          NOT NULL,
    [ThirdPartyId]                     INT          NOT NULL,
    [JournalVoucherId]                 INT          NULL,
    [JournalVoucherTypeId]             INT          NOT NULL,
    [JournalVoucherConsecutive]        BIGINT       NULL,
    [OperativeUnitId]                  INT          NOT NULL,
    [TotalSupplier]                    DECIMAL (18) CONSTRAINT [DF_CausationRecognition_TotalSupplier] DEFAULT ((0)) NOT NULL,
    [VoucherDate]                      DATETIME     NOT NULL,
    [JournalVoucherReverseId]          INT          NULL,
    [JournalVoucherTypeReverseId]      INT          NULL,
    [JournalVoucherReverseConsecutive] BIGINT       NULL,
    [VoucherDateReverse]               DATETIME     NULL,
    [State]                            TINYINT      CONSTRAINT [DF_CausationRecognition_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL,
    CONSTRAINT [PK_CausationRecognition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CausationRecognition_JournalVouchers] FOREIGN KEY ([JournalVoucherId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVouchers_Reverse] FOREIGN KEY ([JournalVoucherReverseId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes_Reverse] FOREIGN KEY ([JournalVoucherTypeReverseId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CausationRecognition_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CausationRecognition_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, llave autonumérica (IDENTITY). Clave primaria de reconocimiento de causación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave autonumérica', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (médico, profesional de salud o agremiación). Referencia al tercero que genera honorarios o causaciones a reconocer', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor (médico o agremiación)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (FK Common.ThirdParty). Entidad asociada al proveedor para fines de facturación y contabilización', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado al proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable generado (FK GeneralLedger.JournalVouchers). Puede ser NULL si no se ha creado aún', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable generado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (FK GeneralLedger.JournalVoucherTypes). Define el tipo de movimiento contable (acreedor, débito, etc.)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo único del comprobante contable generado. Puede ser NULL si el comprobante no existe', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del comprobante contable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa o centro de atención (FK Common.OperatingUnit). Agrupa la causación por sedes o unidades funcionales', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total en decimales (18,0) del proveedor a reconocer mediante comprobante. Valor por defecto 0. Unidad monetaria según contabilidad', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'TotalSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del proveedor por el que se realiza el comprobante contable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'TotalSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'TotalSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del reconocimiento de causación (DATETIME). Marca el momento en que se genera la obligación contable con el proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable de reversión (FK GeneralLedger.JournalVouchers). NULL si no ha sido reversado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante contable generado de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante de reversión (FK GeneralLedger.JournalVoucherTypes). NULL si no hay reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante contable de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante de reversión generado. NULL si no existe reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del comprobante contable generado de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la reversión del reconocimiento (DATETIME). NULL si aún no ha sido reversado. Anula la causación original', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del comprobante de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del reconocimiento (TINYINT): 1=Reconocido (vigente), 2=Reversado (anulado). Por defecto 1', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Reconocido, 2 - Reversado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario (VARCHAR 20) que creó el registro del reconocimiento. Auditoría de quién generó la causación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro. Marca cuándo se registró el reconocimiento en el sistema', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se generó el reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognition_Supplier_OperativeUnit]
    ON [MedicalFees].[CausationRecognition]([SupplierId] ASC, [OperativeUnitId] ASC, [State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que almacena la trazabilidad completa de reconocimientos de causaciones de honorarios médicos agrupados por proveedor (médico, profesional de salud o agremiación), incluyendo comprobantes contables generados y reversiones, vinculados a unidades operativas y terceros', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que almacena la trazabilidad de reconocimientos de causaciones de honorarios médicos agrupados por proveedor (médico o agremiación)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition';

