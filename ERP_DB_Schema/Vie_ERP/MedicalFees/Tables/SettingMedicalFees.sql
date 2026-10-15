CREATE TABLE [MedicalFees].[SettingMedicalFees] (
    [Id]                               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableConceptId]          INT          NOT NULL,
    [JournalVoucherTypeId]             INT          NOT NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL,
    [ModificationUser]                 VARCHAR (20) NULL,
    [ModificationDate]                 DATETIME     NULL,
    [CostRecognitionVoucherId]         INT          NULL,
    [CostRecognitionReversalVoucherId] INT          NULL,
    CONSTRAINT [PK_SettingMedicalFees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingMedicalFees_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingMedicalFees_CostRecognitionReversalVoucher_JournalVoucherTypes] FOREIGN KEY ([CostRecognitionReversalVoucherId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingMedicalFees_CostRecognitionVoucher_JournalVoucherTypes] FOREIGN KEY ([CostRecognitionVoucherId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingMedicalFees_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_AccountPayableConcepts];


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_CostRecognitionReversalVoucher_JournalVoucherTypes];


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_CostRecognitionVoucher_JournalVoucherTypes];


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_JournalVoucherTypes];




GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_AccountPayableConcepts];


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_JournalVoucherTypes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creacion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion de registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo del comprobante contable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuenta por pagar, para cuando se generen las cuentas por pagar en liquidacion de honorarios medicos, el concepto debe de ser de tipo general y NO debe manejar retencion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los parametros', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de comprobantes de reconocimiento de costos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'CostRecognitionVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de comprobantes de reversion de reconocimiento de costos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'SettingMedicalFees', @level2type = N'COLUMN', @level2name = N'CostRecognitionReversalVoucherId';

