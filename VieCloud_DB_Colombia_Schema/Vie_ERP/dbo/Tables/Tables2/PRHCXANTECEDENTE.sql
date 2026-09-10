CREATE TABLE [dbo].[PRHCXANTECEDENTE] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [CODANTECEDENTE] INT NOT NULL,
    CONSTRAINT [PK_PRHCXANTECEDENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXANTECEDENTE_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXANTECEDENTE] NOCHECK CONSTRAINT [FK_PRHCXANTECEDENTE_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de antecedente clínico del paciente (1=Médicos, 2=Quirúrgicos, 3=Anestésicos, 4=Transfusionales, 5=Inmunológicos, 6=Alérgicos, 7=Traumáticos, 8=Psicológicos, 9=Farmacológicos, 10=Familiares, 11=Gineco-Obstétricos, 12=Urológico-sexual, 13=Perinatales, 14=Tóxicos, 15=Hábitos de Vida, 16=Esquema de Vacunación, 17=Escolares, 18=Laborales, 19=Nutricionales, 20=Odontológicos, 21=SocioEconómicos). Clasificación de historia médica, quirúrgica, alérgica, familiar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Antecedente:  1-Médicos  2-Quirurgicio  3-Anestésico  4-Transfunsionales  5-Inmunológicos  6-Alérgicos  7-Traumáticos  8-Psicológicos  9-Farmacológicos  10-Familiares  11-Gineco-Obstétricos  12-Urológico-sexual  13-Perinatales  14-Tóxicos  15-Hábitos de Vida  16-Esquema de Vacunación  17-Escolores  18-Laborales  19-Nutricionales  20-Odontológicos  21-SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de Historia Clínica (HC) asociado. Clave foránea que referencia PRMODELOHC. Define la estructura y plantilla de la historia clínica para este antecedente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la relación entre modelo de HC y tipo de antecedente. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los modelos de historia clínica con los antecedentes médicos configurados para cada uno. Permite definir qué tipos de antecedentes (personales, familiares, quirúrgicos, alérgicos, etc.) están habilitados en cada plantilla o modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTECEDENTE';
