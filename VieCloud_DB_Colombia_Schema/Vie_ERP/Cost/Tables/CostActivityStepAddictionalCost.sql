CREATE TABLE [Cost].[CostActivityStepAddictionalCost] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [Description]        VARCHAR (500)   NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostActivityStepAddictionalCost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepAddictionalCost_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo adicional en valor monetario (DECIMAL 18,2). Monto económico del gasto complementario asociado al paso de actividad, facturación o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del costo adicional o gasto complementario. Texto que detalla la naturaleza, concepto o justificación del cargo extra en la actividad o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paso de actividad (FK → CostActivityStep.Id). Referencia a la etapa o fase de la actividad de costo padre, procedimiento o proceso relacionado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de costo adicional (PK, IDENTITY). Clave primaria secuencial del gasto complementario o cargo extra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costos adicionales asociados a un paso de actividad dentro del modelo de costeo. Registra conceptos de gasto extra (como insumos, servicios o cargos especiales) con su descripción y valor monetario, vinculados a una etapa específica del proceso de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepAddictionalCost';
