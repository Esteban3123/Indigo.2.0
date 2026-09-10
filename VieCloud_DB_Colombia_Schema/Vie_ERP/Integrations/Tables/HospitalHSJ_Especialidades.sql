CREATE TABLE [Integrations].[HospitalHSJ_Especialidades] (
    [LegacyCode] VARCHAR (200) NOT NULL,
    [LegacyName] VARCHAR (200) NOT NULL,
    [IndigoCode] VARCHAR (200) NOT NULL,
    [IndigoName] VARCHAR (200) NOT NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HospitalHSJ_Especialidades] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de integración que mapea las especialidades médicas del sistema legado del Hospital HSJ con sus equivalentes en Indigo Vie Cloud, permitiendo la correspondencia entre ambos catálogos durante la migración o sincronización de datos.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica en el sistema de origen (sistema legado del Hospital HSJ).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'LegacyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'LegacyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad médica tal como está registrada en el sistema legado del Hospital HSJ.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'LegacyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'LegacyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código equivalente de la especialidad médica en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'IndigoCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'IndigoCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la especialidad médica según el catálogo de Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'IndigoName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'IndigoName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de correspondencia de especialidad.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HospitalHSJ_Especialidades', @level2type = N'COLUMN', @level2name = N'Id';
