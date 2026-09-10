CREATE TABLE [dbo].[PRHCXEXAGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDEXAGRUPO] INT NOT NULL,
    CONSTRAINT [PK_PRHCXEXAGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXEXAGRUPO_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_PRHCXEXAGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXEXAGRUPO] NOCHECK CONSTRAINT [FK_PRHCXEXAGRUPO_PRMODELOHC];




GO



GO
ALTER TABLE [dbo].[PRHCXEXAGRUPO] NOCHECK CONSTRAINT [FK_PRHCXEXAGRUPO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de examen físico; referencia a tabla EXAGRUPO. Vincula categorías de exámenes clínicos (vitales, exploración, pruebas funcionales) al modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de examen físico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica; referencia a tabla PRMODELOHC. Relaciona la plantilla o estructura de HC con los grupos de exámenes disponibles para el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable (INT IDENTITY) de la asociación entre modelo HC y grupo de examen. Clave primaria para auditoría y relaciones internas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los grupos de exámenes o secciones que pertenecen a cada modelo de historia clínica, permitiendo configurar qué bloques de exámenes aparecen en un formulario clínico determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO';
