CREATE TABLE [EHR].[OncologicalSchemeByPathologies] (
    [Id]                  INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologicalSchemeId] INT      NOT NULL,
    [PathologyId]         CHAR (4) NOT NULL,
    CONSTRAINT [PK_OncologicalSchemeByPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologicalSchemeByPathologies_INDIAGNOS] FOREIGN KEY ([PathologyId]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_OncologicalSchemeByPathologies_OncologicalSchemeByPathologies] FOREIGN KEY ([OncologicalSchemeId]) REFERENCES [EHR].[OncologicalSchemes] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de patología oncológica (diagnóstico, enfermedad); referencia FK a INDIAGNOS.CODDIAGNO; CHAR(4); identifica la condición médica, tumor o malignidad asociada al esquema de tratamiento', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'PathologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Id patologia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'PathologyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'PathologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema oncológico; referencia FK a EHR.OncologicalSchemes.Id; INT; vincula el protocolo, plan o línea de tratamiento del cáncer a esta patología específica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'OncologicalSchemeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de esquema oncológico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'OncologicalSchemeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'OncologicalSchemeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria); INT IDENTITY; consecutivo autonumérico que identifica unívocamente cada asociación entre esquema oncológico y patología', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los esquemas oncológicos con las patologías (diagnósticos) a las que aplican. Permite saber qué protocolos o esquemas de tratamiento oncológico están asociados a cada tipo de cáncer o enfermedad oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies';
