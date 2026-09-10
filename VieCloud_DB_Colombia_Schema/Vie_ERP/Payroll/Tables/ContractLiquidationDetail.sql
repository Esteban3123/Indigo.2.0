CREATE TABLE [Payroll].[ContractLiquidationDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractLiquidationId]   INT             NOT NULL,
    [Description]             VARCHAR (500)   NOT NULL,
    [Accrued]                 DECIMAL (18, 2) NOT NULL,
    [Deducted]                DECIMAL (18, 2) CONSTRAINT [DF_ContractLiquidationDetail_Deducted] DEFAULT ((0)) NOT NULL,
    [InitialDate]             DATE            NOT NULL,
    [EndingDate]              DATE            NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX)   NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX)   NULL,
    [IdConcept]               INT             NULL,
    [ConceptType]             TINYINT         NULL,
    [RetentionId]             INT             NULL,
    [RetentionPercentage]     DECIMAL (6, 3)  NULL,
    [RetentionBase]           DECIMAL (18)    NULL,
    CONSTRAINT [PK_ContractLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractLiquidationDetail_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ContractLiquidationDetail_ContractLiquidation] FOREIGN KEY ([ContractLiquidationId]) REFERENCES [Payroll].[ContractLiquidation] ([Id]),
    CONSTRAINT [FK_ContractLiquidationDetail_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO





GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidationDetail__ContractLiquidationId]
    ON [Payroll].[ContractLiquidationDetail]([ContractLiquidationId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidationDetail_Values]
    ON [Payroll].[ContractLiquidationDetail] ([ContractLiquidationId] ASC)
    INCLUDE ([Accrued], [Deducted], [IdConcept]);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de retención (DECIMAL 18), monto sobre el cual se calcula el porcentaje de retención fiscal, descuento o contribución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención (DECIMAL 6,3), tasa aplicada sobre la base para calcular impuestos, aportes o descuentos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención (FK GeneralLedger.RetentionConcepts), referencia a impuesto, aporte o descuento configurado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto (TINYINT), clasificación del rubro: salario, bonificación, deducción, aporte, retención o beneficio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nómina (FK Payroll.Concept), referencia a rubro salarial devengado o deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula reemplazada (VARCHAR MAX), expresión matemática anterior sustituida para cálculo del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula Reemplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del concepto (VARCHAR MAX), expresión o algoritmo usado para calcular el valor acumulado o deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórmula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período (DATE), fecha de cierre del rango de liquidación del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'EndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'EndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'EndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período (DATE), fecha de inicio del rango de liquidación del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del concepto deducido (DECIMAL 18,2), monto restado en nómina: impuestos, aportes, descuentos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Concepto Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del concepto devengado (DECIMAL 18,2), monto causado o generado: salario, bonificación, prestaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del concepto Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta informativa del concepto (VARCHAR 500), nombre o descripción legible del rubro para reportes y consultas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Etiqueta informativa del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la liquidación del contrato (FK Payroll.ContractLiquidation INT), referencia al encabezado de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ContractLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabezera de la Liquidacion del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ContractLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ContractLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de liquidación (INT PK Identity), clave primaria del registro de nómina individual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de liquidacion de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos de liquidación de contratos laborales en nómina. Registra cada ítem (devengado, deducción, retención) que compone la liquidación definitiva de un empleado al terminar su contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail';
