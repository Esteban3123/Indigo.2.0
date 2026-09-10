CREATE TABLE [Payroll].[ConceptGroup] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId]       INT     NOT NULL,
    [GroupId]         INT     NOT NULL,
    [LiquidateBy]     TINYINT NOT NULL,
    [MaximunDiscount] BIT     NOT NULL,
    CONSTRAINT [PK_ConceptGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptGroup_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de descuento máximo permitido (BIT: 0=No aplica, 1=Si aplica). Control de límite de descuento en concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'MaximunDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento maximo 0-No 1-Si', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'MaximunDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'MaximunDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de liquidación del concepto en grupo (TINYINT: 1=Grupo completo, 2=Solo seleccionados). Define si se liquida todo el grupo o ítems específicos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'LiquidateBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidar por: 1 - Grupo Completo; 2 - Seleccionados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'LiquidateBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'LiquidateBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Grupo de Nómina (FK a Payroll.Group). Referencia a grupo de conceptos de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Concepto de Nómina (FK a Payroll.Concept). Referencia a concepto salarial, descuento o prestación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de asociación Concepto-Grupo (INT Identity). Clave primaria de relación entre concepto y grupo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa los conceptos de nómina dentro de grupos de liquidación, definiendo cómo se liquida cada concepto y el límite máximo de descuento permitido para el grupo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup';
