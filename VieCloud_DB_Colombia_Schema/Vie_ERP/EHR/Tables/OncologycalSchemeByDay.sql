CREATE TABLE [EHR].[OncologycalSchemeByDay] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT NOT NULL,
    [DayNumber]                   INT NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDay] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDay_OncologycalSchemeByDrugs] FOREIGN KEY ([OncologycalSchemasbyDrugsId]) REFERENCES [EHR].[OncologycalSchemeByDrugs] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del día dentro del esquema oncológico de administración de fármacos (1, 2, 3...). INT. Indica la posición o jornada de aplicación del tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'DayNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'DayNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'DayNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del esquema oncológico por fármacos/drogas asociado. Referencia a EHR.OncologycalSchemeByDrugs. INT. Vincula el día específico de tratamiento al plan quimioterapéutico completo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Esquemas Oncológicos Por Drogas Id', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de día oncológico. INT IDENTITY. Clave primaria que identifica unívocamente cada día programado en los esquemas de quimioterapia o tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los días de aplicación de cada medicamento dentro de un esquema oncológico, indicando en qué día del ciclo de tratamiento debe administrarse el fármaco correspondiente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay';
