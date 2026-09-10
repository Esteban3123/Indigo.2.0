CREATE TABLE [Taxes].[TaxesLiquidationConcept] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]            VARCHAR (20)  NOT NULL,
    [Name]            VARCHAR (200) NOT NULL,
    [ConceptType]     TINYINT       NOT NULL,
    [DebitAccountId]  INT           NOT NULL,
    [CreditAccountId] INT           NOT NULL,
    [BudgetId]        INT           NULL,
    CONSTRAINT [PK_TaxesLiquidationConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationConcept_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts] FOREIGN KEY ([DebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts1] FOREIGN KEY ([CreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador opcional (INT NULL) del presupuesto (Budget) al que pertenece el concepto; vinculación a plan presupuestario para control y seguimiento de impuestos.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de presupuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cuenta contable de crédito (MainAccounts) asociada al concepto; registro donde se abona el movimiento según el tipo de liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'CreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta Credito del concepto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'CreditAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'CreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cuenta contable de débito (MainAccounts) asociada al concepto; registro donde se carga el movimiento según el tipo de liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'DebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta Debito del concepto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'DebitAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'DebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto de liquidación (TINYINT): 1=Predial, 2=Bomberil, 3=CAM; clasificación que determina la naturaleza del impuesto o gravamen.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de concepto   1 - Predial  2 - Bomberil  3 - CAM', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del concepto de liquidación de impuestos (VARCHAR 200); etiqueta comprensible para reportes y operaciones contables.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) que representa el concepto de liquidación de impuestos; identificador legible por el usuario, distinto del Id técnico.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) que identifica unívocamente cada concepto de liquidación de impuestos en la tabla.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de liquidación de impuestos y contribuciones fiscales. Define los rubros contables usados al liquidar tributos, asociando cada concepto con sus cuentas contables de débito y crédito, su tipo, y opcionalmente un presupuesto.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept';
