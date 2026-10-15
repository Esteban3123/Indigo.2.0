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
    CONSTRAINT [FK_CausationRecognition_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVouchers] FOREIGN KEY ([JournalVoucherId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVouchers_Reverse] FOREIGN KEY ([JournalVoucherReverseId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes_Reverse] FOREIGN KEY ([JournalVoucherTypeReverseId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CausationRecognition_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_ThirdParty];


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_JournalVouchers];


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_JournalVouchers_Reverse];


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes];


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_JournalVoucherTypes_Reverse];


GO
ALTER TABLE [MedicalFees].[CausationRecognition] NOCHECK CONSTRAINT [FK_CausationRecognition_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave autonumérica', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor (médico o agremiación)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero asociado al proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante contable generado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del comprobante contable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total del proveedor por el que se realiza el comprobante contable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'TotalSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante contable generado de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante contable de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del comprobante contable generado de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del comprobante de la reversión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 - Reconocido, 2 - Reversado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creó', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se generó el reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognition_Supplier_OperativeUnit]
    ON [MedicalFees].[CausationRecognition]([SupplierId] ASC, [OperativeUnitId] ASC, [State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que almacena la trazabilidad de reconocimientos de causaciones de honorarios médicos agrupados por proveedor (médico o agremiación)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognition';

