CREATE TABLE [dbo].[Containers] (
    [Id]                     INT          NOT NULL,
    [ArchitectureType]       TINYINT      NOT NULL,
    [HISContainer]           VARCHAR (30) NOT NULL,
    [TransactionalContainer] VARCHAR (30) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de contenedores de base de datos del sistema Indigo Vie Cloud, que define la arquitectura de almacenamiento y los nombres de los contenedores HIS (historia clínica) y transaccional asociados a cada configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de contenedor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de arquitectura del contenedor; indica la modalidad o versión de despliegue de la base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ArchitectureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ArchitectureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de la base de datos clínica (HIS / historia clínica) asociado a esta configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'HISContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'HISContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de la base de datos transaccional (facturación, admisiones, operaciones) asociado a esta configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
