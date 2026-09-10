CREATE TABLE [Cost].[CostActivityStep] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId] INT           NOT NULL,
    [Order]          INT           NOT NULL,
    [Description]    VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_CostActivityStep__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStep_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStep__CostActivityId__Order]
    ON [Cost].[CostActivityStep]([CostActivityId] ASC, [Order] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del paso o etapa de la actividad de costo; texto narrativo que explica la tarea, procedimiento o componente específico dentro del flujo de costeo (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden o secuencia numérica del paso dentro de la actividad de costo; define el número de ejecución o prioridad del paso (INT)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Order';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Order';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Order';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Actividad de Costo; clave foránea que referencia Cost.CostActivity.Id, vincula el paso a su actividad padre (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del Registro (Paso de Actividad de Costo); clave primaria autoincremental que identifica cada paso (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pasos o etapas ordenadas que conforman una actividad de costos. Permite desglosar cada actividad del esquema de costeo en una secuencia de acciones o instrucciones numeradas con su descripción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep';
