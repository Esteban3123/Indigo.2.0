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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de partida pendiente en conciliación bancaria: 1=Nota de gastos bancarios (comisiones, intereses), 2=Terceros pendientes por identificar. Tipo INT, clave para categorizar ítems no reconciliados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'TypeOfItemPendingInReconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de partida pendiente en conciliación: 1. Nota de gastos bancarios., 2. Terceros pendientes por identificar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'TypeOfItemPendingInReconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'TypeOfItemPendingInReconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de Costos (FK a CostCenter) asociado a la cuenta contable y regla de reconocimiento. Permite asignación de gastos bancarios a unidades funcionales, departamentos u áreas operativas. Nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Postular los Centros de Costos existentes y relacionados con la cuenta contable del campo anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal (FK a GeneralLedger.MainAccounts) vinculada al concepto de nota seleccionado. Define cómo se registra contablemente el movimiento bancario en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'MainAccountsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable del concepto de nota seleccionado en el campo anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'MainAccountsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'MainAccountsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nota (FK a Treasury.NoteConcepts) del módulo de Administración de Efectivo. Clasifica el tipo de movimiento: gastos bancarios, ajustes, terceros, etc.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de conceptos de Notas del módulo de Administración de efectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'NoteConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la transacción bancaria o regla de reconocimiento. Texto de hasta 80 caracteres que identifica el movimiento en reportes y conciliación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la transacción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'DescriptionTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la regla de reconocimiento automático. VARCHAR(4), identificador corto para búsqueda y referencia rápida en procesos de conciliación. Nullable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de las reglas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco (FK a Payroll.Bank). Define a cuál entidad bancaria aplica esta regla de reconocimiento automático en conciliación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY 1,1) de la regla de reconocimiento bancario. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reglas automáticas de reconocimiento bancario para la conciliación de nómina: define qué código o descripción de transacción bancaria corresponde a qué concepto contable, cuenta principal y centro de costo, permitiendo clasificar automáticamente los movimientos bancarios durante el proceso de conciliación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankAutomaticRecognitionRules';
