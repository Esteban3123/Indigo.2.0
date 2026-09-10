CREATE TABLE [Integrations].[HomologationSpecialties] (
    [CodeIntegration] VARCHAR (50)  NULL,
    [NameIntegration] VARCHAR (200) NULL,
    [CodeIndigo]      VARCHAR (50)  NULL,
    [NameIndigo]      VARCHAR (200) NULL,
    [Observacion]     VARCHAR (500) NULL,
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HomologationSpecialties] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de homologación de especialidades médicas entre el sistema Indigo Vie Cloud y FOMAG. Mapeo bidireccional de códigos y nombres de especialidades clínicas para integración de datos, sincronización de catálogos y compatibilidad interoperativa entre sistemas de salud.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tabla homologacion especialidades indigo con FOMAG', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad en el sistema externo o de integración (código origen de la homologación).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'CodeIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'CodeIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad tal como la define el sistema externo o de integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'NameIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'NameIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad en Indigo Vie Cloud (código destino de la homologación).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'CodeIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'CodeIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad según el catálogo interno de Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'NameIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'NameIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas o comentarios adicionales sobre la equivalencia o particularidades de la homologación de la especialidad.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de homologación de especialidad.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties', @level2type = N'COLUMN', @level2name = N'Id';
