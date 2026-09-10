CREATE TABLE [Payroll].[UnemployedConcept] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUnemployementLiquidation] INT           NOT NULL,
    [IdConcept]                  INT           NOT NULL,
    [AccruedValue]               NUMERIC (18)  NOT NULL,
    [DeductedValue]              NUMERIC (18)  NOT NULL,
    [ConceptFormulate]           VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_UnemployedConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnemployedConcept_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_UnemployedConcept_UnemployedLiquidation] FOREIGN KEY ([IdUnemployementLiquidation]) REFERENCES [Payroll].[UnemployedLiquidation] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del Concepto Reemplazada; expresión de cálculo alternativa o modificada para el concepto de nómina en liquidación de cesantías (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula del Concepto Reemplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del Concepto; expresión de cálculo o regla que define cómo se computa el concepto de nómina en la liquidación de desempleo/cesantías (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Deducido; monto numérico (18 dígitos) de descuentos, retenciones o aportes sustraídos del concepto en la liquidación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Devengado; monto numérico (18 dígitos) acumulado o causado del concepto de nómina en la liquidación de cesantías, antes de deducciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Concepto; clave foránea (INT) que referencia la tabla Payroll.Concept, vinculando el concepto de nómina/cesantía (aportes, salarios, intereses)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cabecera Cesantías; clave foránea (INT) que referencia Payroll.UnemployedLiquidation, asociando el concepto a una liquidación de desempleo/cesantías específica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdUnemployementLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdUnemployementLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdUnemployementLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Tabla; clave primaria (INT IDENTITY) única para cada relación concepto-liquidación en desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de nómina asociados a una liquidación de desempleo (cesantías o liquidación definitiva), registrando los valores devengados y deducidos por cada concepto salarial, junto con las fórmulas de cálculo aplicadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept';
