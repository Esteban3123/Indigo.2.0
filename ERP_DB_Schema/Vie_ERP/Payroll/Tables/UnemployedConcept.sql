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
ALTER TABLE [Payroll].[UnemployedConcept] NOCHECK CONSTRAINT [FK_UnemployedConcept_Concept];


GO
ALTER TABLE [Payroll].[UnemployedConcept] NOCHECK CONSTRAINT [FK_UnemployedConcept_UnemployedLiquidation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula del Concepto Reemplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fórmula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'IdUnemployementLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedConcept', @level2type = N'COLUMN', @level2name = N'Id';

