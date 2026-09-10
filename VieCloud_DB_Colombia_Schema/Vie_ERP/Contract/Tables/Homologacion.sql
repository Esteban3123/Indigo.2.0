CREATE TABLE [Contract].[Homologacion] (
    [CUPS] NVARCHAR (255) NULL,
    [IPS]  NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Institución Prestadora de Servicios (IPS), identificador del prestador de salud o centro de atención que ofrece servicios médicos. Tipo: NVARCHAR(255), referencia a la entidad que ejecuta procedimientos, consultas, urgencias o internaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'IPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'IPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'IPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Procedimientos y Servicios (CUPS), clasificador estándar de procedimientos, servicios, consultas, exámenes de laboratorio, estudios de imagen y prestaciones sanitarias. Tipo: NVARCHAR(255), usado en facturación, RIPS y reportes de atención en salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Homologación entre códigos de procedimientos/servicios CUPS y los códigos internos utilizados por la IPS. Permite traducir la codificación estándar del sistema de salud al manejo interno del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion';
