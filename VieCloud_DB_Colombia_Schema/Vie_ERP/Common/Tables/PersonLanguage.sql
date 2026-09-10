CREATE TABLE [Common].[PersonLanguage] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]      INT     NOT NULL,
    [LanguageId]    INT     NULL,
    [LanguageLevel] TINYINT NOT NULL,
    CONSTRAINT [PK_EmployeeLanguage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__PersonLan__Language] FOREIGN KEY ([LanguageId]) REFERENCES [Payroll].[Language] ([Id]),
    CONSTRAINT [FK_EmployeeLanguage_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de competencia del idioma: 1=Bajo, 2=Medio, 3=Alto. Escala TINYINT que clasifica la capacidad lingüística de la persona en el idioma asociado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de lenguaje 1 - Bajo 2 - Medio 3 - Alto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del idioma. Referencia a [Payroll].[Language].[Id]. Permite relacionar la persona con el idioma que habla o domina.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Lenguaje', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la persona. Referencia a [Common].[Person].[Id]. Clave de relación para vincular el idioma al perfil de la persona (empleado, profesional, etc.).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la relación persona-idioma. Entidad de identidad autoincrementable (INT IDENTITY) que registra cada competencia lingüística.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Idiomas o lenguas asociados a una persona, incluyendo el nivel de dominio del idioma. Permite registrar qué idiomas habla cada persona y con qué nivel de competencia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage';
