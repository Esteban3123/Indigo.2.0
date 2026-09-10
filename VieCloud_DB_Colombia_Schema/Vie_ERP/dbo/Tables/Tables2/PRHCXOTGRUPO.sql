CREATE TABLE [dbo].[PRHCXOTGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDOTGRUPO]  INT NOT NULL,
    CONSTRAINT [PK_PRHCXOTGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXOTGRUPO_OTGRUPO] FOREIGN KEY ([IDOTGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID]),
    CONSTRAINT [FK_PRHCXOTGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de revisión por sistema (FK a OTGRUPO). Agrupa órdenes de trabajo por especialidad o área funcional para organizar la revisión clínica en la historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDOTGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de revision por Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDOTGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDOTGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de Historia Clínica (FK a PRMODELOHC). Referencia la estructura o plantilla de historia que utiliza este grupo de revisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (IDENTITY). Identificador único de la relación entre modelo de historia clínica y grupo de revisión por sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los modelos de historia clínica con los grupos de órdenes u órdenes de trabajo (OT) que les corresponden, permitiendo definir qué grupos de órdenes están habilitados para cada modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO';
