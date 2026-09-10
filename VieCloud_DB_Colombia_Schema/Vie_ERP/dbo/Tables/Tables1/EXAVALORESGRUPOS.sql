CREATE TABLE [dbo].[EXAVALORESGRUPOS] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA] INT NOT NULL,
    [IDEXAGRUPO]  INT NOT NULL,
    [VALOROPCION] INT NOT NULL,
    CONSTRAINT [PK_EXAVALORESGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVALORESGRUPO_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_EXAVALORESGRUPO_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de valoración del examen físico: 1=Normal, 2=Anormal, 3=No Valorado. Indica el resultado o estado de la evaluación clínica en el grupo de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Normal  2 - Anormal  3 - No Valorado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de examen físico (FK a EXAGRUPO). Referencia la categoría o sistema corporal evaluado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID grupo de examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica del paciente (FK a HCHISPACA). Vincula el valor del examen a la atención o ingreso específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID HC (HCHISPACA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de valor de grupo de examen. INT IDENTITY(1,1), autoincrementable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores u opciones seleccionadas por grupo de examen en la historia clínica de un paciente. Vincula cada grupo de examen con la respuesta o valor escogido durante la atención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS';
