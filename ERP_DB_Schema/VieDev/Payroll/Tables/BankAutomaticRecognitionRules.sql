CREATE TABLE [Payroll].[BankAutomaticRecognitionRules] (
    [Id]                                INT          IDENTITY (1, 1) NOT NULL,
    [BankId]                            INT          NOT NULL,
    [Code]                              VARCHAR (4)  NULL,
    [DescriptionTransaction]            VARCHAR (80) NOT NULL,
    [NoteConceptsId]                    INT          NOT NULL,
    [MainAccountsId]                    INT          NOT NULL,
    [CostCenterId]                      INT          NULL,
    [TypeOfItemPendingInReconciliation] INT          NOT NULL,
    CONSTRAINT [PK_BankAutomaticRecognitionRules] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankId_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_CostCenterId_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_MainAccountsId_MainAccounts] FOREIGN KEY ([MainAccountsId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_NoteConceptsId_NoteConcepts] FOREIGN KEY ([NoteConceptsId]) REFERENCES [Treasury].[NoteConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[BankAutomaticRecognitionRules] NOCHECK CONSTRAINT [FK_BankId_Bank];


GO
ALTER TABLE [Payroll].[BankAutomaticRecognitionRules] NOCHECK CONSTRAINT [FK_CostCenterId_CostCenter];


GO
ALTER TABLE [Payroll].[BankAutomaticRecognitionRules] NOCHECK CONSTRAINT [FK_MainAccountsId_MainAccounts];


GO
ALTER TABLE [Payroll].[BankAutomaticRecognitionRules] NOCHECK CONSTRAINT [FK_NoteConceptsId_NoteConcepts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de partida pendiente en conciliación: 1. Nota de gastos bancarios., 2. Terceros pendientes por identificar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'TypeOfItemPendingInReconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Postular los Centros de Costos existentes y relacionados con la cuenta contable del campo anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del concepto de nota seleccionado en el campo anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de conceptos de Notas del módulo de Administración de efectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la transacción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de las reglas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del banco', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Id';

