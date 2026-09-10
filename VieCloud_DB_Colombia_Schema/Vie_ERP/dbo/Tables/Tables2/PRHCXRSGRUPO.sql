CREATE TABLE [dbo].[PRHCXRSGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDRSGRUPO]  INT NOT NULL,
    CONSTRAINT [PK_PRHCXRSGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXRSGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID]),
    CONSTRAINT [FK_PRHCXRSGRUPO_RSGRUPO] FOREIGN KEY ([IDRSGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXRSGRUPO] NOCHECK CONSTRAINT [FK_PRHCXRSGRUPO_PRMODELOHC];




GO
ALTER TABLE [dbo].[PRHCXRSGRUPO] NOCHECK CONSTRAINT [FK_PRHCXRSGRUPO_PRMODELOHC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de revisión por sistema (FK a RSGRUPO). Agrupa reglas o criterios de validación de historias clínicas por dominio clínico o sistema corporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDRSGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de revision por Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDRSGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDRSGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica (FK a PRMODELOHC). Referencia la estructura o plantilla de datos de la historia clínica asociada al grupo de revisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY). Clave primaria de la relación muchos-a-muchos entre modelos de HC y grupos de revisión por sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre modelos de historia clínica y grupos de respuestas (RS), usada para definir qué grupos de campos o secciones pertenecen a cada modelo de historia clínica en el sistema de registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO';
