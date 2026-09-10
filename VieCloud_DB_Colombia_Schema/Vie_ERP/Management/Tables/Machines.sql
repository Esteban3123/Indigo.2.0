CREATE TABLE [Management].[Machines] (
    [Id]            INT           NOT NULL,
    [UID]           VARCHAR (50)  NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [NickName]      VARCHAR (100) NOT NULL,
    [ClientVersion] VARCHAR (12)  NULL,
    [OSName]        VARCHAR (100) NOT NULL,
    [Architecture]  VARCHAR (5)   NOT NULL,
    [IPs]           VARCHAR (500) NOT NULL,
    [MACs]          VARCHAR (500) NOT NULL,
    [RegDate]       DATETIME      NOT NULL,
    [LastUpDate]    DATETIME      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última actualización de la máquina; timestamp DATETIME que registra cuándo se sincronizó o actualizó por última vez el estado del equipo en el sistema de gestión', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'LastUpDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Última actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'LastUpDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'LastUpDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro; timestamp DATETIME que indica cuándo se registró por primera vez la máquina en el inventario de gestión', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'RegDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'RegDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'RegDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direcciones MAC (Media Access Control) de las interfaces de red del equipo; identificadores únicos físicos separados por comas para rastreo en red', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'MACs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordenador', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'MACs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'MACs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direcciones IP asignadas al equipo (IPv4/IPv6); direcciones lógicas de red para conectividad y comunicación dentro de la infraestructura', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'IPs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instituto prestador de salud', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'IPs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'IPs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arquitectura del procesador; tipo de sistema (x86, x64, ARM) que determina compatibilidad de software del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Architecture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arquitectura', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Architecture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Architecture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del sistema operativo instalado; identificación completa del SO (Windows, Linux, macOS) y versión del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'OSName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del sistema operativo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'OSName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'OSName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del cliente/agente de gestión; número de versión del software cliente que reporta estado y métrica de la máquina', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'ClientVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión del cliente', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'ClientVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'ClientVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apodo o alias del equipo; nombre descriptivo informal asignado para referencia rápida y fácil identificación en consultas', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'NickName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apodo del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'NickName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'NickName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del equipo (hostname); identificador administrativo único del computador en la red corporativa', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única universal del equipo; código único VARCHAR(50) que garantiza unicidad global de la máquina en el sistema', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'UID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación única del equipo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'UID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'UID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable único de la tabla; clave primaria INT que genera secuencia automática para cada registro de máquina', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de máquinas o equipos cliente conectados al sistema Indigo Vie Cloud. Guarda la identificación, características técnicas (sistema operativo, arquitectura, versiones), direcciones de red (IP y MAC) y fechas de registro y última actualización de cada dispositivo registrado en la plataforma.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'Machines';
