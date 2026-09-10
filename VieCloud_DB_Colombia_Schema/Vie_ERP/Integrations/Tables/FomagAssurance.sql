CREATE TABLE [Integrations].[FomagAssurance] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [CodeIndigo]      VARCHAR (50)  NOT NULL,
    [CodeIntegration] VARCHAR (50)  NOT NULL,
    [Description]     VARCHAR (250) NOT NULL,
    [URLAz]           VARCHAR (250) NULL,
    CONSTRAINT [PK_FomagAssurance] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del endpoint o Azure Function dedicado a validar derechos de afiliación del asegurado/paciente contra la entidad Fomag-Jersalud. Utilizado en consultas de elegibilidad antes de atención o facturación.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'URLAz';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL de la azure function de validacion de derechos', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'URLAz';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'URLAz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación o nombre comercial de la aseguradora, administradora de salud o entidad promotora de salud (EPS) integrada con Fomag. Campo de búsqueda para identificar la cobertura o plan del paciente.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la entidad aseguradora/administradora asignado por el sistema Fomag-Jersalud. Clave externa para sincronización e integración bidireccional con el proveedor de seguros.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad del lado del Fomag', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la administradora de salud (contrato o HealthAdministrator) dentro de Indigo Vie Cloud. Vincula la aseguradora del paciente con su estructura contractual y de derechos en el EHR.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de indigo , es el code de contrach.HealthAdministrator', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIndigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de integración que mapea y almacena las entidades (aseguradoras, administradoras de salud) sincronizadas entre la aplicación de integración Fomag-Jersalud y el sistema Indigo Vie Cloud. Permite vincular códigos internos de Indigo con códigos externos del lado Fomag/Jersalud para validación de derechos y consulta de afiliados.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tabla de integracion para guardad las entidades que aplicacion integracion fomag - jersalud', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único y autonumérico del registro de aseguradora Fomag.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Id';
