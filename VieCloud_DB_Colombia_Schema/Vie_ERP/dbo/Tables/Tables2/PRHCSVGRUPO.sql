CREATE TABLE [dbo].[PRHCSVGRUPO] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [CODSALUDVISUAL] INT NOT NULL,
    CONSTRAINT [PK_PRHCSVGRUPO] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PRMODELOHC_PRHCSVGRUPO] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de pestaña/sección de valoración oftalmológica de salud visual: 1=Anamnesis/Antecedentes, 2=Examen externo ocular, 3=Agudeza visual, 4=Refracción, 5=Valoración complementaria. INT, clasificador de componentes de examen oftalmológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'CODSALUDVISUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la pestaña de valoracion de salud visual  1: Anamnesis / Antecedentes  2: Exámen externo  3: Agudeza visual  4: Refreacción  5: Valoracion complementaria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'CODSALUDVISUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'CODSALUDVISUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de relación con modelo de historia clínica (PRMODELOHC). INT, vincula grupo de valoración visual a plantilla/template de HC. Referencia a PRMODELOHC.ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relacion con el modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo autonumérico (IDENTITY) de registro en tabla PRHCSVGRUPO. INT PRIMARY KEY, clave primaria clustered, índice de fila.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos o secciones de salud visual asociados a un modelo de historia clínica. Relaciona cada modelo de HC con los grupos de preguntas o campos correspondientes al módulo de salud visual (oftalmología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCSVGRUPO';
