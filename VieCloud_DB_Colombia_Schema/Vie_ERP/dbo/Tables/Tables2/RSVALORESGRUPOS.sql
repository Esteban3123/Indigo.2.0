CREATE TABLE [dbo].[RSVALORESGRUPOS] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA] INT NOT NULL,
    [IDGRUPO]     INT NOT NULL,
    [VALOROPCION] INT NOT NULL,
    CONSTRAINT [PK_RSVALORESGRUPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVALORESGRUPOS_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_RSVALORESGRUPOS_RSGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[RSVALORESGRUPOS] NOCHECK CONSTRAINT [FK_RSVALORESGRUPOS_RSGRUPO];




GO



GO
ALTER TABLE [dbo].[RSVALORESGRUPOS] NOCHECK CONSTRAINT [FK_RSVALORESGRUPOS_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opción marcada en grupo de revisión por sistemas: 1=Refiere (positivo/presente), 2=No Refiere (negativo/ausente). Indica presencia o ausencia de síntoma/hallazgo en revisión de aparatos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opcion marcada grupo:  1 -  Refiere  2 - No Refiere', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de revisión por sistemas (RPS). Referencia a RSGRUPO para agrupar preguntas de examen físico y revisión de aparatos en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de revision por sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Historia Clínica (HC) del paciente. Referencia a HCHISPACA que vincula la respuesta del grupo de revisión a una atención/consulta específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de HC (HCHISPACA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de valor de grupo en revisión por sistemas. INT IDENTITY auto-incrementado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de valores seleccionados por opción dentro de grupos de preguntas o secciones de la historia clínica del paciente. Asocia cada respuesta grupal con su historial clínico correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORESGRUPOS';
