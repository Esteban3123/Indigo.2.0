CREATE TABLE [dbo].[HCLISTACCXCHGRUPO] (
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC] INT NOT NULL,
    [IDCHGRUPO]   INT NOT NULL,
    CONSTRAINT [PK_HCLISTACCXCHGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLISTACCXCHGRUPO_CHGRUPO] FOREIGN KEY ([IDCHGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID]),
    CONSTRAINT [FK_HCLISTACCXCHGRUPO_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de checklist; referencia a tabla CHGRUPO para agrupar items de verificación en listas de chequeo clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDCHGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDCHGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDCHGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de lista de chequeo clínica; referencia a tabla HCLISTACC que contiene el encabezado/registro principal del checklist de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cabecera lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (IDENTITY) que identifica unívocamente cada relación entre grupo de checklist y cabecera de lista de chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona listas de chequeo o control de historia clínica con sus grupos de verificación o categorías. Permite agrupar los ítems de una lista de control clínico bajo distintos grupos de clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHGRUPO';
