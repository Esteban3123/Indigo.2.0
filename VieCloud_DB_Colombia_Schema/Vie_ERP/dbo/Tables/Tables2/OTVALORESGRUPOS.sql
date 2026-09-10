CREATE TABLE [dbo].[OTVALORESGRUPOS] (
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA] INT NOT NULL,
    [IDGRUPO]     INT NOT NULL,
    [VALOROPCION] INT NOT NULL,
    CONSTRAINT [PK_OTVALORESGRUPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVALORESGRUPOS_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_OTVALORESGRUPOS_OTGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción marcada en grupo de revisión por sistemas: 1=Refiere (hallazgo positivo), 2=No Refiere (hallazgo negativo). Valor numérico binario que indica resultado de evaluación clínica por aparato/sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion marcada grupo:  1 -  Refiere  2 - No Refiere', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de revisión por sistemas (cardiovascular, respiratorio, digestivo, etc.). Clave foránea a OTGRUPO. Agrupa preguntas de exploración física sistemática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de revision por sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Historia Clínica (registro en HCHISPACA). Clave foránea que vincula la respuesta de revisión por sistemas a la atención/consulta del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de HC (HCHISPACA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de valor de opción en grupo de revisión por sistemas. Autoincremental INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores de opciones seleccionadas por grupos dentro de una historia clínica o encuesta clínica. Relaciona cada respuesta o valor elegido con el grupo de preguntas y el registro de historia clínica correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS';
