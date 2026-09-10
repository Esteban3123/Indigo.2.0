CREATE TABLE [Integrations].[CIMAHospital_HomologationMaster] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MasterType]  VARCHAR (200) NOT NULL,
    [Observation] VARCHAR (200) NULL,
    [IndigoCode]  VARCHAR (200) NOT NULL,
    [LegacyCode]  VARCHAR (200) NOT NULL,
    [State]       BIT           NOT NULL,
    CONSTRAINT [PK_HospitalCIMA_HomologationMaster] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación del registro (0=Inactivo, 1=Activo). Tipo: BIT. Controla si la homologación está vigente.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 - Inactive  1 - Active   ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del maestro en el sistema externo (CIMA Hospital u otro legado). Tipo: VARCHAR(200). Identificador único en el origen de datos a integrar.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'LegacyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del maestro del sistema externo', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'LegacyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'LegacyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del maestro equivalente en Indigo Vie Cloud. Tipo: VARCHAR(200). Mapeo de convergencia entre sistema legado e Indigo.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'IndigoCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del maestro indigo', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'IndigoCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'IndigoCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o tipo del maestro a homologar (diagnóstico, procedimiento, servicio, unidad funcional, profesional, etc.). Tipo: VARCHAR(200). Categoría de datos que se sincroniza.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'MasterType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Nombre del maestro a homologar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'MasterType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'MasterType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la homologación. Tipo: INT IDENTITY(1,1). Clave primaria para auditoría de mapeos entre sistemas.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de homologación entre los códigos del sistema Indigo y los códigos del sistema legado CIMA Hospital. Permite mapear equivalencias de catálogos maestros (medicamentos, diagnósticos, servicios, etc.) entre ambos sistemas durante integraciones o migraciones.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota u observación adicional sobre el registro de homologación, por ejemplo aclaraciones sobre excepciones o condiciones especiales del mapeo.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_HomologationMaster', @level2type = N'COLUMN', @level2name = N'Observation';
